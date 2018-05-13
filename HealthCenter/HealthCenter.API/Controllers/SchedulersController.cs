

namespace HealthCenter.API.Controllers
{
    using HealthCenter.Domain;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Models;
    using System.Collections.Generic;

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
                });
            }
            return Ok(response);
        }


        // GET: api/Schedulers/5
        [ResponseType(typeof(Scheduler))]
        public async Task<IHttpActionResult> GetScheduler(int id)
        {
            Scheduler scheduler = await db.Schedulers.FindAsync(id);
            if (scheduler == null)
            {
                return NotFound();
            }

            return Ok(scheduler);
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