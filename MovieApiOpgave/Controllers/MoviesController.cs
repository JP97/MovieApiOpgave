using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApiOpgave.Models;
using MovieApiOpgave.ApiWork;

namespace MovieApiOpgave.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieApiOpgaveContext _context;
        ApiHelper helper = new ApiHelper();

        public MoviesController(MovieApiOpgaveContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(int? id)
        {
            List<Movie> movies;
            if (id == null)
            {
                movies = helper.GetApiData();
                return View(movies);
            }

            movies = helper.GetSpecifikApiData((int)id);
            await _context.AddAsync(movies);
            await _context.SaveChangesAsync();
            return View(movies);
            //return View(await _context.Movie.ToListAsync());
        }


        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var movie = helper.GetApiData().Find(x => x.Id == id);
                
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Description,NumSeats")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                string response = helper.PostApiData(movie);
                //_context.Add(movie);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        //brug det her for at booke en plads for en film
        public async Task<IActionResult> Edit(int? id)
        {
            string messageFromApi;
            if (id == null)
            {
                return NotFound();
            }


            try
            {
               messageFromApi = helper.PutApiData((int)id);
            }
            catch (Exception e)
            {
                throw new OutOfMemoryException();
            }
            //var movie = await _context.Movie.FindAsync(id);
            //if (movie == null)
            //{
            //    return NotFound();
            //}
            return RedirectToAction("Index");
            //return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Description,NumSeats")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string deleteMessage;
            if (id == null)
            {
                return NotFound();
            }

            //var movie = helper.DeleteApiData((int)id);
            deleteMessage = helper.DeleteApiData((int)id);

            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //return View(movie);
            return RedirectToAction("Index");
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
