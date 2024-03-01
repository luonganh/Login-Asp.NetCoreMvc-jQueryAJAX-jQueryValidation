namespace Asp.NetCore.Shared.Helpers
{
    public static class ExcelToObjectHelper
    {
        public static bool IsValid<T>(this ExcelWorksheet worksheet, List<PropertyNameResolver> resolvers) where T : new()
        {
            // List of all the column names
            var header = worksheet.Cells.GroupBy(cell => cell.Start.Row).First();

            // Get the properties from the type your are populating
            var properties = typeof(T).GetProperties().ToList();

            var start = worksheet.Dimension.Start;
            var end = worksheet.Dimension.End;

            // Resulting list
            // Iterate the rows starting at row 2 (ie start.Row + 1)

            if (resolvers.Any(c => GetColumnIndex(worksheet, c.ColumnName) < 0))
            {
                return false;
            }

            return true;
        }
        private static int GetColumnIndex(ExcelWorksheet ws, string columnName)
        {
            var start = ws.Dimension.Start;
            var end = ws.Dimension.End;
            for (int col = start.Column; col <= end.Column; col++)
            {
                var column = ws.Cells[start.Row, col].Text.Trim();
                if (column.ToLower() == columnName.ToLower())
                    return col;
            }

            return -1;
        }
        public static IEnumerable<T> ConvertTableToObjects<T>(this ExcelTable table) where T : new()
        {
            //DateTime Conversion
            var convertDateTime = new Func<double, DateTime>(excelDate =>
            {
                if (excelDate < 1)
                    throw new ArgumentException("Excel dates cannot be smaller than 0.");

                var dateOfReference = new DateTime(1900, 1, 1);

                if (excelDate > 60d)
                    excelDate = excelDate - 2;
                else
                    excelDate = excelDate - 1;
                return dateOfReference.AddDays(excelDate);
            });

            //Get the properties of T
            var tprops = (new T())
                .GetType()
                .GetProperties()
                .ToList();

            //Get the cells based on the table address
            var groups = table.WorkSheet.Cells[table.Address.Start.Row, table.Address.Start.Column, table.Address.End.Row, table.Address.End.Column]
                .GroupBy(cell => cell.Start.Row)
                .ToList();

            //Assume the second row represents column data types (big assumption!)
            var types = groups
                .Skip(1)
                .First()
                .Select(rcell => rcell.Value.GetType())
                .ToList();

            //Assume first row has the column names
            var colnames = groups
                .First()
                .Select((hcell, idx) => new { Name = hcell.Value.ToString(), index = idx })
                .Where(o => tprops.Select(p => p.Name).Contains(o.Name))
                .ToList();

            //Everything after the header is data
            var rowvalues = groups
                .Skip(1) //Exclude header
                .Select(cg => cg.Select(c => c.Value).ToList());


            //Create the collection container
            var collection = rowvalues
                .Select(row =>
                {
                    var tnew = new T();
                    colnames.ForEach(colname =>
                    {
                        //This is the real wrinkle to using reflection - Excel stores all numbers as double including int
                        var val = row[colname.index];
                        var type = types[colname.index];
                        var prop = tprops.First(p => p.Name == colname.Name);

                        //If it is numeric it is a double since that is how excel stores all numbers
                        if (type == typeof(double))
                        {
                            //Unbox it
                            var unboxedVal = (double)val;

                            //FAR FROM A COMPLETE LIST!!!
                            if (prop.PropertyType == typeof(Int32))
                                prop.SetValue(tnew, (int)unboxedVal);
                            else if (prop.PropertyType == typeof(double))
                                prop.SetValue(tnew, unboxedVal);
                            else if (prop.PropertyType == typeof(DateTime))
                                prop.SetValue(tnew, convertDateTime(unboxedVal));
                            else
                                throw new NotImplementedException(String.Format("Type '{0}' not implemented yet!", prop.PropertyType.Name));
                        }
                        else
                        {
                            //Its a string
                            prop.SetValue(tnew, val);
                        }
                    });

                    return tnew;
                });

            //Send it back
            return collection;
        }

        public static IEnumerable<T> ToArray<T>(this ExcelWorksheet worksheet, List<PropertyNameResolver> resolvers) where T : new()
        {
            // List of all the column names
            var header = worksheet.Cells.GroupBy(cell => cell.Start.Row).First();

            // Get the properties from the type your are populating
            var properties = typeof(T).GetProperties().ToList();


            var start = worksheet.Dimension.Start;
            var end = worksheet.Dimension.End;

            // Resulting list
            var list = new List<T>();

            // Iterate the rows starting at row 2 (ie start.Row + 1)
            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                var instance = new T();
                for (int col = start.Column; col <= end.Column; col++)
                {
                    object value = worksheet.Cells[row, col].Text;

                    // Get the column name zero based (ie col -1)
                    var column = (string)header.Skip(col - 1).FirstOrDefault()?.Value.ToString().Trim();

                    // Gets the corresponding property to set
                    var property = properties.Property(resolvers, column);

                    try
                    {
                        var propertyName = property.PropertyType.IsGenericType
                          ? property.PropertyType.GetGenericArguments().First().FullName
                          : property.PropertyType.FullName;

                        // Implement setter code as needed. 
                        switch (propertyName)
                        {
                            case "System.String":
                                property.SetValue(instance, Convert.ToString(value));
                                break;
                            case "System.Int32":
                                property.SetValue(instance, Convert.ToInt32(value));
                                break;
                            case "System.Decimal":
                                property.SetValue(instance, Convert.ToDecimal(value));
                                break;
                            case "System.Double":
                                property.SetValue(instance, Convert.ToDouble(value));
                                break;
                            case "System.DateTime":
                                DateTime date = DateTime.Now;
                                if (DateTime.TryParseExact((string)value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                {
                                    property.SetValue(instance, date);
                                }
                                else if (DateTime.TryParseExact((string)value, "d/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                {
                                    property.SetValue(instance, date);
                                }
                                else if (DateTime.TryParseExact((string)value, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                {
                                    property.SetValue(instance, date);
                                }
                                else if (DateTime.TryParseExact((string)value, "dd/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                {
                                    property.SetValue(instance, date);
                                }
                                else if (DateTime.TryParseExact((string)value, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                {
                                    property.SetValue(instance, date);
                                }
                                else if (DateTime.TryParseExact((string)value, "M/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                {
                                    property.SetValue(instance, date);
                                }
                                else
                                {
                                    value = worksheet.Cells[row, col].Value;
                                    if (value is DateTime)
                                    {
                                        property.SetValue(instance, value);
                                    }
                                    else
                                        property.SetValue(instance, FromExcelSerialDate(Convert.ToInt32(value)));
                                }

                                break;
                            case "System.Boolean":
                                property.SetValue(instance, (int)value == 1);
                                break;
                            default:
                                {
                                    property.SetValue(instance, property.PropertyType.BaseType == typeof(Enum) ? Enum.Parse(property.PropertyType, value.ToString()) : value);
                                    break;
                                }
                        }
                    }
                    catch (Exception e)
                    {
                        // instance property is empty because there was a problem.
                    }

                }
                list.Add(instance);
            }
            return list;
        }

        // Utility function taken from the above post's inline function.
        public static DateTime FromExcelSerialDate(int excelDate)
        {
            if (excelDate < 1)
                throw new ArgumentException("Excel dates cannot be smaller than 0.");

            var dateOfReference = new DateTime(1900, 1, 1);

            if (excelDate > 60d)
                excelDate = excelDate - 2;
            else
                excelDate = excelDate - 1;
            return dateOfReference.AddDays(excelDate);
        }

        public static PropertyInfo Property(this List<PropertyInfo> properties, List<PropertyNameResolver> resolvers, string column)
        {
            if (resolvers == null)
                throw new InvalidOperationException("Resolvers can not be null");

            var resolver = resolvers.FirstOrDefault(c => c.ColumnName.Trim() == column.Trim());

            if (resolver != null)
            {
                return properties.FirstOrDefault(c => c.Name.Trim() == resolver.FieldName.Trim());
            }

            return properties.FirstOrDefault();
        }
    }

    public class PropertyNameResolver
    {
        public PropertyNameResolver() { }

        public PropertyNameResolver(string fieldName, string columnName)
        {
            FieldName = fieldName;
            ColumnName = columnName;
        }

        public string FieldName { set; get; }
        public string ColumnName { set; get; }
    }
}
