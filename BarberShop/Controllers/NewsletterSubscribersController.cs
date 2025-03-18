//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using BarberShop.Data;

//namespace BarberShop.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class NewsletterSubscribersController : ControllerBase
//    {
//        private readonly BarberShopDbContext _context;

//        public NewsletterSubscribersController(BarberShopDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/NewsletterSubscribers
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<NewsletterSubscriber>>> GetNewsletterSubscribers()
//        {
//            return await _context.NewsletterSubscribers.ToListAsync();
//        }

//        // GET: api/NewsletterSubscribers/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<NewsletterSubscriber>> GetNewsletterSubscriber(string id)
//        {
//            var newsletterSubscriber = await _context.NewsletterSubscribers.FindAsync(id);

//            if (newsletterSubscriber == null)
//            {
//                return NotFound();
//            }

//            return newsletterSubscriber;
//        }

//        // PUT: api/NewsletterSubscribers/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutNewsletterSubscriber(string id, NewsletterSubscriber newsletterSubscriber)
//        {
//            if (id != newsletterSubscriber.Email)
//            {
//                return BadRequest();
//            }

//            _context.Entry(newsletterSubscriber).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!NewsletterSubscriberExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/NewsletterSubscribers
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<NewsletterSubscriber>> PostNewsletterSubscriber(NewsletterSubscriber newsletterSubscriber)
//        {
//            _context.NewsletterSubscribers.Add(newsletterSubscriber);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (NewsletterSubscriberExists(newsletterSubscriber.Email))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetNewsletterSubscriber", new { id = newsletterSubscriber.Email }, newsletterSubscriber);
//        }

//        // DELETE: api/NewsletterSubscribers/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteNewsletterSubscriber(string id)
//        {
//            var newsletterSubscriber = await _context.NewsletterSubscribers.FindAsync(id);
//            if (newsletterSubscriber == null)
//            {
//                return NotFound();
//            }

//            _context.NewsletterSubscribers.Remove(newsletterSubscriber);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool NewsletterSubscriberExists(string id)
//        {
//            return _context.NewsletterSubscribers.Any(e => e.Email == id);
//        }
//    }
//}
