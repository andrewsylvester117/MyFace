using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFace.Models
{
	public class Post
	{
		public int id { get; set; }
		public string textcontent { get; set; }
		public byte[] imagecontent { get; set; }

	}
}