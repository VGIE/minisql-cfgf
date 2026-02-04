using DbManager;

namespace OurTests
{
    public class RowTests
    {
        //TODO DEADLINE 1A : Create your own tests for Row
        /*
        [Fact]
        public void Test1()
        {

        }
        */
        [Fact]
        public void Row_Constructor_SaveValuesCorrectly() 
        {
            var columns = new List<ColumnDefinition>
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Age")
            };

            var values = new List<String> { "Ane", "21" };
            var row = new Row(columns, values);
            Assert.Equal(values, row.Values);
        }

        [Fact]
        public void Row_Constructor_SameListOfValues() 
        {
			var columns = new List<ColumnDefinition>
			{
				new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
			};
			var values = new List<String> { "Andrea"};
            var row = new Row(columns, values);
            Assert.Same(values, row.Values);
		}
        [Fact]
        public void Row_Constructor_ListOfValuesEmpty()
        {
			var columns = new List<ColumnDefinition>
			{
				new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
			};
			var values = new List<String> ();
			var row = new Row(columns, values);
            Assert.Empty(row.Values);
		}
    }
}