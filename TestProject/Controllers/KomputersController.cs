using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class KomputersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Komputers
        public async Task<ActionResult> Index()
        {
            return View(await db.Komputers.ToListAsync());
        }

        // GET: Komputers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komputer komputer = await db.Komputers.FindAsync(id);
            if (komputer == null)
            {
                return HttpNotFound();
            }
            return View(komputer);
        }

        // GET: Komputers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Komputers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,System,Monitor,Rozdzielczosc,CzyDziala")] Komputer komputer)
        {
            if (ModelState.IsValid)
            {
                db.Komputers.Add(komputer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(komputer);
        }

        // GET: Komputers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komputer komputer = await db.Komputers.FindAsync(id);
            if (komputer == null)
            {
                return HttpNotFound();
            }
            return View(komputer);
        }

        // POST: Komputers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,System,Monitor,Rozdzielczosc,CzyDziala")] Komputer komputer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(komputer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(komputer);
        }

        // GET: Komputers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komputer komputer = await db.Komputers.FindAsync(id);
            if (komputer == null)
            {
                return HttpNotFound();
            }
            return View(komputer);
        }

        // POST: Komputers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Komputer komputer = await db.Komputers.FindAsync(id);
            db.Komputers.Remove(komputer);
            await db.SaveChangesAsync();
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
