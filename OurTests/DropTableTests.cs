using DbManager;

namespace OurTests
{
    public class DropTableTests
    {
        #region constructor tests
        [Fact]
        public void DropTable_Constructor_ShouldInitilizeAttributes()
        {
            var table = "TestTable";
            var dropTable = new DbManager.DropTable(table);
            Assert.Equal(table, dropTable.Table);
        }
        #endregion

        #region execute tests
        [Fact]
        public void DropTable_Execcute_ShouldWorkCorrectly()
        {
            var table = "TestTable";
            var dropTable = new DbManager.DropTable(table);
            var database = Database.CreateTestDatabase();
            var result = dropTable.Execute(database);

            Assert.Equal(Constants.DropTableSuccess, result);
            Assert.Null(database.TableByName(table));//la tabla no debe exisitir en la base de datos si execute lo ha borrado
        }
        [Fact]
        public void DropTable_Execute_TableDoesntExist_ShouldReturnError()
        {
            var table = "NoExiste";
            var dropTable = new DbManager.DropTable(table);
            var database = Database.CreateTestDatabase();
            var result = dropTable.Execute(database);

            Assert.Equal(database.LastErrorMessage, result);
        }
        #endregion
    }
}
