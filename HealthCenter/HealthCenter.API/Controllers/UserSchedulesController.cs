

namespace HealthCenter.API.Controllers
{
    using HealthCenter.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    public class UserSchedulesController : ApiController
    {
        public Scheduler scheduler;

        private DataContext db = new DataContext();

        // GET: api/UserSchedules
        public IQueryable<UserSchedule> GetUserSchedules()
        {
            return db.UserSchedules;
        }

        // GET: api/UserSchedules/5
        [ResponseType(typeof(UserSchedule))]
        public async Task<IHttpActionResult> GetUserSchedule(int id)
        {
            var statesAppointments = new List<int>() { 1,2 };
            var userSchedule = await db.UserSchedules.Where(z => z.UserId == id)
                                                     .Select(x => x.Scheduler)
                                                     .Where(x => x.DateSchedule >= DateTime.Today && 
                                                     statesAppointments.Contains(x.StateId))
                                                     .ToListAsync();           

            if (userSchedule == null)
            {
                return NotFound();
            }

            return Ok(userSchedule);
        }

        // PUT: api/UserSchedules/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserSchedule(int id, UserSchedule userSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userSchedule.UserScheduleId)
            {
                return BadRequest();
            }

            db.Entry(userSchedule).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserScheduleExists(id))
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

        // POST: api/UserSchedules
        [ResponseType(typeof(UserSchedule))]
        public async Task<IHttpActionResult> PostUserSchedule(UserSchedule userSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Scheduler scheduler = await db.Schedulers.FindAsync(userSchedule.AgendaId);
            scheduler.StateId = 2;   
            db.Entry(scheduler).State = EntityState.Modified;
            await db.SaveChangesAsync();
  
            db.UserSchedules.Add(userSchedule);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userSchedule.UserScheduleId }, userSchedule);
        }

        // DELETE: api/UserSchedules/5
        [Authorize]
        [ResponseType(typeof(UserSchedule))]
        public async Task<IHttpActionResult> DeleteUserSchedule(int id)
        {
            UserSchedule userSchedule = await db.UserSchedules.Where(us => us.AgendaId == id)
                                                              .FirstOrDefaultAsync();
            if (userSchedule == null)
            {
                return NotFound();
            }
            Scheduler scheduler = await db.Schedulers.FindAsync(userSchedule.AgendaId);
            scheduler.StateId = 3;
            db.Entry(scheduler).State = EntityState.Modified;            

            db.UserSchedules.Remove(userSchedule);
            await db.SaveChangesAsync();

            return Ok(userSchedule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserScheduleExists(int id)
        {
            return db.UserSchedules.Count(e => e.UserScheduleId == id) > 0;
        }
    }
}