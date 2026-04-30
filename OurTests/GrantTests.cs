using DbManager;
using DbManager.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurTests
{
    public class GrantTests
    {
        #region Constructor Tests
        [Fact]
        public void Grant_Constructor_ShouldInitializeAttributesCorrectly()
        {
            var privilegeName = "INSERT";
            var tableName = "TestTable";
            var profileName = "Mario";
            var grant = new Grant(privilegeName, tableName, profileName);


            Assert.Equal(privilegeName, grant.PrivilegeName);
            Assert.Equal(tableName, grant.TableName);
            Assert.Equal(profileName, grant.ProfileName);

        }
        #endregion

        #region Execute tests
        [Fact]
        public void Grant_Execute_ShouldWork_WhenUserIsAdmin()
        {
            var database = Database.CreateTestDatabase();
            var profile = new Profile { Name = "Profile" };
            database.SecurityManager.Profiles.Add(profile);
            var grant = new Grant("SELECT", "Empleados", "Profile");
            var result = grant.Execute(database);

            Assert.Equal(Constants.GrantPrivilegeSuccess, result);
        }
        [Fact]
        public void Grant_Execute_ShouldReturnError_IfProfileDoesNotExist()
        {
            var database = Database.CreateTestDatabase();
            var grant = new Grant("SELECT", "Empleados", "Profile");
            var result = grant.Execute(database);

            Assert.Equal(Constants.SecurityProfileDoesNotExistError, result);
        }
        [Fact]
        public void Grant_Execute_ShouldReturnError_WhenUserIsNotAdmin()
        {
            var database = new Database("Mario", "1234");
            database.SecurityManager.RemoveProfile(Profile.AdminProfileName);
            var profile = new Profile { Name = "Profile" };
            profile.Users.Add(new User { Username = "Mario" });
            profile.Users.Add(new User { Username = "Jonathan" });
            var grant = new Grant("SELECT", "Empleados", "Profile");
            var result = grant.Execute(database);

            Assert.Equal(Constants.UsersProfileIsNotGrantedRequiredPrivilege, result);
        }
        [Fact]
        public void Grant_Execute_ShouldReturnError_WhenPrivilegeDoesntExist()
        {
            var database = Database.CreateTestDatabase();
            var profile = new Profile { Name = "Profile" };
            database.SecurityManager.Profiles.Add(profile);
            var grant = new Grant("NO EXISTE", "Empleados", "Profile");
            var result = grant.Execute(database);

            Assert.Equal(Constants.PrivilegeDoesNotExistError, result);
        }
        [Fact]
        public void Grant_Execute_ShouldReturnError_WhenProfileHasPrivilege()
        {
            var database = Database.CreateTestDatabase();
            var profile = new Profile { Name = "Profile" };
            profile.GrantPrivilege("Empleados", Privilege.Update);
            database.SecurityManager.Profiles.Add(profile);
            var grant = new Grant("UPDATE", "Empleados", "Profile");
            var result = grant.Execute(database);

            Assert.Equal(Constants.ProfileAlreadyHasPrivilege, result);
        }
        #endregion
    }
}
