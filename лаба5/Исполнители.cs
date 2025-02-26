using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛБ5
{
    public class Freelancer
    {
        public int FreelancerId { get; set; }
        public int Age { get; set; }
        public string Citizenship { get; set; }

        public Freelancer(int freelancerId, int age, string citizenship)
        {
            FreelancerId = freelancerId;
            Age = age;
            Citizenship = citizenship;
        }

        public override string ToString()
        {
            return $"ID: {FreelancerId}, Возраст: {Age}, Гражданство: {Citizenship}";
        }
    }
}
