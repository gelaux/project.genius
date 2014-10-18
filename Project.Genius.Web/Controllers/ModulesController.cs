namespace Project.Genius.Web.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Net;
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using Schema;
	using Schema.Entities;

	public class ModulesController : Controller
	{
		#region Constants and Fields

		private readonly DataContext db = new DataContext();

		#endregion

		// GET: Modules/Create

		#region Public Methods

		public ActionResult Create()
		{
			return this.View();
		}

		// POST: Modules/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,Caption,Description,Name")] Module module)
		{
			if (this.ModelState.IsValid)
			{
				module.Id = Guid.NewGuid();
				this.db.Modules.Add(module);
				await this.db.SaveChangesAsync();
				return this.RedirectToAction("Index");
			}

			return View(module);
		}

		// GET: Modules/Edit/5

		// GET: Modules/Delete/5
		public async Task<ActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Module module = await this.db.Modules.FindAsync(id);
			if (module == null)
			{
				return this.HttpNotFound();
			}
			return View(module);
		}

		// POST: Modules/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(Guid id)
		{
			Module module = await this.db.Modules.FindAsync(id);
			this.db.Modules.Remove(module);
			await this.db.SaveChangesAsync();
			return this.RedirectToAction("Index");
		}

		public async Task<ActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Module module = await this.db.Modules.FindAsync(id);
			if (module == null)
			{
				return this.HttpNotFound();
			}
			return this.PartialView("_Details", module);
		}

		public async Task<ActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Module module = await this.db.Modules.FindAsync(id);
			if (module == null)
			{
				return this.HttpNotFound();
			}
			return View(module);
		}

		// POST: Modules/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Caption,Description,Name")] Module module)
		{
			if (this.ModelState.IsValid)
			{
				this.db.Entry(module).State = EntityState.Modified;
				await this.db.SaveChangesAsync();
				return this.RedirectToAction("Index");
			}
			return View(module);
		}

		public JsonResult GetTypes()
		{
			List<ModuleType> dbResult = this.db.ModuleTypes.ToList();
			var types = (from type in dbResult select new { value = type.Id, text = type.Name });

			return this.Json(types, JsonRequestBehavior.AllowGet);
		}

		public async Task<ActionResult> Index()
		{
			//var viewModel = new ModuleViewModel
			//{
			//	DefinedTask = await this.db.DefinedTasks.ToListAsync(),
			//	Module = await this.db.Modules.ToListAsync()
			//};
			return this.View(await this.db.Modules.ToListAsync());
		}

		#endregion

		#region Methods

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.db.Dispose();
			}
			base.Dispose(disposing);
		}

		#endregion
	}
}