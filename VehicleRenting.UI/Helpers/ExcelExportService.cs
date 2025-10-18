using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace VehicleRenting.UI.Services
{

    public class ExcelExportService 
    {
        public byte[] ExportToExcel<T>(List<T> data, string sheetName, Dictionary<string, Func<T, object>> columnMappings)
        {
            ExcelPackage.License.SetNonCommercialPersonal("Personal Usage");

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add(sheetName);

                int colIndex = 1;
                foreach (var header in columnMappings.Keys)
                {
                    ws.Cells[1, colIndex].Value = header;
                    colIndex++;
                }

                int rowIndex = 2;
                foreach (var item in data)
                {
                    colIndex = 1;
                    foreach (var map in columnMappings.Values)
                    {
                        ws.Cells[rowIndex, colIndex].Value = map(item);
                        colIndex++;
                    }
                    rowIndex++;
                }

                ws.Cells.AutoFitColumns();
                return package.GetAsByteArray();
            }
        }
    }
}
