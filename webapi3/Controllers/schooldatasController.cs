using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using webapi3;

namespace webapi3.Controllers
{
    public class schooldatasController : ApiController
    {
        private schooldbsEntities db = new schooldbsEntities();

        // GET: api/schooldatas
        public IQueryable<schooldata> Getschooldatas()
        {
            return db.schooldatas;
        }

        // GET: api/schooldatas/5
        [ResponseType(typeof(schooldata))]
        public IHttpActionResult Getschooldata(int id)
        {
            schooldata schooldata = db.schooldatas.Find(id);
            if (schooldata == null)
            {
                return NotFound();
            }

            return Ok(schooldata);
        }

        // PUT: api/schooldatas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putschooldata(int id, schooldata schooldata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schooldata.studentID)
            {
                return BadRequest();
            }

            db.Entry(schooldata).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!schooldataExists(id))
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

        // POST: api/schooldatas
        [ResponseType(typeof(schooldata))]
        public IHttpActionResult Postschooldata(schooldata schooldata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.schooldatas.Add(schooldata);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (schooldataExists(schooldata.studentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = schooldata.studentID }, schooldata);
        }

        // DELETE: api/schooldatas/5
        [ResponseType(typeof(schooldata))]
        public IHttpActionResult Deleteschooldata(int id)
        {
            schooldata schooldata = db.schooldatas.Find(id);
            if (schooldata == null)
            {
                return NotFound();
            }

            db.schooldatas.Remove(schooldata);
            db.SaveChanges();

            return Ok(schooldata);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool schooldataExists(int id)
        {
            return db.schooldatas.Count(e => e.studentID == id) > 0;
        }
    }
}