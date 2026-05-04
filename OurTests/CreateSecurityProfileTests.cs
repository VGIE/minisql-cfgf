using DbManager;
using DbManager.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurTests
{
    public class CreateSecurityProfileTests
    {
        #region Constructor Tests
        [Fact]
        public void CreateSecurityProfile_Constructor_ShouldInitializeAttributesCorrectly()
        {
            var profileName = "JuanJose";
            var createSecurityProfile = new CreateSecurityProfile(profileName);

            Assert.Equal(profileName, createSecurityProfile.ProfileName);
        }
        #endregion

        #region execute test
        [Fact]
        public void CreateSecurityProfile_Execute_ShouldWork_WhenUserIsAdmin()
        {
            var database = Database.CreateTestDatabase();
            var createSecurityProfile = new CreateSecurityProfile("Mario");
            var result = createSecurityProfile.Execute(database);

            Assert.Equal(Constants.CreateSecurityProfileSuccess, result);
            Assert.NotNull(database.SecurityManager.ProfileByName("Mario"));
        }

        [Fact]
        public void CreateSecurityProfile_Execute_ShouldReturnError_WhenUserIsNotAdmin()
        {
            var database = new Database("Mario", "1234");
            database.SecurityManager.RemoveProfile(Profile.AdminProfileName);
            var profile = new Profile { Name = "Profile" };
            profile.Users.Add(new User { Username = "Mario" });
            profile.Users.Add(new User { Username = "Jonathan" });
            database.SecurityManager.Profiles.Add(profile);
            var createSecurityProfile = new CreateSecurityProfile("Juanma");
            var result = createSecurityProfile.Execute(database);

            Assert.Equal(Constants.UsersProfileIsNotGrantedRequiredPrivilege, result);
        }
        #endregion
    }
}
