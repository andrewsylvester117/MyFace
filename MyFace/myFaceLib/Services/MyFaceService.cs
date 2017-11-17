using myFaceLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFaceLib.Services
{
	class MyFaceService : MyFaceServicable
	{
		public List<FriendList> GetAllFriends(User user)
		{
			List<FriendList> usersFriends = new List<FriendList>();

			// Need to add refrence to myFaceDal, but complains about a circular dependency when doing so.

			//using ()
			//{

			//}
				return usersFriends;
		}

		public User GetUserByID(int id)
		{

			return new User();
		}

		public Post GetPostByID(int id)
		{

			return new Post();
		}
	}
}
