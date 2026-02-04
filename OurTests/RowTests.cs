using System.Data.Common;
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

        [Fact]
        public void Row_SetValue_ExistColumnAndChangeValue()
        {
            var columns = new List<ColumnDefinition>
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
				new ColumnDefinition(ColumnDefinition.DataType.Double, "Height")
			};

            var values = new List<String> { "Ane", "20", "1.65"};
            var row = new Row(columns, values);

            row.SetValue("Age", "21");

            Assert.Equal("21", row.Values[1]);
            Assert.Equal("Ane", row.Values[0]);
            Assert.Equal("1.62", row.Values[2]);
        }
        [Fact]
        public void Row_SetValue_NoExistColumnDoNothing()
        {
			var columns = new List<ColumnDefinition>
			{
				new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
				new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
			};

			var values = new List<String> { "Ane", "21"};
			var row = new Row(columns, values);

			row.SetValue("Height", "1.65");

			Assert.Equal("Ane", row.Values[0]);
			Assert.Equal("21", row.Values[1]);
		}
    }
}