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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace TestProject.Controllers
{
    public class SecureObjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: SecureObjects
        public async Task<ActionResult> Index()
        {
            //return View(await db.SecureObjects.ToListAsync());

            var user = UserManager.FindByName(User.Identity.Name);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var id = user.Id;
                return View(await db.SecureObjects.Where(usrId => usrId.UserProfile.Id == id).ToListAsync());
            }

        }

        // GET: SecureObjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.FindByName(User.Identity.Name);
            var secureObject = await db.SecureObjects.Where(a => a.Id == id && a.UserProfile.Id == user.Id).ToListAsync();   
            if (secureObject.Count == 0 )
            {
                return HttpNotFound();
            }
            SecureObject obj = secureObject[0];
            /*
            var userId = UserManager.FindByName(User.Identity.Name);
            if (secureObject.UserProfile != userId)
            {
                return HttpNotFound();
            }
            */
            return View(obj);
        }

        // GET: SecureObjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecureObjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,OwnerName,OwnerPhoneNumber,Country,City,StreetName,ObjectNumber,PaidUntil")] SecureObject secureObject)
        {
            if (ModelState.IsValid)
            {
                db.SecureObjects.Add(secureObject);
                var currentUser = db.Users.Find(User.Identity.GetUserId());
                secureObject.UserProfile = currentUser;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(secureObject);
        }

        // GET: SecureObjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.FindByName(User.Identity.Name);
            var secureObject = await db.SecureObjects.Where(a => a.Id == id && a.UserProfile.Id == user.Id).ToListAsync();

            if (secureObject.Count == 0)
            {
                return HttpNotFound();
            }
            SecureObject obj = secureObject[0];
            return View(obj);
        }

        // POST: SecureObjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,OwnerName,OwnerPhoneNumber,Country,City,StreetName,ObjectNumber,PaidUntil")] SecureObject secureObject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secureObject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(secureObject);
        }

        // GET: SecureObjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.FindByName(User.Identity.Name);
            var secureObject = await db.SecureObjects.Where(a => a.Id == id && a.UserProfile.Id == user.Id).ToListAsync();

            if (secureObject.Count == 0)
            {
                return HttpNotFound();
            }
            SecureObject obj = secureObject[0];
            return View(obj);
        }

        // POST: SecureObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = UserManager.FindByName(User.Identity.Name);
            var secureObject = await db.SecureObjects.Where(a => a.Id == id && a.UserProfile.Id == user.Id).ToListAsync();
            if(secureObject.Count == 0)
            {
                return RedirectToAction("Index");
            }
            SecureObject obj = secureObject[0];
            db.SecureObjects.Remove(obj);
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
