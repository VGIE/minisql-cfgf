
using DbManager.Security;
using DbManager;
using System.IO;


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
			adminProfile.Users.Add(new User("adminUser","1234"));

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
		
		#region GrantPrivilegeTest
		 [Fact]
		public void Manager_GrantPrivilege_ShouldGrantPrivilege_WhenProfileExists() 
		{
			var manager = new Manager("admin");

			var adminProfile = new Profile { Name = Profile.AdminProfileName};
            adminProfile.Users.Add(new User("admin", "1234"));

            var userProfile = new Profile { Name = "User" };
            userProfile.Users.Add(new User("Alicia", "1234"));

            manager.Profiles.Add(adminProfile);
			manager.Profiles.Add(userProfile);

			manager.GrantPrivilege("User", "Customers", Privilege.Select);


			var result = manager.IsGrantedPrivilege("Alicia", "Customers", Privilege.Select);
			Assert.True(result);
		}
		[Fact]
		public void Manager_GrantPrivilege_ShouldDoNothing_WhenUserDoesNotHavePrivilege()
		{
			var manager = new Manager("anyUser");

			var profile = new Profile { Name = "HR" };

            var user = new User("ane", "1234");
			profile.Users.Add(user);

            manager.Profiles.Add(profile);

			var result = manager.IsGrantedPrivilege("bob", "Employees", Privilege.Select);
			Assert.False(result);
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
			var manager = new Manager("admin");

			var adminProfile = new Profile { Name = Profile.AdminProfileName};
            adminProfile.Users.Add(new User("admin", "1234"));

            var userProfile = new Profile { Name = "User" };

            manager.Profiles.Add(adminProfile);
			manager.Profiles.Add(userProfile);


			manager.GrantPrivilege("User", "Students", Privilege.Select);
			manager.GrantPrivilege("User", "Students", Privilege.Insert);


			var result1 = userProfile.IsGrantedPrivilege("Students", Privilege.Select);
			Assert.True(result1);

			var result2 = userProfile.IsGrantedPrivilege("Students", Privilege.Insert);
			Assert.True(result2);
		}
		
		[Fact]
		public void Manager_GrantPrivilege_ShouldAllowsSeveralGrantPrivilege_WhenIsOnDifferentTable()
		{
			var manager = new Manager("admin");

			var adminprofile = new Profile { Name = Profile.AdminProfileName };
            adminprofile.Users.Add(new User("admin", "1234"));

            var userProfile = new Profile { Name = "User" };
			
            manager.Profiles.Add(adminprofile);
            manager.Profiles.Add(userProfile);

			manager.GrantPrivilege("User", "Students", Privilege.Select);
			manager.GrantPrivilege("User", "Teachers", Privilege.Select);


			var result1 = userProfile.IsGrantedPrivilege("Students", Privilege.Select);
			Assert.True(result1);

			var result2 = userProfile.IsGrantedPrivilege("Teachers", Privilege.Select);
			Assert.True(result2);
		}

        #endregion

        #region RevokePrivilege Tests
        private Manager CreateManager() 
        {
            var manager = new Manager("admin");

            var adminProfile = new Profile { Name = Profile.AdminProfileName };
            adminProfile.Users.Add(new User("admin", "1234"));

            manager.Profiles.Add(adminProfile);

            return manager;
        }
		[Fact]
		public void Manager_RevokePrivilege_ShouldRevokePrivilege_WhenProfileExists()
		{
            var manager = CreateManager();

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
            var manager = CreateManager();

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
            var manager = CreateManager();

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
            var manager = CreateManager();

			var profile = new Profile { Name = "User" };
			profile.GrantPrivilege("Students", Privilege.Select);
			
            manager.Profiles.Add(profile);

			manager.RevokePrivilege("User", "Students", Privilege.Insert);

			var result = profile.IsGrantedPrivilege("Students", Privilege.Select);
			Assert.True(result);
		}
        [Fact]
        public void Manager_RevokePrivilege_ShouldDoNothing_WhenUserIsNotAdmin()
        {
            var manager = new Manager("normalUser");

            var profile = new Profile { Name = "User" };
            profile.Users.Add(new User("normalUser", "1234"));
            profile.GrantPrivilege("Students", Privilege.Select);

            manager.Profiles.Add(profile);

            manager.RevokePrivilege("User", "Students", Privilege.Select);
            var result = profile.IsGrantedPrivilege("Students", Privilege.Select);
            Assert.True(result);
        }

		#endregion

			#region IsGrantedPrivilege Tests

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
		public void Manager_IsGrantedPrivilege_ShouldReturnTrue_WhenUserIsAdmin()
		{
			var manager = new Manager("adminUser");

			var adminProfile = new Profile { Name = "Admin" };
			adminProfile.Users.Add(new User("adminUser", "1234"));

			manager.Profiles.Add(adminProfile);

			var result = manager.IsGrantedPrivilege("adminUser", "Students", Privilege.Select);

			Assert.True(result);
		}
		[Fact]
        public void Manager_IsGrantesPrivilege_ShouldReturnfalse_WhenUsernameIsNull()
        {
            var manager = new Manager("adminUser");

            var result = manager.IsGrantedPrivilege(null, "Students", Privilege.Select);
            Assert.False(result);
        }
        [Fact]
		public void Manager_IsGrantesPrivilege_ShouldReturnfalse_WhenUsernameIsEmpty()
		{
			var manager = new Manager("adminUser");

			var result = manager.IsGrantedPrivilege("", "Students", Privilege.Select);
			Assert.False(result);
		}
		[Fact]
		public void Manager_IsGrantesPrivilege_ShouldReturnfalse_WhenTableIsEmpty()
		{
			var manager = new Manager("adminUser");

			var result = manager.IsGrantedPrivilege("adminUser", "", Privilege.Select);
			Assert.False(result);
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
		
        #endregion

        #region AddProfile Tests

        [Fact]
        public void Manager_AddProfile_ShouldAddProfile_WhenProfileDoesNotExist()
        {
            var manager = new Manager("adminUser");
            var profile = new Profile {Name = "User"};
            manager.AddProfile(profile);

            Assert.Single(manager.Profiles);
            Assert.Equal("User", manager.Profiles[0].Name);
        }

        [Fact]
        public void Manager_AddProfile_ShouldDoNothing_WhenProfileIsNull()
        {
            var manager = new Manager("adminUser");
            manager.AddProfile(null);
            Assert.Empty(manager.Profiles);
        }

        [Fact]
        public void Manager_AddProfile_ShouldDoNothing_WhenProfileAlreadyExists()
        {
            var manager = new Manager("adminUser");
            var profile1 = new Profile {Name = "User"};
            var profile2 = new Profile {Name = "User"};
            manager.AddProfile(profile1);
            manager.AddProfile(profile2);

            Assert.Equal(1, manager.Profiles.Count);
            Assert.Equal("User", manager.Profiles[0].Name);
        }

        [Fact]
        public void Manager_AddProfile_ShouldAddSeveralProfiles_WhenNamesAreDifferent()
        {
            var manager = new Manager("adminUser");
            var profile1 = new Profile {Name = "Admin"};
            var profile2 = new Profile {Name = "User"};
            profile1.Users.Add(new User("adminUser", "password123"));
            manager.AddProfile(profile1);
            manager.AddProfile(profile2);

            Assert.Equal(2, manager.Profiles.Count);
            Assert.Equal("Admin", manager.Profiles[0].Name);
            Assert.Equal("User", manager.Profiles[1].Name);

        }

        #endregion

        #region UserByName Tests

        [Fact]
        public void Manager_UserByName_ShouldReturnUser_WhenUserExists()
        {
            var manager = new Manager("adminUser");
            var profile = new Profile { Name = "User" };
            var user = new User("user1", "1234");
            profile.Users.Add(user);
            manager.Profiles.Add(profile);

            var result = manager.UserByName("user1");
            Assert.Equal("user1", result.Username);
        }

        [Fact]
        public void Manager_UserByName_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var manager = new Manager("adminUser");
            var profile = new Profile { Name = "User" };
            profile.Users.Add(new User("user1", "1234"));
            manager.Profiles.Add(profile);

            var result = manager.UserByName("NoExiste");
            Assert.Null(result);
        }

        [Fact]
        public void Manager_UserByName_ShouldReturnCorrectUser_WhenSeveralProfilesExist()
        {
            var manager = new Manager("adminUser");
            var profile1 = new Profile { Name = "User" };
            profile1.Users.Add(new User("user1", "1234"));
            var profile2 = new Profile { Name = "Admin" };
            profile2.Users.Add(new User("admin", "1234"));
            manager.Profiles.Add(profile1);
            manager.Profiles.Add(profile2);

            var result = manager.UserByName("admin");
            Assert.Equal("admin", result.Username);
        }

        [Fact]
        public void Manager_UserByName_ShouldReturnNull_WhenNoProfilesExist()
        {
            var manager = new Manager("adminUser");
            var result = manager.UserByName("user1");

            Assert.Null(result);
        }

        #endregion

        #region ProfileByName Tests

        [Fact]
        public void Manager_ProfileByName_ShouldReturnProfile_WhenProfileExists()
        {
            var manager = new Manager("adminUser");
            var profile = new Profile {Name = "User"};
            manager.Profiles.Add(profile);

            var result = manager.ProfileByName("User");
            Assert.NotNull(result);
            Assert.Equal("User", result.Name);
        }

        [Fact]
        public void Manager_ProfileByName_ShouldReturnNull_WhenProfileDoesNotExist()
        {
            var manager = new Manager("adminUser");
            var profile = new Profile {Name = "User"};
            manager.Profiles.Add(profile);

            var result = manager.ProfileByName("Admin");
            Assert.Null(result);
        }

        [Fact]
        public void Manager_ProfileByName_ShouldReturnCorrectProfile_WhenSeveralProfilesExist()
        {
            var manager = new Manager("adminUser");
            var profile1 = new Profile {Name = "User"};
            var profile2 = new Profile {Name = "Admin"};
            manager.Profiles.Add(profile1);
            manager.Profiles.Add(profile2);

			var result = manager.ProfileByName("Admin");
            Assert.NotNull(result);
            Assert.Equal("Admin", result.Name);
        }

        [Fact]
        public void Manager_ProfileByName_ShouldReturnNull_WhenProfileNameIsNull()
        {
            var manager = new Manager("adminUser");
            var profile = new Profile {Name = "User"};
            manager.Profiles.Add(profile);

            var result = manager.ProfileByName(null);
            Assert.Null(result);
        }

        [Fact]
        public void Manager_ProfileByName_ShouldAcceptProfilesWithNullName()
        {
            var manager = new Manager("adminUser");
            var profile1 = new Profile {Name = null};
            var profile2 = new Profile {Name = "User"};
            manager.Profiles.Add(profile1);
            manager.Profiles.Add(profile2);

            var result = manager.ProfileByName("User");
            Assert.NotNull(result);
            Assert.Equal("User", result.Name);
        }

        #endregion

        #region ProfileByUser Tests

        [Fact]
        public void Manager_ProfileByUser_ShouldReturnProfile_WhenUserExists()
        {
            var manager = new Manager("adminUser");
            var profile = new Profile {Name = "User"};
            profile.Users.Add(new User("user1", "1234"));
            manager.Profiles.Add(profile);

            var result = manager.ProfileByUser("user1");
            Assert.NotNull(result);
            Assert.Equal("User", result.Name);
        }

        [Fact]
        public void Manager_ProfileByUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var manager = new Manager("adminUser");
            var profile = new Profile {Name = "User"};
            profile.Users.Add(new User("user1", "1234"));
            manager.Profiles.Add(profile);

            var result = manager.ProfileByUser("NoExiste");
            Assert.Null(result);
        }

        [Fact]
        public void Manager_ProfileByUser_ShouldReturnCorrectProfile_WhenSeveralProfilesExist()
        {
            var manager = new Manager("adminUser");
            var profile1 = new Profile { Name = "User" };
            profile1.Users.Add(new User("user1", "1234"));
            var profile2 = new Profile { Name = "Admin" };
            profile2.Users.Add(new User("admin", "1234"));
            manager.Profiles.Add(profile1);
            manager.Profiles.Add(profile2);

            var result = manager.ProfileByUser("admin");
            Assert.NotNull(result);
            Assert.Equal("Admin", result.Name);
        }

        [Fact]
        public void Manager_ProfileByUser_ShouldReturnNull_WhenNoProfilesExist()
        {
            var manager = new Manager("adminUser");
            var result = manager.ProfileByUser("user1");
            Assert.Null(result);
        }

        #endregion

        #region Save Tests

        [Fact]
        public void Manager_Save_ShouldCreateSecurityFile()
        {
            var manager = new Manager("admin");
            var fileName = "manager_save_test";

            if (File.Exists(fileName + ".security")) File.Delete(fileName + ".security");

            manager.Save(fileName);

            Assert.True(File.Exists(fileName + ".security"));
        }

        [Fact]
        public void Manager_Save_ShouldWriteProfileToSecurityFile()
        {
            var manager = new Manager("admin");
            var profile = new Profile { Name = "Admin" };
            manager.Profiles.Add(profile);
            var fileName = "manager_save_profile";

            if (File.Exists(fileName + ".security")) File.Delete(fileName + ".security");

            manager.Save(fileName);
            var content = File.ReadAllText(fileName + ".security");

            Assert.Contains("PROFILE:Admin", content);
        }

        [Fact]
        public void Manager_Save_ShouldWriteUserToSecurityFile()
        {
            var manager = new Manager("admin");
            var profile = new Profile { Name = "Admin" };
            profile.Users.Add(new User { Username = "admin", EncryptedPassword = "hashedPw" });
            manager.Profiles.Add(profile);
            var fileName = "manager_save_user";

            if (File.Exists(fileName + ".security")) File.Delete(fileName + ".security");

            manager.Save(fileName);
            var content = File.ReadAllText(fileName + ".security");

            Assert.Contains("USER:admin:hashedPw", content);
        }

        [Fact]
        public void Manager_Save_ShouldWritePrivilegesToSecurityFile()
        {
            var manager = new Manager("admin");
            var profile = new Profile { Name = "User" };
            profile.GrantPrivilege("Students", Privilege.Select);
            profile.GrantPrivilege("Students", Privilege.Insert);
            manager.Profiles.Add(profile);
            var fileName = "manager_save_privileges";

            if (File.Exists(fileName + ".security")) File.Delete(fileName + ".security");

            manager.Save(fileName);
            var content = File.ReadAllText(fileName + ".security");

            Assert.Contains("PRIVILEGE:Students:", content);
            Assert.Contains("Select", content);
            Assert.Contains("Insert", content);
        }

        [Fact]
        public void Manager_Save_ShouldSaveMultipleProfiles()
        {
            var manager = new Manager("admin");
            var profile1 = new Profile { Name = "Admin" };
            profile1.Users.Add(new User { Username = "admin", EncryptedPassword = "hash1" });
            var profile2 = new Profile { Name = "User" };
            profile2.Users.Add(new User { Username = "user1", EncryptedPassword = "hash2" });
            manager.Profiles.Add(profile1);
            manager.Profiles.Add(profile2);
            var fileName = "manager_save_multi";

            if (File.Exists(fileName + ".security")) File.Delete(fileName + ".security");

            manager.Save(fileName);
            var content = File.ReadAllText(fileName + ".security");

            Assert.Contains("PROFILE:Admin", content);
            Assert.Contains("PROFILE:User", content);
            Assert.Contains("USER:admin:hash1", content);
            Assert.Contains("USER:user1:hash2", content);
        }

        #endregion

        #region Load Tests

        [Fact]
        public void Manager_Load_ShouldReturnNull_WhenSecurityFileDoesNotExist()
        {
            var result = Manager.Load("nonexistent_db_file", "admin");
            Assert.Null(result);
        }

        [Fact]
        public void Manager_Load_ShouldReturnManagerWithGivenUsername()
        {
            var manager = new Manager("admin");
            var profile = new Profile { Name = Profile.AdminProfileName };
            profile.Users.Add(new User { Username = "admin", EncryptedPassword = Encryption.Encrypt("secret") });
            manager.Profiles.Add(profile);
            var fileName = "manager_load_username";

            if (File.Exists(fileName + ".security")) File.Delete(fileName + ".security");

            manager.Save(fileName);
            var loaded = Manager.Load(fileName, "admin");

            Assert.NotNull(loaded);
            Assert.True(loaded.IsPasswordCorrect("admin", "secret"));
        }

        [Fact]
        public void Manager_Load_ShouldLoadProfilesCorrectly()
        {
            var manager = new Manager("admin");
            var profile1 = new Profile { Name = "Admin" };
            var profile2 = new Profile { Name = "User" };
            manager.Profiles.Add(profile1);
            manager.Profiles.Add(profile2);
            var fileName = "manager_load_profiles";

            if (File.Exists(fileName + ".security")) File.Delete(fileName + ".security");

            manager.Save(fileName);
            var loaded = Manager.Load(fileName, "admin");

            Assert.NotNull(loaded);
            Assert.Equal(2, loaded.Profiles.Count);
            Assert.NotNull(loaded.ProfileByName("Admin"));
            Assert.NotNull(loaded.ProfileByName("User"));
        }

        [Fact]
        public void Manager_Load_ShouldLoadUsersCorrectly()
        {
            var manager = new Manager("admin");
            var profile = new Profile { Name = Profile.AdminProfileName };
            profile.Users.Add(new User { Username = "admin", EncryptedPassword = Encryption.Encrypt("pass123") });
            manager.Profiles.Add(profile);
            var fileName = "manager_load_users";

            if (File.Exists(fileName + ".security")) File.Delete(fileName + ".security");

            manager.Save(fileName);
            var loaded = Manager.Load(fileName, "admin");

            Assert.NotNull(loaded);
            var user = loaded.UserByName("admin");
            Assert.NotNull(user);
            Assert.Equal("admin", user.Username);
            Assert.True(loaded.IsPasswordCorrect("admin", "pass123"));
        }

        [Fact]
        public void Manager_Load_ShouldLoadPrivilegesCorrectly()
        {
            var manager = new Manager("admin");
            var profile = new Profile { Name = "User" };
            profile.Users.Add(new User { Username = "user1", EncryptedPassword = "hash" });
            profile.GrantPrivilege("Students", Privilege.Select);
            profile.GrantPrivilege("Students", Privilege.Insert);
            profile.GrantPrivilege("Teachers", Privilege.Delete);
            manager.Profiles.Add(profile);
            var fileName = "manager_load_privileges";

            if (File.Exists(fileName + ".security")) File.Delete(fileName + ".security");

            manager.Save(fileName);
            var loaded = Manager.Load(fileName, "admin");

            Assert.NotNull(loaded);
            Assert.True(loaded.IsGrantedPrivilege("user1", "Students", Privilege.Select));
            Assert.True(loaded.IsGrantedPrivilege("user1", "Students", Privilege.Insert));
            Assert.True(loaded.IsGrantedPrivilege("user1", "Teachers", Privilege.Delete));
            Assert.False(loaded.IsGrantedPrivilege("user1", "Students", Privilege.Delete));
        }

        [Fact]
        public void Manager_Load_ShouldPreserveIsUserAdmin_AfterRoundTrip()
        {
            var manager = new Manager("admin");
            var adminProfile = new Profile { Name = Profile.AdminProfileName };
            adminProfile.Users.Add(new User { Username = "admin", EncryptedPassword = Encryption.Encrypt("pw") });
            manager.Profiles.Add(adminProfile);
            var fileName = "manager_load_isadmin";

            if (File.Exists(fileName + ".security")) File.Delete(fileName + ".security");

            manager.Save(fileName);
            var loaded = Manager.Load(fileName, "admin");

            Assert.NotNull(loaded);
            Assert.True(loaded.IsUserAdmin());
        }

        #endregion

        #region RemoveProfile Tests

        [Fact]
        public void Manager_RemoveProfile_ShouldRemoveProfile_WhenProfileExists()
        {
            var manager = new Manager("adminUser");
            var profile1 = new Profile { Name = "Admin" };
            var profile2 = new Profile {Name = "User"};
            profile1.Users.Add(new User("adminUser", "password"));
            manager.Profiles.Add(profile1);
            manager.Profiles.Add(profile2);

            var result = manager.RemoveProfile("User");
            Assert.True(result);
            Assert.Single(manager.Profiles);
            Assert.Equal("Admin", manager.Profiles[0].Name);
        }

        [Fact]
        public void Manager_RemoveProfile_ShouldReturnFalse_WhenProfileDoesNotExist()
        {
            var manager = new Manager("adminUser");

            var result = manager.RemoveProfile("User");
            Assert.False(result);
            Assert.Empty(manager.Profiles);
        }

        [Fact]
        public void Manager_RemoveProfile_ShouldRemoveOnlySelectedProfile_WhenSeveralProfilesExist()
        {
            var manager = new Manager("adminUser");
            var profile1 = new Profile { Name = "Admin" };

            var profile2 = new Profile {Name = "User"};
            profile1.Users.Add(new User("adminUser", "password"));
            manager.Profiles.Add(profile1);
            manager.Profiles.Add(profile2);

            var result = manager.RemoveProfile("User");
            Assert.True(result);
            Assert.Single(manager.Profiles);
            Assert.Equal("Admin", manager.Profiles[0].Name);
        }

        [Fact]
        public void Manager_RemoveProfile_ShouldReturnFalse_WhenProfileNameIsNull()
        {
            var manager = new Manager("adminUser");
            var profile = new Profile {Name = "User"};
            manager.Profiles.Add(profile);

            var result = manager.RemoveProfile(null);
            Assert.False(result);
            Assert.Equal("User", manager.Profiles[0].Name);
        }

        #endregion

    }
}
