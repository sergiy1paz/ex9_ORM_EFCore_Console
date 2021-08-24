using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ex9_ORM_EFCore_Console.Models
{
    [Table("Accounts")]
    public class Card
    {
        public int Id { get; set; }
        [Required] public string Number { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
