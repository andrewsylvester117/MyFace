using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myFaceLib;
using myFaceLib.Models;
using myFaceDAL;
using System.IO;

namespace myFaceLib.Services
{
	public class PostService
	{
		public static void AddPost(myFaceLib.Models.Post p, int publisherid, string imgdirpath)
		{
			if(p != null)
			{
                
				using (var db = new myFaceDAL.Entities())
				{
                   
                   
                    myFaceDAL.Post dbp = new myFaceDAL.Post()
                    {
                        postText = p.textcontent,
                        
                        postHeader = p.postheader,
                        publisherId = publisherid,
                        dislikeCount = p.dislikecount,
                        likeCount = p.likecount,
                        originalPostId = p.parentid,
                    };
                    if (p.parentid == 0)
                    {
                        dbp.originalPostId = null;
                    }
                    db.Posts.Add(dbp);
                    db.SaveChanges();
                    if (!string.IsNullOrEmpty(p.imagefname))
                    {
                        AddImage(imgdirpath, p.imagefname, db.Posts.Count() == 0 ? 1:db.Posts.Count());
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
				using (var db = new myFaceDAL.Entities())
				{
                   

                    myFaceDAL.Post dbpost = new myFaceDAL.Post()
					{
						postText = p.textcontent,
						postHeader = p.postheader,
                        
						postId = p.id,
						publisherId = p.publisherid,
						dislikeCount = p.dislikecount,
						likeCount = p.likecount,
						originalPostId = p.parentid,
					};
                    
                    var postp = db.Posts.Where(x => x.postId == dbpost.postId).First();
					postp.postText = dbpost.postText;
					postp.postHeader = dbpost.postHeader;
					postp.likeCount = dbpost.likeCount;
					postp.dislikeCount = dbpost.dislikeCount;
                    if (p.parentid == 0)
                    {
                        postp.originalPostId = null;
                    }
                    
                    db.SaveChanges();
                    if (!string.IsNullOrEmpty(imgdirpath) && p.imagefname != db.Images.Where(x => x.postid == dbpost.postId).First().filename)
                    {
                        AddImage(imgdirpath, p.imagefname,postp.postId);
                    }
                    else
                    {
                        UpdateImage(db.Images.Where(x => x.postid == dbpost.postId).First().Id, p.imagefname, imgdirpath);
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
			using(var db = new myFaceDAL.Entities())
			{
                LoadImagesToFiles(imgdirpath);
				var q = db.Posts.Select(x => x).ToList();
				foreach(myFaceDAL.Post p in q)
				{
                    Models.Post px = new Models.Post()
                    {
                        id = p.postId,
                        parentid = p.originalPostId == null ? 0 : p.originalPostId.Value,

                        dislikecount = p.dislikeCount,
                        likecount = p.likeCount,
                        publisherid = p.publisherId,
                        postheader = p.postHeader,
                        textcontent = p.postText
                    };
                    
                    if (db.Images.Where(x => x.postid == p.postId).First()!= null)
                    {
                        px.imagefname = imgdirpath + db.Images.Where(x => p.postId == x.postid).First().filename;
                    }
                    outlist.Add(px);
				}

			}
			return outlist;
		}
        public static void LoadImagesToFiles(string imgdirpath)
        {
            using(var db = new myFaceDAL.Entities())
            {
                foreach(Image i in db.Images)
                {
                    if (!File.Exists(imgdirpath + i.filename))
                    {
                        File.Create(imgdirpath + i.filename).Write(i.data, 0, 0);
                    }
                }
            }
        }
        public static void AddImage(string imgdirpath, string filename, int postid)
        {
            using(var db = new myFaceDAL.Entities())
            {
                myFaceDAL.Image img = new myFaceDAL.Image()
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
            using(var db = new myFaceDAL.Entities())
            {
                Image i = db.Images.Where(x => x.Id == imageid).First();
                i.filename = filename;
                i.data = File.ReadAllBytes(imgdirpath + filename);
                db.SaveChanges();
            }
        }
	}
}
