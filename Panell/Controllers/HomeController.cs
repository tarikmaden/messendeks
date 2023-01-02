using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Asn1.Cms;
using Panell.Entities;
using Panell.Models;
using System.Diagnostics;

namespace Panell.Controllers
{

    public class HomeController : Controller
    {
        private readonly SayfaContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, SayfaContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var timeSpan = DateTime.Now;
            ViewBag.time = string.Format("{0}-{1}-{2}", timeSpan.Hour, timeSpan.Minute, timeSpan.Second);
            ViewBag.seo = _context.Sayfalar.Find(1);
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.slider = _context.Sayfalar.Where(x => x.sayfa_kategori == 10.ToString()).ToList();
            ViewBag.footer_year = DateTime.Now.Year.ToString();
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.analiz = _context.Sayfalar.Find(14);
            ViewBag.yatirimci = _context.Sayfalar.Find(16);
            ViewBag.endeksi = _context.Sayfalar.Find(17);
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);

            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }

        public IActionResult Kurumsal(int ID)
        {
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.KurumsalListe = _context.Sayfalar.Where(x => x.sayfa_kategori == 2.ToString()).ToList();
            ViewBag.footer_year = DateTime.Now.Year.ToString();
            ViewBag.KurumsalPage = _context.Sayfalar.Find(2);
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }

        [HttpGet]
        public IActionResult Haberler(int ID, int page = 1, int pageSize = 5)
        {
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.KurumsalListe = _context.Sayfalar.Where(x => x.sayfa_kategori == 2.ToString()).ToList();
            ViewBag.footer_year = DateTime.Now.Year.ToString();
            ViewBag.KurumsalPage = _context.Sayfalar.Find(2);
            ViewBag.seo = _context.Sayfalar.Find(3);
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.Haberler = _context.Sayfalar.Where(x => x.sayfa_kategori == 3.ToString()).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.pageSize = pageSize;
            ViewBag.PageNumber = page;
            var sayfalar = _context.Sayfalar.Where(x => x.sayfa_kategori == 3.ToString());
            ViewBag.totalCount = sayfalar.Count();
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }

        public IActionResult Iletisim()
        {
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.temsilci = _context.Temsilci.ToList();
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }
        public IActionResult Yatirimci_endeksi_gorusu()
        {
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.yatirimci = _context.Sayfalar.Find(8);
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.yatirimci_text = _context.Sayfalar.Find(16);
            ViewBag.yatirimci_listesi = _context.Dsayfalar.Where(x => Convert.ToInt32(x.sayfa_kategori) == 8).ToList();
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.profil = _context.Dsayfalar.Find(4);
            ViewBag.girdiler = _context.Dsayfalar.Find(5);
            ViewBag.tabpanel = _context.Sayfalar.Find(37);
            ViewBag.tabpanel2 = _context.Sayfalar.Find(38);
            ViewBag.tabpanel3 = _context.Sayfalar.Find(39);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }
        public IActionResult Yapim_asamasinda()
        {
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Iletisim_send(Mesaj Mesajs)
        {
            Mesajs.phone = Mesajs.phone;
            Mesajs.email = Mesajs.email;
            Mesajs.name = Mesajs.name;
            Mesajs.surname = Mesajs.surname;
            Mesajs.message = Mesajs.message;
            _context.Add(Mesajs);
            _context.SaveChanges();
            TempData["iletisim_success"] = "success";
            return RedirectToAction("iletisim", TempData);
        }


        public IActionResult Haber(int ID)
        {
            ViewBag.Haberler = _context.Sayfalar.Where(x => x.sayfa_kategori == 3.ToString()).ToList();
            ViewBag.Detay = _context.Sayfalar.Find(ID);
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }
        public IActionResult Sayfa(int ID)
        {
            ViewBag.Haberler = _context.Sayfalar.Where(x => x.sayfa_kategori == 3.ToString()).ToList();
            ViewBag.Detay = _context.Sayfalar.Find(ID);
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }

        public IActionResult Arama_sonuclari(string search, int page = 1, int pageSize = 5)
        {

            if (search == null || search == "")
            {
                return RedirectToAction("index");
            }
            ViewBag.search = search.ToString();

            ViewBag.Haberler = _context.Sayfalar.Where(x => x.sayfa_kategori == 3.ToString()).ToList();
            ViewBag.Detay = _context.Sayfalar.Where(c => Convert.ToInt32(c.sayfa_kategori) == 3 || Convert.ToInt32(c.sayfa_kategori) == 2).Where(c => c.sayfa_adi.Contains(search)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.DetayCount = _context.Sayfalar.Where(c => Convert.ToInt32(c.sayfa_kategori) == 3 || Convert.ToInt32(c.sayfa_kategori) == 2).Where(c => c.sayfa_adi.Contains(search)).Count();
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);

            ViewBag.pageSize = pageSize;
            ViewBag.PageNumber = page;
            var sayfalar = _context.Sayfalar.Where(x => x.sayfa_kategori == 3.ToString());
            ViewBag.totalCount = sayfalar.Count();
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }

        public IActionResult Hakkimizda(string ID)
        {
            ViewBag.KurumsalListe = _context.Sayfalar.Where(x => x.sayfa_kategori == 2.ToString()).ToList();
            ViewBag.Haberler = _context.Sayfalar.Where(x => x.sayfa_kategori == 3.ToString()).ToList();
            ViewBag.Detay = _context.Sayfalar.Where(x => x.sayfa_slug == ID).First();
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.slider = _context.Sayfalar.Where(x => x.sayfa_kategori == 10.ToString()).ToList();
            ViewBag.footer_year = DateTime.Now.Year.ToString();
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.analiz = _context.Sayfalar.Find(14);
            ViewBag.yatirimci = _context.Sayfalar.Find(16);
            ViewBag.endeksi = _context.Sayfalar.Find(17);
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Error1(int code)
        {
            ViewBag.sayfalistesi = _context.Sayfalar.Where(x => x.sayfa_kategori == null).OrderBy(x => x.ID).Take(3).ToList();
            ViewBag.slider = _context.Sayfalar.Where(x => x.sayfa_kategori == 10.ToString()).ToList();
            ViewBag.footer_year = DateTime.Now.Year.ToString();
            ViewBag.Iletisim = _context.Iletisim.Find(1);
            ViewBag.analiz = _context.Sayfalar.Find(14);
            ViewBag.yatirimci = _context.Sayfalar.Find(16);
            ViewBag.endeksi = _context.Sayfalar.Find(17);
            ViewBag.yatirimci_gorusu_endeksi = _context.Sayfalar.Find(8);
            ViewBag.sartlar_ve_kosullar = _context.Sayfalar.Find(6);
            ViewBag.gizlilik_politikasi = _context.Sayfalar.Find(7);
            ViewBag.cerez_politikasi = _context.Sayfalar.Find(36);
            ViewBag.linkler = _context.Sayfalar.Find(42);
            return View();
        }


        public IActionResult ChartDetayGetir()
        {
            var drops = new List<DropItem>();
            var renkKods = new List<string>();
            var headers = _context.ChartRadarHeaders.ToList();

            foreach (var header in headers)
            {
                var items = _context.ChartRadarItems.Where(a => a.ChartRadarHeaderId == header.Id).ToList();

                var drop = new DropItem
                {
                    Tanim = header.ChartRadarHeaderAdi,
                    DigerTanim = header.RenkKodu
                };

                foreach (var item in items)
                {
                    drop.ItemValues.Add(new ItemValue
                    {
                        Text = item.ChartRadarItemAdi,
                        IdInt = item.Deger1,
                        IdInt2 = item.Deger2
                    });

                }
                renkKods.Add(header.RenkKodu);
                drops.Add(drop);
            }

            return new JsonResult(new { data = drops, renkKods })
            {
                StatusCode = StatusCodes.Status200OK // 
            };
        }
    }
}