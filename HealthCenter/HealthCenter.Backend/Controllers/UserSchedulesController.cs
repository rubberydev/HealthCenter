using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthCenter.Backend.Models;
using HealthCenter.Domain;

namespace HealthCenter.Backend.Controllers
{
    public class UserSchedulesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: UserSchedules
        public async Task<ActionResult> Index()
        {
            var userSchedules = db.UserSchedules.Include(u => u.Scheduler).Include(u => u.User);
            return View(await userSchedules.ToListAsync());
        }

        // GET: UserSchedules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSchedule userSchedule = await db.UserSchedules.FindAsync(id);
            if (userSchedule == null)
            {
                return HttpNotFound();
            }
            return View(userSchedule);
        }

        // GET: UserSchedules/Create
        public ActionResult Create()
        {
            ViewBag.AgendaId = new SelectList(db.Schedulers, "AgendaId", "DateSchedule");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: UserSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserSchedule userSchedule)
        {
            if (ModelState.IsValid)
            {
                Scheduler scheduler = await db.Schedulers.FindAsync(userSchedule.AgendaId);
                scheduler.StateId = 2;
                db.Entry(scheduler).State = EntityState.Modified;

                db.UserSchedules.Add(userSchedule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AgendaId = new SelectList(db.Schedulers, "AgendaId", "ApplicationUser_Id", userSchedule.AgendaId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userSchedule.UserId);
            return View(userSchedule);
        }

        // GET: UserSchedules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSchedule userSchedule = await db.UserSchedules.FindAsync(id);
            if (userSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgendaId = new SelectList(db.Schedulers, "AgendaId", "ApplicationUser_Id", userSchedule.AgendaId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userSchedule.UserId);
            return View(userSchedule);
        }

        // POST: UserSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserScheduleId,AgendaId,UserId")] UserSchedule userSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSchedule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AgendaId = new SelectList(db.Schedulers, "AgendaId", "ApplicationUser_Id", userSchedule.AgendaId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userSchedule.UserId);
            return View(userSchedule);
        }

        // GET: UserSchedules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSchedule userSchedule = await db.UserSchedules.FindAsync(id);
            if (userSchedule == null)
            {
                return HttpNotFound();
            }
            return View(userSchedule);
        }

        // POST: UserSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserSchedule userSchedule = await db.UserSchedules.FindAsync(id);
            db.UserSchedules.Remove(userSchedule);
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
