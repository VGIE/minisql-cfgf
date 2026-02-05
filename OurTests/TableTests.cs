using DbManager;

namespace OurTests
{
    public class TableTests
    {
        //TODO DEADLINE 1A : Create your own tests for Table
        /*
        [Fact]
        public void Test1()
        {

        }
        */

        [Fact]
        public void Table_Constructor_GuardaNombreCorrecto() 
        {
            var columns = new List<ColumnDefinition>();
            var table = new Table("Personas", columns);
            Assert.Equal("Personas", table.Name);
        }
        [Fact]
        public void Table_NumRows_ReturnsCorrectNum()
        {
            var table = new Table("Test", new List<ColumnDefinition>());
            Assert.Equal(0, table.NumRows());

            var columns = new List<ColumnDefinition> {new ColumnDefinition(ColumnDefinition.DataType.String, "Name")};
            var table2 = new Table("Test2", columns);

            var row = new Row(columns, new List<string> {"Mario"});
            table2.AddRow(row);

            Assert.Equal(1,table2.NumRows());
        }
        [Fact]
        public void Table_AddRow_IncreasesCounter()
        {
            var columns = new List<ColumnDefinition>();
            var table = new Table("TestTable", columns);
            var row = new Row(columns, new List<string> { "Prueba" });

            table.AddRow(row);
            Assert.Equal(1,table.NumRows());
        }
        [Fact]
        public void Table_GetRow_ReturnsCorrectRow()
        {
            var columns = new List<ColumnDefinition> {
            new ColumnDefinition(ColumnDefinition.DataType.String,"Nombre")
            };

            Table table = new Table("TestTable",columns);
            var row1 = new Row(columns, new List<string> {"Mario"});
            table.AddRow(row1);

            Row result = table.GetRow(0);
            Assert.NotNull(result);
            Assert.Equal("Mario",result.Values[0]);
            Assert.Equal(row1, result);

            bool error = false;
            try{table.GetRow(1);}
            catch (ArgumentException){error = true;}
            Assert.True(error); 

            error = false;
            try{table.GetRow(-1);}
            catch (ArgumentException){error = true;}
            Assert.True(error);
        }
        [Fact]
        public void Table_NumColumns_ReturnsCorrectNum()
        {
            var table = new Table("Test", new List<ColumnDefinition>());
            Assert.Equal(0, table.NumColumns());

            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
            new ColumnDefinition(ColumnDefinition.DataType.Int, "Age")
            };


            var table2 = new Table("Test2", columns);
            Assert.Equal(2,table2.NumColumns());
        }
    }
}