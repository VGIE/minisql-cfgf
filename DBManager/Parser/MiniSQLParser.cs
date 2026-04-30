using DbManager.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DbManager
{
    public class MiniSQLParser
    {
        public static MiniSqlQuery Parse(string miniSQLQuery)
        {
            //TODO DEADLINE 2
            const string selectPattern = @"^SELECT\s+([a-zA-Z]\w*(?:,[a-zA-Z]\w*)*)\s+FROM\s+([a-zA-Z]\w*)(?:\s+WHERE\s+([a-zA-Z]\w*)([<=>])'([^']+)')?$";

            const string insertPattern = @"^INSERT\s+INTO\s+([a-zA-Z]\w*)\s+VALUES\s*\(('[^']+'(?:,'[^']+')*)\)$";

            const string dropTablePattern = @"^DROP\s+TABLE\s+([a-zA-Z]\w*)$";

            //Note: The parsing of CREATE TABLE should accept empty columns "()"
            //And then, an execution error should be given if a CreateTable without columns is executed
            const string createTablePattern = @"^CREATE\s+TABLE\s+([a-zA-Z]\w*)\s*\((.*)\)$";

            const string updateTablePattern = @"^UPDATE\s+([a-zA-Z]\w*)\s+SET\s+(.+)\s+WHERE\s+(\w+)([<=>])'([^']+)'$";

            const string deletePattern = @"^DELETE\s+FROM\s+([a-zA-Z]\w*)\s+WHERE\s+(\w+)([<=>])'([^']+)'$";


            //TODO DEADLINE 4
            const string createSecurityProfilePattern = @"^CREATE\s+SECURITY\s+PROFILE\s+([a-zA-Z]+)$";

            const string dropSecurityProfilePattern = null;
            
            const string grantPattern = @"^GRANT\s+(DELETE|INSERT|SELECT|UPDATE)\s+ON\s+(\w+)\s+TO\s+(\w+)\s*$";
            
            const string revokePattern = @"^REVOKE\s+(DELETE|INSERT|SELECT|UPDATE)\s+ON\s+(\w+)\s+TO\s+(\w+)\s*$";

            const string addUserPattern = @"^ADD\s+USER\s+\((?<user>[a-zA-Z]+)[,](?<password>[a-zA-Z]+)[,](?<profile>[a-zA-Z]+)\)$";
            var addUser = new Regex(addUserPattern, RegexOptions.None);

            const string deleteUserPattern = @"^DELETE\s+USER\s+(?<user>[a-zA-Z]+)$";
            var deleteUser = new Regex(deleteUserPattern, RegexOptions.None);


      //TODO DEADLINE 2
      //Parse query using the regular expressions above one by one. If there is a match, create an instance of the query with the parsed parameters
      //For example, if the query is a "SELECT ...", there should be a match with selectPattern. We would create and return an instance of Select
      //initialized with the table name, the columns, and (possibly) an instance of Condition.
      //If there is no match, it means there is a syntax error. We will return null.

      // select
      var match = Regex.Match(miniSQLQuery, selectPattern);
            if (match.Success)
            {
                List<string> columns = CommaSeparatedNames(match.Groups[1].Value)
                    .Select(c => c.Trim())
                    .ToList();
                string tableName = match.Groups[2].Value;
                Condition condition = null;
                if (match.Groups[3].Value != "")
                    condition = new Condition(match.Groups[3].Value, match.Groups[4].Value, match.Groups[5].Value);
                return new Select(tableName, columns, condition);
            }

            //drop table
            match = Regex.Match(miniSQLQuery, dropTablePattern);
            if (match.Success)
            {
                var table = match.Groups[1].Value;
                return new DropTable(table);
            }

            // create table
            match = Regex.Match(miniSQLQuery, createTablePattern);
            if (match.Success)
            {
                const string TYPE_TEXT = "TEXT";
				const string TYPE_INT = "INT";
				const string TYPE_DOUBLE = "DOUBLE";


				string tableName = match.Groups[1].Value;
                string columnsText = match.Groups[2].Value;
                List<ColumnDefinition> columns = new List<ColumnDefinition>();

                if (columnsText != "")
                {
                    if (!Regex.IsMatch(columnsText, @"^[a-zA-Z]\w*\s+(TEXT|INT|DOUBLE)(,[a-zA-Z]\w*\s+(TEXT|INT|DOUBLE))*$"))
                    {
                        return null;
                    }
                    List<string> columnParts = CommaSeparatedNames(columnsText);
                    foreach (string columnPart in columnParts)
                    {
                        Match columnMatch = Regex.Match(columnPart, @"^([a-zA-Z]\w*)\s+(TEXT|INT|DOUBLE)$");
                        if (!columnMatch.Success)
                        {
                            return null;
                        }

                        string columnName = columnMatch.Groups[1].Value;
                        string typeText = columnMatch.Groups[2].Value;

                        ColumnDefinition.DataType type;
                        if (typeText == TYPE_TEXT)
                        {
                            type = ColumnDefinition.DataType.String;
                        }
                        else if (typeText == TYPE_INT)
                        {
                            type = ColumnDefinition.DataType.Int;
                        }
                        else if (typeText == TYPE_DOUBLE)
                        {
                            type = ColumnDefinition.DataType.Double;
                        }
                        else 
                        {
                            return null;
                        }

                        columns.Add(new ColumnDefinition(type, columnName));
                    }
                }
                return new CreateTable(tableName, columns);
            }
            //delete
            match = Regex.Match(miniSQLQuery, deletePattern);
            if (match.Success)
            {
                var table = match.Groups[1].Value;
                var condition = new Condition(match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value);
                return new Delete(table, condition);
            }
			//insert
			match = Regex.Match(miniSQLQuery, insertPattern); 
            if (match.Success)
			{
				string tableName = match.Groups[1].Value; 
                string valuesText = match.Groups[2].Value; 

                List<string> valuesComma = CommaSeparatedNames(valuesText);
                foreach (string value in valuesComma) 
                {
                    Match valueMatch = Regex.Match(value, @"^\s*'[^']*'\s*$");
                    if (!valueMatch.Success)
                    {
                        return null;
                    }
                }
                List<String> values = RemoveComillas(valuesComma);
				return new Insert(tableName, values);
			}

            //update
            match = Regex.Match(miniSQLQuery, updateTablePattern);
            if (match.Success)
            {
                string tableName = match.Groups[1].Value;
                string valuesText = match.Groups[2].Value.TrimEnd();

                List<SetValue> setValues = new List<SetValue>();

                foreach (string setPart in CommaSeparatedNames(valuesText))
                {
                    Match setMatch = Regex.Match(setPart, @"^([a-zA-Z]\w*)='([^']+)'$");
                    if (!setMatch.Success)
                    {
                        return null;
                    }
                    string columnName= setMatch.Groups[1].Value;
                    string value = setMatch.Groups[2].Value;

                    setValues.Add(new SetValue(columnName, value));
                }
         
                string whereValue = match.Groups[5].Value;
                Condition updateCondition = new Condition(match.Groups[3].Value, match.Groups[4].Value, whereValue);
                return new Update(tableName, setValues, updateCondition);
            }

            //TODO DEADLINE 4
            //Do the same for the security queries (CREATE SECURITY PROFILE, ...)
            
            // create security profile
            match = Regex.Match(miniSQLQuery, createSecurityProfilePattern);
            if (match.Success)
            {
                return new CreateSecurityProfile(match.Groups[1].Value);
            }

          //add user
          var trimmedQuery = miniSQLQuery.Trim();
          var addUserMatch = addUser.Match(trimmedQuery);
          if (addUserMatch.Success)
          {
            var user = addUserMatch.Groups["user"].Value;
            var password = addUserMatch.Groups["password"].Value;
            var profile = addUserMatch.Groups["profile"].Value;

            return new AddUser(user, password, profile);
          }

          //delete user
          var deleteUserMatch = deleteUser.Match(trimmedQuery);
          if (deleteUserMatch.Success)
          {
            var user = deleteUserMatch.Groups["user"].Value;

            return new DeleteUser(user);
          }
            //Grant
            var matchGrant = Regex.Match(miniSQLQuery, grantPattern);
            if(matchGrant.Success)
            {
                string PrivilegeName = matchGrant.Groups[1].Value;
				string TableName = matchGrant.Groups[2].Value;
				string ProfileName = matchGrant.Groups[3].Value;

                return new Grant(PrivilegeName, TableName, ProfileName);


			}

            //Revoke
            var matchRevoke = Regex.Match(miniSQLQuery, revokePattern);
            if (matchRevoke.Success)
            {
                string privilegeName = matchRevoke.Groups[1].Value;
				string tableName = matchRevoke.Groups[2].Value;
				string profileName = matchRevoke.Groups[3].Value;

                if (string.IsNullOrEmpty(privilegeName) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(profileName))
                {
                    return null;
                }

				return new Revoke(privilegeName, tableName, profileName);

			}

			return null;
        }


        static List<string> CommaSeparatedNames(string text)
        {
            string[] textParts = text.Split(",", System.StringSplitOptions.RemoveEmptyEntries);
            List<string> commaSeparator = new List<string>();
            for(int i=0; i < textParts.Length; i++)
            {
                commaSeparator.Add(textParts[i]);
            }
            return commaSeparator;
        }

        static List<String> RemoveComillas(List<string> values)
        {
            List<string> result = new List<string>();

            foreach (string value in values)
            { 
                string i = value.Substring(1, value.Length - 2);
                i = i.Trim();
                result.Add(i);
            }
            return result;
        }

	}
}
