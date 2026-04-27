using DbManager;

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

        /*#region Execute Tests

        [Fact]
        public void DeleteUser_Execute_ShouldWork_WhenUserIsAdmin()
        {
            var database = Database.CreateTestDatabase();
            var deleteUser = new DeleteUser("admin");
            var result = deleteUser.Execute(database);

            Assert.Equal(Constants.DeleteUserSuccess, result);
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
            var database = new Database("Juancillo", "1234");
            var deleteUser = new DeleteUser("Juancillo");
            var result = deleteUser.Execute(database);

            Assert.Equal(Constants.UsersProfileIsNotGrantedRequiredPrivilege, result);
        }
        #endregion*/
    }
}
