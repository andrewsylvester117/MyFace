using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace myFaceLib.Models
{
	public class Post
	{
		public int publisherid { get; set; }
		public int id { get; set; }
		public int parentid { get; set; }
		public string postheader {get; set;}
		public string textcontent { get; set; }
		//public Image imagecontent { get; set; }

		public int likecount { get; set; }
		public int dislikecount { get; set; }

	}
}