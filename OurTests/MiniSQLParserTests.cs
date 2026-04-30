using DbManager;
using DbManager.Parser;

namespace OurTests
{
    public class MiniSQLParserTests
    {
        #region parse drop table tests
        [Fact]
        public void Parse_DropTable_ShouldParse()
        {
            var result = MiniSQLParser.Parse("DROP TABLE TestTable");

            Assert.NotNull(result);
            Assert.IsType<DropTable>(result);

            var dropTable = (DropTable)result;
            Assert.Equal("TestTable", dropTable.Table);
        }

        [Fact]
        public void Parse_DropTable_ShouldAcceptAnyCapitalization()
        {
            var result1 = MiniSQLParser.Parse("DROP TABLE Tes2tT5abl3e");
            var result2 = MiniSQLParser.Parse("DROP TABLE Test6Table");
            var result3 = MiniSQLParser.Parse("DROP TABLE Te_st4Tabl_e");

            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotNull(result3);

            Assert.IsType<DropTable>(result1);
            Assert.IsType<DropTable>(result2);
            Assert.IsType<DropTable>(result3);

            var dropT = (DropTable)result1;
            Assert.Equal("Tes2tT5abl3e", dropT.Table);
            dropT = (DropTable)result2;
            Assert.Equal("Test6Table", dropT.Table);
            dropT = (DropTable)result3;
            Assert.Equal("Te_st4Tabl_e", dropT.Table);
        }

        [Fact]
        public void Parse_DropTable_NotAcceptedSyntax_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("DROP TABLE ");
            var result2 = MiniSQLParser.Parse(" ");
            var result3 = MiniSQLParser.Parse("DrO TBle TestTable");
            var result4 = MiniSQLParser.Parse("drop table TestTable");

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
            Assert.Null(result4);
        }

        [Fact]
        public void Parse_DropTable_FirstTableCharacterIsANumber_ShouldReturnNull()
        {
            var result = MiniSQLParser.Parse("DROP TABLE 1TestTable");

            Assert.Null(result);
        }
        #endregion

        #region parse create table tests
        [Fact]
        public void Parse_CreateTable_ShouldParse()
        {
            var result = MiniSQLParser.Parse("CREATE TABLE TestTable (Name TEXT,Age INT,Height DOUBLE)");
            Assert.NotNull(result);
            Assert.IsType<CreateTable>(result);

            var createTable = (CreateTable)result;
            Assert.Equal("TestTable", createTable.Table);
            Assert.Equal(3, createTable.ColumnsParameters.Count);

            Assert.Equal("Name", createTable.ColumnsParameters[0].Name);
            Assert.Equal(ColumnDefinition.DataType.String, createTable.ColumnsParameters[0].Type);

            Assert.Equal("Age", createTable.ColumnsParameters[1].Name);
            Assert.Equal(ColumnDefinition.DataType.Int, createTable.ColumnsParameters[1].Type);

            Assert.Equal("Height", createTable.ColumnsParameters[2].Name);
            Assert.Equal(ColumnDefinition.DataType.Double, createTable.ColumnsParameters[2].Type);
        }
        [Fact]
        public void Parse_CreateTable_ShouldParse_SimpleOneColumn()
        {
            var result = MiniSQLParser.Parse("CREATE TABLE TestTable (Name TEXT)");
            Assert.NotNull(result);
            Assert.IsType<CreateTable>(result);

            var createTable = (CreateTable)result;
            Assert.Equal("TestTable", createTable.Table);
            Assert.Single(createTable.ColumnsParameters);

            Assert.Equal("Name", createTable.ColumnsParameters[0].Name);
            Assert.Equal(ColumnDefinition.DataType.String, createTable.ColumnsParameters[0].Type);
        }

			[Fact]
        public void Parse_CreateTable_AcceptsValidIdentifiers()
        {
            var result = MiniSQLParser.Parse("CREATE TABLE Tes2t_Tab3le (Col_1 TEXT,Age2 INT)");
            Assert.NotNull(result);
            Assert.IsType<CreateTable>(result);

            var createTable = (CreateTable)result;
            Assert.Equal("Tes2t_Tab3le", createTable.Table);
            Assert.Equal(2, createTable.ColumnsParameters.Count);

            Assert.Equal("Col_1", createTable.ColumnsParameters[0].Name);
            Assert.Equal(ColumnDefinition.DataType.String, createTable.ColumnsParameters[0].Type);

            Assert.Equal("Age2", createTable.ColumnsParameters[1].Name);
            Assert.Equal(ColumnDefinition.DataType.Int, createTable.ColumnsParameters[1].Type);
        }

        [Fact]
        public void Parse_CreateTable_AcceptsEmptyColumns()
        {
            var result = MiniSQLParser.Parse("CREATE TABLE EmptyTable ()");
            Assert.NotNull(result);
            Assert.IsType<CreateTable>(result);

            var createTable = (CreateTable)result;
            Assert.Equal("EmptyTable", createTable.Table);
            Assert.Empty(createTable.ColumnsParameters);
        }

        [Fact]
        public void Parse_CreateTable_AcceptsSpaces()
        {
            var result = MiniSQLParser.Parse("CREATE   TABLE   TestTable   (Name TEXT,Age INT,Height DOUBLE)");
            Assert.NotNull(result);
            Assert.IsType<CreateTable>(result);

            var createTable = (CreateTable)result;
            Assert.Equal("TestTable", createTable.Table);
            Assert.Equal(3, createTable.ColumnsParameters.Count);

            Assert.Equal("Name", createTable.ColumnsParameters[0].Name);
            Assert.Equal(ColumnDefinition.DataType.String, createTable.ColumnsParameters[0].Type);

            Assert.Equal("Age", createTable.ColumnsParameters[1].Name);
            Assert.Equal(ColumnDefinition.DataType.Int, createTable.ColumnsParameters[1].Type);

            Assert.Equal("Height", createTable.ColumnsParameters[2].Name);
            Assert.Equal(ColumnDefinition.DataType.Double, createTable.ColumnsParameters[2].Type);
        }

        [Fact]
        public void Parse_CreateTable_NotAcceptedSyntax()
        {
            var result1 = MiniSQLParser.Parse("CREATE TABLE");
            Assert.Null(result1);
            var result2 = MiniSQLParser.Parse("CREATE TABLE TestTable");
            Assert.Null(result2);
            var result3 = MiniSQLParser.Parse("CREATE TABL TestTable (Name TEXT)");
            Assert.Null(result3);
            var result4 = MiniSQLParser.Parse("create table TestTable (Name TEXT)");
            Assert.Null(result4);
            var result5 = MiniSQLParser.Parse(" ");
            Assert.Null(result5);
            var result6 = MiniSQLParser.Parse("CREATE TABLE 1TestTable (Name TEXT)");
            Assert.Null(result6);
            var result7 = MiniSQLParser.Parse("CREATE TABLE TestTable (1Name TEXT)");
            Assert.Null(result7);
            var result8 = MiniSQLParser.Parse("CREATE TABLE TestTable (Name TEXT Age INT)");
            Assert.Null(result8);
			var result9 = MiniSQLParser.Parse("CREATE TABLE  (Name TEXT,Age INT)");
			Assert.Null(result9);

		}

        [Fact]
        public void Parse_CreateTable_NotAcceptedTypes()
        {
            var result1 = MiniSQLParser.Parse("CREATE TABLE TestTable (Name Boolean)");
            Assert.Null(result1);
            var result2 = MiniSQLParser.Parse("CREATE TABLE TestTable (Age Float)");
            Assert.Null(result2);
            var result3 = MiniSQLParser.Parse("CREATE TABLE TestTable (Height Decimal)");
            Assert.Null(result3);
        }

        #endregion

        #region parse delete tests
        [Fact]
        public void Parse_Delete_ShouldParse()
        {
            var result = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age='12'");
            Assert.NotNull(result);
            Assert.IsType<Delete>(result);

            var delete = (Delete)result;
            Assert.Equal("TestTable", delete.Table);
            Assert.Equal("age", delete.Where.ColumnName);
            Assert.Equal("=", delete.Where.Operator);
            Assert.Equal("12", delete.Where.LiteralValue);
        }
        [Fact]
        public void Parse_Delete_ShouldAcceptAnyCapitalization()
        {
            var result1 = MiniSQLParser.Parse("DELETE FROM T3_stTable WHERE a23>'x'");
            var result2 = MiniSQLParser.Parse("DELETE FROM  Test6Table WHERE NaMe='AlFonSo'");
            var result3 = MiniSQLParser.Parse("DELETE FROM Te_st4Tabl_e WHERE yeAr<'2025'");

            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotNull(result3);

            Assert.IsType<Delete>(result1);
            Assert.IsType<Delete>(result2);
            Assert.IsType<Delete>(result3);

            var delete = (Delete)result1;
            Assert.Equal("T3_stTable", delete.Table);
            Assert.Equal("a23", delete.Where.ColumnName);
            Assert.Equal(">", delete.Where.Operator);
            Assert.Equal("x", delete.Where.LiteralValue);

            delete = (Delete)result2;
            Assert.Equal("Test6Table", delete.Table);
            Assert.Equal("NaMe", delete.Where.ColumnName);
            Assert.Equal("=", delete.Where.Operator);
            Assert.Equal("AlFonSo", delete.Where.LiteralValue);

            delete = (Delete)result3;
            Assert.Equal("Te_st4Tabl_e", delete.Table);
            Assert.Equal("yeAr", delete.Where.ColumnName);
            Assert.Equal("<", delete.Where.Operator);
            Assert.Equal("2025", delete.Where.LiteralValue);
        }
        [Fact]
        public void Parse_Delete_NotAcceptedSyntax_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("delete from TestTable where age='12'");
            var result2 = MiniSQLParser.Parse(" ");
            var result3 = MiniSQLParser.Parse("DELETE TABLE TestTable WHER age='12'");
            var result4 = MiniSQLParser.Parse("Delete Table TestTable where age='12'");
            var result5 = MiniSQLParser.Parse("DELET TABLE TestTable WHERE age='12'");
            var result6 = MiniSQLParser.Parse("DELETE TABL TestTable where age='12'");

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
            Assert.Null(result4);
            Assert.Null(result5);
            Assert.Null(result6);
        }


			[Fact]
        public void Parse_Delete_FirstTableCharacterIsANumber_ShouldReturnNull()
        {
            var result = MiniSQLParser.Parse("DELETE FROM 1TestTable WHERE age='12'");

            Assert.Null(result);
        }
        [Fact]
        public void Parse_Delete_CombinedOperators_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("DELETE FROM 1TestTable WHERE age=>'12'");
            var result2 = MiniSQLParser.Parse("DELETE FROM 1TestTable WHERE age=<'12'");
            var result3 = MiniSQLParser.Parse("DELETE FROM 1TestTable WHERE age>='12'");
            var result4 = MiniSQLParser.Parse("DELETE FROM 1TestTable WHERE age<='12'");

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
            Assert.Null(result4);
        }
        [Fact]
        public void Parse_Delete_SpacesAtTheBeginingAndInTheEnd_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse(" DELETE FROM TestTable WHERE age=>'12'");
            var result2 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age=<'12' ");

            Assert.Null(result1);
            Assert.Null(result2);
        }
        [Fact]
        public void Parse_Delete_TableOr__ColumnOr_OperatorOr_LiteralValue_Missing_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("DELETE FROM WHERE age='12'");//table
            var result2 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE='12'");//column
            var result3 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age '12'");//operator
            var result4 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age=''");//literalvalue

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
            Assert.Null(result4);
        }
        [Fact]
        public void Parse_Delete_WithExtraTextAtTheEnd_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age='12' AJLDJF");
            var result2 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age='12';");
            var result3 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age='12' and year=2015");

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
        }
        [Fact]
        public void Parse_Delete_LiteralValueOrColumnWithNumbersAndUnderscoresAnd_SpacesOnLV_ShouldParse()
        {
            var result = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age='value_123'");
            var result1 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE a_g2e='12'");
            var result2 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE name='Adolfo Sanchez'");

            Assert.NotNull(result);
            Assert.NotNull(result1);
            Assert.NotNull(result2);

            Assert.IsType<Delete>(result);
            Assert.IsType<Delete>(result1);
            Assert.IsType<Delete>(result2);

            var delete = (Delete)result;
            Assert.Equal("value_123", delete.Where.LiteralValue);

            delete = (Delete)result1;
            Assert.Equal("a_g2e", delete.Where.ColumnName);

            delete = (Delete)result2;
            Assert.Equal("Adolfo Sanchez", delete.Where.LiteralValue);
        }
        
        [Fact]
        public void Parse_Delete_DoubleStringCondition_ShouldParse()
        {
            var result1 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE height='1.2'");
            var result2 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE place='Galdeano'");

            Assert.NotNull(result1);
            Assert.NotNull(result2);

            Assert.IsType<Delete>(result1);
            Assert.IsType<Delete>(result2);

            var delete = (Delete)result1;
            Assert.Equal("TestTable", delete.Table);
            Assert.Equal("height", delete.Where.ColumnName);
            Assert.Equal("=", delete.Where.Operator);
            Assert.Equal("1.2", delete.Where.LiteralValue);

            delete = (Delete)result2;
            Assert.Equal("TestTable", delete.Table);
            Assert.Equal("place", delete.Where.ColumnName);
            Assert.Equal("=", delete.Where.Operator);
            Assert.Equal("Galdeano", delete.Where.LiteralValue);
        }
        [Fact]
        public void Parse_Delete_WithoutConditionMultipleTables_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("DELETE FROM TestTable Table1 WHERE age='12'");

            Assert.Null(result1);
        }
        #endregion

        #region parse-insertquery tests
        [Fact]
        public void Parse_Insert_ShouldParse() 
        {
			var result = MiniSQLParser.Parse("INSERT INTO Users VALUES('Ane','21','1.68')");
			Assert.NotNull(result);
			Assert.IsType<Insert>(result);

			var insert = (Insert)result;
			Assert.Equal("Users", insert.Table);
            Assert.Equal(3, insert.Values.Count);
			Assert.Equal("Ane", insert.Values[0]);
			Assert.Equal("21", insert.Values[1]);
			Assert.Equal("1.68", insert.Values[2]);
		}

		[Fact]
		public void Parse_Insert_Spaces_ShouldParse()
		{
			var result1 = MiniSQLParser.Parse("INSERT     INTO      Users      VALUES     ('Ane','21','1.68')");
            Assert.NotNull(result1);
            Assert.IsType<Insert>(result1);

            var insert = (Insert)result1;
			Assert.Equal("Users", insert.Table);
			Assert.Equal(3, insert.Values.Count);
			Assert.Equal("Ane", insert.Values[0]);
			Assert.Equal("21", insert.Values[1]);
			Assert.Equal("1.68", insert.Values[2]);
		}

		[Fact]
		public void Parse_Insert_WithExtraTextAtTheEnd_ShouldReturnNull()
		{
			var result1 = MiniSQLParser.Parse("INSERT INTO Users VALUES ('Ane','21','1.68') AJLDJF");
			var result2 = MiniSQLParser.Parse("INSERT INTO Users VALUES('Ane','21','1.68');");
			var result3 = MiniSQLParser.Parse("INSERT INTO Users VALUES('Ane','21','1.68') and");

			Assert.Null(result1);
			Assert.Null(result2);
			Assert.Null(result3);
		}

		[Fact]
		public void Parse_Insert_NotAcceptedSyntax()
		{
			var result1 = MiniSQLParser.Parse("INSERT");
			Assert.Null(result1);
			var result2 = MiniSQLParser.Parse("INSERT INTO Users");
			Assert.Null(result2);
			var result3 = MiniSQLParser.Parse("INSERT IN Users VALUES('Ane','21','1.68')");
			Assert.Null(result3);
			var result4 = MiniSQLParser.Parse("insert into Users VALUES('Ane')");
			Assert.Null(result4);
			var result5 = MiniSQLParser.Parse(" ");
			Assert.Null(result5);
			
		}
        [Fact]
		public void Parse_Insert_TableOr_Missing_ShouldReturnNull()
		{
			var result1 = MiniSQLParser.Parse("INSERT INTO VALUES('Ane','21','1.68')");//table

			Assert.Null(result1);
		}

		[Fact]
        public void Parse_Insert_NotAcceptedEmptyValues()
        {
            var result1 = MiniSQLParser.Parse("INSERT INTO Users VALUES ");
            Assert.Null(result1);
        }

		[Fact]
		public void Parse_Insert_FirstTableCharacterIsANumber_ShouldReturnNull()
		{
			var result = MiniSQLParser.Parse("INSERT INTO 1Users VALUES ('Ane','21','1.68')");
			Assert.Null(result);
		}

		[Fact]
		public void Parse_CreateTable_AcceptsSingleValue()
		{
			var result = MiniSQLParser.Parse("INSERT INTO Users VALUES('Ane')");
			Assert.NotNull(result);
			Assert.IsType<Insert>(result);

			var insert = (Insert)result;
			Assert.Equal("Users", insert.Table);
			Assert.Equal("Ane", insert.Values[0]);
		}
		[Fact]
		public void Parse_CreateTable_AcceptsHalfValue()
		{
			var result = MiniSQLParser.Parse("INSERT INTO Users VALUES('Ane','21')");
			Assert.NotNull(result);
			Assert.IsType<Insert>(result);

			var insert = (Insert)result;
			Assert.Equal("Users", insert.Table);
            Assert.Equal(2, insert.Values.Count);
			Assert.Equal("Ane", insert.Values[0]);
		}
        [Fact]
        public void Parse_Insert_shouldParsewithcomma() 
        {
			var result = MiniSQLParser.Parse("INSERT INTO Users VALUES('Ane','Lete')");
			Assert.NotNull(result);
			Assert.IsType<Insert>(result);

			var insert = (Insert)result;
			Assert.Equal("Users", insert.Table);
			Assert.Equal(2, insert.Values.Count);
			Assert.Equal("Ane", insert.Values[0]);
            Assert.Equal("Lete", insert.Values[1]);
		}

		[Fact]
		public void Parse_Insert_WithoutComma_ShouldReturnNull()
		{
			var result1 = MiniSQLParser.Parse("INSERT INTO Users VALUES (Ane,'21','1.68')");
			var result2= MiniSQLParser.Parse("INSERT INTO Users VALUES (Ane,21,1.68)");
			var result3 = MiniSQLParser.Parse("INSERT INTO Users VALUES ('Ane',21,'1.68')");
			var result4 = MiniSQLParser.Parse("INSERT INTO Users VALUES ('Ane','21',1.68)");



			Assert.Null(result1);
			Assert.Null(result2);
			Assert.Null(result3);
			Assert.Null(result4);


		}

		#endregion

		#region parse update tests

		[Fact]
        public void Parse_Update_ShouldParse()
        {
            var result = MiniSQLParser.Parse("UPDATE TestTable SET age='13' WHERE age='12'");
            Assert.IsType<Update>(result);

            var update = (Update)result;
            Assert.Equal("TestTable", update.Table);
            Assert.Equal("age", update.Columns[0].ColumnName);
            Assert.Equal("13", update.Columns[0].Value);
            Assert.Equal("age", update.Where.ColumnName);
            Assert.Equal("=", update.Where.Operator);
            Assert.Equal("12", update.Where.LiteralValue);
        }

        [Fact]
        public void Parse_Update_AcceptsMultipleSetValues()
        {
            var result = MiniSQLParser.Parse("UPDATE TestTable SET age='13',height='1.70',name='Ane' WHERE age='12'");
            Assert.IsType<Update>(result);

            var update = (Update)result;
            Assert.Equal("TestTable", update.Table);
            Assert.Equal("age", update.Columns[0].ColumnName);
            Assert.Equal("13", update.Columns[0].Value);
            Assert.Equal("height", update.Columns[1].ColumnName);
            Assert.Equal("1.70", update.Columns[1].Value);
            Assert.Equal("name", update.Columns[2].ColumnName);
            Assert.Equal("Ane", update.Columns[2].Value);

            Assert.Equal("age", update.Where.ColumnName);
            Assert.Equal("=", update.Where.Operator);
            Assert.Equal("12", update.Where.LiteralValue);
        }

        [Fact]
        public void Parse_Update_AcceptsSpaces()
        {
            var result1 = MiniSQLParser.Parse("UPDATE            TestTable SET age='13' WHERE age='12'");
            var result2 = MiniSQLParser.Parse("UPDATE TestTable           SET age='13' WHERE age='12'");
            var result3 = MiniSQLParser.Parse("UPDATE TestTable SET age='13',height='1.70'           WHERE age='12'");
            Assert.IsType<Update>(result1);
            Assert.IsType<Update>(result2);
            Assert.IsType<Update>(result3);

            var update = (Update)result1;
            Assert.Equal("TestTable", update.Table);
            Assert.Equal("age", update.Columns[0].ColumnName);
            Assert.Equal("13", update.Columns[0].Value);
            Assert.Equal("age", update.Where.ColumnName);
            Assert.Equal("=", update.Where.Operator);
            Assert.Equal("12", update.Where.LiteralValue);

            update = (Update)result2;
            Assert.Equal("TestTable", update.Table);
            Assert.Equal("age", update.Columns[0].ColumnName);
            Assert.Equal("13", update.Columns[0].Value);
            Assert.Equal("age", update.Where.ColumnName);
            Assert.Equal("=", update.Where.Operator);
            Assert.Equal("12", update.Where.LiteralValue);

            update = (Update)result3;
            Assert.Equal("TestTable", update.Table);
            Assert.Equal("age", update.Columns[0].ColumnName);
            Assert.Equal("13", update.Columns[0].Value);
            Assert.Equal("height", update.Columns[1].ColumnName);
            Assert.Equal("1.70", update.Columns[1].Value);
            Assert.Equal("age", update.Where.ColumnName);
            Assert.Equal("=", update.Where.Operator);
            Assert.Equal("12", update.Where.LiteralValue);
        }

        [Fact]
		public void Parse_Update_WithoutComma_ShouldReturnNull()
		{
			var result1 = MiniSQLParser.Parse("UPDATE TestTable SET age=13 WHERE age=12");
			var result2 = MiniSQLParser.Parse("UPDATE TestTable SET age=13 WHERE age='12'");
			var result3 = MiniSQLParser.Parse("UPDATE TestTable SET age='13' WHERE age=12");


			Assert.Null(result1);
			Assert.Null(result2);
			Assert.Null(result3);
		}

		[Fact]
        public void Parse_Update_AcceptsStringsAndDoublesInWhere()
        {
            var result = MiniSQLParser.Parse("UPDATE TestTable SET name='Ane' WHERE name='Jon'");
            var result1 = MiniSQLParser.Parse("UPDATE TestTable SET height='1.80' WHERE height='1.70'");
            Assert.IsType<Update>(result);
            Assert.IsType<Update>(result1);

            var update = (Update)result;
            Assert.Equal("TestTable", update.Table);
            Assert.Equal("name", update.Columns[0].ColumnName);
            Assert.Equal("Ane", update.Columns[0].Value);
            Assert.Equal("name", update.Where.ColumnName);
            Assert.Equal("=", update.Where.Operator);
            Assert.Equal("Jon", update.Where.LiteralValue);


            var update1 = (Update)result1;
            Assert.Equal("height", update1.Columns[0].ColumnName);
            Assert.Equal("1.80", update1.Columns[0].Value);
            Assert.Equal("height", update1.Where.ColumnName);
            Assert.Equal("=", update1.Where.Operator);
            Assert.Equal("1.70", update1.Where.LiteralValue);
        }

        [Fact]
        public void Parse_Update_NotAcceptedSyntax()
        {
            var result1 = MiniSQLParser.Parse("update TestTable SET age='13' WHERE age='12'");
            var result2 = MiniSQLParser.Parse("");
            var result3 = MiniSQLParser.Parse("UPDATE TABLE TestTable SET age='13' WHERE age='12'");
            var result4 = MiniSQLParser.Parse("UPDATE 1TestTable SET age='13' WHERE age='12'");
            var result5 = MiniSQLParser.Parse("UPDATE TestTable SET age='13' kkj WHERE age='12'");
            var result6 = MiniSQLParser.Parse("UPDATE TestTable SET age='13' WHERE age='12' and year='2015'");
            var result7 = MiniSQLParser.Parse("UPDATE TestTable SET age='13' WHERE age='12' AJLDJF");

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
            Assert.Null(result4);
            Assert.Null(result5);
            Assert.Null(result6);
            Assert.Null(result7);



        }

       

        #endregion

        #region parse select tests
        [Fact]
        public void Parse_Select_SingleColumn_NoWhere_ShouldParse()
        {
            var result = MiniSQLParser.Parse("SELECT Name FROM People");

            Assert.NotNull(result);
            Assert.IsType<Select>(result);

            var select = (Select)result;
            Assert.Equal("People", select.Table);
            Assert.Equal(new List<string> { "Name" }, select.Columns);
            Assert.Null(select.Where);
        }

        [Fact]
        public void Parse_Select_MultipleColumns_NoWhere_ShouldParse()
        {
            var result = MiniSQLParser.Parse("SELECT Name,Age FROM People");

            Assert.NotNull(result);
            Assert.IsType<Select>(result);

            var select = (Select)result;
            Assert.Equal("People", select.Table);
            Assert.Equal(new List<string> { "Name", "Age" }, select.Columns);
            Assert.Null(select.Where);
        }

        [Fact]
        public void Parse_Select_SingleColumn_WithWhereEquals_ShouldParse()
        {
            var result = MiniSQLParser.Parse("SELECT Name FROM People WHERE Age='25'");

            Assert.NotNull(result);
            Assert.IsType<Select>(result);

            var select = (Select)result;
            Assert.Equal("People", select.Table);
            Assert.Equal(new List<string> { "Name" }, select.Columns);
            Assert.NotNull(select.Where);
            Assert.Equal("Age", select.Where.ColumnName);
            Assert.Equal("=", select.Where.Operator);
            Assert.Equal("25", select.Where.LiteralValue);
        }

        [Fact]
        public void Parse_Select_WithWhereLessThan_ShouldParse()
        {
            var result = MiniSQLParser.Parse("SELECT Name FROM People WHERE Age<'30'");

            Assert.NotNull(result);
            Assert.IsType<Select>(result);

            var select = (Select)result;
            Assert.NotNull(select.Where);
            Assert.Equal("<", select.Where.Operator);
        }

        [Fact]
        public void Parse_Select_WithWhereGreaterThan_ShouldParse()
        {
            var result = MiniSQLParser.Parse("SELECT Name FROM People WHERE Age>'18'");

            Assert.NotNull(result);
            Assert.IsType<Select>(result);

            var select = (Select)result;
            Assert.NotNull(select.Where);
            Assert.Equal(">", select.Where.Operator);
        }

        [Fact]
        public void Parse_Select_WithWhereStringValue_ShouldParse()
        {
            var result = MiniSQLParser.Parse("SELECT Name FROM People WHERE Name='Mario'");

            Assert.NotNull(result);
            Assert.IsType<Select>(result);

            var select = (Select)result;
            Assert.NotNull(select.Where);
            Assert.Equal("Name", select.Where.ColumnName);
            Assert.Equal("Mario", select.Where.LiteralValue);
        }

        [Fact]
        public void Parse_Select_InvalidSyntax_ShouldReturnNull()
        {
            Assert.Null(MiniSQLParser.Parse("SELECT FROM People"));
            Assert.Null(MiniSQLParser.Parse("SELECT Name People"));
            Assert.Null(MiniSQLParser.Parse("select Name FROM People"));
        }
        [Fact]
        public void Parse_Select_SpacesBetweenColumns_ShouldReturnNull()
        {
            Assert.Null(MiniSQLParser.Parse("SELECT age, name FROM People"));
            Assert.Null(MiniSQLParser.Parse("SELECT Name ,age People"));
            Assert.Null(MiniSQLParser.Parse("select Name,  FROM People"));
        }
        #endregion

        #region parse create security profile tests

        [Fact]
        public void Parse_CreateSecurityProfile_ShouldParse()
        {
            var result1 = MiniSQLParser.Parse("CREATE SECURITY PROFILE User");
            var result2 = MiniSQLParser.Parse("CREATE SECURITY PROFILE AdminProfile");
            var result3 = MiniSQLParser.Parse("CREATE   SECURITY   PROFILE   Admin");
            Assert.IsType<CreateSecurityProfile>(result1);
            Assert.IsType<CreateSecurityProfile>(result2);
            Assert.IsType<CreateSecurityProfile>(result3);

            var createSecurityProfile = (CreateSecurityProfile)result1;
            Assert.Equal("User", createSecurityProfile.ProfileName);
            createSecurityProfile = (CreateSecurityProfile)result2;
            Assert.Equal("AdminProfile", createSecurityProfile.ProfileName);
            createSecurityProfile = (CreateSecurityProfile)result3;
            Assert.Equal("Admin", createSecurityProfile.ProfileName);

        }

        [Fact]
        public void Parse_CreateSecurityProfile_NotAcceptedSyntax_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("create SECURITY PROFILE Admin");
            var result2 = MiniSQLParser.Parse("CREATE SECURITY PROFILE");
            var result3 = MiniSQLParser.Parse("CREATE SECURITY PROFILE ");
            var result4 = MiniSQLParser.Parse(" CREATE SECURITY PROFILE Admin");
            var result5 = MiniSQLParser.Parse("CREATE SECURITY PROFILE Admin ");

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
            Assert.Null(result4);
            Assert.Null(result5);
        }

        [Fact]
        public void Parse_CreateSecurityProfile_NumbersOrUnderscores_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("CREATE SECURITY PROFILE Admin1");
            var result2 = MiniSQLParser.Parse("CREATE SECURITY PROFILE Admin_Profile");
            var result3 = MiniSQLParser.Parse("CREATE SECURITY PROFILE 1Admin");

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
        }

        #endregion
        #region Parse Grant Tests
        [Fact]
        public void Parse_Grant_AllCorrect_ShouldParse()
        {
            var result1 = MiniSQLParser.Parse("GRANT DELETE ON Table TO User");
            var grant1 = (Grant)result1;
            Assert.Equal("DELETE", grant1.PrivilegeName);
			Assert.Equal("Table", grant1.TableName);
			Assert.Equal("User", grant1.ProfileName);

			var result2 = MiniSQLParser.Parse("GRANT INSERT ON Table TO User");
			var grant2 = (Grant)result2;
			Assert.Equal("INSERT", grant2.PrivilegeName);
			Assert.Equal("Table", grant2.TableName);
			Assert.Equal("User", grant2.ProfileName);

			var result3 = MiniSQLParser.Parse("GRANT SELECT ON Table TO User");
			var grant3 = (Grant)result3;
			Assert.Equal("SELECT", grant3.PrivilegeName);
			Assert.Equal("Table", grant3.TableName);
			Assert.Equal("User", grant3.ProfileName);

			var result4 = MiniSQLParser.Parse("GRANT UPDATE ON Table TO User");
			var grant4 = (Grant)result4;
			Assert.Equal("UPDATE", grant4.PrivilegeName);
			Assert.Equal("Table", grant4.TableName);
			Assert.Equal("User", grant4.ProfileName);
		}
        [Fact]
		public void Parse_Grant_Spaces_ShouldParse()
		{
			var result1 = MiniSQLParser.Parse("GRANT       DELETE          ON Table TO User");
			var grant1 = (Grant)result1;
			Assert.Equal("DELETE", grant1.PrivilegeName);
			Assert.Equal("Table", grant1.TableName);
			Assert.Equal("User", grant1.ProfileName);

			var result2 = MiniSQLParser.Parse("GRANT       INSERT ON Table       TO         User");
			var grant2 = (Grant)result2;
			Assert.Equal("INSERT", grant2.PrivilegeName);
			Assert.Equal("Table", grant2.TableName);
			Assert.Equal("User", grant2.ProfileName);

			var result3 = MiniSQLParser.Parse("GRANT       SELECT ON          Table TO User");
			var grant3 = (Grant)result3;
			Assert.Equal("SELECT", grant3.PrivilegeName);
			Assert.Equal("Table", grant3.TableName);
			Assert.Equal("User", grant3.ProfileName);

			var result4 = MiniSQLParser.Parse("GRANT    UPDATE       ON      Table      TO       User");
			var grant4 = (Grant)result4;
			Assert.Equal("UPDATE", grant4.PrivilegeName);
			Assert.Equal("Table", grant4.TableName);
			Assert.Equal("User", grant4.ProfileName);
		}
        [Fact]
        public void Parse_Grant_IncorrectCapitalization_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("Grant DELETE ON Table TO User");
            Assert.Null(result1);

			var result2 = MiniSQLParser.Parse("GRANT Insert ON Table TO User");
			Assert.Null(result2);

			var result3 = MiniSQLParser.Parse("GRANT DELETE on Table TO User");
			Assert.Null(result3);

			var result4 = MiniSQLParser.Parse("GRANT DELETE ON Table To User");
			Assert.Null(result4);
		}
        [Fact]
		public void Parse_Grant_NotAcceptedSyntax_ShouldReturnNull()
		{
			var result1 = MiniSQLParser.Parse("GRANT DELETE ON Table TO User 4");
			Assert.Null(result1);

			var result2 = MiniSQLParser.Parse("GRANT INSERT ON Table TO Use r");
			Assert.Null(result2);
		}
		[Fact]
		public void Parse_Grant_EmptyValues_ShouldReturnNull()
		{
			var result1 = MiniSQLParser.Parse("GRANT ON Table TO User");
			Assert.Null(result1);

			var result2 = MiniSQLParser.Parse("GRANT INSERT ON TO User");
			Assert.Null(result2);

			var result3 = MiniSQLParser.Parse("GRANT DELETE ON Table TO");
			Assert.Null(result3);

			var result4 = MiniSQLParser.Parse("GRANT DELETE Table TO User");
			Assert.Null(result4);
		}
        #endregion

        #region Parse Revoke Tests
        [Fact]
        public void Parse_Revoke_Correct_ShouldParse()
        {
            var result1 = MiniSQLParser.Parse("REVOKE DELETE ON Table TO User");
			var revoke1 = Assert.IsType<Revoke>(result1);
            Assert.Equal("DELETE", revoke1.PrivilegeName);
            Assert.Equal("Table", revoke1.TableName);
            Assert.Equal("User", revoke1.ProfileName);

			var result2 = MiniSQLParser.Parse("REVOKE INSERT ON Table TO User");
			var revoke2 = Assert.IsType<Revoke>(result2);
			Assert.Equal("INSERT", revoke2.PrivilegeName);
			Assert.Equal("Table", revoke2.TableName);
			Assert.Equal("User", revoke2.ProfileName);

			var result3 = MiniSQLParser.Parse("REVOKE SELECT ON Table TO User");
			var revoke3 = Assert.IsType<Revoke>(result3);
			Assert.Equal("SELECT", revoke3.PrivilegeName);
			Assert.Equal("Table", revoke3.TableName);
			Assert.Equal("User", revoke3.ProfileName);

			var result4 = MiniSQLParser.Parse("REVOKE UPDATE ON Table TO User");
			var revoke4 = Assert.IsType<Revoke>(result4);
			Assert.Equal("UPDATE", revoke4.PrivilegeName);
			Assert.Equal("Table", revoke4.TableName);
			Assert.Equal("User", revoke4.ProfileName);
		}
		[Fact]
		public void Parse_Revoke_Spaces_ShouldParse()
		{
			var result1 = MiniSQLParser.Parse("REVOKE         DELETE ON Table TO User");
			var revoke1 = Assert.IsType<Revoke>(result1);
			Assert.Equal("DELETE", revoke1.PrivilegeName);
			Assert.Equal("Table", revoke1.TableName);
			Assert.Equal("User", revoke1.ProfileName);

			var result2 = MiniSQLParser.Parse("REVOKE INSERT           ON Table TO User");
			var revoke2 = Assert.IsType<Revoke>(result2);
			Assert.Equal("INSERT", revoke2.PrivilegeName);
			Assert.Equal("Table", revoke2.TableName);
			Assert.Equal("User", revoke2.ProfileName);

			var result3 = MiniSQLParser.Parse("REVOKE SELECT ON Table           TO User");
			var revoke3 = Assert.IsType<Revoke>(result3);
			Assert.Equal("SELECT", revoke3.PrivilegeName);
			Assert.Equal("Table", revoke3.TableName);
			Assert.Equal("User", revoke3.ProfileName);

			var result4 = MiniSQLParser.Parse("REVOKE      UPDATE        ON Table TO            User");
			var revoke4 = Assert.IsType<Revoke>(result4);
			Assert.Equal("UPDATE", revoke4.PrivilegeName);
			Assert.Equal("Table", revoke4.TableName);
			Assert.Equal("User", revoke4.ProfileName);
		}
		[Fact]
		public void Parse_Revoke_AcceptedSyntax_ShouldParse()
		{
			var result1 = MiniSQLParser.Parse("REVOKE DELETE ON table TO User");
			var revoke1 = Assert.IsType<Revoke>(result1);
			Assert.Equal("DELETE", revoke1.PrivilegeName);
			Assert.Equal("table", revoke1.TableName);
			Assert.Equal("User", revoke1.ProfileName);

			var result2 = MiniSQLParser.Parse("REVOKE INSERT ON Table1 TO User123");
			var revoke2 = Assert.IsType<Revoke>(result2);
			Assert.Equal("INSERT", revoke2.PrivilegeName);
			Assert.Equal("Table1", revoke2.TableName);
			Assert.Equal("User123", revoke2.ProfileName);

			var result3 = MiniSQLParser.Parse("REVOKE SELECT ON Table_Name TO User_Name");
			var revoke3 = Assert.IsType<Revoke>(result3);
			Assert.Equal("SELECT", revoke3.PrivilegeName);
			Assert.Equal("Table_Name", revoke3.TableName);
			Assert.Equal("User_Name", revoke3.ProfileName);
		}

		[Fact]
        public void Parse_Revoke_NotAcceptedSyntax_ShouldReturnNUll()
        {
            var result1 = MiniSQLParser.Parse("REVOKE DELETE ON Table TO User 1");
            Assert.Null(result1);

			var result2 = MiniSQLParser.Parse("REVOKE DELETE ON Table TO Us er");
			Assert.Null(result2);

			var result3 = MiniSQLParser.Parse("DELETE ON Table TO User");
			Assert.Null(result3);

			var result4 = MiniSQLParser.Parse("REVOKE DELETE Table TO User");
			Assert.Null(result4);

			var result5 = MiniSQLParser.Parse("REVOKE DELETE ON Table User");
			Assert.Null(result5);
		}
        [Fact]
		public void Parse_Revoke_IncorrectCapitalization_ShouldReturnNull()
		{
			var result1 = MiniSQLParser.Parse("Revoke DELETE ON Table TO User");
			Assert.Null(result1);

			var result2 = MiniSQLParser.Parse("REVOKE Insert ON Table TO User");
			Assert.Null(result2);

			var result3 = MiniSQLParser.Parse("REVOKE SELECT on Table TO User"); 
            Assert.Null(result3);

			var result4 = MiniSQLParser.Parse("REVOKE UPDATE ON Table to User");
            Assert.Null(result4);
		}
        [Fact]
		public void Parse_Revoke_IncorrectPrivileges_ShouldReturnNull()
		{
			var result1 = MiniSQLParser.Parse("REVOKE REMOVE ON Table TO User");
			Assert.Null(result1);

			var result2 = MiniSQLParser.Parse("REVOKE UPGRADE ON Table TO User");
			Assert.Null(result2);

			var result3 = MiniSQLParser.Parse("REVOKE SET ON Table TO User");
			Assert.Null(result3);
		}
		[Fact]
		public void Parse_Revoke_EmptyValue_ShouldReturnNull()
		{
			var result1 = MiniSQLParser.Parse("REVOKE ON Table TO User");
			Assert.Null(result1);

			var result2 = MiniSQLParser.Parse("REVOKE SELECT ON TO User");
			Assert.Null(result2);

			var result3 = MiniSQLParser.Parse("REVOKE SELECT ON Table TO");
			Assert.Null(result3);
		}
		[Fact]
		public void Parse_Revoke_IncorrectOrder_ShouldReturnNull()
		{
			var result1 = MiniSQLParser.Parse("SELECT REVOKE ON Table TO User");
			Assert.Null(result1);

			var result2 = MiniSQLParser.Parse("REVOKE ON SELECT Table TO User");
			Assert.Null(result2);

			var result3 = MiniSQLParser.Parse("REVOKE TO User SELECT ON Table");
			Assert.Null(result3);

			var result4 = MiniSQLParser.Parse("REVOKE TO User ON Table SELECT");
			Assert.Null(result4);
		}
		[Fact]
		public void Parse_Revoke_NullOrEmptyInput_ShouldReturnNUll()
		{
			var result1 = MiniSQLParser.Parse("");
			Assert.Null(result1);

			var result2 = MiniSQLParser.Parse("Null");
			Assert.Null(result2);
		}

		#endregion

	}
}
