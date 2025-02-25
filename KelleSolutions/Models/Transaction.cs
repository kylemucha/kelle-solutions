using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Deposit Date")]
        public DateTime? DepositDate { get; set; }

        [Display(Name = "Inspection Date")]
        public DateTime? InspectionDate { get; set; }

        [Display(Name = "Loan Date")]
        public DateTime? LoanDate { get; set; }

        [Display(Name = "Appraisal Date")]
        public DateTime? AppraisalDate { get; set; }

        [Required]
        [Display(Name = "Property Information")]
        public string PropertyInformation { get; set; }

        [Required]
        [Display(Name = "Transition Details")]
        public string TransitionDetails { get; set; }

        [Required]
        public string Status { get; set; }

        public string Comments { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
