namespace HealthCenter.Backend.Controllers
{
    using HealthCenter.Backend.Models;
    using Microsoft.AspNet.Identity;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize(Roles = "Medic")]
    public class AgendaController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Agenda
        public async Task<ActionResult> Index()
        {
            var agenda = db.Agenda.Include(a => a.WorkDay);
            return View(await agenda.ToListAsync());
        }

        // GET: Agenda/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = await db.Agenda.FindAsync(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // GET: Agenda/Create
        public ActionResult Create()
        {
            ViewBag.idWorkDay = new SelectList(db.WorkDays.OrderBy(x => x.DateToday), "idWorkDay", "DateToday");
            return View();
        }

        // POST: Agenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AgendaId,Medics,startHour,endHour,idWorkDay")] Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                agenda.ApplicationUser_Id = User.Identity.GetUserId();
                db.Agenda.Add(agenda);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idWorkDay = new SelectList(db.WorkDays, "idWorkDay", "idWorkDay", agenda.idWorkDay);
            return View(agenda);
        }

        // GET: Agenda/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = await db.Agenda.FindAsync(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.idWorkDay = new SelectList(db.WorkDays.OrderBy(x => x.DateToday), "idWorkDay", "DateToday");
            return View(agenda);
        }

        // POST: Agenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AgendaId,Medics,startHour,endHour,idWorkDay")] Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agenda).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idWorkDay = new SelectList(db.WorkDays, "idWorkDay", "idWorkDay", agenda.idWorkDay);
            return View(agenda);
        }

        // GET: Agenda/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = await db.Agenda.FindAsync(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Agenda agenda = await db.Agenda.FindAsync(id);
            db.Agenda.Remove(agenda);
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
