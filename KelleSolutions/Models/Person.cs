using System;

namespace KelleSolutions.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime Created { get; set; }
        public string? OperatorName { get; set; }
    }
}
