using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panell.Models
{
    public class Users
    {
        [Key]
        public int ID { get; set; }

        public string? user_name { get; set; }

        public string? user_email { get; set; }

        public string? user_password { get; set; }
    }
}
