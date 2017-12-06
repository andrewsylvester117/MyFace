using MyFaceLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFaceLib.Services
{
	/// <summary>
	/// kmurphy@student.neumont.edu
	/// A service layer to facilitate communication 
	/// between the data context and the view
	/// </summary>
	public static class MyFaceService
	{	
		/// <summary>
		/// Gets the list of friends based on the passed in user
		/// </summary>
		/// <param name="user">The user from which to get friends</param>
		public static List<User> GetAllFriends(string userName)
		{
			List<User> friends = new List<User>();

			using (var dataContext = new MyFaceDAL.Entities())
			{
				// get the DAL User
				var dataContextUser = dataContext.Users.Where(theUser => theUser.userName == userName).First();

				// get the dictionary of friends
				var friendCollection = dataContextUser.CreateLibUser().Friends;

				// iterate through dictionary of friends to add to a List
				foreach (var friend in friendCollection.Values)
				{
					friends.Add(friend);
				}
			}
			
			// return the list of friends
			return friends;
		}


		/// <summary>
		/// How is the DAL UserId passed in from the view controller 
		/// if the ID of a AspNetUser is a guid-type id??
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static User GetUserByID(int id)
		{
			// placeholder
			User libUser = null;

			//connect and get the dalUser
			using (var dataContext = new MyFaceDAL.Entities())
			{
				// get the dalUser
				var dalUser = dataContext.Users.Where(dbUser => dbUser.userId == id).First();

				// translate user data from DAL to Lib
				libUser = dalUser.CreateLibUser();
			}

			// return the converted user
			return libUser;
		}


		/// <summary>
		/// Returns the MyFaceLib.User if found,
		/// ELSE: returns null
		/// Throws: ArgumentNullException
		/// </summary>
		/// <param name="email">The email from the logged-in user</param>
		public static User GetUserByEmail(string email)
		{
			// make sure input is valid
			if (string.IsNullOrEmpty(email))
			{
				// complain if input is not valid
				throw new ArgumentNullException("The Email cannot be null");
			}
			else
			// input is good?
			{
				// connect to db
				using (var dataContext = new MyFaceDAL.Entities())
				{
					// write base query
					var query = dataContext.Users.Select(contextUser => contextUser);

					// try to get the currently logged in user from our custom users table by email
					IQueryable<MyFaceDAL.User> dbUser = query.Where(contextUser => contextUser.email == email);

					// if user is found
					if (dbUser.Count() != 0)
					{
						var theUser = dbUser.First();

						// get and return the user
						return theUser.CreateLibUser();
					}

					// did not find the user
					return null;
				}
			}
		}


		/// <summary>
		/// Creates a new User entry in the 
		/// DataContext based on the given Library User
		/// </summary>
		/// <param name="newUser">The user to be translated to the data context</param>
		/// <returns>bool representing successful placement (or not)</returns>
		public static bool CreateNewUser(User newUser)
		{
			// try/catch for error catching
			//try
			//{
				// create the db user
				MyFaceDAL.User dbUser = new MyFaceDAL.User();

				// translate data from the Lib.User to DAL.User
				dbUser.CreateDALUserFrom(newUser);

				// create data context connection
				using (var dataContext = new MyFaceDAL.Entities())
				{
					// add user to db
					dbUser.userId = dataContext.Users.Max(u => u.userId) + 1;
					dataContext.Users.Add(dbUser);

					// save
					dataContext.SaveChanges();
				}

				// added with success
				return true;
			//}
			//catch (Exception e)
			//{
			//	// consume
			//	Console.WriteLine(e.StackTrace);

			//	// failed to add
			//	return false;
			//}
		}


		/// <summary>
		/// Gets a list of all registered users, unless a username is specified.
		/// </summary>
		/// <param name="loggedInUserName">The current user. Removes the user and their friends from the list</param>
		public static List<User> GetAllUsers(string loggedInUserName = null)
		{
			// create the listmodel
			List<User> userList = new List<User>();

			// connect to context
			using (var dataContext = new MyFaceDAL.Entities())
			{
				// get all the users except for the one who is logged in
				var allButCurrentUser = dataContext.Users.Select(user => user).ToList();

				// add each of the DBUsers to the listModel list
				allButCurrentUser.ForEach(user =>
				{
					// extension method call to convert data
					userList.Add(user.CreateLibUser());
				});
			}

			// if the userName was supplied, remove the friends and current user from the list
			if (loggedInUserName != null)
			{
				// get the friends
				var friendOrSelf = GetAllFriends(loggedInUserName);

				// for each of the friends,
				friendOrSelf.ForEach(known =>
				{
					if (userList.Contains(known))
					{
						// remove them
						userList.Remove(known);
					}
				});
				
				// remove the current user
				userList.Remove(GetUserByEmail(loggedInUserName));
			}

			return userList;
		}
	}
}
