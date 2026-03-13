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

        #region decode tests
        [Fact]
        public void ColumnDefinition_AsText_ShouldNotChangeName_WhenNameHasNoDelimiter() 
        {
            var column = new ColumnDefinition(ColumnDefinition.DataType.Int, "Age");

            string text = column.AsText();

            Assert.Equal("Age -> Int", text);
        }

        [Fact]
        public void ColumnDefinition_AsText_ShouldEncodeIt_WhenNameHasDelimiter()
        {
            var column = new ColumnDefinition(ColumnDefinition.DataType.String, "A->B");

            string text = column.AsText();

            Assert.Equal("A[ARROW]B->String", text);
        }

        [Fact]
        public void ColumnDefinition_AsText_ShouldEncodeAll_WhenNameContainsMultipleDelimiters()
        {
			var column = new ColumnDefinition(ColumnDefinition.DataType.Double, "A->B->C");

			string text = column.AsText();

			Assert.Equal("A[ARROW]B[ARROW]C->Double", text);
		}
        #endregion

    }

}
