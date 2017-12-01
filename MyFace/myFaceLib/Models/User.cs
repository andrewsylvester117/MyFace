using System;
using System.Collections.Generic;

namespace MyFaceLib.Models
{
	public class User
	{
		public int Id { get; set; }
		public string RealName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public byte[] Image { get; set; }
		public string Status { get; set; }
		public DateTime? Dob { get; set; }
		public string ZodiacSign { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string PhoneNumber { get; set; }
		public string PrefferedSSN { get; set; }
		public string Description { get; set; }
		public bool? IsMaleGender { get; set; }
		public IDictionary<int, User> Friends { get; set; }

		//key: string type and number of info value: string value of info
		public IDictionary<string, string> ContactInfo { get; set; }

		//key: int postId number: string text of post
		public IDictionary<int, string> Posts { get; set; }




	}
}