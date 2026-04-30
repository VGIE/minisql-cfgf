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

        #region Parser Tests
        [Fact]
        public void DropSecurityProfile_Parse_ShouldWorkCorrectly()
        {
            var dspquery = MiniSQLParser.Parse("DROP SECURITY PROFILE Profile");
            var dropSecurityProfile = (DropSecurityProfile)dspquery;

            Assert.Equal("Profile", dropSecurityProfile.ProfileName);
        }

        [Fact]
        public void DropSecurityProfile_Parse_ShouldReturnNull_WhenProfileHasNumbers()
        {
            var dspquery = MiniSQLParser.Parse("DROP SECURITY PROFILE Profile92");

            Assert.Null(dspquery);
        }

        [Fact]
        public void DropSecurityProfile_Parse_ShouldReturnNull_WhenProfileHasIncorrectSyntax()
        {
            var dspquery = MiniSQLParser.Parse("DROP SECURITY Profile");
            var dspquery1 = MiniSQLParser.Parse("DROP PROFILE Profile");
            var dspquery2 = MiniSQLParser.Parse("SECURITY PROFILE Profile");
            var dspquery3 = MiniSQLParser.Parse("drop security profile Profile");
            var dspquery4 = MiniSQLParser.Parse("Dop SECURITY PROFILE Profile");

            Assert.Null(dspquery);
            Assert.Null(dspquery1);
            Assert.Null(dspquery2);
            Assert.Null(dspquery3);
            Assert.Null(dspquery4);
        }

        [Fact]
        public void DropSecurityProfile_Parse_ShouldReturnNull_WhenProfileHasSymbols()
        {
            var dspquery = MiniSQLParser.Parse("DROP SECURITY PROFILE Prof_ile");
            var dspquery1 = MiniSQLParser.Parse("DROP SECURITY PROFILE Prof-ile");
            var dspquery2 = MiniSQLParser.Parse("DROP SECURITY PROFILE Prof/ile");
            var dspquery3 = MiniSQLParser.Parse("DROP SECURITY PROFILE Prof$ile");
            var dspquery4 = MiniSQLParser.Parse("DROP SECURITY PROFILE Prof%ile");

            Assert.Null(dspquery);
            Assert.Null(dspquery1);
            Assert.Null(dspquery2);
            Assert.Null(dspquery3);
            Assert.Null(dspquery4);
        }

        [Fact]
        public void DropSecurityProfile_Parse_ShouldParse_WithSpacesBetween()
        {
            var dspquery = MiniSQLParser.Parse("DROP             SECURITY            PROFILE           Profile");
            var dropSecurityProfile = (DropSecurityProfile)dspquery;

            Assert.Equal("Profile", dropSecurityProfile.ProfileName);
        }
        #endregion

        #region Execute Tests
        
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
        public void DropSecurityProfile_Execute_ShouldReturnError_IfProfileDoesNotExist()
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
            database.SecurityManager.RemoveProfile(Profile.AdminProfileName);//conseguimos que Mario deje de ser Admin
            var profile = new Profile { Name = "Jonathan" };
            database.SecurityManager.Profiles.Add(profile);
            var dropSecurityProfile = new DropSecurityProfile("Jonathan");
            var result = dropSecurityProfile.Execute(database);

            Assert.Equal(Constants.UsersProfileIsNotGrantedRequiredPrivilege, result);
        }
        #endregion
    }
}
