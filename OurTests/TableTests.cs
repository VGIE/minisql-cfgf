using DbManager;

namespace OurTests
{
    public class TableTests
    {
        //TODO DEADLINE 1A : Create your own tests for Table     

        [Fact]
        public void Table_Constructor_SavesCorrectName()
        {
            var columns = new List<ColumnDefinition>();
            var table = new Table("Personas", columns);
            Assert.Equal("Personas", table.Name);
        }
        [Fact]
        public void Table_Constructor_NameShoulNotBeEmpty()
        {
            var table = new Table("", new List<ColumnDefinition>());
            Assert.Null(table.Name);
        }
        [Fact]
        public void Table_Constructor_NameShoulNotBeNull()
        {
            var table = new Table(null, new List<ColumnDefinition>());
            Assert.Null(table.Name);
        }
        [Fact]
        public void Table_NumRows_EmptyTable_ReturnsZero()
        {
            var table = new Table("Test", new List<ColumnDefinition>());
            Assert.Equal(0, table.NumRows());
        }
        [Fact]
        public void Table_NumRows_NotEmptyTable_ReturnsOne()
        {
            var columns = new List<ColumnDefinition> {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name")
            };
            var table = new Table("Test2", columns);
            var row = new Row(columns, new List<string> { "Mario" });

            table.AddRow(row);
            Assert.Equal(1, table.NumRows());
        }
        [Fact]
        public void Table_AddRow_IncreasesCounter()
        {
            var columns = new List<ColumnDefinition> {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name")
            };
            var table = new Table("TestTable", columns);
            var row = new Row(columns, new List<string> { "Test" });

            table.AddRow(row);
            Assert.Equal(1, table.NumRows());
        }
        [Fact]
        public void Table_GetRow_CorrectIndex_ReturnsCorrectRow()
        {
            var columns = new List<ColumnDefinition> {
            new ColumnDefinition(ColumnDefinition.DataType.String,"Name")
            };

            Table table = new Table("TestTable", columns);
            var row1 = new Row(columns, new List<string> { "Mario" });
            table.AddRow(row1);

            Row result = table.GetRow(0);
            Assert.NotNull(result);
            Assert.Equal("Mario", result.Values[0]);
            Assert.Equal(row1, result);
        }
        [Fact]
        public void Table_GetRow_OutOfRange_ReturnsNull()
        {
            var columns = new List<ColumnDefinition> {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name")
            };

            var table = new Table("TestTable", columns);
            table.AddRow(new Row(columns, new List<string> { "Mario" }));

            Assert.Null(table.GetRow(1));
            Assert.Null(table.GetRow(-1));
        }
        [Fact]
        public void Table_NumColumns_EmptyColumns_ReturnsZero()
        {
            var columns = new List<ColumnDefinition>();
            var table = new Table("TestTable", columns);

            Assert.Equal(0, table.NumColumns());
        }
        [Fact]
        public void Table_NumColumns_NotEmptyColumns_ReturnsCorrectNumber()
        {
            var columns = new List<ColumnDefinition> {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
            new ColumnDefinition(ColumnDefinition.DataType.Int, "Age")
            };

            var table = new Table("Test2", columns);
            Assert.Equal(2, table.NumColumns());
        }
        [Fact]
        public void Table_GetColumn_CorrectIndex_ReturnsCorrectColumn()
        {
            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String,"Name"),
            new ColumnDefinition(ColumnDefinition.DataType.Int,"Age")
            };

            Table table = new Table("TestTable", columns);
            ColumnDefinition col = table.GetColumn(1);

            Assert.NotNull(col);
            Assert.Equal("Age", col.Name);
            Assert.Equal(ColumnDefinition.DataType.Int, col.Type);
        }
        [Fact]
        public void Table_GetColumn_OutOfRange_ReturnsNull()
        {
            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
            new ColumnDefinition(ColumnDefinition.DataType.Int, "Age")
            };

            Table table = new Table("TestTable", columns);

            Assert.Null(table.GetColumn(100));
            Assert.Null(table.GetColumn(-1));
        }
        [Fact]
        public void Table_ColumnByName_UnknownColumn_ReturnsNull()
        {
            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String,"Name"),
            new ColumnDefinition(ColumnDefinition.DataType.Int,"Age")
            };

            Table table = new Table("TestTable", columns);
            Assert.Null(table.ColumnByName("jalkfjdlk"));
        }
        [Fact]
        public void Table_ColumnByName_ExistingColumn_ReturnsCorrectColumn()
        {
            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
            new ColumnDefinition(ColumnDefinition.DataType.Int, "Age")
            };

            Table table = new Table("TestTable", columns);
            var col = table.ColumnByName("Age");
            Assert.NotNull(col);
            Assert.Equal("Age", col.Name);
        }
        [Fact]
        public void Table_ColumnIndexByName_ExistingColumn_ReturnsCorrectIndex()
        {
            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String,"Name"),
            new ColumnDefinition(ColumnDefinition.DataType.Int,"Age")
            };

            Table table = new Table("TestTable", columns);
            int idx = table.ColumnIndexByName("Age");
            Assert.Equal(1, idx);
        }
        [Fact]
        public void Table_ColumnIndexByName_UnknownColumn_ReturnsZero()
        {
            var columns = new List<ColumnDefinition>
            {
            new ColumnDefinition(ColumnDefinition.DataType.String,"Nombre"),
            new ColumnDefinition(ColumnDefinition.DataType.Int,"Edad")
            };

            Table table = new Table("TestTable", columns);
            Assert.Equal(0, table.ColumnIndexByName("jkajdfklj"));
        }



    #region ToString validation

        [Fact]
        public void Table_ToString_NoColumnsNoRows_ReturnsEmptyString()
        {
            var table = new Table("T", new List<ColumnDefinition>());

            Assert.Equal("", table.ToString());
        }

        [Fact]
        public void Table_ToString_OneColumnNoRows_ReturnsOnlyHeader()
        {
            var columns = new List<ColumnDefinition>
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name")
            };

            var table = new Table("T", columns);

            Assert.Equal("['Name']", table.ToString());
        }

        [Fact]
        public void Table_ToString_OneColumnTwoRows()
        {
            var columns = new List<ColumnDefinition>
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name")
            };

            var table = new Table("T", columns);

            table.AddRow(new Row(columns, new List<string> { "Adolfo" }));
            table.AddRow(new Row(columns, new List<string> { "Jacinto" }));

            Assert.Equal("['Name']{'Adolfo'}{'Jacinto'}", table.ToString());
        }

        [Fact]
        public void Table_ToString_TwoColumnsTwoRows()
        {
            var columns = new List<ColumnDefinition>
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
            };

            var table = new Table("T", columns);

            table.AddRow(new Row(columns, new List<string> { "Adolfo", "23" }));
            table.AddRow(new Row(columns, new List<string> { "Jacinto", "24" }));

            Assert.Equal("['Name','Age']{'Adolfo','23'}{'Jacinto','24'}", table.ToString());
        }

        [Fact]
        public void Table_ToString_UsingProjectConstants()
        {
            var table = Table.CreateTestTable();

            string expected =
                "['Name','Height','Age']" +
                "{'Rodolfo','1.62','25'}" +
                "{'Maider','1.67','67'}" +
                "{'Pepe','1.55','51'}";

            Assert.Equal(expected, table.ToString());
        }

    #endregion

    #region RowIndicesWhereConditionIsTrue Tests
    [Fact]
    public void Table_RowIndicesWhereConditionIsTrue_ShouldReturnCorrectIndexes()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      List<string> valuesRow1 = new List<string> { "30" };
      List<string> valuesRow2 = new List<string> { "31" };
      List<string> valuesRow3 = new List<string> { "21" };
      List<string> valuesRow4 = new List<string> { "41" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      table.AddRow(row1);
      table.AddRow(row2);
      table.AddRow(row3);
      table.AddRow(row4);

      var expectedList = new List<int> { 1, 3 };
      var condition = new DbManager.Condition("Age", ">", "30");

      //Act
      var result = table.RowIndicesWhereConditionIsTrue(condition);

      //Assert
      Assert.Equal(expectedList, result);
    }
    #endregion

    #region Select Tests
    [Fact]
    public void Table_Select_ShouldReturnTheCorrectTable_WhenConditionIsTrue()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      table.AddRow(row1);
      table.AddRow(row2);
      table.AddRow(row3);
      table.AddRow(row4);

      var condition = new DbManager.Condition("Age", ">", "30");

      var columnNames = new List<string>() { "Name", "Height" };
      //Act
      var resultTable = table.Select(columnNames, condition);

      //Assert
      Assert.Equal("['Name','Height']{'Pedro','188.57'}", resultTable.ToString());
    }
    [Fact]
    public void Table_Select_ShouldReturnTheCorrectTableName_WhenConditionIsTrue()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      table.AddRow(row1);
      table.AddRow(row2);
      table.AddRow(row3);
      table.AddRow(row4);

      string expectedName = "Result";

      var condition = new DbManager.Condition("Age", ">", "30");

      var columnNames = new List<string>() { "Name", "Height" };
      //Act
      var resultTable = table.Select(columnNames, condition);

      //Assert
      Assert.Equal(expectedName, resultTable.Name);
    }
    [Fact]
    public void Table_Select_ShouldReturnTheSameTable_WhenConditionIsNull()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      table.AddRow(row1);
      table.AddRow(row2);
      table.AddRow(row3);
      table.AddRow(row4);

      List<DbManager.ColumnDefinition> expectedColumnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> evaluesRow1 = new List<string> { "Paco", "183.22" };
      List<string> evaluesRow2 = new List<string> { "Miren", "165.08" };
      List<string> evaluesRow3 = new List<string> { "Pedro", "188.57" };
      List<string> evaluesRow4 = new List<string> { "Paco", "154.77" };

      DbManager.Row erow1 = new DbManager.Row(expectedColumnDefinitions, evaluesRow1);
      DbManager.Row erow2 = new DbManager.Row(expectedColumnDefinitions, evaluesRow2);
      DbManager.Row erow3 = new DbManager.Row(expectedColumnDefinitions, evaluesRow3);
      DbManager.Row erow4 = new DbManager.Row(expectedColumnDefinitions, evaluesRow4);

      DbManager.Table expectedTable = new DbManager.Table(tableName, expectedColumnDefinitions);

      expectedTable.AddRow(erow1);
      expectedTable.AddRow(erow2);
      expectedTable.AddRow(erow3);
      expectedTable.AddRow(erow4);

      DbManager.Condition condition = null;

      var columnNames = new List<string>() { "Name", "Height" };
      //Act
      var resultTable = table.Select(columnNames, condition);

      //Assert
      Assert.Equal(expectedTable.ToString(), resultTable.ToString());
    }
    #endregion

    #region Insert Tests
    [Fact]
    public void Table_Insert_ShouldReturnTrue_WhenTheNumberOfValuesIsValid()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      table.AddRow(row1);
      table.AddRow(row2);
      table.AddRow(row3);
      table.AddRow(row4);

      List<string> insertValues = new List<string> { "8", "Irene", "84.92" };
      var expectedResult = true;

      //Act & Assert
      Assert.Equal(expectedResult, table.Insert(insertValues));
    }
    [Fact]
    public void Table_Insert_ShouldReturnFalse_WhenTheNumberOfValuesIsInvalid()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      table.AddRow(row1);
      table.AddRow(row2);
      table.AddRow(row3);
      table.AddRow(row4);

      List<string> insertValues = new List<string> { "Irene", "84.92" };
      var expectedResult = false;

      //Act & Assert
      Assert.Equal(expectedResult, table.Insert(insertValues));
    }
    [Fact]
    public void Table_Insert_ShouldInsertRow_WhenTheNumberOfValuesIsValid()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      table.AddRow(row1);
      table.AddRow(row2);
      table.AddRow(row3);
      table.AddRow(row4);

      List<string> insertValues = new List<string> { "8", "Irene", "84.92" };

      var expectedResult = new DbManager.Row(columnDefinitions, insertValues);

      //Act
      var act = table.Insert(insertValues);

      //Assert
      Assert.Equal(expectedResult.Values, table.GetRow(4).Values);
    }
    [Fact]
    public void Table_Insert_ShouldNotInsertRow_WhenTheNumberOfValuesIsInvalid()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      table.AddRow(row1);
      table.AddRow(row2);
      table.AddRow(row3);
      table.AddRow(row4);

      List<string> insertValues = new List<string> { "Irene", "84.92" };

      var expectedResult = new DbManager.Row(columnDefinitions, insertValues);

      //Act
      var act = table.Insert(insertValues);

      //Assert
      Assert.Equal(4, table.NumRows());
    }
    #endregion

    #region DeleteWhere Tests
    [Fact]
    public void Table_DeleteWhere_ShouldDeleteRows_WhenConditionIsTrue()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      List<string> valuesRow1 = new List<string> { "30" };
      List<string> valuesRow2 = new List<string> { "31" };
      List<string> valuesRow3 = new List<string> { "21" };
      List<string> valuesRow4 = new List<string> { "41" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      table.AddRow(row1);
      table.AddRow(row2);
      table.AddRow(row3);
      table.AddRow(row4);

      DbManager.Table expectedTable = new DbManager.Table(tableName, columnDefinitions);

      expectedTable.AddRow(row1);
      expectedTable.AddRow(row3);

      var condition = new DbManager.Condition("Age", ">", "30");

      //Act
      table.DeleteWhere(condition);

      //Assert
      Assert.Equal(expectedTable.ToString(), table.ToString());
    }
    #endregion

    #region DeleteIthRow validation

    [Fact]
        public void Table_DeleteIthRow_RemovesCorrectRow()
        {
            var table = Table.CreateTestTable();
            Assert.Equal(3, table.NumRows());

            table.DeleteIthRow(1);
            Assert.Equal(2, table.NumRows());

            table.CheckForTesting(new System.Collections.Generic.List<System.Collections.Generic.List<string>>
            {
                new System.Collections.Generic.List<string> { "Rodolfo", "1.62", "25" },
                new System.Collections.Generic.List<string> { "Pepe", "1.55", "51" }
            });
        }

        [Fact]
        public void Table_DeleteIthRow_WithNegativeIndex()
        {
            var table = Table.CreateTestTable();
            table.DeleteIthRow(-1);
            Assert.Equal(3, table.NumRows());
        }

        [Fact]
        public void Table_DeleteIthRow_WithTooLargeIndex()
        {
            var table = Table.CreateTestTable();
            table.DeleteIthRow(5);
            Assert.Equal(3, table.NumRows());
        }



        #endregion

    }
}