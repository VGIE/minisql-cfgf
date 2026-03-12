using DbManager;
namespace OurTests
{
  public class ColumnDefinitionsTests
  {
    //[Theory]
    //[InlineData(ColumnDefinition.DataType.String, "Pepe")] 
    //[InlineData(ColumnDefinition.DataType.Int, "2")]
    //[InlineData(ColumnDefinition.DataType.Double, "2.0")]
    //[InlineData(ColumnDefinition.DataType.String, "")]
    //public void ColumnDefinition_Constructor_SetsPropertiesCorrectly(ColumnDefinition.DataType type, string name)
    //{
    //  // Arrange & Act
    //  var columnDefinition = new ColumnDefinition(type, name);
    //  // Assert
    //  Assert.Equal(type, columnDefinition.Type);
    //  Assert.Equal(name, columnDefinition.Name);
    //}

    //[Theory]
    //[InlineData(ColumnDefinition.DataType.String, "Pepe", "Pepe->String")]
    //[InlineData(ColumnDefinition.DataType.Int, "2", "2->Int")]
    //[InlineData(ColumnDefinition.DataType.Double, "2.0", "2.0->Double")]
    //[InlineData(ColumnDefinition.DataType.Double, "", "->Double")]
    //public void ColumnDefinition_AsText_WorkCorrectly(ColumnDefinition.DataType type, string name, string expectedReturn)
    //{
    //  // Arrange
    //  var columnDefinition = new ColumnDefinition(type, name);
    //  //Act
    //  var result = columnDefinition.AsText();
    //  //Assert
    //  Assert.Equal(expectedReturn, result);
    //}
        [Fact]
        public void ColumnDefinition_Constructor_ShoulHaveTypeCorrecto() 
        {
            ColumnDefinition column = new ColumnDefinition(ColumnDefinition.DataType.Int, "Age");
            Assert.Equal(ColumnDefinition.DataType.Int, column.Type);
        }
        [Fact]
        public void ColumnDefinition_Constructor_ShouldHaveNameCorrecto() 
        {
            ColumnDefinition column = new ColumnDefinition(ColumnDefinition.DataType.String, "Name");
            Assert.Equal("Name", column.Name);
        }
        [Fact]
        public void ColumnDefinition_Constructor_ShouldNotHaveNameVacio()
        {
            var column = new ColumnDefinition(ColumnDefinition.DataType.String, "");
            Assert.Null(column.Name);
        }
        [Fact]
        public void ColumnDefinition_Constructor_NameShoulNotBeNull()
        {
            var column = new ColumnDefinition(ColumnDefinition.DataType.String, null);
            Assert.Null(column.Name);
        }


        #region AsText Tests
        [Fact]
        public void ColumnDefinition_AsText_ReturnsCorrectString()
        {
            var column = new ColumnDefinition(ColumnDefinition.DataType.String, "Name");
            var result = column.AsText();
            Assert.Equal("Name->String", result);
        }

        [Fact]
        public void ColumnDefinition_AsText_ReturnsCorrectInt()
        {
            var column = new ColumnDefinition(ColumnDefinition.DataType.Int, "Age");
            var result = column.AsText();
            Assert.Equal("Age->Int", result);
        }

        [Fact]
        public void ColumnDefinition_AsText_ReturnsCorrectDouble()
        {
            var column = new ColumnDefinition(ColumnDefinition.DataType.Double, "Height");
            var result = column.AsText();
            Assert.Equal("Height->Double", result);
        }

        [Fact]
        public void ColumnDefinition_AsText_ReturnsNullWhenNameIsNull()
        {
            var column = new ColumnDefinition(ColumnDefinition.DataType.String, null);
            var result = column.AsText();
            Assert.Null(result);
        }

        [Fact]
        public void ColumnDefinition_AsText_EncodesDelimiterCorrectly()
        {
            var column = new ColumnDefinition(ColumnDefinition.DataType.String, "Price->USD");
            var result = column.AsText();
            Assert.Equal("Price[ARROW]USD->String", result);
        }

        #endregion
    }

}
