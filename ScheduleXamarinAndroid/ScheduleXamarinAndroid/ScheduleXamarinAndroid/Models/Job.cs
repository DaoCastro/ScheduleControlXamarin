using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleXamarinAndroid.Models
{
    public class Job
    {
        public string CustomerName { get; set; }
        public int ScheduleID { get; set; }
        public int CustomerPropertyID { get; set; }
        public DateTime ScheduleDate { get; set; }
        public Nullable<DateTime> ScheduleDateEnd { get; set; }
        public int LengthHours { get; set; }
        public int LengthMinutess { get; set; }
        public int Status { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ServicesName { get; set; }
        public int Id { get; set; }
        public string Nota { get; set; }
        public string IdUser { get; set; }
        public string PropertyName { get; set; }
        public Nullable<DateTime> CheckIn { get; set; }
        public Nullable<DateTime> CheckOut { get; set; }
        public string NameUser { get; set; }
    }
}
