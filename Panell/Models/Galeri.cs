using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panell.Models
{
    public class Galeri
    {
        [Key]
        public int ID { get; set; }

        public string? gelen_id { get; set; }

        [NotMapped]
        public IFormFile Dosya { get; set; }

        public string? resim { get; set; }

    }
}
