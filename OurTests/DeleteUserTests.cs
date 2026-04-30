using DbManager;
using DbManager.Security;

namespace OurTests
{
    public class DeleteUserTests
    {
        #region Constructor Tests
        [Fact]
        public void DeleteUser_Constructor_ShouldInitializeAttributesCorrectly()
        {
            var username = "Juancillo";
            var deleteUser = new DeleteUser(username);

            Assert.Equal(username, deleteUser.Username);
        }
    #endregion

    #region Parser Tests
      [Fact]
      public void Correct()
      {
        DeleteUser query = MiniSQLParser.Parse("DELETE USER user") as DeleteUser;
        Assert.Equal("user", query.Username);

        query = MiniSQLParser.Parse("DELETE USER OtherUser") as DeleteUser;
        Assert.Equal("OtherUser", query.Username);
      }

      [Fact]
      public void CorrectWithSpaces()
      {
        DeleteUser query = MiniSQLParser.Parse("DELETE     USER      USER") as DeleteUser;
        Assert.Equal("USER", query.Username);

        query = MiniSQLParser.Parse("DELETE USER    OtherUser") as DeleteUser;
        Assert.Equal("OtherUser", query.Username);
      }

      [Fact]
      public void IncorrectCapitalization()
      {
        DeleteUser query = MiniSQLParser.Parse("Delete User User") as DeleteUser;
        Assert.Null(query);

        query = MiniSQLParser.Parse("delete user User") as DeleteUser;
        Assert.Null(query);

        query = MiniSQLParser.Parse("DELETE USER User") as DeleteUser;
        Assert.NotNull(query);
      }

      [Fact]
      public void IncorrectUserWithForbiddenChars()
      {
        DeleteUser query = MiniSQLParser.Parse("DELETE USER User_1") as DeleteUser;
        Assert.Null(query);

        query = MiniSQLParser.Parse("DELETE USER User 1") as DeleteUser;
        Assert.Null(query);

        query = MiniSQLParser.Parse("DELETE USER User") as DeleteUser;
        Assert.NotNull(query);
      }

      [Fact]
      public void IncorrectWithoutProfile()
      {
        DeleteUser query = MiniSQLParser.Parse("DELETE USER") as DeleteUser;
        Assert.Null(query);

        query = MiniSQLParser.Parse("DELETE USER ") as DeleteUser;
        Assert.Null(query);

        query = MiniSQLParser.Parse("DELETE USER User") as DeleteUser;
        Assert.NotNull(query);
      }
    #endregion
    #region Execute Tests
   
    [Fact]
    public void DeleteUser_Execute_ShouldWork_WhenUserIsAdmin()
    {
        var database = Database.CreateTestDatabase();
        var profile = new Profile { Name = "Mario" };
        profile.Users.Add(new User { Username = "Jonathan" });
        database.SecurityManager.Profiles.Add(profile);
        var deleteUser = new DeleteUser("Jonathan");
        var result = deleteUser.Execute(database);

        Assert.Equal(Constants.DeleteUserSuccess, result);
        Assert.Null(database.SecurityManager.UserByName("Jonathan"));
    }

    [Fact]
    public void DeleteUser_Execute_ShouldReturnError_IfUserDoesNotExist()
    {
        var database = Database.CreateTestDatabase();
        var deleteUser = new DeleteUser("desconocidoo");
        var result = deleteUser.Execute(database);

        Assert.Equal(Constants.UserDoesNotExistError, result);
    }

    [Fact]
    public void DeleteUser_Execute_ShouldReturnError_WhenUserIsNotAdmin()
    {
            var database = new Database("Mario", "1234");
            database.SecurityManager.RemoveProfile(Profile.AdminProfileName);//conseguimos que Mario deje de ser Admin
            var profile = new Profile { Name = "Profile" };//añadimos un perfil normal sin privileges
            profile.Users.Add(new User { Username = "Mario" });
            profile.Users.Add(new User { Username = "Jonathan" });
            database.SecurityManager.Profiles.Add(profile);
            var deleteUser = new DeleteUser("Jonathan");
            var result = deleteUser.Execute(database);

            Assert.Equal(Constants.UsersProfileIsNotGrantedRequiredPrivilege, result);
        }
    #endregion
  }
}
