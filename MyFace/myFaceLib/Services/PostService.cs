using System.Collections.Generic;
using System.Linq;
using MyFaceDAL;
using System.IO;

namespace MyFaceLib.Services
{
	public class PostService
	{
		public static void AddPost(MyFaceLib.Models.Post p, int publisherid, string imgdirpath)
		{
			if(p != null)
			{
                
				using (var db = new MyFaceDAL.Entities())
				{
                   
                   
                    MyFaceDAL.Post dbp = new MyFaceDAL.Post()
                    {
                        postText = p.Textcontent,
                        
                        postHeader = p.Postheader,
                        publisherId = publisherid,
                        dislikeCount = p.Dislikecount,
                        likeCount = p.Likecount,
                        originalPostId = p.Parentid,
                    };
                    if (p.Parentid == 0)
                    {
                        dbp.originalPostId = null;
                    }
                    db.Posts.Add(dbp);
                    db.SaveChanges();
                    if (!string.IsNullOrEmpty(p.Imagefname))
                    {
                        AddImage(imgdirpath, p.Imagefname, db.Posts.Count() == 0 ? 1:db.Posts.Count());
                    }
                    db.SaveChanges();
				}
			}
			else
			{
				return;
			}
		}

		public static void UpdatePost(Models.Post p, string imgdirpath)
		{
			if(p != null)
			{
				using (var db = new MyFaceDAL.Entities())
				{
                   

                    MyFaceDAL.Post dbpost = new MyFaceDAL.Post()
					{
						postText = p.Textcontent,
						postHeader = p.Postheader,
                        
						postId = p.Id,
						publisherId = p.PublisherId,
						dislikeCount = p.Dislikecount,
						likeCount = p.Likecount,
						originalPostId = p.Parentid,
					};
                    
                    var postp = db.Posts.Where(x => x.postId == dbpost.postId).First();
					postp.postText = dbpost.postText;
					postp.postHeader = dbpost.postHeader;
					postp.likeCount = dbpost.likeCount;
					postp.dislikeCount = dbpost.dislikeCount;
                    if (p.Parentid == 0)
                    {
                        postp.originalPostId = null;
                    }
                    
                    db.SaveChanges();
                    if (!string.IsNullOrEmpty(imgdirpath) && p.Imagefname != db.Images.Where(x => x.postid == dbpost.postId).First().filename)
                    {
                        AddImage(imgdirpath, p.Imagefname,postp.postId);
                    }
                    else
                    {
                        UpdateImage(db.Images.Where(x => x.postid == dbpost.postId).First().Id, p.Imagefname, imgdirpath);
                    }
                    db.SaveChanges();
                }
			}
			else
			{
				return;
			}
		}
		public static List<Models.Post> MakePostList(string imgdirpath)
		{
			List<Models.Post> outlist = new List<Models.Post>();
			using(var db = new MyFaceDAL.Entities())
			{
                //LoadImagesToFiles(imgdirpath);
				var q = db.Posts.Select(x => x).ToList();
				foreach(MyFaceDAL.Post p in q)
				{
                    Models.Post px = new Models.Post()
                    {
                        Id = p.postId,
                        Parentid = p.originalPostId == null ? 0 : p.originalPostId.Value,

                        Dislikecount = p.dislikeCount,
                        Likecount = p.likeCount,
                        PublisherId = p.publisherId,
                        Postheader = p.postHeader,
                        Textcontent = p.postText
                    };
                    
                    if (db.Images.Where(x => x.postid == p.postId).First()!= null)
                    {
                        px.Imagefname = db.Images.Where(x => p.postId == x.postid).First().filename;
                    }
                    outlist.Add(px);
				}

			}
			return outlist;
		}
        public static void LoadImagesToFiles(string imgdirpath)
        {
            using(var db = new MyFaceDAL.Entities())
            {
                foreach(Image i in db.Images)
                {
                    if (!File.Exists(imgdirpath + i.filename))
                    {
                        File.Create(imgdirpath + i.filename);
                        File.WriteAllBytes(imgdirpath + i.filename, i.data);
                    }
                }
            }
        }
        public static void AddImage(string imgdirpath, string filename, int postid)
        {
            using(var db = new MyFaceDAL.Entities())
            {
                MyFaceDAL.Image img = new MyFaceDAL.Image()
                {
                    filename = filename,
                    data = File.ReadAllBytes(imgdirpath + filename),
                    postid = postid
                };
                
                db.Images.Add(img);
                db.SaveChanges();
            }
        }
        public static void UpdateImage(int imageid, string filename, string imgdirpath)
        {
            using(var db = new MyFaceDAL.Entities())
            {
                Image i = db.Images.Where(x => x.Id == imageid).First();
                i.filename = filename;
                i.data = File.ReadAllBytes(imgdirpath + filename);
                db.SaveChanges();
            }
        }
	}
}
