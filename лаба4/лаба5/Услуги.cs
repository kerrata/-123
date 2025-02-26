using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛБ5
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }

        public Service(int serviceId, string name)
        {
            ServiceId = serviceId;
            Name = name;
        }

        public override string ToString()
        {
            return $"ID: {ServiceId}, Название: {Name}";
        }
    }
}
