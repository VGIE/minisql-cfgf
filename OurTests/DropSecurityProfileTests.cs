using DbManager;
using DbManager.Security;
namespace OurTests
{
    public class DropSecurityProfileTests
    {
        #region Constructor Tests
        [Fact]
        public void DropSecurityProfile_Constructor_ShouldInitializeAttributesCorrectly()
        {
            var profileName = "Jonathan";
            var dropSecurityProfile = new DropSecurityProfile(profileName);

            Assert.Equal(profileName, dropSecurityProfile.ProfileName);
        }
        #endregion

        #region Execute Tests
        /*
        [Fact]
        public void DropSecurityProfile_Execute_ShouldWork_WhenUserIsAdmin()
        {
            var database = Database.CreateTestDatabase();
            var profile = new Profile { Name = "Jonathan" };
            database.SecurityManager.Profiles.Add(profile);
            var dropSecurityProfile = new DropSecurityProfile("Jonathan");
            var result = dropSecurityProfile.Execute(database);

            Assert.Equal(Constants.DropSecurityProfileSuccess,result);
            Assert.Null(database.SecurityManager.ProfileByName("Jonathan"));
        }

        [Fact]
        public void DropSecurityProfile_Execute_ShouldReturnError_IfUserDoesNotExist()
        {
            var database = Database.CreateTestDatabase();
            var dropSecurityProfile = new DropSecurityProfile("Jonathan");
            var result = dropSecurityProfile.Execute(database);

            Assert.Equal(Constants.SecurityProfileDoesNotExistError, result);
        }

        [Fact]
        public void DropSecurityProfile_Execute_ShouldReturnError_WhenUserIsNotAdmin()
        {
            var database = new Database("Mario", "1234");
            var profile = new Profile { Name = "Jonathan" };
            database.SecurityManager.Profiles.Add(profile);
            var dropSecurityProfile = new DropSecurityProfile("Jonathan");
            var result = dropSecurityProfile.Execute(database);

            Assert.Equal(Constants.UsersProfileIsNotGrantedRequiredPrivilege, result);
        }*/
        #endregion
    }
}
