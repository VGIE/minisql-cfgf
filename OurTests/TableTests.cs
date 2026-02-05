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
        }
    }
}