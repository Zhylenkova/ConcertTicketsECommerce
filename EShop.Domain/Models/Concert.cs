using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EShop.Domain.Models
{
    public class Concert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int concert_id { get; set; }

        [Required]
        [MaxLength(255)]
        public string name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string artist { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string city { get; set; } = string.Empty;


        [MaxLength(50)]
        public string venue { get; set; } = string.Empty;
        public DateTime date { get; set; }

        public int totalSeats { get; set; }
        public int availableSeats { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal price { get; set; }

        [MaxLength(50)]
        public string category { get; set; } = string.Empty;
    }
}
