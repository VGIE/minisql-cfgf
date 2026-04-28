using DbManager;
using DbManager.Parser;

namespace OurTests
{
    public class UpdateTests
    {
        #region constructor tests
        [Fact]
        public void Update_Constructor_ShouldInitializeAttributesCorrectly()
        {
            var table = "TestTable";
            var setValues = new List<SetValue> { new SetValue("Age", "30") };
            var condition = new Condition("Age", ">", "18");
            var update = new DbManager.Update(table, setValues, condition);

            Assert.Equal(table, update.Table);
            Assert.Equal(setValues, update.Columns);
            Assert.Equal(condition, update.Where);
        }

        [Fact]
        public void Update_Constructor_NoCondition_WhereShouldBeNull()
        {
            var table = "TestTable";
            var setValues = new List<SetValue> { new SetValue("Name", "Pedro") };
            var update = new DbManager.Update(table, setValues, null);

            Assert.Equal(table, update.Table);
            Assert.Equal(setValues, update.Columns);
            Assert.Null(update.Where);
        }
        #endregion


        #region execute tests
        [Fact]
        public void Update_Execute_ShouldWorkCorrectly()
        {
            var database = Database.CreateTestDatabase();
            var setValues = new List<SetValue>()
            {
                new SetValue("Name", "UpdatedName")
            };

            var condition = new Condition("Age", "=", Table.TestColumn3Row1);
            var update = new Update(Table.TestTableName, setValues, condition);
            var result = update.Execute(database);
            var table = database.TableByName(Table.TestTableName);

            Assert.Equal(Constants.UpdateSuccess, result);
            Assert.Equal("UpdatedName", table.GetRow(0).GetValue("Name"));
        }

        [Fact]
        public void Update_Execute_TableDoesntExist_ShouldReturnError()
        {
            var database = Database.CreateTestDatabase();
            var setValues = new List<SetValue>()
            {
                new SetValue("Name", "UpdatedName")
            };

            var condition = new Condition("Age", "=", Table.TestColumn3Row1);
            var update = new Update("NoExiste", setValues, condition);
            var result = update.Execute(database);

            Assert.Equal(Constants.TableDoesNotExistError, result);
        }

        [Fact]
        public void Update_Execute_ConditionColumnDoesntExist_ShouldReturnError()
        {
            var database = Database.CreateTestDatabase();
            var setValues = new List<SetValue>()
            {
                new SetValue("Name", "UpdatedName")
            };

            var condition = new Condition("NoColumn", "=", "25");
            var update = new Update(Table.TestTableName, setValues, condition);
            var result = update.Execute(database);

            Assert.Equal(Constants.ColumnDoesNotExistError, result);
        }

        [Fact]
        public void Update_Execute_SetColumnDoesntExist_ShouldReturnError()
        {
            var database = Database.CreateTestDatabase();
            var setValues = new List<SetValue>()
            {
                new SetValue("NoColumn", "UpdatedName")
            };

            var condition = new Condition("Age", "=", Table.TestColumn3Row1);
            var update = new Update(Table.TestTableName, setValues, condition);
            var result = update.Execute(database);

            Assert.Equal(Constants.ColumnDoesNotExistError, result);
        }

        #endregion
    }
}
