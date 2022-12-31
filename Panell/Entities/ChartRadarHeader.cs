using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Panell.Entities
{
    public class ChartRadarHeader
    {
        [Key]
        public int Id { get; set; }

        public string ChartRadarHeaderAdi { get; set; }

        public string RenkKodu { get; set; }
    }
    public class ChartRadarItem
    {
        [Key]
        public int Id { get; set; }

        public int ChartRadarHeaderId { get; set; }

        public string ChartRadarItemAdi { get; set; }

        public string RenkKodu { get; set; }

        public int Deger1 { get; set; }

        public int Deger2 { get; set; }
    }
}
