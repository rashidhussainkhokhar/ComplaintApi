using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Complaint
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerCity { get; set; }
        public string ComplaintType { get; set; }
        public string ComplaintDescription { get; set; }
        public DateTime DateOfComplaint { get; set; }
        public string Status { get; set; }
        public string NameOfTechnician { get; set; }
        public string RegisteredBy { get; set; }

    }
}
