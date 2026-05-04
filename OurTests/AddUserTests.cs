using DbManager;
using DbManager.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurTests
{
  public class AddUserTests
  {
    #region Constructor Tests
    [Fact]
    public void AddUser_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var username = "admin";
      var password = "admin";
      var profileName = "pepito";

      //Act
      var addUser = new AddUser(username, password, profileName);

      //Assert
      Assert.Equal(username, addUser.Username);
      Assert.Equal(password, addUser.Password);
      Assert.Equal(profileName, addUser.ProfileName);

    }
    #endregion

    #region Parser

    [Fact]
    public void Correct()
    {
      AddUser query = MiniSQLParser.Parse("ADD USER (user,password,profile)") as AddUser;
      Assert.Equal("user", query.Username);

      query = MiniSQLParser.Parse("ADD USER (User,Password,Profile)") as AddUser;
      Assert.Equal("User", query.Username);
    }

    [Fact]
    public void CorrectWithSpaces()
    {
      AddUser query = MiniSQLParser.Parse("ADD     USER      (user,password,profile)") as AddUser;
      Assert.Equal("user", query.Username);

      query = MiniSQLParser.Parse("ADD USER     (OtherUser,password,profile)") as AddUser;
      Assert.Equal("OtherUser", query.Username);
    }

    [Fact]
    public void IncorrectCapitalization()
    {
      AddUser query = MiniSQLParser.Parse("ADD USER (user,password,profile)") as AddUser;
      Assert.NotNull(query);

      query = MiniSQLParser.Parse("Add User (user,password,profile)") as AddUser;
      Assert.Null(query);

      query = MiniSQLParser.Parse("add user (user,password,profile)") as AddUser;
      Assert.Null(query);
    }

    [Fact]
    public void IncorrectUserWithForbiddenChars()
    {
      AddUser query = MiniSQLParser.Parse("ADD USER (user,password,profile)") as AddUser;
      Assert.NotNull(query);

      query = MiniSQLParser.Parse("ADD USER (user_1,password,profile)") as AddUser;
      Assert.Null(query);

      query = MiniSQLParser.Parse("ADD USER (user 1,password,profile)") as AddUser;
      Assert.Null(query);
    }

    [Fact]
    public void IncorrectWithoutProfile()
    {
      AddUser query = MiniSQLParser.Parse("ADD USER (user,password,profile)") as AddUser;
      Assert.NotNull(query);

      query = MiniSQLParser.Parse("ADD USER ()") as AddUser;
      Assert.Null(query);

      query = MiniSQLParser.Parse("ADD USER (,,)") as AddUser;
      Assert.Null(query);
    }

    #endregion

    #region execute test
    [Fact]
    public void AddUser_Execute_ShouldWork_WhenUserIsAdmin()
    {
        var database = Database.CreateTestDatabase();
        var profile = new Profile { Name = "Profile" };
        database.SecurityManager.Profiles.Add(profile);
        var addUser = new AddUser("Mario", "Password", "Profile");
        var result = addUser.Execute(database);

        Assert.Equal(Constants.AddUserSuccess, result);
        Assert.NotNull(database.SecurityManager.UserByName("Mario"));
    }
    [Fact]
    public void AddUser_Execute_ShouldReturnError_IfProfileDoesNotExist()
    {
        var database = Database.CreateTestDatabase();
        var profile = new Profile { Name = "Profile" };
        database.SecurityManager.Profiles.Add(profile);
        var addUser = new AddUser("Mario", "Password", "Profile1");
        var result = addUser.Execute(database);

        Assert.Equal(Constants.SecurityProfileDoesNotExistError, result);
    }
    [Fact]
    public void AddUser_Execute_ShouldReturnError_WhenUserIsNotAdmin()
    {
        var database = new Database("Mario", "1234");
        database.SecurityManager.RemoveProfile(Profile.AdminProfileName);
        var profile = new Profile { Name = "Profile" };
        profile.Users.Add(new User { Username = "Mario" });
        profile.Users.Add(new User { Username = "Jonathan" });
        database.SecurityManager.Profiles.Add(profile);
        var addUser = new AddUser("Mario", "1234", "Profile");
        var result = addUser.Execute(database);

        Assert.Equal(Constants.UsersProfileIsNotGrantedRequiredPrivilege, result);
      }
        #endregion
    }
}
