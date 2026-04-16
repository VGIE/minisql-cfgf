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
    }
}
