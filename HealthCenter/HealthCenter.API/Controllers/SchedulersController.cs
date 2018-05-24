namespace HealthCenter.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using HealthCenter.Domain;
    using Models;

    public class SchedulersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Schedulers
        public async Task<IHttpActionResult> GetSchedulers()
        {
            var response = new List<SchedulerResponse>();
            var Schedulers = await db.Schedulers.ToListAsync();
            foreach (var s in Schedulers)
            {
                response.Add(new SchedulerResponse
                {
                    AgendaId = s.AgendaId,
                    startHour = s.startHour,
                    endHour = s.endHour,
                    idWorkDay = s.idWorkDay,
                    ApplicationUser_Id = s.ApplicationUser_Id,
                    WorkDay = s.WorkDay,
                    DateSchedule = s.DateSchedule,
                    State = s.State,
                    StateId = s.StateId,
                });
            }
            return Ok(response);
        }      

        //This a method to get Scheduler by each doctor
        // GET: api/Schedulers/5
        [ResponseType(typeof(Scheduler))]
        public async Task<IHttpActionResult> GetScheduler(string id)
        {
            var response = new List<SchedulerResponse>();
            var Schedulers = await db.Schedulers.Where(s => s
                                                .ApplicationUser_Id == id && s
                                                .StateId == 1 || s.StateId == 3 && s
                                                .DateSchedule >= DateTime.Now).ToListAsync();

            foreach (var s in Schedulers)
            {
                response.Add(new SchedulerResponse
                {
                    AgendaId = s.AgendaId,
                    startHour = s.startHour,
                    endHour = s.endHour,
                    idWorkDay = s.idWorkDay,
                    ApplicationUser_Id = s.ApplicationUser_Id,
                    WorkDay = s.WorkDay,
                    DateSchedule = s.DateSchedule,
                    State = s.State,
                    StateId = s.StateId,
                });
            }
            return Ok(response);
        }

        // PUT: api/Schedulers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutScheduler(int id, Scheduler scheduler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scheduler.AgendaId)
            {
                return BadRequest();
            }

            db.Entry(scheduler).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchedulerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Schedulers
        [ResponseType(typeof(Scheduler))]
        public async Task<IHttpActionResult> PostScheduler(Scheduler scheduler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Schedulers.Add(scheduler);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = scheduler.AgendaId }, scheduler);
        }

        // DELETE: api/Schedulers/5
        [ResponseType(typeof(Scheduler))]
        public async Task<IHttpActionResult> DeleteScheduler(int id)
        {
            Scheduler scheduler = await db.Schedulers.FindAsync(id);
            if (scheduler == null)
            {
                return NotFound();
            }

            db.Schedulers.Remove(scheduler);
            await db.SaveChangesAsync();

            return Ok(scheduler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchedulerExists(int id)
        {
            return db.Schedulers.Count(e => e.AgendaId == id) > 0;
        }
    }
}