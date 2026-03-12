using DbManager.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            if (columnDefinitions == null || values == null) { return; }
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
            if (Values == null || ColumnDefinitions == null || columnName == null) { return; }
            for (int i = 0; i < ColumnDefinitions.Count(); i++)
            {
                if (ColumnDefinitions[i].Name == columnName)
                {
                    if (i >= Values.Count) { return; }
                    Values[i] = value;
                    return;
                }
            }
        }

        public string GetValue(string columnName)
        {
            //TODO DEADLINE 1.A: Given a column name, return the value in that column
            if (Values == null || ColumnDefinitions == null || columnName == null) { return null; }
            for (int i = 0; i < ColumnDefinitions.Count(); i++)
            {
                if (ColumnDefinitions[i].Name == columnName)
                {
                    if (i >= Values.Count) { return null; }
                    return Values[i];
                }
            }
            return null;       
        }

    public bool IsTrue(Condition condition)
    {
      //TODO DEADLINE 1.A: Given a condition (column name, operator and literal value, return whether it is true or not
      //for this row. Check Condition.IsTrue method
      if (Values == null || condition == null) { return false; }
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

            if (value == null) {return null; }
            return value.Replace(Delimiter, DelimiterEncoded);
            
        }

        private static string Decode(string value)
        {
            //TODO DEADLINE 1.C: Decode the value doing the opposite of Encode()
            if (value == null)
            {
                return null;
            }

             return value.Replace(DelimiterEncoded, Delimiter);
        }

        public string AsText()
        {
            //TODO DEADLINE 1.C: Return the row as string with all values separated by the delimiter

            string result = ""; 
            if (Values == null) { return null; }
            for (int i = 0; i < Values.Count(); i++) 
            { 
                string encodedValue = Encode(Values[i]); 
                if (i == Values.Count - 1) 
                { 
                    result = result + encodedValue; 
                } 
                else 
                { 
                    result = result + encodedValue + Delimiter; 
                } 
            }
            return result;

        }

        public static Row Parse(List<ColumnDefinition> columns, string value)
        {
            //TODO DEADLINE 1.C: Parse a rowReturn the row as string with all values separated by the delimiter
            if (value == null)
            {
                return new Row(columns, null);
            }

            string[] parts = value.Split(new[] { Delimiter }, StringSplitOptions.None);

            if (parts.Length != columns.Count)
            {
                return new Row(columns, null);
            }

            List<String> decodedValues = new List<String>();

            foreach (string part in parts)
            {
                decodedValues.Add(Decode(part));
            }
            return new Row(columns, decodedValues);   
        }
    }
}
