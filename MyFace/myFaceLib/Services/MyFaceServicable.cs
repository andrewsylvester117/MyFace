using myFaceLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFaceLib.Services
{
	interface MyFaceServicable
	{
		List<FriendList> GetAllFriends(User user);
		User GetUserByID(int id);
		Post GetPostByID(int id);

	}
}
