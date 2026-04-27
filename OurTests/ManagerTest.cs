
using DbManager.Security;
using DbManager;


namespace OurTests
{
	public class ManagerTest
	{
		#region IsUserAdminTests
		/*
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
		*/
		#endregion
		
		#region IsPasswordCorrect Test
		/*
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
		*/
		#endregion
		
		#region GrantPrivilegeTest
		 [Fact]
		public void Manager_GrantPrivilege_ShouldGrantPrivilege_WhenProfileExists() 
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			manager.Profiles.Add(profile);

			manager.GrantPrivilege("User", "Students", Privilege.Select);

			var result = profile.IsGrantedPrivilege("Students", Privilege.Select);
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


			var result1 = profile.IsGrantedPrivilege("Students", Privilege.Select);
			Assert.True(result1);

			var result2 = profile.IsGrantedPrivilege("Students", Privilege.Insert);
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


			var result1 = profile.IsGrantedPrivilege("Students", Privilege.Select);
			Assert.True(result1);

			var result2 = profile.IsGrantedPrivilege("Teachers", Privilege.Select);
			Assert.True(result2);
		}

		#endregion
		
		#region RevokePrivilege Tests
		
		[Fact]
		public void Manager_RevokePrivilege_ShouldRevokePrivilege_WhenProfileExists()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			profile.GrantPrivilege("Students", Privilege.Select);
			manager.Profiles.Add(profile);

			manager.RevokePrivilege("User", "Students", Privilege.Select);

			var result = profile.IsGrantedPrivilege("Student", Privilege.Select);
			Assert.False(result);
		}
		[Fact]
		public void Manager_RevokePrivilege_ShouldDoNothing_WhenProfileDoesNotExists()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			profile.GrantPrivilege("Students", Privilege.Select);
			manager.Profiles.Add(profile);

			manager.RevokePrivilege("Unknow", "Students", Privilege.Select);

			var result = profile.IsGrantedPrivilege("Students", Privilege.Select);
			Assert.True(result);
		}
		[Fact]
		public void Manager_RevokePrivilege_ShouldDoNothing_WhenTableDoesNotExists()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			profile.GrantPrivilege("Students", Privilege.Select);
			manager.Profiles.Add(profile);

			manager.RevokePrivilege("User", "Teachers", Privilege.Select);

			var result = profile.IsGrantedPrivilege("Students", Privilege.Select);
			Assert.True(result);
		}
		[Fact]
		public void Manager_RevokePrivilege_ShouldDoNothing_WhenPrivilegeDoesNotExists()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			profile.GrantPrivilege("Students", Privilege.Select);
			manager.Profiles.Add(profile);

			manager.RevokePrivilege("User", "Students", Privilege.Insert);

			var result = profile.IsGrantedPrivilege("Students", Privilege.Select);
			Assert.True(result);
		}
		
		#endregion
		
		#region IsGrantedPrivilege Tests
		/*
		[Fact]
		public void Manager_IsGrantedPrivilege_ShouldReturnTrue_WhenUserHasPrivilege()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			profile.Users.Add(new User("user1", "1234"));
			profile.GrantPrivilege("Students", Privilege.Select);
			
			manager.Profiles.Add(profile);


			var result = manager.IsGrantedPrivilege("user1", "Students", Privilege.Select);
			Assert.True(result);
		}
		[Fact]
		public void Manager_IsGrantedPrivilege_ShouldReturnFalse_WhenUserDoesHasPrivilege()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			profile.Users.Add(new User("user1", "1234"));
			profile.GrantPrivilege("Students", Privilege.Select);

			manager.Profiles.Add(profile);


			var result = manager.IsGrantedPrivilege("user1", "Students", Privilege.Insert);
			Assert.False(result);
		}
		[Fact]
		public void Manager_IsGrantedPrivilege_ShouldReturnFalse_WhenUserDoesNotExist()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			profile.Users.Add(new User("user1", "1234"));
			profile.GrantPrivilege("Students", Privilege.Select);

			manager.Profiles.Add(profile);


			var result = manager.IsGrantedPrivilege("unknow", "Students", Privilege.Select);
			Assert.False(result);
		}
		[Fact]
		public void Manager_IsGrantedPrivilege_ShouldReturnFalse_WhenTableDoesNotExist()
		{
			var manager = new Manager("adminUser");

			var profile = new Profile { Name = "User" };
			profile.Users.Add(new User("user1", "1234"));

			manager.Profiles.Add(profile);


			var result = manager.IsGrantedPrivilege("user1", "Students", Privilege.Select);
			Assert.False(result);
		}
		[Fact]
		public void Manager_IsGrantedPrivilege_ShouldReturnFalse_WhenTableIsNull()
		{
			var manager = new Manager("adminUser");

			var result = manager.IsGrantedPrivilege("user1", null, Privilege.Select);
			Assert.False(result);
		}
		*/
		#endregion
		


	}
}
