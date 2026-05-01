using DbManager;
using DbManager.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurTests
{
  public class RevokeTests
  {
    #region Constructor Tests
    [Fact]
    public void Revoke_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var privilegeName = "pepito";
      var tableName = "TestTable";
      var profileName = "juan";

      //Act
      var revoke = new Revoke(privilegeName, tableName, profileName);

      //Assert
      Assert.Equal(privilegeName, revoke.PrivilegeName);
      Assert.Equal(tableName, revoke.TableName);
      Assert.Equal(profileName, revoke.ProfileName);

    }
        #endregion

    #region execute test
    [Fact]
    public void Revoke_Execute_ShouldWork_WhenUserIsAdmin()
    {
        var database = Database.CreateTestDatabase();
        var profile = new Profile { Name = "Profile" };
        profile.GrantPrivilege("Table", Privilege.Select);
        database.SecurityManager.Profiles.Add(profile);
        var revoke = new Revoke("SELECT", "Table", "Profile");
        var result = revoke.Execute(database);

        Assert.Equal(Constants.RevokePrivilegeSuccess, result);
        Assert.False(profile.IsGrantedPrivilege("Table", Privilege.Select));
    }

    [Fact]
    public void Revoke_Execute_ShouldReturnError_IfProfileDoesNotExist()
    {
        var database = Database.CreateTestDatabase();
        var profile = new Profile { Name = "Profile" };
        database.SecurityManager.Profiles.Add(profile);
        database.SecurityManager.Profiles.Add(profile);
        var revoke = new Revoke("SELECT", "Table", "Profile1");
        var result = revoke.Execute(database);

        Assert.Equal(Constants.SecurityProfileDoesNotExistError, result);
    }

    [Fact]
    public void Revoke_Execute_ShouldReturnError_WhenUserIsNotAdmin()
    {
        var database = new Database("Mario", "1234");
        database.SecurityManager.RemoveProfile(Profile.AdminProfileName);
        var profile = new Profile { Name = "Profile" };
        profile.Users.Add(new User { Username = "Mario" });
        database.SecurityManager.Profiles.Add(profile);
        var revoke = new Revoke("SELECT", "Table", "Profile");
        var result = revoke.Execute(database);

        Assert.Equal(Constants.UsersProfileIsNotGrantedRequiredPrivilege, result);
    }

    [Fact]
    public void Revoke_Execute_ShouldReturnError_IfPrivilegeDoesNotExist()
    {
        var database = Database.CreateTestDatabase();
        var profile = new Profile { Name = "Profile" };
        database.SecurityManager.Profiles.Add(profile);
        var revoke = new Revoke("kajdkjdflk", "Table", "Profile");
        var result = revoke.Execute(database);

        Assert.Equal(Constants.PrivilegeDoesNotExistError, result);
    }

        #endregion
    }
}
