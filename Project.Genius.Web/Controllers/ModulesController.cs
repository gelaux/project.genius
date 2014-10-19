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

		public JsonResult GetTypes()
		{
			List<ModuleType> dbResult = this.db.ModuleTypes.ToList();
			var types = (dbResult.Select(type => new { value = type.Id, text = type.Name }));

			return this.Json(types, JsonRequestBehavior.AllowGet);
		}

		public async Task<ActionResult> Index()
		{
			return this.View(await this.db.Modules.ToListAsync());
		}

		[HttpPost]
		public async Task<ActionResult> InlineEdit(string pk, string name, string value)
		{
			Module module = this.db.Modules.Find(new Guid(pk));
			if (module == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Format("Resource not found"));
			}

			//switch (name.ToLower())
			//{
			//	case "caption":
			//		module.Caption = value;
			//		break;
			//	case "description":
			//		module.Description = value;
			//		break;
			//	case "type":
			//		module.Type = this.db.ModuleTypes.Find(Convert.ToInt32(value));
			//		break;
			//}

			try
			{
				Utilities.SetPropertyValue(module, name, value, this.db);
				module.UpdateBy = this.db.ApplicationUsers.Find(this.User.Identity.GetUserId());
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