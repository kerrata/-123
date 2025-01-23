using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarmeladhubApp.BL
{
    public class Магазин : IEntity
    {
        public int код_магазина { get; set; }
        public string название_магазина { get; set; }
        public string адрес_магазина { get; set; }

        public Магазин(int код_магазина, string название_магазина, string адрес_магазина)
        {
            this.код_магазина = код_магазина;
            this.название_магазина = название_магазина;
            this.адрес_магазина = адрес_магазина;
        }

        public Магазин() { }

        public int GetId() => код_магазина;
    }
}