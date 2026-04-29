using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbManager;

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
  }
}
