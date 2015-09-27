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
    public class SecurityTeamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SecurityTeams
        public async Task<ActionResult> Index()
        {
            return View(await db.SecurityTeams.ToListAsync());
        }

        // GET: SecurityTeams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityTeam securityTeam = await db.SecurityTeams.FindAsync(id);
            if (securityTeam == null)
            {
                return HttpNotFound();
            }
            return View(securityTeam);
        }

        // GET: SecurityTeams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecurityTeams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Country,City,Number,IsReady,LastSeen")] SecurityTeam securityTeam)
        {
            if (ModelState.IsValid)
            {
                db.SecurityTeams.Add(securityTeam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(securityTeam);
        }

        // GET: SecurityTeams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var securityTeamsViewModel = new SecurityTeamsVievModel
            {
                SecurityTeam = db.SecurityTeams.Include(i => i.SecureObjects).First(i => i.Id == id)
            };
            if (securityTeamsViewModel.SecurityTeam == null) return HttpNotFound();

            var allSecureObjectsList = db.SecureObjects.ToList();
            securityTeamsViewModel.AllSecurityTeams = allSecureObjectsList.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });

            ViewBag.EmployerID =
                    new SelectList(db.Users, "Id", "FullName", securityTeamsViewModel.SecurityTeam.Name);

            return View(securityTeamsViewModel);
        }

        // POST: SecurityTeams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]//[Bind(Include = "Title,Id,EmployerID,SelectedJobTags")]
        public ActionResult Edit(SecurityTeamsVievModel SecurityTeamsVievModel)
        {

            if (SecurityTeamsVievModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                var jobToUpdate = db.SecurityTeams
                    .Include(i => i.SecureObjects ).First(i => i.Id == SecurityTeamsVievModel.SecurityTeam.Id);

                if (TryUpdateModel(jobToUpdate, "SecurityTeam", new string[] { "Title", "EmployerID" }))
                {
                    var newJobTags = db.SecureObjects.Where(
                        m => SecurityTeamsVievModel.SelectedSecureObjects.Contains(m.Id)).ToList();
                    var updatedSecureObjects = new HashSet<int>(SecurityTeamsVievModel.SelectedSecureObjects);
                    foreach (SecureObject secObj in db.SecureObjects)
                    {
                        if (!updatedSecureObjects.Contains(secObj.Id))
                        {
                            jobToUpdate.SecureObjects.Remove(secObj);
                        }
                        else
                        {
                            jobToUpdate.SecureObjects.Add((secObj));
                        }
                    }

                    db.Entry(jobToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            //ViewBag.EmployerID = new SelectList(db.Employers, "Id", "FullName", jobpostView.JobPost.EmployerID);
            return View(SecurityTeamsVievModel);
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Country,City,Number,IsReady,LastSeen,SecureObjects")] SecurityTeam securityTeam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(securityTeam).State = EntityState.Modified;
               // securityTeam.SecureObjects = aa.SelectedSecureObjects;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(securityTeam);
        }
        

        // GET: SecurityTeams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityTeam securityTeam = await db.SecurityTeams.FindAsync(id);
            if (securityTeam == null)
            {
                return HttpNotFound();
            }
            return View(securityTeam);
        }
        */

        // POST: SecurityTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SecurityTeam securityTeam = await db.SecurityTeams.FindAsync(id);
            db.SecurityTeams.Remove(securityTeam);
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
