namespace Project.Genius.Web.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Validation;
	using System.Linq;
	using System.Net;
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using Helpers;

	using Microsoft.AspNet.Identity;

	using Schema;
	using Schema.Entities;

	using ViewModels;

	public class ModulesController : Controller
	{
		#region Constants and Fields

		private readonly DataContext db = new DataContext();

		#endregion

		#region Public Methods

		[HttpPost]
		public async Task<ActionResult> ArrangeTasks(string[] tasks)
		{
			try
			{
				for (int i = 0; i < tasks.Length; i++)
				{
					DefinedTask task = this.db.DefinedTasks.Find(new Guid(tasks[i]));
					task.Order = i;
					await this.db.SaveChangesAsync();
				}
				return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			catch (DbEntityValidationException ex)
			{
				DbValidationError error = ex.EntityValidationErrors.First().ValidationErrors.First();
				this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				return new HttpStatusCodeResult(
					HttpStatusCode.BadRequest,
					string.Format("{0}: {1}", error.PropertyName, error.ErrorMessage));
			}
		}

		// GET: Modules/Create
		public ActionResult Create()
		{
			var viewModel = new CreateModuleViewModel { ModuleTypes = this.db.ModuleTypes.ToList()};
			return this.PartialView("_Create", viewModel);
		}

		// POST: Modules/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Name,Description,Type")] Module module)
		{
			if (this.ModelState.IsValid)
			{
				var selectedType = await this.db.ModuleTypes.FindAsync(module.Type.Id);
				var currentUser = await this.db.ApplicationUsers.FindAsync(this.User.Identity.GetUserId());
				var currentTime = DateTime.Now;

				module.Id = Guid.NewGuid();
				module.Type = selectedType;
				module.Caption = module.Name;
				module.CreateBy = currentUser;
				module.CreatedOn = currentTime;
				module.UpdateBy = currentUser;
				module.UpdateOn = currentTime;

				this.db.Modules.Add(module);
				await this.db.SaveChangesAsync();

				var viewModel = new ModuleViewModels { Modules = await this.db.Modules.ToListAsync() };

				return this.PartialView("_Modules", viewModel); ;
			}

			return View(module);
		}

		public async Task<ActionResult> CreateTask(Guid? moduleId)
		{
			Module module = await this.db.Modules.FindAsync(moduleId);
			if (module == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Format("Resource not found"));
			}

			return this.PartialView("_CreateTask", new DefinedTask { Module = module });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateTask([Bind(Include = "Name,Duration,Module")] DefinedTask task)
		{
			if (this.ModelState.IsValid)
			{
				Module module = await this.db.Modules.FindAsync(task.Module.Id);

				task.Id = Guid.NewGuid();
				task.Module = module;
				task.Order = module.Tasks.Count;
				task.Owner = await this.db.ApplicationUsers.FindAsync(this.User.Identity.GetUserId());

				this.db.DefinedTasks.Add(task);
				await this.db.SaveChangesAsync();

				return this.PartialView("_Tasks", module);
			}

			return this.PartialView("_Tasks");
		}

		// GET: Modules/Delete/5
		public async Task<ActionResult> DeleteTask(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			DefinedTask task = await this.db.DefinedTasks.FindAsync(id);
			if (task == null)
			{
				return this.HttpNotFound();
			}
			return this.PartialView("_DeleteTask", task);
		}

		// POST: Modules/Delete/5
		[HttpPost]
		[ActionName("DeleteTask")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteTaskConfirmed(Guid id)
		{
			DefinedTask task = await this.db.DefinedTasks.FindAsync(id);
			Module module = await this.db.Modules.FindAsync(task.Module.Id);

			this.db.DefinedTasks.Remove(task);

			var i = 0;
			foreach (var x in module.Tasks.OrderBy(x => x.Order))
			{
				x.Order = i++;
			}

			await this.db.SaveChangesAsync();
			return this.PartialView("_Tasks", module);
		}

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

		public JsonResult GetTypes()
		{
			List<ModuleType> dbResult = this.db.ModuleTypes.ToList();
			var types = (dbResult.Select(type => new { value = type.Id, text = type.Name }));

			return this.Json(types, JsonRequestBehavior.AllowGet);
		}

		public async Task<ActionResult> Index()
		{
			var viewModel = new ModuleViewModels
			{
				Modules = await this.db.Modules.ToListAsync(),
				ModuleTypes = await this.db.ModuleTypes.ToListAsync(),
				ProductVersions = await this.db.ProductVersions.ToListAsync()
			};
			return this.View(viewModel);
		}

		[HttpPost]
		public async Task<ActionResult> InlineEdit(string pk, string name, string value)
		{
			var whitelist = new[] { "Caption", "Name", "Description", "Type", "Optional" };
			if (!Array.Exists(whitelist, e => e == name))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Module module = this.db.Modules.Find(new Guid(pk));
			if (module == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Format("Resource not found"));
			}

			try
			{
				Utilities.SetPropertyValue(module, name, value, this.db);
				module.UpdateBy = await this.db.ApplicationUsers.FindAsync(this.User.Identity.GetUserId());
				module.UpdateOn = DateTime.Now;

				await this.db.SaveChangesAsync();
				return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			catch (DbEntityValidationException ex)
			{
				DbValidationError error = ex.EntityValidationErrors.First().ValidationErrors.First();
				this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				return new HttpStatusCodeResult(
					HttpStatusCode.BadRequest,
					string.Format("{0}: {1}", error.PropertyName, error.ErrorMessage));
			}
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