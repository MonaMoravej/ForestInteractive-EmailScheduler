using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForestInteractive_EmailingService.Models
{
    public class EmailSchedule
    {
     
      public int Id { get; set; }

       [Required]
        public DateTime SendDateTime { get; set; } //= DateTime.Now;

        [Required]
        [MaxLength(160)]
        [MinLength(20)]
        public string EmailBody { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string EmailSubject { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string FromEmail { get; set; }

        public byte[] Recipients { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
      
        public string JobId { get; set; }
    }
}