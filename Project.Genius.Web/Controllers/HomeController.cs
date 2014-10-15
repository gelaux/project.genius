namespace Project.Genius.Web.Controllers
{
	using System.Web.Mvc;

	[Authorize]
	public class HomeController : Controller
	{
		#region Public Methods

		public ActionResult About()
		{
			this.ViewBag.Message = "Your application description page.";

			return this.View();
		}

		public ActionResult Contact()
		{
			this.ViewBag.Message = "Your contact page.";

			return this.View();
		}

		public ActionResult Index()
		{
			return this.View();
		}

		#endregion
	}
}