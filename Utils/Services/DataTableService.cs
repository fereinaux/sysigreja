using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Utils.Services
{  
    public class DatatableService : IDatatableService
    {
        // Reflection used to identify DateTime fields for formatting
        private readonly Type _typeNullDatetime = typeof(DateTime?);
        private readonly Type _typeDateTime = typeof(DateTime);


        public MemoryStream GenerateExcel<T>(IEnumerable<T> source)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Planilha");

                if (source.Count() > 0)
                {

                    //Do NOT include Col1
                    var mi = typeof(T)
                        .GetProperties()
                        .Where(pi => pi.Name != "Id")
                        .Select(pi => (MemberInfo)pi)
                        .ToArray();

                    ws.Cells.LoadFromCollection(source
                                                , true,
                                                OfficeOpenXml.Table.TableStyles.Light1
                                                , BindingFlags.Public | BindingFlags.Instance
                                                , mi);
                }

                PropertyInfo[] _modelProperties = typeof(T).GetProperties().Where(pi => pi.Name != "Id").ToArray();

                FormatDateTimeAsDate(ws, _modelProperties);

                MemoryStream result = new MemoryStream();
                pck.SaveAs(result);
                result.Position = 0;
                return result;
            }
        }

        private void FormatDateTimeAsDate(ExcelWorksheet worksheet, PropertyInfo[] _modelProperties)
        {
            for (var j = 0; j < _modelProperties.Length; j++)
            {
                if (_modelProperties[j].PropertyType == _typeNullDatetime
                        && DateTimeFormatInfo.CurrentInfo != null)
                    worksheet.Column(j + 1).Style.Numberformat.Format =
                        DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                if (_modelProperties[j].PropertyType != _typeDateTime) continue;
                if (DateTimeFormatInfo.CurrentInfo != null)
                    worksheet.Column(j + 1).Style.Numberformat.Format =
                        DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
            }
        }

    }
}