namespace HealthCenter.Backend.Controllers
{
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

    [Authorize(Roles = "Admin")]
    public class WorkDaysController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: WorkDays
        public async Task<ActionResult> Index()
        {
            var workDays = await db.WorkDays.Where(x => x
                                            .DateToday >= DateTime.Today)
                                            .ToListAsync();

            return View( workDays);
        }

        // GET: WorkDays/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDay workDay = await db.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return HttpNotFound();
            }
            return View(workDay);
        }

        // GET: WorkDays/Create
        public ActionResult Create()
        {
            List<string> ListItems = new List<string>();
            ListItems.Add("Select an item...");
            ListItems.Add("15 Days");            
            ListItems.Add("1 Month");

            SelectList parameter = new SelectList(ListItems);

            ViewBag.parameterWorkDays = parameter;
            
            return View();
        }

        // POST: WorkDays/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkDay workDay)
        {
            
            if (ModelState.IsValid)
            {
                if (workDay.parameterWorkDays == "Select an item...")
                {
                    var hh = "/Content/sweetalert2.min.css";
                    return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                   "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                   "<script>swal({title: 'ERROR..'," +
                                   "text: 'you must choose an item to schedule workdays, " +
                                   "please try again'," +
                                   "type: 'error'," +
                                   "showCancelButton: false," +
                                   "confirmButtonColor: '#3085d6'," +
                                   "cancelButtonColor: '#d33'," +
                                   "confirmButtonText: 'Acceptt'}).then(function() " +
                                   "{swal(''," +
                                    "''," +
                                    "'success', window.location.href='/Workdays/Create')});</script>");
                }

                if(workDay.startDayHour.Hour >= workDay.endDayHour.Hour || workDay.durationCite > 60 ||
                    workDay.DateToday <= DateTime.Today.Date)
                {
                    var hh = "/Content/sweetalert2.min.css";
                    return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                   "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                   "<script>swal({title: 'ERROR..'," +
                                   "text: 'The start hour should be biggest than end hour, or date workday it`s fewer than today " +
                                   "or duration for appointment it`s too spread out, consider to type less time in minutes, please try again'," +
                                   "type: 'error'," +
                                   "showCancelButton: false," +
                                   "confirmButtonColor: '#3085d6'," +
                                   "cancelButtonColor: '#d33'," +
                                   "confirmButtonText: 'Acceptt'}).then(function() " +
                                   "{swal(''," +
                                    "''," +
                                    "'success', window.location.href='/Workdays/Create')});</script>");

                }

                var queryValidator = db.WorkDays.OrderByDescending(x => x.DateToday).FirstOrDefaultAsync();

                if (queryValidator == null || queryValidator.Result.DateToday >= workDay.DateToday)
                {
                    var hh = "/Content/sweetalert2.min.css";
                    return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                   "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                   "<script>swal({title: 'ERROR..'," +
                                   "text: 'The workday already has been to programmed, " +
                                   "please try again'," +
                                   "type: 'error'," +
                                   "showCancelButton: false," +
                                   "confirmButtonColor: '#3085d6'," +
                                   "cancelButtonColor: '#d33'," +
                                   "confirmButtonText: 'Acceptt'}).then(function() " +
                                   "{swal(''," +
                                    "''," +
                                    "'success', window.location.href='/Workdays/Create')});</script>");
                }
                var ctrl = 0;

                if (workDay.parameterWorkDays == "15 Days")
                {
                    ctrl = 15;
                    for (int i = 0; i < ctrl; i++)
                    {
                        db.WorkDays.Add(workDay);
                        await db.SaveChangesAsync();

                        workDay.DateToday = workDay.DateToday.AddDays(1);
                    }
                    
                }


                if (workDay.parameterWorkDays == "1 Month")
                {
                    ctrl = 30;
                    for (int i = 0; i < ctrl; i++)
                    {
                        db.WorkDays.Add(workDay);
                        await db.SaveChangesAsync();

                        workDay.DateToday = workDay.DateToday.AddDays(1);
                    }
                }       
                return RedirectToAction("Index");
            }       
            return View(workDay);
        }

        // GET: WorkDays/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDay workDay = await db.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return HttpNotFound();
            }
            return View(workDay);
        }

        // POST: WorkDays/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WorkDay workDay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workDay).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workDay);
        }

        // GET: WorkDays/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDay workDay = await db.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return HttpNotFound();
            }
            return View(workDay);
        }

        // POST: WorkDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkDay workDay = await db.WorkDays.FindAsync(id);
            db.WorkDays.Remove(workDay);
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
