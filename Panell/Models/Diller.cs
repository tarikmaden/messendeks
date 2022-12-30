using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panell.Models
{
    public class Diller
    {
        [Key]
        public int ID { get; set; }

        public string? dil_adi { get; set; }

        public string? dil_kisaltma { get; set; }


    }
}
