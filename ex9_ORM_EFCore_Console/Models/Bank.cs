using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ex9_ORM_EFCore_Console.Models
{
    public class Bank
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        public List<Client> Clients { get; set; }
    }
}
