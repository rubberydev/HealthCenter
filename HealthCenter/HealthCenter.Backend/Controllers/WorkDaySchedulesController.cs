namespace HealthCenter.Backend.Controllers
{
    using Models;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize(Roles = "Admin")]
    public class WorkDaySchedulesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: WorkDaySchedules
        public async Task<ActionResult> Index()
        {
            return View(await db.WorkDays.ToListAsync());
        }

        // GET: WorkDaySchedules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDaySchedule workDaySchedule = await db.WorkDays.FindAsync(id);
            if (workDaySchedule == null)
            {
                return HttpNotFound();
            }
            return View(workDaySchedule);
        }

        // GET: WorkDaySchedules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkDaySchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idWorkDay,startDayHour,endDayHour,DateToday,durationCite")] WorkDaySchedule workDaySchedule)
        {
            if (ModelState.IsValid)
            {
                db.WorkDays.Add(workDaySchedule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(workDaySchedule);
        }

        // GET: WorkDaySchedules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDaySchedule workDaySchedule = await db.WorkDays.FindAsync(id);
            if (workDaySchedule == null)
            {
                return HttpNotFound();
            }
            return View(workDaySchedule);
        }

        // POST: WorkDaySchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idWorkDay,startDayHour,endDayHour,DateToday,durationCite")] WorkDaySchedule workDaySchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workDaySchedule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workDaySchedule);
        }

        // GET: WorkDaySchedules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDaySchedule workDaySchedule = await db.WorkDays.FindAsync(id);
            if (workDaySchedule == null)
            {
                return HttpNotFound();
            }
            return View(workDaySchedule);
        }

        // POST: WorkDaySchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkDaySchedule workDaySchedule = await db.WorkDays.FindAsync(id);
            db.WorkDays.Remove(workDaySchedule);
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
