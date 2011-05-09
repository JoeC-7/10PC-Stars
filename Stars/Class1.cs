using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OmegaWeb.util
{
    public class CsvWriter
    {
        public static string WriteToString(DataTable table, bool header, bool quoteall)
        {
            StringWriter writer = new StringWriter();
            WriteToStream(writer, table, header, quoteall);
            return writer.ToString();
        }

        public static string WriteToString(IEnumerable enumerable, String[] propertiesToSkip, bool header, bool quoteall, IList modifiers)
        {
            DataTable table = GetDataTableFromIEnumerable(enumerable, propertiesToSkip, modifiers);
            StringWriter writer = new StringWriter();
            WriteToStream(writer, table, header, quoteall);
            return writer.ToString();
        }

        public static void WriteToStream(TextWriter stream, IEnumerable enumerable, String[] propertiesToSkip, bool header, bool quoteall, IList modifiers)
        {
            DataTable table = GetDataTableFromIEnumerable(enumerable, propertiesToSkip, modifiers);
            WriteToStream(stream, table, header, quoteall);
        }

        public static void WriteToStream(TextWriter stream, DataTable table, bool header, bool quoteall)
        {
            if (header)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, table.Columns[i].Caption, quoteall);
                    if (i < table.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write('\n');
                }
            }
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, row[i], quoteall);
                    if (i < table.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write('\n');
                }
            }
        }

        private static void WriteItem(TextWriter stream, object item, bool quoteall)
        {
            if (item == null)
                return;
            string s = item.ToString();
            if (quoteall || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
                stream.Write("\"" + s.Replace("\"", "\"\"") + "\"");
            else
                stream.Write(s);
        }

        public static DataTable GetDataTableFromIEnumerable(IEnumerable aIEnumerable, String[] propertiesToSkip, IList modifiers)
        {
            DataTable _returnTable = new DataTable();

            if (aIEnumerable.Count() > 0)
            {
                //Creates the table structure looping in the in the first element of the list
                object _baseObj = aIEnumerable.First();

                Type objectType = _baseObj.GetType();

                PropertyInfo[] properties = objectType.GetProperties();

                DataColumn _col;

                foreach (PropertyInfo property in properties)
                {
                    if (!propertiesToSkip.Contains((String)property.Name))
                    {
                        _col = new DataColumn();
                        _col.ColumnName = (string)property.Name;
                        if (property.PropertyType == typeof(DateTime?))
                        {
                            _col.DataType = typeof(DateTime);
                        }
                        else if (property.PropertyType == typeof(Int32?))
                        {
                            _col.DataType = typeof(Int32);
                        }
                        else
                        {
                            _col.DataType = property.PropertyType;
                        }
                        _returnTable.Columns.Add(_col);
                    }
                }

                foreach (CsvModifier modifier in modifiers)
                {
                    _col = new DataColumn();
                    _col.ColumnName = modifier.columnName();
                    _col.DataType = modifier.getDataType();
                    _returnTable.Columns.Add(_col);
                }

                //Adds the rows to the table
                DataRow _row;

                foreach (object objItem in aIEnumerable)
                {

                    _row = _returnTable.NewRow();


                    foreach (PropertyInfo property in properties)
                    {
                        Object value = property.GetValue(objItem, null);
                        if (value != null && !propertiesToSkip.Contains(property.Name))
                        {
                            _row[property.Name] = value;
                        }
                    }

                    foreach (CsvModifier modifier in modifiers)
                    {
                        _row[modifier.columnName()] = modifier.getData(objItem);
                    }

                    _returnTable.Rows.Add(_row);
                }
            }
            return _returnTable;
        }
    }
}