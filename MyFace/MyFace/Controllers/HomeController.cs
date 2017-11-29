using MyFace.Models;
using myFaceLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myFaceLib.Services;
using System.Web.Hosting;
using System.IO;

namespace MyFace.Controllers
{
	public class HomeController : Controller
	{
        static string imgpath;
        List<Post> PostList = new List<Post>();
        public HomeController()
        {
            imgpath = HostingEnvironment.MapPath("~/Content/Images/");
            PostList.AddRange(PostService.MakePostList(imgpath).AsEnumerable());
        }

        public ActionResult Index()
		{
            PostList = PostService.MakePostList(imgpath);

            return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View("Post/PostForm");
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
        public ActionResult AddPost()
        {
            return View("Post/PostForm");
        }
        public ActionResult LikePost(int id)
		{
			Post p = PostList.Where(x => x.id == id).First();
			p.likecount++;
            PostService.UpdatePost(p,imgpath);
            PostList = PostService.MakePostList(imgpath);
            return View("PostViewing", PostList);
		}
        public ActionResult DislikePost(int id)
		{
			Post p = PostList.Where(x => x.id == id).First();
			p.dislikecount++;
            PostService.UpdatePost(p,imgpath);
            PostList = PostService.MakePostList(imgpath);
            return View("PostViewing", PostList);
		}
        [HttpPost]
        public ActionResult PostPost(Post p)
		{
            Post px = p;
            string fileName = "";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(imgpath, fileName);
                    file.SaveAs(path);
                }
            }
            p.imagefname = fileName;
            PostService.AddPost(px,1,imgpath);
            PostList = PostService.MakePostList(imgpath);
			return View("PostViewing", PostList);
		}
		public ActionResult ViewPosts()
		{
           // int userid = 0;
            PostList = PostService.MakePostList(imgpath);
            //myFaceLib.Models.User cuser = (new MyFaceService()).GetUserByID(userid); //Might use User.Identity to get current user once I figure out how I want to get their id
            //List<Post> YourPosts = new List<Post>();
            //YourPosts = PostList.Where(x => x.publisherid == userid).ToList(); //Will be changed once I figure out how to get posts who's publisherid is one of their friends
            return View("PostViewing", PostList);
		}
		public ActionResult Friends()
		{
			return View();
		}

		public ActionResult MyProfile()
		{
			return View();
		}

		/// <summary>
		/// Get the view which adds a new user
		/// </summary>		
		[HttpGet]
		public ActionResult CreateNewUser()
		{
			// show the form to create a user

			return View(new User());
		}


		[HttpPost]
		public ActionResult CreateNewUser(User model)
		{
			// add info/pictures if necessary

			// save data to the service
			return RedirectToAction("MyProfile", model);
		}

	}
}