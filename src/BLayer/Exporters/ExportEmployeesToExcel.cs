﻿using BLayer.DTO;
using BLayer.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BLayer.Exporters
{
    public class ExportEmployeesToExcel : IExportToExcel<EmployeeDTO>
    {
        public MemoryStream Export(IEnumerable<EmployeeDTO> collection)
        {
            var stream = new MemoryStream();
            Type type = typeof(ProjectDTO);
            PropertyInfo[] propertyInfos = type.GetProperties();
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add($"{type.Name}");
            int column = 1;
            int row = 1;
            int count = 1;
            int i;

            foreach (var property in propertyInfos)
            {
                excelWorksheet.Cells[row, count].Value = property.Name;
                count++;
            }
            row++;

            foreach (var item in collection)
            {
                var totalTasks = item.EmployeeTasks.Count;
                excelWorksheet.Cells[row, column].Value = item.Id;
                excelWorksheet.Cells[row, column, row + totalTasks - 1, column].Merge = true;
                column++;
                excelWorksheet.Cells[row, column].Value = item.Name;
                excelWorksheet.Cells[row, column, row + totalTasks - 1, column].Merge = true;
                column++;
                excelWorksheet.Cells[row, column].Value = item.Surname;
                excelWorksheet.Cells[row, column, row + totalTasks - 1, column].Merge = true;
                column++;
                excelWorksheet.Cells[row, column].Value = item.SecondName;
                excelWorksheet.Cells[row, column, row + totalTasks - 1, column].Merge = true;
                column++;
                excelWorksheet.Cells[row, column].Value = item.Position;
                excelWorksheet.Cells[row, column, row + totalTasks - 1, column].Merge = true;
                column++;
                excelWorksheet.Cells[row, column].Value = item.FilePath;
                excelWorksheet.Cells[row, column, row + totalTasks - 1, column].Merge = true;
                column++;
                foreach (var employeeTasks in item.EmployeeTasks)
                {
                    excelWorksheet.Cells[row, column].Value = employeeTasks.Task.Name;
                    row++;
                }
                column = 1;
            }

            excelPackage.SaveAs(stream);
            return stream;
        }
    }
}