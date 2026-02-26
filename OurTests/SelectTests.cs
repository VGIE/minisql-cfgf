using DbManager;

namespace OurTests
{
    public class SelectTests
    {
        #region constructor tests
        [Fact]
        public void Select_Constructor_ShouldInitializeAttributes()
        {
            var table = "TestTable";
            var columns = new List<String> { "Name", "Age" };
            var condition = new DbManager.Condition("Name", "=", "Mario");
            var select = new DbManager.Select(table, columns, condition);

            Assert.Equal(table, select.Table);
            Assert.Equal(columns, select.Columns);
            Assert.Equal(condition, select.Where);
        }
        [Fact]
        public void Select_Constructor_NoCondition_WhereShouldBeNull()
        {
            var table = "TestTable";
            var columns = new List<String> { "Name", "Age" };
            var select = new DbManager.Select(table, columns);

            Assert.Equal(table, select.Table);
            Assert.Equal(columns, select.Columns);
            Assert.Null(select.Where);
        }
        #endregion
    }
}
