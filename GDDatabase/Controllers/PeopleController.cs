using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Game_Design_DB.Data;
using Game_Design_DB.Models;
using Game_Design_DB.ViewModels;
using Game_Design_DB.Helpers;

namespace Game_Design_DB.Controllers
{
    public class PeopleController : Controller
    {
        private readonly Game_Design_DBContext _context;

        public PeopleController(Game_Design_DBContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _context.Person.Include(p => p.Websites).Include(p => p.Resources).ToListAsync());
        }

        private async Task PropagatePersonWithViewModel(Person person, PersonViewModel viewModel)
        {
            //TODO Stupidly messy, but I don't want to write a constructor.
            person.Name = viewModel.Name;
            person.Websites = await _context.PersonalWebsite.Where(p => viewModel.Websites.Contains(p.ID)).ToListAsync();
            person.Resources = await _context.Resource.Where(p => viewModel.Resources.Contains(p.ID)).ToListAsync();
            person.Games = await _context.Game.Where(p => viewModel.Games.Contains(p.ID)).ToListAsync();

        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        private async Task PropagateViewModel(PersonViewModel viewModel)
        {
            // TODO group tasks
            var games = await _context.Game.ToListAsync();
            var resources = await _context.Resource.ToListAsync();
            var websites = await _context.PersonalWebsite.ToListAsync();

            viewModel.Games = new AssignedSet { Objects = games.Select(p => new AssignedObject { ID = p.ID, Name = p.Name, Assigned = false, }).ToList() };
            viewModel.Resources = new AssignedSet { Objects = resources.Select(p => new AssignedObject { ID = p.ID, Name = p.Title, Assigned = false, }).ToList() };
            viewModel.Websites = new AssignedSet { Objects = websites.Select(p => new AssignedObject { ID = p.ID, Name = p.URL, Assigned = false, }).ToList() };
        }

        // GET: People/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new PersonViewModel();
            await PropagateViewModel(viewModel);
            return View(viewModel);
        }

        public async Task<Person?> FetchModel(int? id)
        {
            return await _context.Person.Include(p => p.Websites).Include(p => p.Resources).Include(p => p.Games).FirstAsync(p => p.ID == id);
        }

        public async Task<PersonViewModel?> FetchViewModel(int? id)
        {
            var model = await FetchModel(id);
            if (model == null)
            {
                return null;
            }

            return await PersonViewModel.FromModel(model, _context);
        }


        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ID,SelectedGames,SelectedResources,SelectedWebsites")] PersonViewModel personViewModel)
        {
            var person = new Person();
            await PropagatePersonWithViewModel(person, personViewModel);

            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ID")] Person person)
        {
            if (id != person.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.ID))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.ID == id);
        }
    }
}
