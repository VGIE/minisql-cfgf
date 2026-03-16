using DbManager;

namespace OurTests
{
  public class DeleteTests
  {
    #region Constructor Tests
    [Fact]
    public void Delete_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var condition = new DbManager.Condition("Name", "=", "Paco");

      //Act
      var delete = new DbManager.Parser.Delete(table, condition);

      //Assert
      Assert.Equal(table, delete.Table);
      Assert.Equal(condition, delete.Where);
    }
    #endregion
  }
}
