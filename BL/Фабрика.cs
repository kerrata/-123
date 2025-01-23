using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarmeladhubApp.BL
{
    public class Фабрика : IEntity
    {
        public int код_фабрики { get; set; }
        public string название_фабрики { get; set; }

        public Фабрика(int код_фабрики, string название_фабрики)
        {
            this.код_фабрики = код_фабрики;
            this.название_фабрики = название_фабрики;
        }

        public Фабрика() { }

        public int GetId() => код_фабрики;
    }
}