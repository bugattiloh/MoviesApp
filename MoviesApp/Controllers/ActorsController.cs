using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Filters;
using MoviesApp.Models;
using MoviesApp.ViewModels;


namespace MoviesApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly MoviesContext _context;
        private readonly ILogger<ActorsController> _logger;
        private readonly IMapper _mapper;

        public ActorsController(MoviesContext context, ILogger<ActorsController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Actors
        [Authorize]
        public IActionResult Index()
        {
            var actors = _mapper.Map<IEnumerable<Actor>, IEnumerable<ActorViewModel>>(_context.Actor.ToList());
            return View(actors);

        }

        // GET: Actors/Details/5
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<Actor, ActorViewModel>(_context.Actor.FirstOrDefault(a => a.Id == id));

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Actors/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AgeFilters]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Id,Name,LastName,Birthday")] InputActorViewModel inputModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(_mapper.Map<InputActorViewModel, Actor>(inputModel));
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(inputModel);
        }

        // GET: Actors/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _mapper.Map<Actor, EditActorViewModel>(_context.Actor.FirstOrDefault(a => a.Id == id));

            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AgeFilters]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, [Bind("Id,Name,Surname,Birthdate")] EditActorViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var actor = _mapper.Map<EditActorViewModel, Actor>(editModel);
                    actor.Id = id;

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
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel = _mapper.Map<Actor, DeleteActorViewModel>(_context.Actor.FirstOrDefault(a => a.Id == id));

            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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