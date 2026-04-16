using DbManager;
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
    }
}
