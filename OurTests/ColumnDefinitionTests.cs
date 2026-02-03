using DbManager;
namespace OurTests
{
  public class ColumnDefinitionsTests
  {
		[Theory]
		[InlineData(ColumnDefinition.DataType.String, "Pepe")]
		[InlineData(ColumnDefinition.DataType.Int, "2")]
		[InlineData(ColumnDefinition.DataType.Double, "2.0")]
		[InlineData(ColumnDefinition.DataType.String, "")]
		public void ColumnDefinition_Constructor_SetsPropertiesCorrectly(ColumnDefinition.DataType type, string name)
		{
			// Arrange & Act
			var columnDefinition = new ColumnDefinition(type, name);
			// Assert
			Assert.Equal(type, columnDefinition.Type);
			Assert.Equal(name, columnDefinition.Name);
		}

		[Theory]
		[InlineData(ColumnDefinition.DataType.String, "Pepe", "Pepe->String")]
		[InlineData(ColumnDefinition.DataType.Int, "2", "2->Int")]
		[InlineData(ColumnDefinition.DataType.Double, "2.0", "2.0->Double")]
		[InlineData(ColumnDefinition.DataType.Double, "", "->Double")]
		public void ColumnDefinition_AsText_WorkCorrectly(ColumnDefinition.DataType type, string name, string expectedReturn)
		{
			// Arrange
			var columnDefinition = new ColumnDefinition(type, name);
			//Act
			var result = columnDefinition.AsText();
			//Assert
			Assert.Equal(expectedReturn, result);
		}
		[Fact]
        public void ColumnDefinition_Constructor_TypeCorrecto() 
        {
            ColumnDefinition column = new ColumnDefinition(ColumnDefinition.DataType.Int, "Age");
            Assert.Equal(ColumnDefinition.DataType.Int, column.Type);
        }

        [Fact]
        public void ColumnDefinition_Constructor_NameCorrecto() 
        {
            ColumnDefinition column = new ColumnDefinition(ColumnDefinition.DataType.String, "Name");
            Assert.Equal("Name", column.Name);
        }
     }
}