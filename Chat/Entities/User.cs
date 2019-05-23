using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entities
{
    [Table("WRI_Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(maximumLength: 75)]
        public string FirstName { get; set; }
        [Required, StringLength(maximumLength: 75)]
        public string LastName { get; set; }
        [Required, StringLength(maximumLength: 250)]
        public string Email { get; set; }
        [Required]
        public string Photo { get; set; }
    }
}
