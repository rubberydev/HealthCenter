
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

    public class WorkDaysController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/WorkDays
        public IQueryable<WorkDay> GetWorkDays()
        {
            return db.WorkDays;
        }

        // GET: api/WorkDays/5
        [ResponseType(typeof(WorkDay))]
        public async Task<IHttpActionResult> GetWorkDay(int id)
        {
            WorkDay workDay = await db.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return NotFound();
            }

            return Ok(workDay);
        }

        // PUT: api/WorkDays/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWorkDay(int id, WorkDay workDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workDay.idWorkDay)
            {
                return BadRequest();
            }

            db.Entry(workDay).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkDayExists(id))
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

        // POST: api/WorkDays
        [ResponseType(typeof(WorkDay))]
        public async Task<IHttpActionResult> PostWorkDay(WorkDay workDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkDays.Add(workDay);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = workDay.idWorkDay }, workDay);
        }

        // DELETE: api/WorkDays/5
        [ResponseType(typeof(WorkDay))]
        public async Task<IHttpActionResult> DeleteWorkDay(int id)
        {
            WorkDay workDay = await db.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return NotFound();
            }

            db.WorkDays.Remove(workDay);
            await db.SaveChangesAsync();

            return Ok(workDay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkDayExists(int id)
        {
            return db.WorkDays.Count(e => e.idWorkDay == id) > 0;
        }
    }
}