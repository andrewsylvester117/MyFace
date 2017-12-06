using MyFace.Models;
using MyFaceLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFaceLib.Services;
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
			Post p = PostList.Where(x => x.Id == id).First();
			p.Likecount++;
            PostService.UpdatePost(p,imgpath);
            PostList = PostService.MakePostList(imgpath);
            return View("PostViewing", PostList);
		}
        public ActionResult DislikePost(int id)
		{
			Post p = PostList.Where(x => x.Id == id).First();
			p.Dislikecount++;
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
            p.Imagefname = fileName;
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
			// get the friends list

			// send it into the view
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
			DateTime dob = new DateTime();

			// results =  1 realname, 2 status, 3 dob, 4 zodiak, 5 isMale, 6 descr

			model.RealName = Request.Form.Get(1);
			model.Status = Request.Form.Get(2);
			DateTime.TryParse(Request.Form.Get(3), out dob);			
			model.ZodiacSign = Request.Form.Get(4) as string;
			model.IsMaleGender = bool.Parse(Request.Form.Get(5));
			model.Description = Request.Form.Get(6) as string;
			model.UserName = User.Identity.Name;

			// dummy data
			model.Password = "hello";
			model.PrefferedSSN = "232-45-0293";
			model.Email = model.UserName;

			// save data to the service
			MyFaceService.CreateNewUser(model);

			return RedirectToAction("MyProfile", model);
		}

		[HttpGet]
		public ActionResult ShowAllUsers()
		{
			ViewBag.Title = "All Registered Users";

			return View("_AllUsersPartial", MyFaceService.GetAllUsers());
		}

		[HttpGet]
		[Authorize]
		public ActionResult ShowAllUsers(string id)
		{
			// id == current userName
			return View("_AllUsersPartial", MyFaceService.GetAllUsers(id));
		}

	}
}