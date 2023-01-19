using System;
using System.ComponentModel.DataAnnotations;

namespace RPS.Core.Models
{
    public class PtObjectBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name ="Date Created")]
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
