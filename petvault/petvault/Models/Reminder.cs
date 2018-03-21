using System;
namespace petvault.Models
{
    public class Reminder
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
		public string Pet { get; set; }
        public string Type { get; set; }
    }
}
