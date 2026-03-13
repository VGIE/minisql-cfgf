using System.Security.Cryptography.X509Certificates;
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


        #region Parse Tests

        [Fact]
        public void ColumnDefinition_Parse_ReturnsStringTypeCorrectly()
        {
            string text = "Name->String";
            ColumnDefinition result = ColumnDefinition.Parse(text);

            Assert.NotNull(result);
            Assert.Equal("Name", result.Name);
            Assert.Equal(ColumnDefinition.DataType.String, result.Type);
        }

        [Fact]
        public void ColumnDefinition_Parse_ReturnsIntTypeCorrectly()
        {
            string text = "Age->Int";
            ColumnDefinition result = ColumnDefinition.Parse(text);

            Assert.NotNull(result);
            Assert.Equal("Age", result.Name);
            Assert.Equal(ColumnDefinition.DataType.Int, result.Type);
        }

        [Fact]
        public void ColumnDefinition_Parse_ReturnsDoubleTypeCorrectly()
        {
            string text = "Height->Double";
            ColumnDefinition result = ColumnDefinition.Parse(text);

            Assert.NotNull(result);
            Assert.Equal("Height", result.Name);
            Assert.Equal(ColumnDefinition.DataType.Double, result.Type);
        }

        [Fact]
        public void ColumnDefinition_Parse_ReturnsDelimiterCorrectly()
        {
            string text = "Price[ARROW]USD->String";
            ColumnDefinition result = ColumnDefinition.Parse(text);

            Assert.NotNull(result);
            Assert.Equal("Price->USD", result.Name);
            Assert.Equal(ColumnDefinition.DataType.String, result.Type);
        }

        [Fact]
        public void ColumnDefinition_Parse_WhenAsTextThenParse()
        {
            var column = new ColumnDefinition(ColumnDefinition.DataType.Double, "Price->USD");
            string text = column.AsText();
            ColumnDefinition result = ColumnDefinition.Parse(text);

            Assert.NotNull(result);
            Assert.Equal(column.Name, result.Name);
            Assert.Equal(column.Type, result.Type);
        }

        [Fact]
        public void ColumnDefinition_Parse_WhenValueIsNull()
        {
            ColumnDefinition result = ColumnDefinition.Parse(null);

            Assert.NotNull(result);
            Assert.Null(result.Name);
        }

        [Fact]
        public void ColumnDefinition_Parse_WhenFormatIsInvalid()
        {
            string text = "InvalidFormat";
            ColumnDefinition result = ColumnDefinition.Parse(text);

            Assert.NotNull(result);
            Assert.Null(result.Name);
        }

        [Fact]
        public void ColumnDefinition_Parse_WhenTypeIsInvalid()
        {
            string text = "Name->Boolean";
            ColumnDefinition result = ColumnDefinition.Parse(text);

            Assert.NotNull(result);
            Assert.Null(result.Name);
        }

        #endregion
    }

}
