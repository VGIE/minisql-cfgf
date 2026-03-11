using DbManager;

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
    }
}
