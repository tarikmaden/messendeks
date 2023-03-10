using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panell.Models
{
    public class Sayfalar
    {
        [Key]
        public int ID { get; set; }

        public string? sayfa_adi { get; set; }
        public string? sayfa_desc { get; set; }
        public string? sayfa_ozet { get; set; }

        public string? sayfa_icerik { get; set; }

        public string? sayfa_kategori { get; set; }

        public string? sayfa_slug { get; set; }

        public string? sayfa_title { get; set; }

        public string? sayfa_tarih { get; set; }

        public string? sayfa_order { get; set; }

        [NotMapped]
        public IFormFile Dosya { get; set; }

        public string? sayfa_resim { get; set; }
        
        [NotMapped]
        public IFormFile Dosya2 { get; set; }

        public string? sayfa_resimm { get; set; }

    }
}
