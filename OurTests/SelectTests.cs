using DbManager;
using System.Data.Common;

namespace OurTests
{
    public class SelectTests
    {
        #region constructor tests
        [Fact]
        public void Select_Constructor_ShouldInitializeAttributes()
        {
            var table = "TestTable";
            var columns = new List<String> { "Name", "Age" };
            var condition = new DbManager.Condition("Name", "=", "Mario");
            var select = new DbManager.Select(table, columns, condition);

            Assert.Equal(table, select.Table);
            Assert.Equal(columns, select.Columns);
            Assert.Equal(condition, select.Where);
        }
        [Fact]
        public void Select_Constructor_NoCondition_WhereShouldBeNull()
        {
            var table = "TestTable";
            var columns = new List<String> { "Name", "Age" };
            var select = new DbManager.Select(table, columns);

            Assert.Equal(table, select.Table);
            Assert.Equal(columns, select.Columns);
            Assert.Null(select.Where);
        }
        #endregion
        #region execute tests
        [Fact]
        public void Select_Execute_ShouldWork_WhenTableExists()
        {
            var database = Database.CreateTestDatabase();
            var columns = new List<ColumnDefinition>
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Nombre")
            };
            var createTable = new CreateTable("Alumnos", columns);
            createTable.Execute(database);
            var insert = new Insert("Alumnos", new List<string> { "Mario" });
            insert.Execute(database);
            var select = new Select("Alumnos", new List<string> { "Nombre" });
            var result = select.Execute(database);

            Assert.NotNull(result);
            Assert.Contains("Mario", result);
        }

        [Fact]
        public void Select_Execute_ShouldReturnError_WhenTableDoesNotExist()
        {
            var database = Database.CreateTestDatabase();
            var select = new Select("desconocidou", new List<string> { "Name" });
            var result = select.Execute(database);

            Assert.Equal(Constants.TableDoesNotExistError, result);
        }

        #endregion
    }

}
