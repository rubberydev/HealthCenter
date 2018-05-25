namespace HealthCenter.Backend.Controllers
{
    using HealthCenter.Backend.Models;
    using HealthCenter.Domain;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize(Roles = "Medic")]
    public class SchedulersController : Controller
    {
        private LocalDataContext db = new LocalDataContext();
        
        // GET: Schedulers
        public async Task<ActionResult> Index()
        {
            var userAuthenticated = User.Identity.GetUserId();
            var schedulers = db.Schedulers.Where(x => x
                                          .ApplicationUser_Id == userAuthenticated && x
                                          .DateSchedule >= DateTime.Today && x.StateId == 1 || x.StateId == 3)
                                          .Include(s => s.WorkDay);

            return View(await schedulers.ToListAsync());
        }


        // GET: Schedulers/SearchDetail/5
        public async Task<ActionResult> SearchDetail()
        {
            var userAuthenticated = User.Identity.GetUserId();

            
            var scheduler = await db.Schedulers.Where(s => s
                                               .ApplicationUser_Id == userAuthenticated && s
                                               .StateId == 2 && s.DateSchedule >= DateTime.Today).ToListAsync();
            
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            return View(scheduler);
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
            ViewBag.idWorkDay = new SelectList(db.WorkDays.Where(x => x
                                                 .DateToday >= DateTime.Today), "idWorkDay", "DateToday");
            return View();
        }

        // POST: Schedulers/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Scheduler scheduler)
        {
            if (ModelState.IsValid)
            {
                var userSesion = User.Identity.GetUserId();
                var querySchedule = await db.Schedulers.Where(x => x
                                                       .idWorkDay == scheduler.idWorkDay && x
                                                       .ApplicationUser_Id == userSesion)
                                                       .FirstOrDefaultAsync();

                if (querySchedule != null)
                {
                    var hh = "/Content/sweetalert2.min.css";
                    return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                   "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                   "<script>swal({title: 'ERROR..'," +
                                   "text: 'This workday already has been to programmed, " +
                                   "try again with another date...'," +
                                   "type: 'error'," +
                                   "showCancelButton: false," +
                                   "confirmButtonColor: '#3085d6'," +
                                   "cancelButtonColor: '#d33'," +
                                   "confirmButtonText: 'Acceptt'}).then(function() " +
                                   "{swal(''," +
                                    "''," +
                                    "'success', window.location.href='/Schedulers/index')});</script>");
                }                

                var validate = await db.WorkDays.Select(x => x)
                                       .Where(z => z.idWorkDay == scheduler.idWorkDay)
                                       .FirstOrDefaultAsync();

                scheduler.DateSchedule = validate.DateToday;

                if (validate.DateToday >= scheduler.DateSchedule && validate.startDayHour.Hour <= scheduler.startHour.Hour &&
                    validate.startDayHour.Hour <= validate.endDayHour.Hour)
                {
                    var validateEndHour = scheduler.startHour.Hour;

                    while (validateEndHour < validate.endDayHour.Hour)
                    {
                        scheduler.StateId = 1;

                        
                        scheduler.endHour = scheduler.startHour.AddMinutes(validate.durationCite);
                        scheduler.ApplicationUser_Id = User.Identity.GetUserId();
                        db.Schedulers.Add(scheduler);
                        await db.SaveChangesAsync();
                       
                       var endDate = await db.Schedulers.Where(x => x
                                                        .idWorkDay == scheduler.idWorkDay && x
                                                        .ApplicationUser_Id == scheduler.ApplicationUser_Id)
                                                        .OrderByDescending(x => x.endHour).FirstOrDefaultAsync();

                        validateEndHour = endDate.endHour.Hour;
                        scheduler.startHour = endDate.endHour;
                    }                    
                }
                else
                {
                    var hh = "/Content/sweetalert2.min.css";
                    return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                   "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                   "<script>swal({title: 'ERROR..'," +
                                   "text: 'you cannot scheduler an agenda in hour different a workday or previous days, " +
                                   "you should consider to validate with your boss the work days shedule...'," +
                                   "type: 'error'," +
                                   "showCancelButton: false," +
                                   "confirmButtonColor: '#3085d6'," +
                                   "cancelButtonColor: '#d33'," +
                                   "confirmButtonText: 'Acceptt'}).then(function() " +
                                   "{swal(''," +
                                    "''," +
                                    "'success', window.location.href='/Schedulers/index')});</script>");
                }            
                return RedirectToAction("Index");
            }

            ViewBag.idWorkDay = new SelectList(db.WorkDays, "idWorkDay", "idWorkDay", scheduler.idWorkDay);
            return View(scheduler);
        }

        //GET: Schedulers/Edit/5
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
            ViewBag.StateId = new SelectList(db.States , "StateId", "stateName");
            return View(scheduler);
        }

        //POST: Schedulers/Edit/5        
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
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateId", scheduler.StateId);
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
