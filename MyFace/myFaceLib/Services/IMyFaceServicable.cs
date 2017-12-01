using MyFaceLib.Models;
using System.Collections.Generic;

namespace MyFaceLib.Services
{
	interface IMyFaceServicable
	{
		List<User> GetAllFriends( User user );
		User GetUserByID( int id );
		User GetUserByEmail( string email );
		bool CreateNewUser( User newUser );
	}
}
