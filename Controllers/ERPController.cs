using MVCFilmSON.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MVCFilmSON.Controllers
{
    public class ERPController : Controller
    {
        // GET: ERP
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PartialSlider()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.Filmler.Take(3).ToList());
        }
        public ActionResult PartialOzetler()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.Filmler.OrderByDescending(x=>x.FilmID).Take(4).ToList());
        }


        public ActionResult Yerli(string sirala, int? y, int? gelensayfa)
        {
            int simdikisayfa = gelensayfa ?? 1;
            int sayfaBasiGösterim = 1;

            ViewBag.s = sirala;
            ViewBag.secilen = y;

            ApplicationDbContext ctx = new ApplicationDbContext();
            var liste = ctx.Filmler.Where(x => !x.Yabanci).ToList();
            if (y != null) liste = liste.Where(x => x.Yil == y).ToList();
            switch (sirala)
            {
                case "Alfabetik A-Z": liste = liste.OrderBy(x => x.FilmAdi).ToList(); break;
                case "Alfabetik Z-A": liste = liste.OrderByDescending(x => x.FilmAdi).ToList(); break;
                case "Yeniden Eskiye": liste = liste.OrderByDescending(x => x.FilmID).ToList(); break;
                case "Eskiden Yeniye": liste = liste.OrderBy(x => x.FilmID).ToList(); break;
                default: break;

            }
            return View(liste.ToPagedList(simdikisayfa, sayfaBasiGösterim));
        }

        public ActionResult Yabanci(string sirala,int? y,int? gelensayfa)
        {
            int simdikisayfa = gelensayfa ?? 1;
            int sayfaBasiGösterim = 1;

            ViewBag.s = sirala;
            ViewBag.secilen = y;

            ApplicationDbContext ctx = new ApplicationDbContext();
            var liste = ctx.Filmler.Where(x => x.Yabanci).ToList();
        
            if (y != null) liste = liste.Where(x => x.Yil == y).ToList();
            switch (sirala)
            {
                case "Alfabetik A-Z": liste = liste.OrderBy(x => x.FilmAdi).ToList(); break;
                case "Alfabetik Z-A": liste = liste.OrderByDescending(x => x.FilmAdi).ToList(); break;
                case "Yeniden Eskiye": liste = liste.OrderByDescending(x => x.FilmID).ToList(); break;
                case "Eskiden Yeniye": liste = liste.OrderBy(x => x.FilmID).ToList(); break;
                default: break;

            }
            return View(liste.ToPagedList(simdikisayfa, sayfaBasiGösterim));
        }

        public ActionResult Arama(string aranan)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var liste = ctx.Filmler.Where(x => x.FilmAdi.Contains(aranan)).ToList();
            ViewBag.aranankelime = aranan;
            return View(liste);
        }
        public ActionResult Yil(short y,string sirala,int? gelensayfa)
        {
           
            ViewBag.secilen = y;
            int simdikisayfa = gelensayfa ?? 1;
            int sayfaBasiGösterim = 1;

            ViewBag.s = sirala;
            ViewBag.secilen = y;

            ApplicationDbContext ctx = new ApplicationDbContext();
            var liste = ctx.Filmler.Where(x => x.Yabanci).ToList();

            if (y != null) liste = liste.Where(x => x.Yil == y).ToList();
            switch (sirala)
            {
                case "Alfabetik A-Z": liste = liste.OrderBy(x => x.FilmAdi).ToList(); break;
                case "Alfabetik Z-A": liste = liste.OrderByDescending(x => x.FilmAdi).ToList(); break;
                case "Yeniden Eskiye": liste = liste.OrderByDescending(x => x.FilmID).ToList(); break;
                case "Eskiden Yeniye": liste = liste.OrderBy(x => x.FilmID).ToList(); break;
                default: break;

            }
            return View(liste.ToPagedList(simdikisayfa, sayfaBasiGösterim));

        }
    }
}