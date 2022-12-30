using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panell.Models
{
    public class Iletisim
    {
        [Key]
        public int ID { get; set; }

        public string? email { get; set; }
        public string? phone { get; set; }
        public string? facebook { get; set; }
        public string? instagram { get; set; }
        public string? twitter { get; set; }
        public string? youtube { get; set; }
        public string? google { get; set; }

        [NotMapped]
        public IFormFile Dosya { get; set; }

        [NotMapped]
        public IFormFile Dosya2 { get; set; }
        public string? logo { get; set; }
        public string? favicon { get; set; }
        public string? maps { get; set; }
        public string? adres { get; set; }
        public string? text1 { get; set; }
        public string? text2 { get; set; }
        public string? text3 { get; set; }
        public string? text4 { get; set; }
        public string? text5 { get; set; }
        public string? text6 { get; set; }
        public string? text7 { get; set; }
        public string? text8 { get; set; }
        public string? text9 { get; set; }
        public string? text10 { get; set; }
    }
}
