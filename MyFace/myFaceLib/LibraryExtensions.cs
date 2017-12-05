namespace MyFaceLib
{
	/// <summary>
	/// @KMurphy@student.neumont.edu
	/// Extension methods which interact between the DAL and Library.
	///	Methods:
	///		MyFaceDAL.User CreateDALUserFrom(this MyFaceLib.User)
	///		void CreateDALUserFrom(this MyFaceDAL.User, MyFaceLib.User)
	///		Models.User CreateLibUserFrom(this MyFaceDAL.User dalUser)
	///		void CreateLibUserFrom(this Models.User baseModel, MyFaceDAL.User dalUser)
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Extension method which transforms a DAL.User to a Lib.User
		/// </summary>
		/// <returns>A new DAL.User with the given data</returns>
		public static MyFaceDAL.User CreateDALUser( this Models.User libUser )
		{
			//create a new DAL.User 
			MyFaceDAL.User newDbUser = new MyFaceDAL.User()
			{
				// translate the data from the Lib.User to the new DAL.User
				realName = libUser.RealName,
				userName = libUser.UserName,
				password = libUser.Password,
				email = libUser.Email,
				image = libUser.Image,
				status = libUser.Status,
				dateOfBirth = libUser.Dob,
				zodiacSign = libUser.ZodiacSign,
				address1 = libUser.Address1,
				adress2 = libUser.Address2,
				phoneNumber = libUser.PhoneNumber,
				prefferedSSN = libUser.PrefferedSSN,
				description = libUser.Description,
				isMaleGender = libUser.IsMaleGender
			};

			// public IDictionary<int, User> Friends { get; set; }
			// is a "public virtual" property, cannot access it, apparently.

			return newDbUser;
		}


		/// <summary>
		/// Extension method which transforms a DAL.User to a Lib.User
		/// Acts on a DAL.User
		/// </summary>
		public static void CreateDALUserFrom( this MyFaceDAL.User baseModel, Models.User libUser )
		{
			// modify a the DAL.User 
			// translate the data from the Lib.User to the new DAL.User
			baseModel.realName = libUser.RealName;
			baseModel.userName = libUser.UserName;
			baseModel.password = libUser.Password;
			baseModel.email = libUser.Email;
			baseModel.image = libUser.Image;
			baseModel.status = libUser.Status;
			baseModel.dateOfBirth = libUser.Dob;
			baseModel.zodiacSign = libUser.ZodiacSign;
			baseModel.address1 = libUser.Address1;
			baseModel.adress2 = libUser.Address2;
			baseModel.phoneNumber = libUser.PhoneNumber;
			baseModel.prefferedSSN = libUser.PrefferedSSN;
			baseModel.description = libUser.Description;
			baseModel.isMaleGender = libUser.IsMaleGender;

			// public IDictionary<int, User> Friends { get; set; }
			// is a "public virtual" property, cannot access it, apparently.
		}


		/// <summary>
		/// Extension method which transforms a Lib.User to a DAL.User
		/// </summary>
		/// <returns>A new DAL.User with the given data</returns>
		public static Models.User CreateLibUser( this MyFaceDAL.User dalUser )
		{
			//create a new DAL.User 
			Models.User newLibUser = new Models.User()
			{
				// translate the data from the Lib.User to the new DAL.User
				RealName = dalUser.realName,
				UserName = dalUser.userName,
				Password = dalUser.password,
				Email = dalUser.email,
				Image = dalUser.image,
				Status = dalUser.status,
				Dob = dalUser.dateOfBirth,
				ZodiacSign = dalUser.zodiacSign,
				Address1 = dalUser.address1,
				Address2 = dalUser.adress2,
				PhoneNumber = dalUser.phoneNumber,
				PrefferedSSN = dalUser.prefferedSSN,
				Description = dalUser.description,
				IsMaleGender = dalUser.isMaleGender
			};

			// translate friends over to the list
			int i = 0;
			foreach (var user in dalUser.Users)
			{
				newLibUser.Friends.Add(i++, CreateLibUser(user));
			}

			return newLibUser;
		}


		/// <summary>
		/// Extension method which transforms a Lib.User to a DAL.User
		/// Acts on a Lib.User
		/// </summary>
		public static void CreateLibUserFrom( this Models.User baseModel, MyFaceDAL.User dalUser )
		{
			//create a new DAL.User 
			// translate the data from the Lib.User to the new DAL.User
			baseModel.RealName = dalUser.realName;
			baseModel.UserName = dalUser.userName;
			baseModel.Password = dalUser.password;
			baseModel.Email = dalUser.email;
			baseModel.Image = dalUser.image;
			baseModel.Status = dalUser.status;
			baseModel.Dob = dalUser.dateOfBirth;
			baseModel.ZodiacSign = dalUser.zodiacSign;
			baseModel.Address1 = dalUser.address1;
			baseModel.Address2 = dalUser.adress2;
			baseModel.PhoneNumber = dalUser.phoneNumber;
			baseModel.PrefferedSSN = dalUser.prefferedSSN;
			baseModel.Description = dalUser.description;
			baseModel.IsMaleGender = dalUser.isMaleGender;

			// translate friends over to the list
			int i = 0;
			foreach (var user in dalUser.Users)
			{
				baseModel.Friends.Add(i++, CreateLibUser(user));
			}
		}
	}

}
