using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using webapi.Models;

namespace webapi.Controllers
{
    public class PhotoController : ODataController
    {
        BookContext db = new BookContext();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        // GET /Photo(1)/Person
        [EnableQuery]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(db.Photo.Where(p => p.Id == key).Select(p => p.Person));
        }

        [EnableQuery]
        [AcceptVerbs("POST", "PUT")]
        public async Task<IHttpActionResult> CreateRef([FromODataUri] int key,
            string navigationProperty, [FromBody] Uri link)
        {
            var product = await db.Photo.SingleOrDefaultAsync(p => p.Id == key);
            if (product == null)
            {
                return NotFound();
            }
            switch (navigationProperty)
            {
                case "Person":
                    // Note: The code for GetKeyFromUri is shown later in this topic.
                    var relatedKey = Helpers.GetKeyFromUri<int>(Request, link);
                    var supplier = await db.Person.SingleOrDefaultAsync(f => f.Id == relatedKey);
                    if (supplier == null)
                    {
                        return NotFound();
                    }

                    product.Person = supplier;
                    break;

                default:
                    return StatusCode(HttpStatusCode.NotImplemented);
            }
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [EnableQuery]
        public async Task<IHttpActionResult> DeleteRef([FromODataUri] int key,
        string navigationProperty, [FromBody] Uri link)
        {
            var photo = db.Photo.SingleOrDefault(p => p.Id == key);
            if (photo == null)
            {
                return NotFound();
            }

            switch (navigationProperty)
            {
                case "Person":
                    photo.Person = null;
                    break;

                default:
                    return StatusCode(HttpStatusCode.NotImplemented);
            }
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PhotoPrice([FromODataUri] int key, ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int rating = (int)parameters["Price"];
            db.PhotoPrice.Add(new PhotoPrice
            {
                Price = rating,
                PhotoId = key
            });

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (!PhotoExist(key))
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

        private bool PhotoExist(int key)
        {
            return db.Photo.Any(p => p.Id == key);
        }
    }
}