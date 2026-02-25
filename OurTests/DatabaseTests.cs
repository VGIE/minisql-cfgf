using DbManager;

namespace OurTests
{
    public class UnitTest1
    {
        #region AddTable Tests
        [Fact]
        public void Database_AddTable_ShouldAddTableToDatabaseCorrectly()
        {
            //Assert
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
            var database = DbManager.Database.CreateTestDatabase();

            //Act
            database.AddTable(table);

            //Assert
            Assert.Equal(table, database.TableByName(tableName));
        }
        #endregion

        #region TableByName Tests
        [Fact]
        public void Database_TableByName_ShouldReturnTheCorrectTable()
        {
            //Assert
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
            var database = DbManager.Database.CreateTestDatabase();
            database.AddTable(table);

            //Act
            var table1 = database.TableByName(tableName);
            var table2 = database.TableByName("TestTable");

            //Assert
            Assert.Equal(DbManager.Table.CreateTestTable().ToString(), table2.ToString());
            Assert.Equal(table.ToString(), table1.ToString());
        }
        #endregion

        #region CreateTable Tests
        [Fact]
        public void Database_CreateTable_TableAlreadyExists_ReturnsFalse_AndSetsCorrectLastErrorMessage()
        {
            var columns = new List<DbManager.ColumnDefinition>
            {
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
            };

            var database = DbManager.Database.CreateTestDatabase();
            bool result = database.CreateTable("TestTable", columns);

            Assert.False(result);
            Assert.Equal(DbManager.Constants.TableAlreadyExistsError, database.LastErrorMessage);
        }
        [Fact]
        public void Database_CreateTable_NullColumns_ReturnsFalse_AndSetsCorrectLastErrorMessage()
        {
            var database = DbManager.Database.CreateTestDatabase();
            bool result = database.CreateTable("Mario", null);

            Assert.False(result);
            Assert.Equal(DbManager.Constants.DatabaseCreatedWithoutColumnsError, database.LastErrorMessage);
        }
        [Fact]
        public void Database_CreateTable_ColumnDefinitionsListIsEmpty_ReturnsFalse_AndSetsCorrectLastErrorMessage()
        {
            var database = DbManager.Database.CreateTestDatabase();
            var empty = new List<DbManager.ColumnDefinition>();
            bool result = database.CreateTable("Mario", empty);

            Assert.False(result);
            Assert.Equal(DbManager.Constants.DatabaseCreatedWithoutColumnsError, database.LastErrorMessage);
        }
        [Fact]
        public void Database_CreateTable_EverythingCorrect_ReturnsTrue_AndSetsCorrectLastErrorMessage()
        {
            var columns = new List<DbManager.ColumnDefinition>
            {
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
            };

            var database = DbManager.Database.CreateTestDatabase();
            bool result = database.CreateTable("Mario", columns);

            Assert.True(result);
            Assert.Equal(DbManager.Constants.CreateTableSuccess, database.LastErrorMessage);
        }
        [Fact]
        public void Database_CreateTable_EverythingCorrect_TableAdded()
        {
            var columns = new List<DbManager.ColumnDefinition>
            {
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
            };

            var database = DbManager.Database.CreateTestDatabase();
            bool result = database.CreateTable("Mario", columns);

            Assert.True(result);
            Assert.NotNull(database.TableByName("Mario"));
        }
        #endregion

        #region DropTable Tests
        [Fact]
        public void Database_DropTable_TableDoesNotExist_ReturnsFalse_And_SetCorrectLastErrorMessage()
        {
            var database = DbManager.Database.CreateTestDatabase();
            bool result = database.DropTable("DoesntExist");
            Assert.False(result);
            Assert.Equal(DbManager.Constants.TableDoesNotExistError, database.LastErrorMessage);
        }
        [Fact]
        public void Database_DropTable_TableExists_ReturnsTrue_SetCorrectLastErrorMessage_And_RemovesTable()
        {
            var database = DbManager.Database.CreateTestDatabase();
            bool result = database.DropTable("TestTable");
            Assert.True(result);
            Assert.Equal(DbManager.Constants.DropTableSuccess, database.LastErrorMessage);
            Assert.Null(database.TableByName("TestTable"));
        }

        #endregion


        #region Insert Tests
        [Fact]
        public void Database_Insert_TableDoesNotExist()
        {
            var db = Database.CreateTestDatabase();
            bool ok = db.Insert("NotExistingTable", new List<string> { "a", "b", "c" });
            Assert.False(ok);
            Assert.Equal(Constants.TableDoesNotExistError, db.LastErrorMessage);
        }

        [Fact]
        public void Database_Insert_WrongNumberOfValues()
        {
            var db = Database.CreateTestDatabase();
            bool ok = db.Insert(Table.TestTableName, new List<string> { "OnlyOneValue" });
            Assert.False(ok);
            Assert.Equal(Constants.ColumnCountsDontMatch, db.LastErrorMessage);
        }

        [Fact]
        public void Database_Insert_CorrectInsert()
        {
            var db = Database.CreateTestDatabase();
            var newRow = new List<string> { "NewGuy", "1.80", "30" };
            bool ok = db.Insert(Table.TestTableName, newRow);
            Assert.True(ok);
            Assert.Equal(Constants.InsertSuccess, db.LastErrorMessage);

            db.CheckForTesting(Table.TestTableName, new List<List<string>>
            {
                new List<string> { Table.TestColumn1Row1, Table.TestColumn2Row1, Table.TestColumn3Row1 },
                new List<string> { Table.TestColumn1Row2, Table.TestColumn2Row2, Table.TestColumn3Row2 },
                new List<string> { Table.TestColumn1Row3, Table.TestColumn2Row3, Table.TestColumn3Row3 },
                new List<string> { "NewGuy", "1.80", "30" }
            });
        }

        #endregion



        #region Select Tests
        [Fact]
        public void Database_Select_TableDoesNotExist()
        {
            var db = Database.CreateTestDatabase();
            var result = db.Select("NoSuchTable", new List<string> { Table.TestColumn1Name }, null);
            Assert.Null(result);
            Assert.Equal(Constants.TableDoesNotExistError, db.LastErrorMessage);
        }

        [Fact]
        public void Database_Select_ColumnDoesNotExist()
        {
            var db = Database.CreateTestDatabase();
            var result = db.Select(Table.TestTableName, new List<string> { "NoSuchColumn" }, null);
            Assert.Null(result);
            Assert.Equal(Constants.ColumnDoesNotExistError, db.LastErrorMessage);
        }

        [Fact]
        public void Database_Select_ColumnsIsNull()
        {
            var db = Database.CreateTestDatabase();
            var result = db.Select(Table.TestTableName, null, null);
            Assert.Null(result);
            Assert.Equal(Constants.ColumnDoesNotExistError, db.LastErrorMessage);
        }

        [Fact]
        public void Database_Select_ColumnsIsEmpty()
        {
            var db = Database.CreateTestDatabase();
            var result = db.Select(Table.TestTableName, new List<string>(), null);
            Assert.Null(result);
            Assert.Equal(Constants.ColumnDoesNotExistError, db.LastErrorMessage);
        }

        [Fact]
        public void Database_Select_NoCondition()
        {
            var db = Database.CreateTestDatabase();
            var cols = new List<string> { Table.TestColumn1Name, Table.TestColumn3Name };
            var result = db.Select(Table.TestTableName, cols, null);

            Assert.NotNull(result);
            Assert.Equal("Result", result.Name);
            Assert.Equal(2, result.NumColumns());
            Assert.Equal(3, result.NumRows());
            Assert.Equal(Table.TestColumn1Name, result.GetColumn(0).Name);
            Assert.Equal(Table.TestColumn3Name, result.GetColumn(1).Name);

            result.CheckForTesting(new List<List<string>>
                {
                    new List<string> { Table.TestColumn1Row1, Table.TestColumn3Row1 },
                    new List<string> { Table.TestColumn1Row2, Table.TestColumn3Row2 },
                    new List<string> { Table.TestColumn1Row3, Table.TestColumn3Row3 }
                });
        }

        [Fact]
        public void Database_Select_WithCondition()
        {

            var db = Database.CreateTestDatabase();
            var cols = new List<string> { Table.TestColumn1Name, Table.TestColumn3Name };
            var condition = new Condition(Table.TestColumn3Name, ">", "50");
            var result = db.Select(Table.TestTableName, cols, condition);

            Assert.NotNull(result);
            Assert.Equal("Result", result.Name);
            Assert.Equal(2, result.NumColumns());
            Assert.Equal(2, result.NumRows());

            result.CheckForTesting(new List<List<string>>
                {
                    new List<string> { Table.TestColumn1Row2, Table.TestColumn3Row2 },
                    new List<string> { Table.TestColumn1Row3, Table.TestColumn3Row3 }
                });
        }


        #endregion

        #region DeleteWhere Test
        [Fact]
        public void Database_DeleteWhere_ShouldReturnFalse_IfTableNotExists()
        {
            Database db = Database.CreateTestDatabase();
            Condition cond = new Condition("Age", ">", "60");

            bool result = db.DeleteWhere("Pedro", cond);

            Assert.False(result);
            Assert.Equal(Constants.TableDoesNotExistError, db.LastErrorMessage);
        }

        [Fact]
        public void Database_DeleteWhere_ShouldReturnFalse_IfConditionColumnNotExist()
        {
            var db = Database.CreateTestDatabase();
            Condition cond = new Condition("Poblation", ">", "60");

            bool result = db.DeleteWhere(Table.TestTableName, cond);

            Assert.False(result);
			Assert.Equal(Constants.ColumnDoesNotExistError, db.LastErrorMessage);
		}

        [Fact]
        public void Database_DeleteWhere_ShouldDeleteRows_AndReturnsTrue_IfIsok()
        {
            var db = Database.CreateTestDatabase();
            var cond = new Condition("Age", "=", "50");
            var expected = Table.CreateTestTable();
            expected.DeleteWhere(cond);

            bool result = db.DeleteWhere(Table.TestTableName, cond);

            Assert.True(result);
            Assert.Equal(expected.ToString(), db.TableByName("TestTable").ToString());
        }
		#endregion
	}
} 