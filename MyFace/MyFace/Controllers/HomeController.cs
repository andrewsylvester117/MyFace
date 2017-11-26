﻿using MyFace.Models;
using myFaceLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myFaceLib.Services;

namespace MyFace.Controllers
{
	public class HomeController : Controller
	{
		List<Post> PostList = PostService.MakePostList();
		public ActionResult Index()
		{
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
        public ActionResult LikePost(int id)
		{
			Post p = PostList.Where(x => x.id == id).First();
			p.likecount++;
            PostService.UpdatePost(p);
            PostList = PostService.MakePostList();
            return View("PostViewing", PostList);
		}
        public ActionResult DislikePost(int id)
		{
			Post p = PostList.Where(x => x.id == id).First();
			p.dislikecount++;
            PostService.UpdatePost(p);
            PostList = PostService.MakePostList();
            return View("PostViewing", PostList);
		}
		public ActionResult PostPost(Post p)
		{
            
            PostService.AddPost(p,1);
            PostList = PostService.MakePostList();

			return View("PostViewing", PostList);
		}
		public ActionResult ViewPosts()
		{
            PostList = PostService.MakePostList();
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