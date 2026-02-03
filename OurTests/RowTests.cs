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
        private List<ColumnDefinition> columns;

        [SetUp]
        public void SetUp() 
        {
            columns = new List<ColumnDefinition>
            {
                new ColumnDefinition (ColumnDefinition.DataType.String, "Name"),
				new ColumnDefinition (ColumnDefinition.DataType.Int, "Age"),
				new ColumnDefinition (ColumnDefinition.DataType.Double, "Height")
			};
        }

        
    }
}