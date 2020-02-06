using System;
using System.Collections.Generic;
using System.Text;

namespace IamSafe.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
