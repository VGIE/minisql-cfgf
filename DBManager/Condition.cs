using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DbManager;

namespace DbManager
{
    public class Condition
    {
        public string ColumnName { get; private set; }
        public string Operator { get; private set; }
        public string LiteralValue { get; private set; }

        public Condition(string column, string op, string literalValue)
        {
            //TODO DEADLINE 1A: Initialize member variables
            if (string.IsNullOrWhiteSpace(column) || string.IsNullOrWhiteSpace(op)) { return; }
            ColumnName = column;
            Operator = op;
            LiteralValue = literalValue;
        }


        public bool IsTrue(string value, ColumnDefinition.DataType type)
        {
            //TODO DEADLINE 1A: return true if the condition is true for this value
            //Depending on the type of the column, the comparison should be different:
            //"ab" < "cd
            //"9" > "10"
            //9 < 10
            //Convert first the strings to the appropriate type and then compare (depending on the operator of the condition)
            if (value == null || LiteralValue == null || Operator == null) { return false; }

            int cmp;

            if (type == ColumnDefinition.DataType.String)
            {
                cmp = string.Compare(value, LiteralValue);
            }
            else if (type == ColumnDefinition.DataType.Int ||  type == ColumnDefinition.DataType.Double)
            {
                double v, lit;

                if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out v)) { return false; }

                if (!double.TryParse(LiteralValue, NumberStyles.Float, CultureInfo.InvariantCulture, out lit)) { return false; }

                cmp = v.CompareTo(lit);
            }
            else { return false; }

            switch (Operator)
            {
                case "=": return cmp == 0;
                case "!=": return cmp != 0;
                case "<": return cmp < 0;
                case "<=": return cmp <= 0;
                case ">": return cmp > 0;
                case ">=": return cmp >= 0;
                default: return false;
            }            
        }
    }
}