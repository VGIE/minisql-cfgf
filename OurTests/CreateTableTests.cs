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

        #region execute tests
        [Fact]
        public void CreateTable_Execute_ShouldWorkCorrectly()
        {
            var tableName = "TestTable1";
            var columns = new List<ColumnDefinition>()
            {
                (new ColumnDefinition(ColumnDefinition.DataType.String, "Name")),
                (new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"))
            };
            var database = Database.CreateTestDatabase();
            var table = new Table(tableName, columns);
            var createTable = new DbManager.CreateTable(tableName, columns);
            var result = createTable.Execute(database);

            Assert.Equal(Constants.CreateTableSuccess, result);
            Assert.Equal(table.ToString(), database.TableByName(tableName).ToString());
        }
        [Fact]
        public void CreateTable_Execute_IfTableExists_ShouldReturnError() 
        {
            var tableName = "TestTable";
            var columns = new List<ColumnDefinition>()
            {
                (new ColumnDefinition(ColumnDefinition.DataType.String, "Name")),
                (new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"))
            };
            var database = Database.CreateTestDatabase();
            var createTable = new DbManager.CreateTable(tableName, columns);
            var result = createTable.Execute(database);//como database ya tiene test table debe devolver error

            Assert.Equal(Constants.TableAlreadyExistsError, result);
        }
        [Fact]
        public void CreateTable_Execute_IfDatabaseCreatedWithoutColumns_ShouldReturnError()
        {
            var tableName = "TestTable2";
            var columns = new List<ColumnDefinition>();
            var database = Database.CreateTestDatabase();
            var createTable = new DbManager.CreateTable(tableName, columns);
            var result = createTable.Execute(database);

            Assert.Equal(Constants.DatabaseCreatedWithoutColumnsError, result);
        }
        #endregion
    }
}
