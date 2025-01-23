using MarmeladhubApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MarmeladhubApp.BL; 
using MarmeladhubApp.DataAccess; 

namespace MarmeladhubApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICrudOperations<object>? _currentRepository; 
        private readonly string _connectionString = "Host=localhost;Port=8000;Database=marm1;Username=postgres;Password=1111";
        private Type? _currentTableType;

        public MainWindow()
        {
            InitializeComponent();
            UpdateUI(false);
        }

        private async void TableSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IRepositoryManager repositoryManage = new RepositoryManager(_connectionString);

            // Проверяем, что выбранный элемент не является null и не является заглушкой
            if (TableSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                // Проверяем, является ли выбранный элемент заглушкой
                if (selectedItem.Content.ToString() == "Выберите таблицу")
                {
                    // Здесь можно обработать случай, когда выбрана заглушка
                 
                    return; // Выходим из метода
                }

                // Получаем тег выбранного элемента
                var selectedTable = selectedItem.Tag?.ToString(); // Используем оператор безопасного доступа

                if (!string.IsNullOrEmpty(selectedTable))
                {
                    _currentRepository = repositoryManage.CreateRepository(selectedTable);
                    _currentTableType = repositoryManage.GetTableType(selectedTable);

                    if (_currentRepository != null)
                    {
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось создать репозиторий для выбранной таблицы.");
                    }
                }
            }
        }


        private async Task LoadDataAsync() // загрузка данных из репозитория и их отображение в компоненте
        {
            if (_currentRepository == null)
            {
                MessageBox.Show("Данные не были загружены!");
                return;
            }

            var data = await _currentRepository.GetAllAsync();
            DataGrid.ItemsSource = data;
            UpdateUI(true);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)  // изменения выделения в дата грид
        {
            EditButton.IsEnabled = DataGrid.SelectedItem != null;
            DeleteButton.IsEnabled = DataGrid.SelectedItem != null;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)  // добавление новой запист
        {
            if (!ValidateCurrentTable()) return;

            var addEditWindow = new AddEditWindow(_currentTableType, _currentRepository);
            if (addEditWindow.ShowDialog() == true)
            {
                await SaveNewEntity(addEditWindow.Tag);
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)// обрабатывание кнопки редактирования, проверка на то, что запись выбрана
        {
            if (!ValidateCurrentTable() || DataGrid.SelectedItem == null) return;

            var selectedItem = DataGrid.SelectedItem as IEntity;

            if (selectedItem != null)
            {
                var selectedEntity = DataGrid.SelectedItem;
                var addEditWindow = new AddEditWindow(_currentTableType, _currentRepository, selectedEntity);

                PrepopulateEditWindow(addEditWindow, selectedEntity);

                if (addEditWindow.ShowDialog() == true) // открываем диалоговое окно
                {
                    await UpdateEntity(addEditWindow.Tag);
                }
            }
            else
            {
                MessageBox.Show("Выбранный элемент не является корректной сущностью.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentRepository == null || DataGrid.SelectedItem == null) return;

            var result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
                                         "Подтверждение удаления",
                                         MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var selectedItem = DataGrid.SelectedItem as IEntity;

                if (selectedItem != null)
                {
                    int id = selectedItem.GetId();

                    // Используем метод удаления из репозитория
                    await _currentRepository.DeleteAsync(id);
                    await LoadDataAsync();
                    MessageBox.Show("Выбранный элемент успешно удален.", "Удалено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Выбранный элемент не является корректной сущностью.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }





        private void UpdateUI(bool isTableSelected)
        {
            AddButton.IsEnabled = isTableSelected;
            UpdateEditDeleteButtonsState();
        }

        private bool ValidateCurrentTable()
        {
            if (_currentTableType == null)
            {
                MessageBox.Show("Сначала выберите таблицу.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private async Task SaveNewEntity(object? newEntity)// добавление новой записи в репоззиторий
        {
            if (_currentRepository != null && newEntity != null)
            {
                try
                {
                    await _currentRepository.AddAsync(newEntity);
                    MessageBox.Show("Запись успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadDataAsync();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool ConfirmDeletion()
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
                                         "Подтверждение удаления",
                                         MessageBoxButton.YesNo, MessageBoxImage.Warning);
            return result == MessageBoxResult.Yes;
        }

        private void PrepopulateEditWindow(AddEditWindow addEditWindow, object selectedEntity)// крутое редактирование
        {
            foreach (var property in _currentTableType!.GetProperties().Where(p => p.CanWrite))
            {
                if (addEditWindow.FieldInputs.TryGetValue(property.Name, out var textBox))
                {
                    var value = property.GetValue(selectedEntity);
                    textBox.Text = value?.ToString() ?? string.Empty;

                    if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                    {
                        textBox.IsEnabled = false;
                    }
                }
            }
        }

        private async Task UpdateEntity(object? updatedEntity)  //добавление записи в репозиторий для обновлении записи
        {
            if (_currentRepository != null && updatedEntity != null)
            {
                try
                {
                    await _currentRepository.UpdateAsync(updatedEntity);
                    MessageBox.Show("Запись успешно обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadDataAsync();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateEditDeleteButtonsState()
        {
            var hasSelection = DataGrid.SelectedItem != null;
            EditButton.IsEnabled = hasSelection;
            DeleteButton.IsEnabled = hasSelection;
        }
    }
}
