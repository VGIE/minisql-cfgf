using DbManager.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DbManager
{
    public class Row
    {
        private List<ColumnDefinition> ColumnDefinitions = new List<ColumnDefinition>();
        public List<string> Values { get; set; }

        public Row(List<ColumnDefinition> columnDefinitions, List<string> values)
        {
            //TODO DEADLINE 1.A: Initialize member variables
            //Check that the number of column definitions matches the number of values, otherwise return without doing anything
            if (columnDefinitions.Count() != values.Count()) { return; }
            //Check that there is at least one column definition, otherwise return without doing anything
            if (columnDefinitions.Count() == 0) { return; }
            ColumnDefinitions = columnDefinitions;
            Values = values;
        }

        public void SetValue(string columnName, string value)
        {
            //TODO DEADLINE 1.A: Given a column name and value, change the value in that column
            for (int i = 0; i < ColumnDefinitions.Count(); i++)
            {
                if (ColumnDefinitions[i].Name == columnName)
                {
                    Values[i] = value;
                }
            }
        }

        public string GetValue(string columnName)
        {
            //TODO DEADLINE 1.A: Given a column name, return the value in that column
            for (int i = 0; i < ColumnDefinitions.Count(); i++)
            {
                if (ColumnDefinitions[i].Name == columnName)
                {
                    return Values[i];
                }
            }
            return null;       
        }

    public bool IsTrue(Condition condition)
    {
      //TODO DEADLINE 1.A: Given a condition (column name, operator and literal value, return whether it is true or not
      //for this row. Check Condition.IsTrue method
      foreach (ColumnDefinition column in ColumnDefinitions)
      {
        if (column.Name == condition.ColumnName)
        {
          int index = ColumnDefinitions.IndexOf(column);
          string value = Values[index];
          return condition.IsTrue(value, column.Type);
        }
      }
      return false;
    }

    private const string Delimiter = ":";
        private const string DelimiterEncoded = "[SEPARATOR]";

        private static string Encode(string value)
        {
            //TODO DEADLINE 1.C: Encode the delimiter in value

            
            return null;
            
        }

        private static string Decode(string value)
        {
            //TODO DEADLINE 1.C: Decode the value doing the opposite of Encode()
            
            return null;
            
        }

        public string AsText()
        {
            //TODO DEADLINE 1.C: Return the row as string with all values separated by the delimiter
            
            return null;
            
        }

        public static Row Parse(List<ColumnDefinition> columns, string value)
        {
            //TODO DEADLINE 1.C: Parse a rowReturn the row as string with all values separated by the delimiter
            
            return null;
            
        }
    }
}
