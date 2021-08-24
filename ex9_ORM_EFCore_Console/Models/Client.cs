using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ex9_ORM_EFCore_Console.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Bank> Banks { get; set; }

    }
}
