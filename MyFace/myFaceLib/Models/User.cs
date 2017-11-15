using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace myFaceLib.Models
{
	public class User
	{
		public int id { get; set; }
		public string username { get; set; }
		public string realname { get; set; }
		public string password { get; set; }
		public string status { get; set; }
		public string dob { get; set; }
		public byte[] image { get; set; }
		//public Image image { get; set; }

		//key: int address# value: string address value
		public IDictionary<int,string> Addresses { get; set; }

		//key: string type and number of info value: string value of info
		public IDictionary<string, string> ContactInfo { get; set; }

		public IDictionary<int, User> Friends { get; set; }

		public string sign { get; set; }
		public string gender { get; set; }
		public string description { get; set; }



	}
}