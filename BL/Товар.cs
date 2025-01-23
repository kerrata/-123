using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarmeladhubApp.BL
{
    public class Товар : IEntity
    {
        public int код_товара { get; set; }
        public string название_товара { get; set; }
        public int код_магазина { get; set; }
        public int код_фабрики { get; set; }

        public Товар(int код_товара, string название_товара, int код_магазина, int код_фабрики)
        {
            this.код_товара = код_товара;
            this.название_товара = название_товара;
            this.код_магазина = код_магазина;
            this.код_фабрики = код_фабрики;
        }

        public Товар() { }

        public int GetId() => код_товара;
    }
}