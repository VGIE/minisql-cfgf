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
    }
}
