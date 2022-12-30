using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panell.Models
{
    public class Temsilci
    {
        [Key]
        public int ID { get; set; }

        public string? title { get; set; }
        public string? phone { get; set; }
        public string? phone2 { get; set; }
        public string? email { get; set; }
        public string? adres { get; set; }
    }
}
