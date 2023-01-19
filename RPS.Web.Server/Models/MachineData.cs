using System;
using System.ComponentModel.DataAnnotations;

namespace RPS.Web.Server.Models
{
    public class MachineData
    {
        public int id { get; set; }
        [Display(Name = "Coil Number")]
        public int coilNumber { get; set; }
        [Display(Name = "Date Ran")]
        public string dateRan { get; set; }
        [Display(Name = "Start")]
        public string start { get; set; }
        [Display(Name = "Stop")]
        public string stop { get; set; }
        [Display(Name = "Crew")]
        public string crew { get; set; }
        [Display(Name = "Duration")]
        public string duration { get; set; }
        public int grade { get; set; }
        public int product { get; set; }
        public int passes { get; set; }
        public double gauge { get; set; }
        public double nominal { get; set; }
        public double reduction { get; set; }
        public double width { get; set; }
        public double speed { get; set; }
        public double feet { get; set; }
        public int heatNo { get; set; }
        public double bonus { get; set; }
        public double time { get; set; }
    }
}
