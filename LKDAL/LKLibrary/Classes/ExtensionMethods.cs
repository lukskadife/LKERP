using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using System.Reflection;

namespace LKLibrary.Classes
{
    public static class ExtensionMethods
    {
        public static string CheckString(this string _str)
        {
            _str = _str.Replace("'", "");
            _str = _str.Replace("!", "");
            _str = _str.Replace("--", "");
            _str = _str.Replace("/", "");
            _str = _str.Replace("(", "");
            _str = _str.Replace(")", "");
            _str = _str.Replace("[", "");
            _str = _str.Replace("]", "");
            _str = _str.Replace("%", "");
            _str = _str.Replace("&", "");
            _str = _str.Replace("<", "");
            _str = _str.Replace(">", "");
            _str = _str.Replace("-", "");
            _str = _str.Replace("+", "");
            _str = _str.Replace("*", "");
            _str = _str.Replace("=", "");
            _str = _str.Replace("#", "");
            _str = _str.Replace("$", "");
            _str = _str.Replace("?", "");
            _str = _str.Replace(",", "");
            _str = _str.Replace(".", "");

            return _str;

        }

        /// <summary>
        /// Verilen string fonksiyonunu execute eder.
        /// </summary>
        /// <param name="expr">Fonksiyon</param>
        /// <returns></returns>
        public static double Evaluate(string expr)
        {
            if (string.IsNullOrEmpty(expr)) return 0;

            Stack<String> stack = new Stack<String>();

            string value = "";
            for (int i = 0; i < expr.Length; i++)
            {
                String s = expr.Substring(i, 1);
                char chr = s.ToCharArray()[0];

                if (!char.IsDigit(chr) && chr != '.' && value != "" && chr!= ',')
                {
                    stack.Push(value);
                    value = "";
                }

                if (s.Equals("("))
                {

                    string innerExp = "";
                    i++;
                    int bracketCount = 0;
                    for (; i < expr.Length; i++)
                    {
                        s = expr.Substring(i, 1);

                        if (s.Equals("("))
                            bracketCount++;

                        if (s.Equals(")"))
                            if (bracketCount == 0)
                                break;
                            else
                                bracketCount--;


                        innerExp += s;
                    }

                    stack.Push(Evaluate(innerExp).ToString());

                }
                else if (s.Equals("+")) stack.Push(s);
                else if (s.Equals("-")) stack.Push(s);
                else if (s.Equals("*")) stack.Push(s);
                else if (s.Equals("/")) stack.Push(s);
                else if (s.Equals("sqrt")) stack.Push(s);
                else if (s.Equals(")"))
                {
                }
                else if (char.IsDigit(chr) || chr == '.' || chr ==',')
                {
                    value += s;

                    if (value.Split('.').Length > 2)
                        throw new Exception("Invalid decimal.");

                    if (i == (expr.Length - 1))
                        stack.Push(value);

                }
                else
                    throw new Exception("Invalid character.");

            }


            double result = 0;
            while (stack.Count >= 3)
            {

                double right = Convert.ToDouble(stack.Pop());
                string op = stack.Pop();
                double left = Convert.ToDouble(stack.Pop());

                if (op == "+") result = left + right;
                else if (op == "+") result = left + right;
                else if (op == "-") result = left - right;
                else if (op == "*") result = left * right;
                else if (op == "/") result = left / right;

                stack.Push(result.ToString());
            }


            return Convert.ToDouble(stack.Pop());
        }

        public static bool StringSayisalMi(this string _myString)
        {
            bool sonuc = true;

            string girisStr = _myString;

            try
            {
                Convert.ToDouble(girisStr);
            }
            catch
            {
                return false;
            }

            return sonuc;
        }

        /// <summary>
        /// Verilen dizinin boyutunu mb cinsinden hesaplar
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static double GetDirectorySize(this DirectoryInfo directory)
        {
            // 1
            // Get array of all file names.
            string[] a = Directory.GetFiles(directory.FullName, "*.*");

            // 2
            // Calculate total bytes of all files in a loop.
            double b = 0;
            foreach (string name in a)
            {
                // 3
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }

            foreach (string dirPath in Directory.GetDirectories(directory.FullName))
                b += new DirectoryInfo(dirPath).GetDirectorySize();
            // 4
            // Return total size
            return ((double) b );
        }

        public static DataTable ToDataTable<T>(this List<T> items) where T: class
        {

            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                
                DataColumn clm ;
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    clm = new DataColumn(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else clm = new DataColumn(prop.Name, prop.PropertyType);
                dataTable.Columns.Add(clm);
            }

            foreach (T item in items)
            {

                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);

                }

                dataTable.Rows.Add(values);

            }

            return dataTable;

        }

        public static List<T> ConvertTo<T>(IList<DataRow> rows)
        {
            List<T> list = null;
            if (rows != null)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<T> ConvertTo<T>(this DataTable table)
        {
            if (table == null)
                return null;

            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                rows.Add(row);

            return ConvertTo<T>(rows);
        }

        //Convert DataRow into T Object
        public static T CreateItem<T>(DataRow row)
        {
            string columnName;
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    columnName = column.ColumnName;
                    //Get property with same columnName
                    PropertyInfo prop = obj.GetType().GetProperty(columnName);
                    try
                    {
                        //Get value for the column
                        object value = (row[columnName].GetType() == typeof(DBNull))
                        ? null : row[columnName];
                        //Set property value
                        prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        throw;
                        //Catch whatever here
                    }
                }
            }
            return obj;
        }
        
        /// <summary>
        /// ByteArray türünden bir veriyi dosyaya çeviren fonksiyon
        /// </summary>
        /// <param name="_FileName">Dosyanın tam ismi (d:\newDocument.txt gibi)</param>
        /// <param name="_ByteArray">Byte array türünden veri</param>
        /// <returns>dosya kaydetme başarılı ise return true, değilse return false</returns>
        public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // dosya okumak için açılıyor
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());

                return false;

            }
        }

        /// <summary>
        /// Verilen dosya byte array 'e çevirir
        /// </summary>
        /// <param name="_FileName">Byte array 'e çevrilecek dosyanın tam adı (d:\newDocument.txt gibi)</param>
        /// <returns>Byte Array</returns>
        public static byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;

            try
            {
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }
            catch (Exception _Exception)
            {
                string s = _Exception.Message;
                return null;
            }

            return _Buffer;
        }

        public static string ToDateString(this DateTime date)
        {
            return date.Year.ToString() + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0');
        }
    }

}
