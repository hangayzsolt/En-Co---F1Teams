using System.ComponentModel.DataAnnotations;

namespace F1Teams.Models.DTOs
{
    public class TeamDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        
        [Required]
        [Range(1900,2020)]
        [Display(Name = "Year of foundation")]
        public int FoundationYear { get; set; }
        
        [Required]
        [Display(Name = "Number of Champions title")]
        [Range(0, int.MaxValue)]
        public int WonChampionsTitle { get; set; }
        
        [Required]
        [Display(Name = "Is Entry Fee Paid?")]
        public bool IsEntryFeePayed { get; set; }
        
    }
}
