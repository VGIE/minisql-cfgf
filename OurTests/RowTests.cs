using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
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
    public void Row_SetValue_ShouldChangeValueWhenExistColumn()
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
	public void Row_SetValue_ShouldChangeValue_WhenNotEnoughValues()
	{
		var columns = new List<ColumnDefinition>
	    {
	        new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
	        new ColumnDefinition(ColumnDefinition.DataType.Int, "Age")
	    };

		var values = new List<String> { "Ane", null};
		var row = new Row(columns, values);

			row.SetValue("Age", "21");

			Assert.Equal("21", row.Values[1]);
			Assert.Equal("Ane", row.Values[0]);
		}

		[Fact]
    public void Row_SetValue_ShouldDoNothingWhenNoExistColumn()
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
    public void Row_SetValue_ShouldDoNothing_WhenColumnNameIsNull()
    {
		var columns = new List<ColumnDefinition>
	    {
	        new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
	    };

		var values = new List<String> { "Ane" };
		var row = new Row(columns, values);

		row.SetValue(null, "Ane");

		Assert.Equal("Ane", row.Values[0]);
	}
    [Fact]
	public void Row_SetValue_ShouldDoNothing_WhenColumnNameIsEmpty()
	{
		var columns = new List<ColumnDefinition>
	{
		new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
	};

		var values = new List<String> { "Ane" };
		var row = new Row(columns, values);

		row.SetValue("", "Ane");

		Assert.Equal("Ane", row.Values[0]);
	}

	[Fact]
	public void Row_SetValue_ShouldDoNothing_WhenColumnNameIsWithSpace()
	{
		var columns = new List<ColumnDefinition>
        {
	        new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
        };

		var values = new List<String> { "Ane" };
		var row = new Row(columns, values);

		row.SetValue(" ", "Ane");

		Assert.Equal("Ane", row.Values[0]);
	}

		[Fact]
    public void Row_GetValue_ShouldReturnCorrectvalue()
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
    public void Row_GetValue_SloulReturnNullWhenColumnNotExist()
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

		#region Row IsTrue Tests
		[Fact]
    public void Row_IsTrue_ShouldReturnTrue_WhenConditionIsSatisfied()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel", "30" };
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);

      DbManager.Condition condition = new DbManager.Condition("Age", "=", "30");

      //Act
      bool result = row.IsTrue(condition);

      //Assert
      Assert.True(result);
    }
    [Fact]
    public void Row_IsTrue_ShouldReturnFalse_WhenConditionIsNotSatisfied()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel", "30" };
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);
      DbManager.Condition condition = new DbManager.Condition("Age", ">", "30");
      //Act
      bool result = row.IsTrue(condition);
      //Assert
      Assert.False(result);
    }
        #endregion

        #region Row AsText tests
        [Fact]
        public void Row_AsText_ReturnsCorrectResult()
        {
            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
            new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
            new ColumnDefinition(ColumnDefinition.DataType.Double, "Height")
            };
            var values = new List<string> { "Ane", "20", "1.65" };
            var row = new Row(columns, values);
            var result = row.AsText();
            Assert.Equal("Ane:20:1.65", result);
        }
        [Fact]
        public void Row_AsText_ReturnsCorrectResult_SingleValue()
        {
            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
            };
            var values = new List<string> { "Ane"};
            var row = new Row(columns, values);
            var result = row.AsText();
            Assert.Equal("Ane", result);
        }
        [Fact]
        public void Row_AsText_ReturnsCorrectResult_NullValues()
        {
            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
            };
            var row = new Row(columns, null);
            var result = row.AsText();
            Assert.Null(result);
        }
        [Fact]
        public void Row_AsText_ValuesContainsDelimiter_EncodesItCorrectly()
        {
            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
            new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
            new ColumnDefinition(ColumnDefinition.DataType.Double, "Height")
            };
            var values = new List<string> { "Jone:Miren", "20", "1.65" };
            var row = new Row(columns, values);
            var result = row.AsText();
            Assert.Equal("Jone[SEPARATOR]Miren:20:1.65", result);
        }
		#endregion

		#region Row Parse Tests
		[Fact]
		public void Row_Parse_ShouldCreateRowCorrectly_WhenValueIsValid()
		{
            var columns = new List<ColumnDefinition>
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
                new ColumnDefinition(ColumnDefinition.DataType.Double, "Heigh")
            };
            string text = "Ane:21:1.68";

            Row result = Row.Parse(columns, text);

            Assert.Equal("Ane", result.Values[0]);
			Assert.Equal("21", result.Values[1]);
			Assert.Equal("1.68", result.Values[2]);
		}

        [Fact]
        public void Row_Parse_ShouldDecodeEncodedSeparatorCorrectly()
        {
			var columns = new List<ColumnDefinition>
			{
				new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
				new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
				new ColumnDefinition(ColumnDefinition.DataType.Double, "Heigh")
			};

			string text = "Ane[SEPARATOR]Andrea:21:1.68";

			Row result = Row.Parse(columns, text);

			Assert.Equal("Ane:Andrea", result.Values[0]);
			Assert.Equal("21", result.Values[1]);
			Assert.Equal("1.68", result.Values[2]);
		}

        [Fact]
        public void Row_Parse_ShouldReturnRowCorrectly_WhenAsTextThenParse()
        {
            var columns = new List<ColumnDefinition>
            {
				new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
				new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
				new ColumnDefinition(ColumnDefinition.DataType.Double, "Heigh")
			};

            var row = new Row(columns, new List<String> { "Ane:Andrea", "21", "1.68" });
			string text = row.AsText();

            Row result = Row.Parse(columns, text);

            Assert.Equal(row.Values[0], result.Values[0]);
			Assert.Equal(row.Values[1], result.Values[1]);
			Assert.Equal(row.Values[2], result.Values[2]);
		}

        [Fact]
        public void Row_ShouldReturnRowWithNullValues_WhenValueIsNull()
        {
			var columns = new List<ColumnDefinition>
			{
				new ColumnDefinition(ColumnDefinition.DataType.String, "Name")
			};

			Row result = Row.Parse(columns, null);

            Assert.NotNull(result);
            Assert.Null(result.Values);
		}

        [Fact]
        public void Row_Parse_shouldReturnWowWithNull_WhenNumberOfValuesIsless()
        {
			var columns = new List<ColumnDefinition>
			{
				new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
				new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
				new ColumnDefinition(ColumnDefinition.DataType.Double, "Heigh")
			};

			string text = "Abe:21";

			Row result = Row.Parse(columns, text);

            Assert.NotNull(result);
            Assert.Null(result.Values);
		}

        [Fact]
        public void Row_Parse_ShouldReturnRowCorrectly_WhenThereIsOnlyOneValue()
        {
            var columns = new List<ColumnDefinition>
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name")
             };

			string text = "Ane";

			Row result = Row.Parse(columns, text);

			Assert.Equal("Ane", result.Values[0]);
		}

		#endregion
	}
}