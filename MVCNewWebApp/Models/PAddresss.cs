using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCNewWebApp.Models
{
    [Table("Addresses")]
    public class PAddresss
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string Pin { get; set; }
    }
}
