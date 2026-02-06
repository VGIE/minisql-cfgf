using DbManager;

namespace OurTests
{
    public class ConditionTests
    {
        //TODO DEADLINE 1A : Create your own tests for Condition
        [Fact]
        public void Condition_Constructor_ShouldSaveValuesCorrectly()
        {
            var c = new Condition("Age", ">=", "18");

            Assert.Equal("Age", c.ColumnName);
            Assert.Equal(">=", c.Operator);
            Assert.Equal("18", c.LiteralValue);
        }

        [Fact]
        public void Condition_Constructor_ShouldNotInitializeIfColumnIsNullOrEmpty()
        {
            var c1 = new Condition(null, "=", "1");
            Assert.Null(c1.ColumnName);
            Assert.Null(c1.Operator);
            Assert.Null(c1.LiteralValue);

            var c2 = new Condition("", "=", "1");
            Assert.Null(c2.ColumnName);
            Assert.Null(c2.Operator);
            Assert.Null(c2.LiteralValue);
        }

        [Fact]
        public void Condition_Constructor_ShouldNotInitializeIfOperatorIsNullOrEmpty()
        {
            var c1 = new Condition("Age", null, "1");
            Assert.Null(c1.ColumnName);
            Assert.Null(c1.Operator);
            Assert.Null(c1.LiteralValue);

            var c2 = new Condition("Age", "", "1");
            Assert.Null(c2.ColumnName);
            Assert.Null(c2.Operator);
            Assert.Null(c2.LiteralValue);
        }
    }
}