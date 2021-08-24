using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex9_ORM_EFCore_Console.Models
{
    [Table("ClientInfo")]
    public class ClientInfo
    {
        public int Id { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public int Age { get; set; }
        [Required] public string Sex { get; set; }
        public string Address { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public override string ToString()
        {
            return $"FirstName = {FirstName}\n" +
                $"LastName = {LastName}\n" +
                $"Age = {Age}\n" +
                $"Sex = {Sex}\n" +
                $"Address = {Address}\n";
        }
    }
}
