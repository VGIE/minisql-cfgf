using DbManager;

namespace OurTests
{
  public class CreateTableTests
  {
    #region Constructor Tests
    [Fact]
    public void CreateTable_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var columns = new List<ColumnDefinition>()
      {
        (new ColumnDefinition(ColumnDefinition.DataType.String, "Name")),
        (new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"))
      };

      //Act
      var createTable = new DbManager.CreateTable(table, columns);

      //Assert
      Assert.Equal(table, createTable.Table);
      Assert.Equal(columns.Count, createTable.ColumnsParameters.Count);
    }
    #endregion
  }
}
