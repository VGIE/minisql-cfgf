using DbManager.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurTests
{
    public class SetValueTests
    {
        //TODO DEADLINE 1A : Create your own tests for Condition
        [Fact]
        public void SetValue_Constructor_ShouldSaveValuesCorrectly()
        {
            var s = new SetValue("Age", "21");
            Assert.Equal("Age", s.ColumnName);
            Assert.Equal("21", s.Value);
        }

        [Fact]
        public void SetValue_Constructor_ShouldNotInitializeIfColumnIsNull()
        {
            var s = new SetValue(null, "21");
            Assert.Null(s.ColumnName);
            Assert.Null(s.Value);
        }

        [Fact]
        public void SetValue_Constructor_ShouldNotInitializeIfColumnIsEmpty()
        {
            var s = new SetValue("", "21");
            Assert.Null(s.ColumnName);
            Assert.Null(s.Value);
        }

        [Fact]
        public void SetValue_Constructor_ShouldNotInitializeIfColumnIsWhitespace()
        {
            var s = new SetValue(" ", "21");
            Assert.Null(s.ColumnName);
            Assert.Null(s.Value);
        }

        [Fact]
        public void SetValue_Constructor_AllowsNullValue()
        {
            var s = new SetValue("Age", null);
            Assert.Equal("Age", s.ColumnName);
            Assert.Null(s.Value);
        }
    }
}
