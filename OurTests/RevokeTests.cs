using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbManager;

namespace OurTests
{
  public class RevokeTests
  {
    #region Constructor Tests
    [Fact]
    public void Revoke_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var privilegeName = "pepito";
      var tableName = "TestTable";
      var profileName = "juan";

      //Act
      var revoke = new Revoke(privilegeName, tableName, profileName);

      //Assert
      Assert.Equal(privilegeName, revoke.PrivilegeName);
      Assert.Equal(tableName, revoke.TableName);
      Assert.Equal(profileName, revoke.ProfileName);

    }
    #endregion
  }
}
