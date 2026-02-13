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




        #region IsTrue - Invalid inputs

        [Fact]
        public void Condition_IsTrue_ReturnsFalse_IfValueIsNull()
        {
            var c = new Condition("Age", ">", "10");
            Assert.False(c.IsTrue(null, ColumnDefinition.DataType.Int));
        }

        [Fact]
        public void Condition_IsTrue_ReturnsFalse_IfLiteralValueIsNull()
        {
            var c = new Condition("Age", ">", null);
            Assert.False(c.IsTrue("20", ColumnDefinition.DataType.Int));
        }

        [Fact]
        public void Condition_IsTrue_ReturnsFalse_IfConditionWasNotInitialized()
        {
            var c = new Condition("", ">", "10");

            Assert.False(c.IsTrue("20", ColumnDefinition.DataType.Int));
            Assert.False(c.IsTrue("ab", ColumnDefinition.DataType.String));
        }

        [Fact]
        public void Condition_IsTrue_ReturnsFalse_IfTypeIsUnsupported()
        {
            var c = new Condition("X", "=", "1");
            Assert.False(c.IsTrue("1", (ColumnDefinition.DataType)999));
        }

        [Fact]
        public void Condition_IsTrue_ReturnsFalse_IfOperatorIsUnknown()
        {
            var c = new Condition("Age", "LIKE", "10");
            Assert.False(c.IsTrue("10", ColumnDefinition.DataType.Int));
        }

        #endregion



        #region IsTrue - String comparisons

        [Fact]
        public void Condition_IsTrue_String_Lexicographic()
        {
            var l = new Condition("Name", "<", "cd");
            Assert.True(l.IsTrue("ab", ColumnDefinition.DataType.String));
            Assert.False(l.IsTrue("cg", ColumnDefinition.DataType.String));
            Assert.False(l.IsTrue("cd", ColumnDefinition.DataType.String));

            var le = new Condition("S", "<=", "b");
            Assert.True(le.IsTrue("b", ColumnDefinition.DataType.String));
            Assert.True(le.IsTrue("a", ColumnDefinition.DataType.String));
            Assert.False(le.IsTrue("c", ColumnDefinition.DataType.String));
        

            var h = new Condition("Name", ">", "cd");
            Assert.False(h.IsTrue("ab", ColumnDefinition.DataType.String));
            Assert.True(h.IsTrue("cg", ColumnDefinition.DataType.String));
            Assert.False(h.IsTrue("cd", ColumnDefinition.DataType.String));

            var he = new Condition("S", ">=", "b");
            Assert.True(he.IsTrue("b", ColumnDefinition.DataType.String));
            Assert.False(he.IsTrue("a", ColumnDefinition.DataType.String));
            Assert.True(he.IsTrue("c", ColumnDefinition.DataType.String));
        

            var eq = new Condition("S", "=", "hello");
            Assert.True(eq.IsTrue("hello", ColumnDefinition.DataType.String));
            Assert.False(eq.IsTrue("HELLO", ColumnDefinition.DataType.String)); 

            var ne = new Condition("S", "!=", "hello");
            Assert.True(ne.IsTrue("HELLO", ColumnDefinition.DataType.String));
            Assert.False(ne.IsTrue("hello", ColumnDefinition.DataType.String));
        }


        [Fact]
        public void Condition_IsTrue_String_NumericComparisons()
        {
            var h_nu = new Condition("S", ">", "10");
            Assert.True(h_nu.IsTrue("9", ColumnDefinition.DataType.String));
            Assert.True(h_nu.IsTrue("11", ColumnDefinition.DataType.String));
            Assert.False(h_nu.IsTrue("10", ColumnDefinition.DataType.String));
            Assert.False(h_nu.IsTrue("0", ColumnDefinition.DataType.String));

            var h_nue = new Condition("S", ">=", "10");
            Assert.True(h_nue.IsTrue("9", ColumnDefinition.DataType.String));
            Assert.True(h_nue.IsTrue("11", ColumnDefinition.DataType.String));
            Assert.True(h_nue.IsTrue("10", ColumnDefinition.DataType.String));


            var l_nu = new Condition("S", "<", "11");
            Assert.False(l_nu.IsTrue("9", ColumnDefinition.DataType.String));
            Assert.False(l_nu.IsTrue("11", ColumnDefinition.DataType.String));
            Assert.True(l_nu.IsTrue("10", ColumnDefinition.DataType.String));

            var l_nue = new Condition("S", "<=", "11");
            Assert.False(l_nue.IsTrue("9", ColumnDefinition.DataType.String));
            Assert.True(l_nue.IsTrue("11", ColumnDefinition.DataType.String));
            Assert.True(l_nue.IsTrue("10", ColumnDefinition.DataType.String));


            var e_nu = new Condition("S", "=", "10");
            Assert.False(e_nu.IsTrue("9", ColumnDefinition.DataType.String));
            Assert.True(e_nu.IsTrue("10", ColumnDefinition.DataType.String));

            var ne_nue = new Condition("S", "!=", "10");
            Assert.True(ne_nue.IsTrue("9", ColumnDefinition.DataType.String));
            Assert.False(ne_nue.IsTrue("10", ColumnDefinition.DataType.String));
        }

        #endregion



        #region IsTrue - Numeric comparisons 

        [Fact]
        public void Condition_IsTrue_Int()
        {
            var l = new Condition("Age", "<", "10");
            Assert.True(l.IsTrue("9", ColumnDefinition.DataType.Int));
            Assert.False(l.IsTrue("11", ColumnDefinition.DataType.Int));
            Assert.False(l.IsTrue("10", ColumnDefinition.DataType.Int));

            var le = new Condition("Age", "<=", "10");
            Assert.True(le.IsTrue("9", ColumnDefinition.DataType.Int));
            Assert.True(le.IsTrue("10", ColumnDefinition.DataType.Int));
            Assert.False(le.IsTrue("11", ColumnDefinition.DataType.Int));
        

            var h = new Condition("Age", ">", "10");
            Assert.False(h.IsTrue("9", ColumnDefinition.DataType.Int));
            Assert.False(h.IsTrue("10", ColumnDefinition.DataType.Int));
            Assert.True(h.IsTrue("18", ColumnDefinition.DataType.Int));

            var he = new Condition("Age", ">=", "10");
            Assert.False(he.IsTrue("9", ColumnDefinition.DataType.Int));
            Assert.True(he.IsTrue("10", ColumnDefinition.DataType.Int));
            Assert.True(he.IsTrue("11", ColumnDefinition.DataType.Int));
        

            var e = new Condition("Age", "=", "10");
            Assert.False(e.IsTrue("11", ColumnDefinition.DataType.Int));
            Assert.True(e.IsTrue("10", ColumnDefinition.DataType.Int));

            var ne = new Condition("Age", "!=", "10");
            Assert.False(ne.IsTrue("10", ColumnDefinition.DataType.Int));
            Assert.True(ne.IsTrue("11", ColumnDefinition.DataType.Int));
        }


        [Fact]
        public void Condition_IsTrue_Double()
        {
            var l = new Condition("Height", "<", "1.70");
            Assert.True(l.IsTrue("1.65", ColumnDefinition.DataType.Double));
            Assert.False(l.IsTrue("1.70", ColumnDefinition.DataType.Double));
            Assert.False(l.IsTrue("1.78", ColumnDefinition.DataType.Double));

            var le = new Condition("Age", "<=", "10.40");
            Assert.True(le.IsTrue("10.2", ColumnDefinition.DataType.Double));
            Assert.True(le.IsTrue("10.40", ColumnDefinition.DataType.Double));
            Assert.True(le.IsTrue("10.4", ColumnDefinition.DataType.Double));
            Assert.False(le.IsTrue("11.54", ColumnDefinition.DataType.Double));


            var g = new Condition("Age", ">", "1.7");
            Assert.True(g.IsTrue("25", ColumnDefinition.DataType.Double));
            Assert.False(g.IsTrue("1.70", ColumnDefinition.DataType.Double));
            Assert.False(g.IsTrue("1,69", ColumnDefinition.DataType.Double));

            var ge = new Condition("Age", ">=", "10.40");
            Assert.False(ge.IsTrue("10.20", ColumnDefinition.DataType.Double));
            Assert.True(ge.IsTrue("10.40", ColumnDefinition.DataType.Double));
            Assert.True(ge.IsTrue("11.54", ColumnDefinition.DataType.Double));


            var e = new Condition("Age", "=", "10.28");
            Assert.False(e.IsTrue("10", ColumnDefinition.DataType.Double));
            Assert.False(e.IsTrue("10.20", ColumnDefinition.DataType.Double));
            Assert.True(e.IsTrue("10.28", ColumnDefinition.DataType.Double));

            var ne = new Condition("Age", "!=", "10.28");
            Assert.False(ne.IsTrue("10.28", ColumnDefinition.DataType.Double));
            Assert.True(ne.IsTrue("10", ColumnDefinition.DataType.Double));
            Assert.True(ne.IsTrue("10.20", ColumnDefinition.DataType.Double));

        }


        [Fact]
        public void Condition_IsTrue_Numeric_ParseFailure()
        {
            var c = new Condition("Age", ">", "10");
            Assert.False(c.IsTrue("not-a-number", ColumnDefinition.DataType.Int));
            Assert.False(c.IsTrue("not-a-number", ColumnDefinition.DataType.Double));
        }

        #endregion
    }
}