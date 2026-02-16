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
        public void Database_CreateTable_TableAlreadyExists_ReturnFalse()
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
        }
        [Fact]
        public void Database_CreateTable_TableAlreadyExists_SetCorrectLastErrorMessage()
        {
            var columns = new List<DbManager.ColumnDefinition>
            {
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
            };

            var database = DbManager.Database.CreateTestDatabase();
            database.CreateTable("TestTable", columns);

            Assert.Equal(DbManager.Constants.TableAlreadyExistsError, database.LastErrorMessage);
        }
        [Fact]
        public void Database_CreateTable_NullColumns_ReturnsFalse()
        {
            var database = DbManager.Database.CreateTestDatabase();
            bool result=database.CreateTable("Mario", null);

            Assert.False(result);
        }
        [Fact]
        public void Database_CreateTable_NullColumns_SetCorrectLastErrorMessage()
        {
            var database = DbManager.Database.CreateTestDatabase();
            database.CreateTable("Mario", null);

            Assert.Equal(DbManager.Constants.DatabaseCreatedWithoutColumnsError, database.LastErrorMessage);
        }
        [Fact]
        public void Database_CreateTable_ColumnDefinitionsListIsEmpty_ReturnsFalse()
        {
            var database = DbManager.Database.CreateTestDatabase();
            var empty = new List<DbManager.ColumnDefinition>();
            bool result=database.CreateTable("Mario", empty);

            Assert.False(result);
        }
        [Fact]
        public void Database_CreateTable_ColumnDefinitionsListIsEmpty_SetCorrectLastErrorMessage()
        {
            var database = DbManager.Database.CreateTestDatabase();
            var empty = new List<DbManager.ColumnDefinition>();
            database.CreateTable("Mario", empty);

            Assert.Equal(DbManager.Constants.DatabaseCreatedWithoutColumnsError, database.LastErrorMessage);
        }
        [Fact]
        public void Database_CreateTable_EverythingCorrect_ReturnsTrue()
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
        }
        [Fact]
        public void Database_CreateTable_EverythingCorrect_SetCorrectLastErrorMessage()
        {
            var columns = new List<DbManager.ColumnDefinition>
            {
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
                (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
            };

            var database = DbManager.Database.CreateTestDatabase();
            database.CreateTable("Mario", columns);

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
            database.CreateTable("Mario", columns);

            Assert.NotNull(database.TableByName("Mario"));
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
    }

    #endregion
}