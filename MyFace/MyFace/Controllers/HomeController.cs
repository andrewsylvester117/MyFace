using MyFace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFace.Controllers
{
	public class HomeController : Controller
	{
		List<Post> PostList;
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult LikePost(int id)
		{

			return View("");
		}
		public ActionResult DislikePost(int id)
		{

			return View("");
		}
		public ActionResult PostPost(Post p)
		{
			PostList.Add(p);


		}

		public ActionResult MyProfile()
		{


			return View();
		}
	}
}