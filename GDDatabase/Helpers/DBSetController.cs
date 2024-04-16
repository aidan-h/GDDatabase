using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Game_Design_DB.Data;

namespace Game_Design_DB.Helpers
{
    public interface IDBSetViewModel<T, V> where T : class where V : class
    {
        int ID { get; set; }
        Task PropagateModel(T model, Game_Design_DBContext context);
        static abstract Task<V> FromContext(Game_Design_DBContext context);

    }

    public abstract class DBSetController<T, V> : Controller where T : class, IAssignedObject<T> where V : class, IDBSetViewModel<T, V>
    {
        public Game_Design_DBContext context;
        public DbSet<T> dbSet;

        public async Task<IActionResult> ApplyEdit(int id, V viewModel)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            try
            {
                var model = await FetchModel(id);
                if (model == null)
                {
                    return NotFound();
                }
                await viewModel.PropagateModel(model, context);
                context.Update(model);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(viewModel.ID))
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
        public async Task<IActionResult> ApplyCreate(V viewModel)
        {
            var model = T.Default();
            await viewModel.PropagateModel(model, context);

            if (ModelState.IsValid)
            {
                context.Add(model);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public abstract IQueryable<T> IncludeDBSet();

        public async Task<IActionResult> Index()
        {
            return View(await IncludeDBSet().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await IncludeDBSet().FirstOrDefaultAsync(m => m.ID == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return View(await V.FromContext(context));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await dbSet.FindAsync(id);
            if (model != null)
            {
                dbSet.Remove(model);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await dbSet
                .FirstOrDefaultAsync(m => m.ID == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        public bool ModelExists(int id)
        {
            return dbSet.Any(e => e.ID == id);
        }
        public async Task<T> FetchModel(int? id)
        {
            return await IncludeDBSet().FirstAsync(g => g.ID == id);
        }

        public abstract Task<V> PropogateViewModel(T model);

        public async Task<V?> FetchViewModel(int? id)
        {
            var model = await FetchModel(id);
            if (model == null)
            {
                return null;
            }
            return await PropogateViewModel(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await FetchViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

    }

}
