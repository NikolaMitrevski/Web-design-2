using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrutoNetoApp.Models;

namespace BrutoNetoApp.Controllers
{
    public class RadnikController : Controller
    {
        private BrutoNetoDBEntities db = new BrutoNetoDBEntities();

        // GET: Radnik
        public ActionResult Index()
        {
            return View(db.Radniks.ToList());
        }

        // GET: Radnik/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radnik radnik = db.Radniks.Find(id);
            if (radnik == null)
            {
                return HttpNotFound();
            }
            return View(radnik);
        }

        // GET: Radnik/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Radnik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_radnika,Ime,Prezime,Adresa,Iznos_NP,Iznos_BP,OZOP,Porez,PNTR,ZNTR,NNTR,UDNTR,PNTP,ZNTP,UDNP,TPZI")] Radnik radnik)
        {
            if (ModelState.IsValid)
            {
                double poresko_oslobadjanje = 18300;   // za 2021 godinu

                radnik.Iznos_BP = (radnik.Iznos_NP - 1830) / 0.701;
                radnik.OZOP = radnik.Iznos_BP - poresko_oslobadjanje;
                radnik.Porez = radnik.OZOP * 0.1;       // 10% - za 2021 godinu
                radnik.PNTR = radnik.Iznos_BP * 0.14;   // 14% - za 2021 godinu
                radnik.ZNTR = radnik.Iznos_BP * 0.0515; // 5.15% - za 2021 godinu
                radnik.NNTR = radnik.Iznos_BP * 0.0075; // 0.75% - za 2021 godinu
                radnik.UDNTR = radnik.PNTR + radnik.ZNTR + radnik.NNTR;
                radnik.PNTP = radnik.Iznos_BP * 0.115;  // 11.5% - za 2021 godinu
                radnik.ZNTP = radnik.Iznos_BP * 0.0515; // 5.15% - za 2021 godinu
                radnik.UDNTP = radnik.PNTP + radnik.ZNTP;
                radnik.TPZI = radnik.Iznos_BP + radnik.UDNTP;

                db.Radniks.Add(radnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(radnik);
        }

        // GET: Radnik/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radnik radnik = db.Radniks.Find(id);
            if (radnik == null)
            {
                return HttpNotFound();
            }
            return View(radnik);
        }

        // POST: Radnik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_radnika,Ime,Prezime,Adresa,Iznos_NP,Iznos_BP,OZOP,Porez,PNTR,ZNTR,NNTR,UDNTR,PNTP,ZNTP,UDNP,TPZI")] Radnik radnik)
        {
            if (ModelState.IsValid)
            {
                double poresko_oslobadjanje = 18300;   // za 2021 godinu

                radnik.Iznos_BP = (radnik.Iznos_NP - 1830) / 0.701;
                radnik.OZOP = radnik.Iznos_BP - poresko_oslobadjanje;
                radnik.Porez = radnik.OZOP * 0.1;       // 10% - za 2021 godinu
                radnik.PNTR = radnik.Iznos_BP * 0.14;   // 14% - za 2021 godinu
                radnik.ZNTR = radnik.Iznos_BP * 0.0515; // 5.15% - za 2021 godinu
                radnik.NNTR = radnik.Iznos_BP * 0.0075; // 0.75% - za 2021 godinu
                radnik.UDNTR = radnik.PNTR + radnik.ZNTR + radnik.NNTR;
                radnik.PNTP = radnik.Iznos_BP * 0.115;  // 11.5% - za 2021 godinu
                radnik.ZNTP = radnik.Iznos_BP * 0.0515; // 5.15% - za 2021 godinu
                radnik.UDNTP = radnik.PNTP + radnik.ZNTP;
                radnik.TPZI = radnik.Iznos_BP + radnik.UDNTP;

                db.Entry(radnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(radnik);
        }

        // GET: Radnik/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radnik radnik = db.Radniks.Find(id);
            if (radnik == null)
            {
                return HttpNotFound();
            }
            return View(radnik);
        }

        // POST: Radnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Radnik radnik = db.Radniks.Find(id);
            db.Radniks.Remove(radnik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
