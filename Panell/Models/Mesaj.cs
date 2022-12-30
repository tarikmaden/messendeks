using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panell.Models
{
    public class Mesaj
    {
        [Key]
        public int ID { get; set; }

        public string? email { get; set; }
        public string? phone { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? message { get; set; }
    }
}
