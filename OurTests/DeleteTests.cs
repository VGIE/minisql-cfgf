using DbManager;
using DbManager.Parser;

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


        #region execute tests
        [Fact]
        public void Delete_Execute_ShouldWorkCorrectly()
        {
            var database = Database.CreateTestDatabase();
            var condition = new Condition("Age", "=", Table.TestColumn3Row1);
            var delete = new Delete(Table.TestTableName, condition);
            var result = delete.Execute(database);

            Assert.Equal(Constants.DeleteSuccess, result);
            Assert.Equal(2, database.TableByName(Table.TestTableName).NumRows());
        }

        [Fact]
        public void Delete_Execute_TableDoesntExist_ShouldReturnError()
        {
            var database = Database.CreateTestDatabase();
            var condition = new Condition("Age", "=", Table.TestColumn3Row1);
            var delete = new Delete("NoExiste", condition);
            var result = delete.Execute(database);

            Assert.Equal(Constants.TableDoesNotExistError, result);
        }

        [Fact]
        public void Delete_Execute_ConditionColumnDoesntExist_ShouldReturnError()
        {
            var database = Database.CreateTestDatabase();
            var condition = new Condition("NoColumn", "=", "25");
            var delete = new Delete(Table.TestTableName, condition);
            var result = delete.Execute(database);

            Assert.Equal(Constants.ColumnDoesNotExistError, result);
        }

        #endregion

    }
}
