using MarmeladhubApp.DataAccess;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;

namespace MarmeladhubApp
{
    public partial class AddEditWindow : Window
    {
        private readonly Type _entityType;
        private readonly ICrudOperations<object> _currentRepository;
        private readonly Dictionary<string, System.Windows.Controls.TextBox> _fieldInputs = new();
        public Dictionary<string, System.Windows.Controls.TextBox> FieldInputs => _fieldInputs;

        public AddEditWindow(Type entityType, ICrudOperations<object> repository, object? existingEntity = null)
        {
            InitializeComponent();
            _entityType = entityType;
            _currentRepository = repository;
            GenerateFields(existingEntity);
        }

        private async void GenerateFields(object? existingEntity = null)
        {
            var properties = _entityType.GetProperties()
                .Where(p => p.CanWrite)
                .ToList();

            foreach (var property in properties)
            {
                // подпись поля
                var label = new System.Windows.Controls.TextBlock
                {
                    Text = property.Name,
                    Margin = new Thickness(0, 5, 0, 5),
                    FontWeight = FontWeights.Bold
                };
                FieldsPanel.Children.Add(label);

                // сам textbox
                var textBox = new System.Windows.Controls.TextBox
                {
                    Margin = new Thickness(0, 5, 0, 10),
                    IsEnabled = !property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)
                };

                // если это поле Id, предлагаем сгенерированный идентификатор
                if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) && property.PropertyType == typeof(int))
                {
                    if (existingEntity == null)
                    {
                        // режим добавления
                        var nextId = await _currentRepository.GetNextIdAsync(_entityType.Name);
                        textBox.Text = nextId.ToString();
                    }
                    else
                    {
                        // режим редактирования
                        var currentId = property.GetValue(existingEntity);
                        textBox.Text = currentId?.ToString() ?? string.Empty;
                    }
                }
                else
                {
                    // в режиме редактирования подставляем текущие значения
                    if (existingEntity != null)
                    {
                        var value = property.GetValue(existingEntity);
                        textBox.Text = value?.ToString() ?? string.Empty;
                    }
                    
                }

                FieldsPanel.Children.Add(textBox);
                _fieldInputs[property.Name] = textBox;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // создаём экземпляр сущности
            var entity = Activator.CreateInstance(_entityType);
            var isValid = true;
            var errors = new List<string>();

            // поля, где разрешены пустые значения
            var fieldsCanBeEmpty = new List<string> { "dataDeregistration" };

            // пробегаемся по всем свойствам
            foreach (var property in _entityType.GetProperties().Where(p => p.CanWrite))
            {
                if (!_fieldInputs.TryGetValue(property.Name, out var textBox))
                    continue;

                var textValue = textBox.Text.Trim();

                // проверка на пустоту (если поле не в списке допустимо-пустых)
                if (string.IsNullOrWhiteSpace(textValue) && !fieldsCanBeEmpty.Contains(property.Name))
                {
                    errors.Add($"Поле '{property.Name}' не может быть пустым.");
                    isValid = false;
                    continue;
                }

                try
                {
                    object? convertedValue = null;

                    // преобразуем тип
                    if (property.PropertyType == typeof(int))
                    {
                        if (!int.TryParse(textValue, out var intVal))
                        {
                            errors.Add($"Поле '{property.Name}' должно быть целым числом.");
                            isValid = false;
                            continue;
                        }
                        convertedValue = intVal;
                    }
                    else if (property.PropertyType == typeof(double))
                    {
                        if (!double.TryParse(textValue, out var doubleVal))
                        {
                            errors.Add($"Поле '{property.Name}' должно быть числом (double).");
                            isValid = false;
                            continue;
                        }
                        convertedValue = doubleVal;
                    }
                    else if (property.PropertyType == typeof(DateTime))
                    {
                        if (!string.IsNullOrEmpty(textValue))
                        {
                            if (!DateTime.TryParse(textValue, out var dateTimeVal))
                            {
                                errors.Add($"Поле '{property.Name}' должно быть датой/временем (yyyy-MM-dd).");
                                isValid = false;
                                continue;
                            }
                            convertedValue = dateTimeVal;
                        }
                    }
                    else
                    {
                        // все остальные типы (включая string)
                        convertedValue = Convert.ChangeType(textValue, property.PropertyType);
                    }

                    if (convertedValue != null)
                    {
                        property.SetValue(entity, convertedValue);
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Ошибка в поле '{property.Name}': {ex.Message}");
                    isValid = false;
                }
            }

            if (!isValid)
            {
                MessageBox.Show(string.Join("\n", errors), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // если базовые проверки ок, закрываем диалог и возвращаем объект
            DialogResult = true;
            Tag = entity;
            Close();
        }
    }
}
