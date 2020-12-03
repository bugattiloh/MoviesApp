using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly MoviesContext _context;
        private readonly ILogger<ActorsController> _logger;

        public ActorsController(MoviesContext context, ILogger<ActorsController> logger)
        {
            _context = context;

            _logger = logger;
        }

        // GET: Actors
        public IActionResult Index()
        {
            return View(_context.Actor.Select(a => new ActorViewModel
            {
                Id = a.Id,
                Name = a.Name,
                LastName = a.LastName,
                Birthdate = a.Birthday
            }).ToList());
        }

        // GET: Actors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _context.Actor.Where(a => a.Id == id).Select(a => new ActorViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                LastName = a.LastName,
                Birthdate = a.Birthday
            }).FirstOrDefault();


            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,LastName,Birthday")] InputActorViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Actor
                {
                    Name = inputModel.Name,
                    LastName = inputModel.LastName,
                    Birthday = inputModel.Birthdate
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(inputModel);
        }

        // GET: Actors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _context.Actor.Where(a => a.Id == id).Select(a => new EditActorViewModel()
            {
                Name = a.Name,
                LastName = a.LastName,
                Birthdate = a.Birthday
            }).FirstOrDefault();

            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Surname,Birthdate")] EditActorViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var actor = new Actor
                    {
                        Id = id,
                        Name = editModel.Name,
                        LastName = editModel.LastName,
                        Birthday = editModel.Birthdate
                    };

                    _context.Update(actor);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(id))
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

            return View(editModel);
        }

        // GET: Actors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel = _context.Actor.Where(a => a.Id == id).Select(a => new DeleteActorViewModel
            {
                Name = a.Name,
                LastName = a.LastName,
                Birthdate = a.Birthday
            }).FirstOrDefault();

            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var actor = _context.Actor.Find(id);
            _context.Actor.Remove(actor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actor.Any(e => e.Id == id);
        }
    }
}