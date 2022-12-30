using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Panell.Models;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using System.Drawing;
using MessagePack;

namespace Panell.Controllers
{
    public class PanelController : Controller
    {
        private readonly SayfaContext _context;
        private readonly ILogger<PanelController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PanelController(ILogger<PanelController> logger, SayfaContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // seo
        public static string SeoUrl(object a)
        {
            string s = a.ToString();

            //s = oncul + id + "-" + s;
            if (string.IsNullOrEmpty(s)) //string yok veya boş ise true döndürür
            {
                return "";
            }

            if (s.Length > 80)
            {
                s = s.Substring(0, 80); //string den belli karakter alır.
            }
            s = s.Replace("ş", "s"); //karakter değişimi için kullanılır
            s = s.Replace("Ş", "S");
            s = s.Replace("ğ", "g");
            s = s.Replace("Ğ", "G");
            s = s.Replace("İ", "i");
            s = s.Replace("ı", "i");
            s = s.Replace("ç", "c");
            s = s.Replace("Ç", "C");
            s = s.Replace("ö", "o");
            s = s.Replace("Ö", "O");
            s = s.Replace("ü", "u");
            s = s.Replace("Ü", "U");
            s = s.Replace("'", "");
            s = s.Replace("\"", "");
            Regex r = new Regex("[^a-zA-Z0-9_-]");
            //if (r.IsMatch(s))
            s = r.Replace(s, "-");
            if (!string.IsNullOrEmpty(s))
                while (s.IndexOf("--") > -1)
                    s = s.Replace("--", "-");
            if (s.StartsWith("-")) s = s.Substring(1);
            if (s.EndsWith("-")) s = s.Substring(0, s.Length - 1);
            return s.ToLower();
        }
        // seo

        // login işlemi

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Loginn(Users user)
        {
            var girisyapam = await _context.Users.Where(x => x.user_email == user.user_email && x.user_password == user.user_password).SingleOrDefaultAsync();
            if (girisyapam != null)
            {
                var claims = new List<Claim>
                {
                   new Claim(ClaimTypes.Name, girisyapam.user_name)
                };

                HttpContext.Response.Cookies.Append("user_id", girisyapam.ID.ToString());

                var useridentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error_login"] = "error_login";
                return RedirectToAction("login", TempData);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
        // login işlemi

        // ayarlar

        [Authorize]
        public IActionResult Ayarlar(int ID)
        {
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            var guncellenecekSayfa = _context.Users.Find(ID);
            return View(guncellenecekSayfa);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Ayarlar_update([Bind("ID,user_name,user_email,user_password")] Users User)
        {
            _context.Update(User);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        public IActionResult Iletisim(int ID)
        {
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            var guncellenecekSayfa = _context.Iletisim.Find(ID);
            return View(guncellenecekSayfa);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Iletisim_update(Iletisim Iletisim)
        {
            Iletisim updateiletisim = _context.Iletisim.Find(Iletisim.ID);

            updateiletisim.email = Iletisim.email;
            updateiletisim.phone = Iletisim.phone;
            updateiletisim.facebook = Iletisim.facebook;
            updateiletisim.instagram = Iletisim.instagram;
            updateiletisim.twitter = Iletisim.twitter;
            updateiletisim.youtube = Iletisim.youtube;
            updateiletisim.google = Iletisim.google;
            updateiletisim.adres = Iletisim.adres;
            updateiletisim.maps = Iletisim.maps;
            updateiletisim.text1 = Iletisim.text1;
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logo(Iletisim Iletisim)
        {
            Iletisim updatelogo = _context.Iletisim.Find(Iletisim.ID);

            if (Iletisim.Dosya != null)
            {
                var dosya_yolu = Path.Combine(_hostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(dosya_yolu))
                {
                    Directory.CreateDirectory(dosya_yolu);
                }

                var filename = $"{DateTime.Now:MMddHHmmss}.{Iletisim.Dosya.FileName.Split('.').Last()}";

                var tamDosyaAdi = Path.Combine(dosya_yolu, filename);
                using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                {
                    await Iletisim.Dosya.CopyToAsync(dosyaAkisi);
                }

                updatelogo.logo = filename;
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                var dosya_yolu = Path.Combine(_hostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(dosya_yolu))
                {
                    Directory.CreateDirectory(dosya_yolu);
                }

                var filename = $"{DateTime.Now:MMddHHmmss}.{Iletisim.Dosya2.FileName.Split('.').Last()}";

                var tamDosyaAdi = Path.Combine(dosya_yolu, filename);
                using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                {
                    await Iletisim.Dosya2.CopyToAsync(dosyaAkisi);
                }

                updatelogo.favicon = filename;
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        // ayarlar

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.toplamSayfalar = _context.Sayfalar.Count();
            ViewBag.toplamDigersayfalar = _context.Dsayfalar.Count();
            ViewBag.diller = _context.Diller.Count();
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Sayfa_listesi()
        {
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult mesajlar()
        {
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            ViewBag.mesajlar = _context.Mesaj.ToList();
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult temsilci()
        {
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            ViewBag.temsilci = _context.Temsilci.ToList();
            return View();
        }

        [Authorize]
        public IActionResult Sayfa_olustur()
        {
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            return View();
        }
        [Authorize]
        public IActionResult Chart_yonetimi()
        {
            ViewBag.cartlar = _context.Dsayfalar.ToList();
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Sayfa_olustur(Sayfalar Sayfa)
        {
            Sayfa.sayfa_adi = Sayfa.sayfa_adi;
            Sayfa.sayfa_slug = SeoUrl(Sayfa.sayfa_adi);
            Sayfa.sayfa_tarih = DateTime.Now.ToString("dd.MM.yyyy");
            _context.Add(Sayfa);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Temsilci_ekle(Temsilci temsilci)
        {
            temsilci.title = temsilci.title;
            temsilci.adres = temsilci.adres;
            temsilci.phone = temsilci.phone;
            temsilci.phone2 = temsilci.phone2;
            temsilci.email = temsilci.email;
            _context.Add(temsilci);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Temsilci_duzenle(Temsilci Temsilci)
        {
            Temsilci updatetemsilci = _context.Temsilci.Find(Temsilci.ID);
            updatetemsilci.title = Temsilci.title;
            updatetemsilci.adres = Temsilci.adres;
            updatetemsilci.phone = Temsilci.phone;
            updatetemsilci.phone2 = Temsilci.phone2;
            updatetemsilci.email = Temsilci.email;
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Chart_olustur(Dsayfalar Dsayfa)
        {
            Dsayfa.sayfa_adi = Dsayfa.sayfa_adi;
            Dsayfa.sayfa_slug = SeoUrl(Dsayfa.sayfa_adi);
            Dsayfa.sayfa_tarih = DateTime.Now.ToString("dd.MM.yyyy");
            _context.Add(Dsayfa);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Yatirimci_alt_kategori(Dsayfalar Dsayfa)
        {
            if (Dsayfa.Dosya != null)
            {
                var dosya_yolu = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(dosya_yolu))
                {
                    Directory.CreateDirectory(dosya_yolu);
                }
                var filename = $"{DateTime.Now:MMddHHmmss}.{Dsayfa.Dosya.FileName.Split('.').Last()}";
                var tamDosyaAdi = Path.Combine(dosya_yolu, filename);
                using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                {
                    await Dsayfa.Dosya.CopyToAsync(dosyaAkisi);
                }

                Dsayfa.sayfa_resim = filename;
                Dsayfa.sayfa_adi = Dsayfa.sayfa_adi;
                Dsayfa.sayfa_ozet = Dsayfa.sayfa_ozet;
                Dsayfa.sayfa_kategori = Dsayfa.sayfa_kategori;
                _context.Add(Dsayfa);
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                Dsayfa.sayfa_adi = Dsayfa.sayfa_adi;
                Dsayfa.sayfa_ozet = Dsayfa.sayfa_ozet;
                Dsayfa.sayfa_kategori = Dsayfa.sayfa_kategori;
                _context.Add(Dsayfa);
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        [Authorize]
        public IActionResult Sayfa_guncelle(int ID)
        {
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            var guncellenecekSayfa = _context.Sayfalar.Find(ID);
            return View(guncellenecekSayfa);
        }

        [Authorize]
        public async Task<IActionResult> Sayfa_sil(int ID)
        {
            var silineceksayfa = await _context.Sayfalar.FindAsync(ID);
            _context.Remove(silineceksayfa);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        public async Task<IActionResult> Chart_sil(int ID)
        {
            var silinecekchart = await _context.Dsayfalar.FindAsync(ID);
            _context.Remove(silinecekchart);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        public async Task<IActionResult> Yatirimci_alt_sil(int ID)
        {
            var silinecek = await _context.Dsayfalar.FindAsync(ID);
            _context.Remove(silinecek);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        public async Task<IActionResult> DSayfa_sil(int ID)
        {
            var silineceksayfa = await _context.Dsayfalar.FindAsync(ID);
            _context.Remove(silineceksayfa);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        public async Task<IActionResult> Mesaj_sil(int ID)
        {
            var s = await _context.Mesaj.FindAsync(ID);
            _context.Remove(s);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        public async Task<IActionResult> Temsilci_sil(int ID)
        {
            var s = await _context.Temsilci.FindAsync(ID);
            _context.Remove(s);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }


        // dinamik yapı
        [Authorize]
        [HttpGet]
        public IActionResult Sayfa(int ID, Dsayfalar Dsayfa)
        {
            ViewBag.yatirimci_alt = _context.Dsayfalar.ToList();
            ViewBag.dillistesiKontrol = _context.Diller.Count();
            ViewBag.dillistesi = _context.Diller.ToList();
            ViewBag.Digersayfalar = _context.Dsayfalar.Where(x => x.baglantili_sayfa == ID.ToString()).ToList();
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            ViewBag.galerilistesi = _context.Galeri.Where(x => x.gelen_id == ID.ToString()).ToList();
            var guncellenecekSayfa = _context.Sayfalar.Find(ID);

            return View(guncellenecekSayfa);

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sayfa_guncel(Sayfalar Sayfa)
        {
            Sayfalar updatesayfa = _context.Sayfalar.Find(Sayfa.ID);

            if (Sayfa.Dosya != null)
            {
                var dosya_yolu = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(dosya_yolu))
                {
                    Directory.CreateDirectory(dosya_yolu);
                }
                var filename = $"{DateTime.Now:MMddHHmmss}.{Sayfa.Dosya.FileName.Split('.').Last()}";
                var tamDosyaAdi = Path.Combine(dosya_yolu, filename);
                using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                {
                    await Sayfa.Dosya.CopyToAsync(dosyaAkisi);
                }

                updatesayfa.sayfa_resim = filename;
                updatesayfa.sayfa_adi = Sayfa.sayfa_adi;
                updatesayfa.sayfa_ozet = Sayfa.sayfa_ozet;
                updatesayfa.sayfa_icerik = Sayfa.sayfa_icerik;
                updatesayfa.sayfa_slug = SeoUrl(Sayfa.sayfa_adi);
                updatesayfa.sayfa_title = Sayfa.sayfa_title;
                updatesayfa.sayfa_desc = Sayfa.sayfa_desc;
                updatesayfa.sayfa_order = Sayfa.sayfa_order;
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                updatesayfa.sayfa_adi = Sayfa.sayfa_adi;
                updatesayfa.sayfa_ozet = Sayfa.sayfa_ozet;
                updatesayfa.sayfa_icerik = Sayfa.sayfa_icerik;
                updatesayfa.sayfa_slug = SeoUrl(Sayfa.sayfa_adi);
                updatesayfa.sayfa_title = Sayfa.sayfa_title;
                updatesayfa.sayfa_desc = Sayfa.sayfa_desc;
                updatesayfa.sayfa_order = Sayfa.sayfa_order;
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Chart_kategori_guncel(Dsayfalar Dsayfa)
        {
            Dsayfalar updatechart = _context.Dsayfalar.Find(Dsayfa.ID);

            updatechart.sayfa_adi = Dsayfa.sayfa_adi;
            updatechart.sayfa_ozet = Dsayfa.sayfa_ozet;
            updatechart.sayfa_icerik = Dsayfa.sayfa_icerik;
            updatechart.sayfa_slug = SeoUrl(Dsayfa.sayfa_adi);
            updatechart.sayfa_title = Dsayfa.sayfa_title;
            updatechart.sayfa_desc = Dsayfa.sayfa_desc;
            updatechart.sayfa_order = Dsayfa.sayfa_order;
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> yatirimci_liste_guncel(Dsayfalar Dsayfa)
        {
            Dsayfalar update = _context.Dsayfalar.Find(Dsayfa.ID);

            if (Dsayfa.Dosya != null)
            {
                var dosya_yolu = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(dosya_yolu))
                {
                    Directory.CreateDirectory(dosya_yolu);
                }
                var filename = $"{DateTime.Now:MMddHHmmss}.{Dsayfa.Dosya.FileName.Split('.').Last()}";
                var tamDosyaAdi = Path.Combine(dosya_yolu, filename);
                using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                {
                    await Dsayfa.Dosya.CopyToAsync(dosyaAkisi);
                }

                update.sayfa_resim = filename;
                update.sayfa_adi = Dsayfa.sayfa_adi;
                update.sayfa_ozet = Dsayfa.sayfa_ozet;
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                update.sayfa_adi = Dsayfa.sayfa_adi;
                update.sayfa_ozet = Dsayfa.sayfa_ozet;
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }

            

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sayfa_guncel_diger(Dsayfalar Dsayfa)
        {
            Dsayfalar updatesayfa = _context.Dsayfalar.Find(Dsayfa.ID);
            ViewBag.Dsayfam = _context.Dsayfalar.Where(x => x.ID == Dsayfa.ID).First();
            ViewBag.Sayfalarr = _context.Sayfalar.Where(x => x.ID.ToString() == updatesayfa.baglantili_sayfa).First();

            if (Dsayfa.Dosya != null)
            {
                var dosya_yolu = Path.Combine(_hostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(dosya_yolu))
                {
                    Directory.CreateDirectory(dosya_yolu);
                }

                var tamDosyaAdi = Path.Combine(dosya_yolu, Dsayfa.Dosya.FileName);
                using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                {
                    await Dsayfa.Dosya.CopyToAsync(dosyaAkisi);
                }

                updatesayfa.sayfa_resim = Dsayfa.Dosya.FileName;
                updatesayfa.sayfa_adi = Dsayfa.sayfa_adi;
                updatesayfa.sayfa_ozet = Dsayfa.sayfa_ozet;
                updatesayfa.sayfa_icerik = Dsayfa.sayfa_icerik;
                updatesayfa.sayfa_slug = ViewBag.Sayfalarr.sayfa_slug;
                updatesayfa.sayfa_kategori = ViewBag.Sayfalarr.sayfa_kategori;
                updatesayfa.sayfa_title = Dsayfa.sayfa_title;
                updatesayfa.sayfa_desc = Dsayfa.sayfa_desc;
                updatesayfa.sayfa_order = Dsayfa.sayfa_order;
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                updatesayfa.sayfa_adi = Dsayfa.sayfa_adi;
                updatesayfa.sayfa_ozet = Dsayfa.sayfa_ozet;
                updatesayfa.sayfa_icerik = Dsayfa.sayfa_icerik;
                updatesayfa.sayfa_slug = ViewBag.Sayfalarr.sayfa_slug;
                updatesayfa.sayfa_kategori = ViewBag.Sayfalarr.sayfa_kategori;
                updatesayfa.sayfa_title = Dsayfa.sayfa_title;
                updatesayfa.sayfa_desc = Dsayfa.sayfa_desc;
                updatesayfa.sayfa_order = Dsayfa.sayfa_order;
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sayfa_galeri([Bind("ID,gelen_id,Dosya")] Galeri galeri)
        {
            var dosya_yolu = Path.Combine(_hostEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(dosya_yolu))
            {
                Directory.CreateDirectory(dosya_yolu);
            }

            var tamDosyaAdi = Path.Combine(dosya_yolu, galeri.Dosya.FileName);
            using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
            {
                await galeri.Dosya.CopyToAsync(dosyaAkisi);
            }

            galeri.gelen_id = galeri.gelen_id;
            galeri.resim = galeri.Dosya.FileName;
            _context.Add(galeri);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        public async Task<IActionResult> Galeri_sil(int ID)
        {
            var silineceksayfa = await _context.Galeri.FindAsync(ID);
            _context.Remove(silineceksayfa);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Sayfa_kategori(Sayfalar Sayfa)
        {
            Sayfa.sayfa_adi = Sayfa.sayfa_adi;
            Sayfa.sayfa_slug = SeoUrl(Sayfa.sayfa_adi);
            Sayfa.sayfa_kategori = Sayfa.sayfa_kategori;
            Sayfa.sayfa_tarih = DateTime.Now.ToString("dd.MM.yyyy");
            _context.Add(Sayfa);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Chart_kategori(Dsayfalar Dsayfa)
        {
            Dsayfa.sayfa_adi = Dsayfa.sayfa_adi;
            Dsayfa.sayfa_slug = SeoUrl(Dsayfa.sayfa_adi);
            Dsayfa.sayfa_kategori = Dsayfa.sayfa_kategori;
            Dsayfa.sayfa_tarih = DateTime.Now.ToString("dd.MM.yyyy");
            _context.Add(Dsayfa);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // dinamik yapı

        // dil yapı
        [HttpGet]
        [Authorize]
        public IActionResult Dil_listesi()
        {
            ViewBag.dillistesi = _context.Diller.ToList();
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            return View();
        }

        [Authorize]
        public IActionResult Dil_olustur()
        {
            ViewBag.dillistesi = _context.Diller.ToList();
            ViewBag.digersayfalar = _context.Dsayfalar.ToList();
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Dil_olustur(Diller Dil)
        {
            Dil.dil_adi = Dil.dil_adi;
            Dil.dil_kisaltma = Dil.dil_kisaltma;
            _context.Add(Dil);
            _context.SaveChanges();
            return RedirectToAction(nameof(Dil_olustur));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Sayfalara_dil_ata(Dsayfalar dsayfalar)
        {
            _context.Add(dsayfalar);
            _context.SaveChanges();
            return RedirectToAction(nameof(Dil_olustur));
        }

        [Authorize]
        public IActionResult Dil_guncelle(int ID)
        {
            ViewBag.dillistesi = _context.Diller.ToList();
            ViewBag.sayfalistesi = _context.Sayfalar.ToList();
            var dilguncelle = _context.Diller.Find(ID);
            return View(dilguncelle);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Dil_update([Bind("ID,dil_adi,dil_kisaltma")] Diller Dil)
        {
            _context.Update(Dil);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }


        [Authorize]
        public async Task<IActionResult> Dil_sil(int ID)
        {
            var silinecekdil = await _context.Diller.FindAsync(ID);
            _context.Remove(silinecekdil);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dil_listesi));
        }
        // dil yapı

    }
}