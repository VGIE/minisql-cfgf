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
	}
}
