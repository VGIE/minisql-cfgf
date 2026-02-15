namespace OurTests
{
    public class UnitTest1
    {
    #region AddTable Tests
    [Fact]
    public void Database_AddTable_ShouldAddTableToDatabaseCorrectly()
    {
      //Assert
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);
      var database = DbManager.Database.CreateTestDatabase();

      //Act
      database.AddTable(table);

      //Assert
      Assert.Equal(table, database.TableByName(tableName));
    }
    #endregion
  }
}