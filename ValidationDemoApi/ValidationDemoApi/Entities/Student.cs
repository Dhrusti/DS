using System;
using System.Collections.Generic;

namespace ValidationDemoApi.Entities
{
    public partial class Student
    {
        public int RollNo { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int? Age { get; set; }
    }
}
