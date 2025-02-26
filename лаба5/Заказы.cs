using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛБ5
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public int FreelancerId { get; set; }
        public decimal Cost { get; set; }

        public Order(int orderId, int serviceId, int freelancerId, decimal cost)
        {
            OrderId = orderId;
            ServiceId = serviceId;
            FreelancerId = freelancerId;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"ID: {OrderId}, Код услуги: {ServiceId}, Код исполнителя: {FreelancerId}, Стоимость: {Cost}";
        }
    }
}
