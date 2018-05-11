namespace HealthCenter.Backend.Controllers
{
    using HealthCenter.Backend.Models;
    using HealthCenter.Domain;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class SchedulersController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Schedulers
        public async Task<ActionResult> Index()
        {
            var schedulers = db.Schedulers.Include(s => s.WorkDay);
            return View(await schedulers.ToListAsync());
        }

        // GET: Schedulers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scheduler scheduler = await db.Schedulers.FindAsync(id);
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            return View(scheduler);
        }

        // GET: Schedulers/Create
        public ActionResult Create()
        {
            ViewBag.idWorkDay = new SelectList(db.WorkDays.OrderBy(x => x.DateToday), "idWorkDay", "DateToday");
            return View();
        }

        // POST: Schedulers/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Scheduler scheduler)
        {
            if (ModelState.IsValid)
            {
                scheduler.DateToday = DateTime.Today.Date;

                var validate = await db.WorkDays.Select(x => x).Where(z => z.idWorkDay == scheduler.idWorkDay).FirstOrDefaultAsync();     
               
                if (validate.DateToday == scheduler.DateToday && validate.startDayHour.Hour <= scheduler.startHour.Hour &&
                    validate.startDayHour.Hour <= validate.endDayHour.Hour)
                {

                    var endDate = await db.Schedulers.Where(x => x
                                                     .idWorkDay == scheduler.idWorkDay)
                                                     .OrderByDescending(x => x.endHour).FirstOrDefaultAsync();


                    scheduler.endHour = scheduler.startHour.AddMinutes(validate.durationCite);
                    scheduler.ApplicationUser_Id = User.Identity.GetUserId();
                    db.Schedulers.Add(scheduler);
                    await db.SaveChangesAsync();
                }
                else
                {

                }                
            
                //scheduler.ApplicationUser_Id = User.Identity.GetUserId();
                //db.Schedulers.Add(scheduler);
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idWorkDay = new SelectList(db.WorkDays, "idWorkDay", "idWorkDay", scheduler.idWorkDay);
            return View(scheduler);
        }

        // GET: Schedulers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scheduler scheduler = await db.Schedulers.FindAsync(id);
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            ViewBag.idWorkDay = new SelectList(db.WorkDays.OrderBy(x => x.DateToday), "idWorkDay", "DateToday");
            return View(scheduler);
        }

        // POST: Schedulers/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Scheduler scheduler)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(scheduler).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idWorkDay = new SelectList(db.WorkDays, "idWorkDay", "idWorkDay", scheduler.idWorkDay);
            return View(scheduler);
        }

        // GET: Schedulers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scheduler scheduler = await db.Schedulers.FindAsync(id);
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            return View(scheduler);
        }

        // POST: Schedulers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Scheduler scheduler = await db.Schedulers.FindAsync(id);
            db.Schedulers.Remove(scheduler);
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
