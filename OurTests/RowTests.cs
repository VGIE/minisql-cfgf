using System.Data.Common;
using DbManager;

namespace OurTests
{
  public class RowTests
  {
    #region Row Constructor Tests
    [Fact]
    public void Row_Constructor_ShouldSaveValuesCorrectly() 
    {
      //Arrange
      var columns = new List<ColumnDefinition>
      {
        new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
        new ColumnDefinition(ColumnDefinition.DataType.Int, "Age")
      };
      var values = new List<String> { "Ane", "21" };

      //Act
      var row = new Row(columns, values);

      //Assert
      Assert.Equal(values, row.Values);
    }
    [Fact]
    public void Row_Constructor_ShouldNotInitializeIfColumnValueCountMismatch() 
    {
      //Arrange
      var columns = new List<ColumnDefinition>
      {
        new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
        new ColumnDefinition(ColumnDefinition.DataType.Int, "Age")
      };
      var values = new List<String> { "Ane" };

      //Act
      var row = new Row(columns, values);

      //Assert
      Assert.Null(row.Values);
    }
    [Fact]
    public void Row_Constructor_ShouldNotInitializeIfColumnsIsEmpty() 
    {
      //Arrange
      var columns = new List<ColumnDefinition> {};
      var values = new List<String> {};

      //Act
      var row = new Row(columns, values);

      //Assert
      Assert.Null(row.Values);
    }

    #endregion
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
    Assert.Equal("1.65", row.Values[2]);
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

    [Fact]
    public void Row_GetValue_ReturnCorrectvalue()
    {
    var columns = new List<ColumnDefinition>
    {
    new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
    new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
    new ColumnDefinition(ColumnDefinition.DataType.Double, "Height")
    };

    var values = new List<String> { "Ane", "21", "1.65"};
    var row = new Row(columns, values);

    var result = row.GetValue("Age");

    Assert.Equal("21", result);
    }
    [Fact]
    public void Row_GetValue_ColumnNotExistReturnNull()
    {
    var columns = new List<ColumnDefinition>
    {
    new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
    new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
    };

    var values = new List<String> {"Ane", "21"};
    var row = new Row(columns, values);

    var result = row.GetValue("Height");

    Assert.Null(result);
    }
  }
}