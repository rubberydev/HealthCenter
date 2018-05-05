namespace HealthCenter.Backend.Controllers
{
    using HealthCenter.Backend.Models;
    using HealthCenter.Domain;
    using Microsoft.AspNet.Identity;
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
                scheduler.ApplicationUser_Id = User.Identity.GetUserId();
                db.Schedulers.Add(scheduler);
                await db.SaveChangesAsync();
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
            ViewBag.idWorkDay = new SelectList(db.WorkDays, "idWorkDay", "idWorkDay", scheduler.idWorkDay);
            return View(scheduler);
        }

        // POST: Schedulers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AgendaId,startHour,endHour,idWorkDay,ApplicationUser_Id")] Scheduler scheduler)
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
