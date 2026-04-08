using DbManager;
using DbManager.Parser;

namespace OurTests
{
    public class UpdateTests
    {
        #region constructor tests
        [Fact]
        public void Update_Constructor_ShouldInitializeAttributesCorrectly()
        {
            var table = "TestTable";
            var setValues = new List<SetValue> { new SetValue("Age", "30") };
            var condition = new Condition("Age", ">", "18");
            var update = new DbManager.Update(table, setValues, condition);

            Assert.Equal(table, update.Table);
            Assert.Equal(setValues, update.Columns);
            Assert.Equal(condition, update.Where);
        }

        [Fact]
        public void Update_Constructor_NoCondition_WhereShouldBeNull()
        {
            var table = "TestTable";
            var setValues = new List<SetValue> { new SetValue("Name", "Pedro") };
            var update = new DbManager.Update(table, setValues, null);

            Assert.Equal(table, update.Table);
            Assert.Equal(setValues, update.Columns);
            Assert.Null(update.Where);
        }
        #endregion
    }
}
