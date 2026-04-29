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
  }
}
