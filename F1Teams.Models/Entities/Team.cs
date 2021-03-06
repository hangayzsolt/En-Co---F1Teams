﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1Teams.Models.Entities
{
    [Table("Teams")]
    public class Team
    {
        [Key]
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
