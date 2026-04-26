
using DbManager.Security;
using DbManager;


namespace OurTests
{
	public class ManagerTest
	{
		#region IsUserAdminTests
		[Fact]
		public void Manager_IsUserAdmin_ShouldReturnTrue_WhenUserIsadminProfile() 
		{
			var manager = new Manager("adminUser");

			var adminProfile = new Profile { Name = Profile.AdminProfileName };
			adminProfile.Users.Add(new User("adminUser", "1234"));

			manager.Profiles.Add(adminProfile);

			var result = manager.IsUserAdmin();
			Assert.True(result);
		}
		[Fact]
		public void Manager_IsUserAdmin_ShouldReturnFalse_WhenUserIsNotAdminProfile()
		{
			var manager = new Manager("normalUser");

			var adminProfile = new Profile { Name = Profile.AdminProfileName };
			adminProfile.Users.Add(new User("adminUser", "1234"));

			manager.Profiles.Add(adminProfile);

			var result = manager.IsUserAdmin();
			Assert.False(result);
		}
		[Fact]
		public void Manager_IsUserAdmin_ShouldReturnFalse_WhenAdminProfileDoesNotExist()
		{
			var manager = new Manager("adminUser");

			var adminProfile = new Profile { Name = "User"};
			adminProfile.Users.Add(new User("adminUser", "1234"));

			manager.Profiles.Add(adminProfile);

			var result = manager.IsUserAdmin();
			Assert.False(result);
		}
		#endregion
		
		#region IsPasswordCorrect Test
		[Fact]
		public void Manager_IsPasswordCorrect_ShouldReturnTrue_WhenPasswordIsCorrect()
		{
			var manager = new Manager("adminUser");

			var adminProfile = new Profile { Name = "Admin" };
			adminProfile.Users.Add(new User("adminUser", "1234"));

			manager.Profiles.Add(adminProfile);

			var result = manager.IsPasswordCorrect("adminUser", "1234");
			Assert.True(result);
		}
		[Fact]
		public void Manager_IsPasswordCorrect_ShouldReturnFalse_WhenPasswordIsNotCorrect()
		{
			var manager = new Manager("adminUser");

			var adminProfile = new Profile { Name = "Admin" };
			adminProfile.Users.Add(new User("adminUser", "1234"));

			manager.Profiles.Add(adminProfile);

			var result = manager.IsPasswordCorrect("adminUser", "1111");
			Assert.False(result);
		}
		[Fact]
		public void Manager_IsPasswordCorrect_ShouldReturnFalse_WhenUserDoesNotExist()
		{
			var manager = new Manager("adminUser");

			var adminProfile = new Profile { Name = "Admin" };
			adminProfile.Users.Add(new User("adminUser", "1234"));

			manager.Profiles.Add(adminProfile);

			var result = manager.IsPasswordCorrect("unknow", "1234");
			Assert.False(result);
		}
		[Fact]
		public void Manager_IsPasswordCorrect_ShouldReturnFalse_WhenPasswordIsNull()
		{
			var manager = new Manager("adminUser");

			var adminProfile = new Profile { Name = "Admin" };
			adminProfile.Users.Add(new User("adminUser", "1234"));

			manager.Profiles.Add(adminProfile);

			var result = manager.IsPasswordCorrect("adminUser", null);
			Assert.False(result);
		}
		#endregion
		/*
		#region GrantPrivilegeTest
		 [Fact]
		public void Manager_GrantPrivilege_ShouldGrantPrivilege_WhenProfileExists() 
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			manager.Profiles.Add(profile);

			manager.GrantPrivilege("User", "Students", Privilege.Select);

			var result = profile.IsGrantedPrivilege("Student", Privilege.Select);
			Assert.True(result);
		}
		[Fact]
		public void Manager_GrantPrivilege_ShouldDoNothing_WhenProfileDoesNotExists()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			manager.Profiles.Add(profile);

			manager.GrantPrivilege("Unknow", "Students", Privilege.Select);

			var result = profile.IsGrantedPrivilege("Student", Privilege.Select);
			Assert.False(result);
		}
		[Fact]
		public void Manager_GrantPrivilege_ShouldDoNothing_WhenTableDoesNotExists()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			manager.Profiles.Add(profile);

			manager.GrantPrivilege("User", null, Privilege.Select);

			var result = profile.IsGrantedPrivilege(null, Privilege.Select);
			Assert.False(result);
		}
		
		[Fact]
		public void Manager_GrantPrivilege_ShouldAllowsSeveralGrantPrivilege_WhenIsOnSameTable()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			manager.Profiles.Add(profile);

			manager.GrantPrivilege("User", "Students", Privilege.Select);
			manager.GrantPrivilege("User", "Students", Privilege.Insert);


			var result1 = profile.IsGrantedPrivilege("Student", Privilege.Select);
			Assert.True(result1);

			var result2 = profile.IsGrantedPrivilege("Student", Privilege.Insert);
			Assert.True(result2);
		}
		
		[Fact]
		public void Manager_GrantPrivilege_ShouldAllowsSeveralGrantPrivilege_WhenIsOnDifferentTable()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			manager.Profiles.Add(profile);

			manager.GrantPrivilege("User", "Students", Privilege.Select);
			manager.GrantPrivilege("User", "Teachers", Privilege.Select);


			var result1 = profile.IsGrantedPrivilege("Student", Privilege.Select);
			Assert.True(result1);

			var result2 = profile.IsGrantedPrivilege("Teachers", Privilege.Insert);
			Assert.True(result2);
		}
		
		#endregion
		*/

	}
}
