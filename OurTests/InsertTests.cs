using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbManager;

namespace OurTests
{
	public class InsertTests
	{
		#region constructor tests
		[Fact]
		public void Insert_Constructor_ShouldInitializeAttributesCorrectly()
		{
			var table = "TestTable";
			var values = new List<string> { "Ane", "21", "1.68" };
			var insert = new DbManager.Insert(table, values);

			Assert.Equal(table, insert.Table);
			Assert.Equal(values, insert.Values);
		}
		#endregion

		#region execute tests
		[Fact]
		public void Insert_Execute_ShouldReturnInsertSuccess()
		{
			var database = Database.CreateTestDatabase();
			var values = new List<string> { "Marta", "1.70", "30" };
			var insert = new DbManager.Insert(Table.TestTableName, values);

			var result = insert.Execute(database);

			Assert.Equal(Constants.InsertSuccess, result);
		}

		[Fact]
		public void Insert_Execute_TableDoesNotExist_ShouldReturnError()
		{
			var database = Database.CreateTestDatabase();
			var values = new List<string> { "Marta", "1.70", "30" };
			var insert = new DbManager.Insert("NonExistentTable", values);

			var result = insert.Execute(database);

			Assert.Equal(Constants.TableDoesNotExistError, result);
		}

		[Fact]
		public void Insert_Execute_WrongColumnCount_ShouldReturnError()
		{
			var database = Database.CreateTestDatabase();
			var values = new List<string> { "Marta", "1.70" };
			var insert = new DbManager.Insert(Table.TestTableName, values);

			var result = insert.Execute(database);

			Assert.Equal(Constants.ColumnCountsDontMatch, result);
		}
		#endregion
	}
}
