using System.ComponentModel.DataAnnotations;

namespace RPS.Core.Models
{
    public class PtUser : PtObjectBase
    {
        [Display(Name="Full Name")]
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }

    }
}
