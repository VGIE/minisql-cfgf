using DbManager;
using DbManager.Security;

namespace OurTests
{
	public class ProfileTests
	{
		#region GrantPrivilege Tests
		[Fact]
		public void Profile_GrantPrivilege_shouldReturnTrue_WhenTableDoesNosExistYet()
		{
			var profile = new Profile { Name = "User" };

			var result = profile.GrantPrivilege("Students", Privilege.Select);

			Assert.True(result);
			Assert.True(profile.IsGrantedPrivilege("Students", Privilege.Select));
		}
		[Fact]
		public void Profile_GrantPrivilege_shouldReturnTrue_WhenWeAddDifferentPrivilegeForSameTable()
		{
			var profile = new Profile { Name = "User" };

			var result1 = profile.GrantPrivilege("Students", Privilege.Select);
			var result2 = profile.GrantPrivilege("Students", Privilege.Insert);


			Assert.True(result1);
			Assert.True(profile.IsGrantedPrivilege("Students", Privilege.Select));
			Assert.True(result2);
			Assert.True(profile.IsGrantedPrivilege("Students", Privilege.Insert));
		}
		[Fact]
		public void Profile_GrantPrivilege_shouldReturnFalse_WhenPrivilegeAlreadyExist()
		{
			var profile = new Profile { Name = "User" };

			profile.GrantPrivilege("Students", Privilege.Select);
			var result = profile.GrantPrivilege("Students", Privilege.Select);

			Assert.False(result);

		}
		[Fact]
		public void Profile_GrantPrivilege_shouldReturnFalse_WhenTableIsNull()
		{
			var profile = new Profile { Name = "User" };

			var result = profile.GrantPrivilege(null, Privilege.Select);

			Assert.False(result);
		}
		[Fact]
		public void Profile_GrantPrivilege_shouldReturnFalse_WhenTableIsEmpty()
		{
			var profile = new Profile { Name = "User" };

			var result = profile.GrantPrivilege("", Privilege.Select);

			Assert.False(result);

		}
		#endregion
	}
}
