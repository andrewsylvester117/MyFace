using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myFaceLib;
using myFaceLib.Models;
using myFaceDAL;

namespace myFaceLib.Services
{
	public class PostService
	{
		public static void AddPost(myFaceLib.Models.Post p)
		{
			if(p != null)
			{
				using (var db = new MyFaceEntities())
				{
					
					myFaceDAL.Post dbpost = new myFaceDAL.Post() {
						postText = p.textcontent,
						postImage = p.imagecontent,
						postHeader = p.postheader,
						postId = p.id,
						publisherId = p.publisherid,
						dislikeCount = p.dislikecount,
						likeCount = p.likecount,
						originalPostId = p.parentid,
					};

					db.Posts.Add(dbpost);
					db.SaveChanges();
				}
			}
			else
			{
				return;
			}
		}

		public static void UpdatePost(Models.Post p)
		{
			if(p != null)
			{
				using (var db = new MyFaceEntities())
				{
					
					myFaceDAL.Post dbpost = new myFaceDAL.Post()
					{
						postText = p.textcontent,
						postImage = p.imagecontent,
						postHeader = p.postheader,
						postId = p.id,
						publisherId = p.publisherid,
						dislikeCount = p.dislikecount,
						likeCount = p.likecount,
						originalPostId = p.parentid,
					};

					var postp = db.Posts.Where(x => x.postId == dbpost.postId).First();
					postp.postText = dbpost.postText;
					postp.postImage = dbpost.postImage;
					postp.postHeader = dbpost.postHeader;
					postp.likeCount = dbpost.likeCount;
					postp.dislikeCount = dbpost.dislikeCount;
					db.SaveChanges();
				}
			}
			else
			{
				return;
			}
		}
		public static List<Models.Post> MakePostList()
		{
			List<Models.Post> outlist = new List<Models.Post>();
			using(var db = new MyFaceEntities())
			{

				var q = db.Posts.Select(x => x).ToList();
				foreach(myFaceDAL.Post p in q)
				{
					outlist.Add(new Models.Post()
					{
						id = p.postId,
						parentid = p.originalPostId.Value,
						dislikecount = p.dislikeCount,
						likecount=p.likeCount,
						publisherid =p.publisherId,
						postheader = p.postHeader,
						imagecontent = p.postImage,
						textcontent = p.postText
					});
				}

			}
			return outlist;
		}
	}
}
