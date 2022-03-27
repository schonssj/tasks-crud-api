using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using TasksControl.Models;

namespace TasksControl.Controllers
{
    public class TasksController : ApiController
    {
        private TasksContext db = new TasksContext();

        /// <summary>
        /// Returns a list of all registered tasks.
        /// </summary>
        public IQueryable<Task> Get() => db.Tasks;

        /// <summary>
        /// Find a task by ID.
        /// </summary>
        /// <param name="id">Task's ID.</param>
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetById(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        /// <summary>
        /// Update values of an existing task.
        /// </summary>
        /// <param name="id">Task's ID.</param>
        /// <param name="task">Task's body data to be changed.</param>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Task task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != task.Id)
                return BadRequest();

            db.Entry(task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoesTaskExists(id))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new task.
        /// </summary>
        /// <param name="task">Task's body data.</param>
        [ResponseType(typeof(Task))]
        public IHttpActionResult Post(Task task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Tasks.Add(task);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = task.Id }, task);
        }

        /// <summary>
        /// Delete a task.
        /// </summary>
        /// <param name="id">Task's ID.</param>
        [ResponseType(typeof(Task))]
        public IHttpActionResult Delete(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
                return NotFound();

            db.Tasks.Remove(task);
            db.SaveChanges();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        private bool DoesTaskExists(int id) => db.Tasks.Count(e => e.Id == id) > 0;
    }
}