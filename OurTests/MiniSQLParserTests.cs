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
            var result = MiniSQLParser.Parse("CREATE TABLE TestTable (Name String,Age Int,Height Double)");
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
        public void Parse_CreateTable_AcceptsValidIdentifiers()
        {
            var result = MiniSQLParser.Parse("CREATE TABLE Tes2t_Tab3le (Col_1 String,Age2 Int)");
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
            var result = MiniSQLParser.Parse("CREATE   TABLE   TestTable   (Name   String,   Age   Int,   Height   Double)");
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
            var result3 = MiniSQLParser.Parse("CREATE TABL TestTable (Name String)");
            Assert.Null(result3);
            var result4 = MiniSQLParser.Parse("create table TestTable (Name String)");
            Assert.Null(result4);
            var result5 = MiniSQLParser.Parse(" ");
            Assert.Null(result5);
            var result6 = MiniSQLParser.Parse("CREATE TABLE 1TestTable (Name String)");
            Assert.Null(result6);
            var result7 = MiniSQLParser.Parse("CREATE TABLE TestTable (1Name String)");
            Assert.Null(result7);
            var result8 = MiniSQLParser.Parse("CREATE TABLE TestTable (Name String Age Int)");
            Assert.Null(result8);
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
            var result = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age=12");
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
            var result1 = MiniSQLParser.Parse("DELETE FROM T3_stTable WHERE a23 > x");
            var result2 = MiniSQLParser.Parse("DELETE FROM  Test6Table WHERE NaMe=AlFonSo");
            var result3 = MiniSQLParser.Parse("DELETE FROM Te_st4Tabl_e WHERE yeAr<2025");

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
            var result1 = MiniSQLParser.Parse("delete from TestTable where age=12");
            var result2 = MiniSQLParser.Parse(" ");
            var result3 = MiniSQLParser.Parse("DELETE TABLE TestTable WHER age=12");
            var result4 = MiniSQLParser.Parse("Delete Table TestTable where age=12");
            var result5 = MiniSQLParser.Parse("DELET TABLE TestTable WHERE age=12");
            var result6 = MiniSQLParser.Parse("DELETE TABL TestTable where age=12");

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
            var result = MiniSQLParser.Parse("DELETE FROM 1TestTable WHERE age=12");

            Assert.Null(result);
        }
        [Fact]
        public void Parse_Delete_CombinedOperators_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("DELETE FROM 1TestTable WHERE age=>12");
            var result2 = MiniSQLParser.Parse("DELETE FROM 1TestTable WHERE age=<12");
            var result3 = MiniSQLParser.Parse("DELETE FROM 1TestTable WHERE age>=12");
            var result4 = MiniSQLParser.Parse("DELETE FROM 1TestTable WHERE age<=12");

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
            Assert.Null(result4);
        }
        [Fact]
        public void Parse_Delete_SpacesAtTheBeginingAndInTheEnd_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse(" DELETE FROM TestTable WHERE age=>12");
            var result2 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age=<12 ");

            Assert.Null(result1);
            Assert.Null(result2);
        }
        [Fact]
        public void Parse_Delete_TableOr__ColumnOr_OperatorOr_LiteralValue_Missing_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("DELETE FROM WHERE age=12");//table
            var result2 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE=12");//column
            var result3 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age 12");//operator
            var result4 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age=");//literalvalue

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
            Assert.Null(result4);
        }
        [Fact]
        public void Parse_Delete_WithExtraTextAtTheEnd_ShouldReturnNull()
        {
            var result1 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age=12 AJLDJF");
            var result2 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age=12;");
            var result3 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age=12 and year=2015");

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
        }
        [Fact]
        public void Parse_Delete_LiteralValueOrColumnWithNumbersAndUnderscores_ShouldParse()
        {
            var result = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age=value_123");//because we are using \w*
            var result1 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE a_g2e=12");

            Assert.NotNull(result);
            Assert.NotNull(result1);

            Assert.IsType<Delete>(result);
            Assert.IsType<Delete>(result1);

            var delete = (Delete)result;
            Assert.Equal("value_123", delete.Where.LiteralValue);

            delete = (Delete)result1;
            Assert.Equal("a_g2e", delete.Where.ColumnName);
        }
        [Fact]
        public void Parse_Delete_SpacesAfterBeforeOperator_ShouldParse()
        {
            var result = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age= 12");
            var result1 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age =12");
            var result2 = MiniSQLParser.Parse("DELETE FROM TestTable WHERE age = 12");

            Assert.NotNull(result);
            Assert.NotNull(result1);
            Assert.NotNull(result2);

            Assert.IsType<Delete>(result);
            Assert.IsType<Delete>(result1);
            Assert.IsType<Delete>(result2);

            var delete = (Delete)result;
            Assert.Equal("TestTable", delete.Table);
            Assert.Equal("age", delete.Where.ColumnName);
            Assert.Equal("=", delete.Where.Operator);
            Assert.Equal("12", delete.Where.LiteralValue);

            delete = (Delete)result1;
            Assert.Equal("TestTable", delete.Table);
            Assert.Equal("age", delete.Where.ColumnName);
            Assert.Equal("=", delete.Where.Operator);
            Assert.Equal("12", delete.Where.LiteralValue);

            delete = (Delete)result2;
            Assert.Equal("TestTable", delete.Table);
            Assert.Equal("age", delete.Where.ColumnName);
            Assert.Equal("=", delete.Where.Operator);
            Assert.Equal("12", delete.Where.LiteralValue);
        }
        #endregion

    }
}
