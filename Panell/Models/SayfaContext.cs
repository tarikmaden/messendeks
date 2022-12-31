using Microsoft.EntityFrameworkCore;
using Panell.Entities;

namespace Panell.Models
{
    public class SayfaContext : DbContext
    {
        public SayfaContext(DbContextOptions<SayfaContext> options) : base(options) { }

        public DbSet<Sayfalar> Sayfalar { get; set; }
        public DbSet<Galeri> Galeri { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Dsayfalar> Dsayfalar { get; set; }
        public DbSet<Mesaj> Mesaj { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Diller> Diller { get; set; }
        public DbSet<Temsilci> Temsilci { get; set; }
        public DbSet<ChartRadarHeader> ChartRadarHeaders { get; set; }
        public DbSet<ChartRadarItem> ChartRadarItems { get; set; }
    }
}
