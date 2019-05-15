﻿using System;
using System.IO;
using System.Linq;
using EPPlusTest.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;

namespace EPPlusTest.Drawing.Slicers
{
	[TestClass]
	public class PivotTableSlicerRefreshTest
	{
		#region Test Methods
		[TestMethod]
		[DeploymentItem(@"..\..\Workbooks\PivotTables\Slicers.xlsx")]
		public void RefreshSlicerAllValuesSelected()
		{
			var file = new FileInfo("Slicers.xlsx");
			Assert.IsTrue(file.Exists);
			using (var newFile = new TempTestFile())
			{
				using (var package = new ExcelPackage(file))
				{
					package.Workbook.PivotCacheDefinitions.First().UpdateData();
					package.SaveAs(newFile.File);
				}
				using (var package = new ExcelPackage(newFile.File))
				{
					var sheetName = "Sheet3";
					TestHelperUtility.ValidateWorksheet(newFile.File, sheetName, new[]
					{
						new ExpectedCellValue(sheetName, 3, 2, "Sum of Amount"),
						new ExpectedCellValue(sheetName, 3, 3, "Column Labels"),
						new ExpectedCellValue(sheetName, 4, 3, "2012"),
						new ExpectedCellValue(sheetName, 4, 5, "2013"),
						new ExpectedCellValue(sheetName, 4, 9, "Grand Total"),
						new ExpectedCellValue(sheetName, 5, 2, "Row Labels"),
						new ExpectedCellValue(sheetName, 5, 3, "MIDDLE"),
						new ExpectedCellValue(sheetName, 5, 4, "START"),
						new ExpectedCellValue(sheetName, 5, 5, "END"),
						new ExpectedCellValue(sheetName, 5, 6, "PURCHASES"),
						new ExpectedCellValue(sheetName, 5, 7, "SALES"),
						new ExpectedCellValue(sheetName, 5, 8, "START"),
						new ExpectedCellValue(sheetName, 6, 2, "Entries, January 2013"),
						new ExpectedCellValue(sheetName, 6, 3, null),
						new ExpectedCellValue(sheetName, 6, 4, null),
						new ExpectedCellValue(sheetName, 6, 5, -218303.72),
						new ExpectedCellValue(sheetName, 6, 6, null),
						new ExpectedCellValue(sheetName, 6, 7, 8277.85),
						new ExpectedCellValue(sheetName, 6, 8, 22024.94),
						new ExpectedCellValue(sheetName, 6, 9, -188000.93),
						new ExpectedCellValue(sheetName, 7, 2, "13100"),
						new ExpectedCellValue(sheetName, 7, 3, null),
						new ExpectedCellValue(sheetName, 7, 4, null),
						new ExpectedCellValue(sheetName, 7, 5, null),
						new ExpectedCellValue(sheetName, 7, 6, null),
						new ExpectedCellValue(sheetName, 7, 7, null),
						new ExpectedCellValue(sheetName, 7, 8, 8277.85),
						new ExpectedCellValue(sheetName, 7, 9, 8277.85),
						new ExpectedCellValue(sheetName, 8, 2, "13200"),
						new ExpectedCellValue(sheetName, 8, 3, null),
						new ExpectedCellValue(sheetName, 8, 4, null),
						new ExpectedCellValue(sheetName, 8, 5, 8277.85),
						new ExpectedCellValue(sheetName, 8, 6, null),
						new ExpectedCellValue(sheetName, 8, 7, null),
						new ExpectedCellValue(sheetName, 8, 8, null),
						new ExpectedCellValue(sheetName, 8, 9, 8277.85),
						new ExpectedCellValue(sheetName, 9, 2, "22700"),
						new ExpectedCellValue(sheetName, 9, 3, null),
						new ExpectedCellValue(sheetName, 9, 4, null),
						new ExpectedCellValue(sheetName, 9, 5, null),
						new ExpectedCellValue(sheetName, 9, 6, null),
						new ExpectedCellValue(sheetName, 9, 7, null),
						new ExpectedCellValue(sheetName, 9, 8, 82.28),
						new ExpectedCellValue(sheetName, 9, 9, 82.28),
						new ExpectedCellValue(sheetName, 10, 2, "43100"),
						new ExpectedCellValue(sheetName, 10, 3, null),
						new ExpectedCellValue(sheetName, 10, 4, null),
						new ExpectedCellValue(sheetName, 10, 5, null),
						new ExpectedCellValue(sheetName, 10, 6, null),
						new ExpectedCellValue(sheetName, 10, 7, 8277.85),
						new ExpectedCellValue(sheetName, 10, 8, null),
						new ExpectedCellValue(sheetName, 10, 9, 8277.85),
						new ExpectedCellValue(sheetName, 11, 2, "43200"),
						new ExpectedCellValue(sheetName, 11, 3, null),
						new ExpectedCellValue(sheetName, 11, 4, null),
						new ExpectedCellValue(sheetName, 11, 5, -244909.87),
						new ExpectedCellValue(sheetName, 11, 6, null),
						new ExpectedCellValue(sheetName, 11, 7, null),
						new ExpectedCellValue(sheetName, 11, 8, null),
						new ExpectedCellValue(sheetName, 11, 9, -244909.87),
						new ExpectedCellValue(sheetName, 12, 2, "44100"),
						new ExpectedCellValue(sheetName, 12, 3, null),
						new ExpectedCellValue(sheetName, 12, 4, null),
						new ExpectedCellValue(sheetName, 12, 5, 8277.85),
						new ExpectedCellValue(sheetName, 12, 6, null),
						new ExpectedCellValue(sheetName, 12, 7, null),
						new ExpectedCellValue(sheetName, 12, 8, null),
						new ExpectedCellValue(sheetName, 12, 9, 8277.85),
						new ExpectedCellValue(sheetName, 13, 2, "61100"),
						new ExpectedCellValue(sheetName, 13, 3, null),
						new ExpectedCellValue(sheetName, 13, 4, null),
						new ExpectedCellValue(sheetName, 13, 5, 9336.83),
						new ExpectedCellValue(sheetName, 13, 6, null),
						new ExpectedCellValue(sheetName, 13, 7, null),
						new ExpectedCellValue(sheetName, 13, 8, 9336.83),
						new ExpectedCellValue(sheetName, 13, 9, 18673.66),
						new ExpectedCellValue(sheetName, 14, 2, "61200"),
						new ExpectedCellValue(sheetName, 14, 3, null),
						new ExpectedCellValue(sheetName, 14, 4, null),
						new ExpectedCellValue(sheetName, 14, 5, 427.31),
						new ExpectedCellValue(sheetName, 14, 6, null),
						new ExpectedCellValue(sheetName, 14, 7, null),
						new ExpectedCellValue(sheetName, 14, 8, 183.13),
						new ExpectedCellValue(sheetName, 14, 9, 610.44),
						new ExpectedCellValue(sheetName, 15, 2, "61300"),
						new ExpectedCellValue(sheetName, 15, 3, null),
						new ExpectedCellValue(sheetName, 15, 4, null),
						new ExpectedCellValue(sheetName, 15, 5, null),
						new ExpectedCellValue(sheetName, 15, 6, null),
						new ExpectedCellValue(sheetName, 15, 7, null),
						new ExpectedCellValue(sheetName, 15, 8, 4144.85),
						new ExpectedCellValue(sheetName, 15, 9, 4144.85),
						new ExpectedCellValue(sheetName, 16, 2, "64300"),
						new ExpectedCellValue(sheetName, 16, 3, null),
						new ExpectedCellValue(sheetName, 16, 4, null),
						new ExpectedCellValue(sheetName, 16, 5, 286.31),
						new ExpectedCellValue(sheetName, 16, 6, null),
						new ExpectedCellValue(sheetName, 16, 7, null),
						new ExpectedCellValue(sheetName, 16, 8, null),
						new ExpectedCellValue(sheetName, 16, 9, 286.31),
						new ExpectedCellValue(sheetName, 17, 2, "Opening Entry"),
						new ExpectedCellValue(sheetName, 17, 3, 6358.14),
						new ExpectedCellValue(sheetName, 17, 4, -2804975.21),
						new ExpectedCellValue(sheetName, 17, 5, null),
						new ExpectedCellValue(sheetName, 17, 6, null),
						new ExpectedCellValue(sheetName, 17, 7, null),
						new ExpectedCellValue(sheetName, 17, 8, null),
						new ExpectedCellValue(sheetName, 17, 9, -2798617.07),
						new ExpectedCellValue(sheetName, 18, 2, "11200"),
						new ExpectedCellValue(sheetName, 18, 3, null),
						new ExpectedCellValue(sheetName, 18, 4, 232.02),
						new ExpectedCellValue(sheetName, 18, 5, null),
						new ExpectedCellValue(sheetName, 18, 6, null),
						new ExpectedCellValue(sheetName, 18, 7, null),
						new ExpectedCellValue(sheetName, 18, 8, null),
						new ExpectedCellValue(sheetName, 18, 9, 232.02),
						new ExpectedCellValue(sheetName, 19, 2, "11400"),
						new ExpectedCellValue(sheetName, 19, 3, null),
						new ExpectedCellValue(sheetName, 19, 4, -752562.89),
						new ExpectedCellValue(sheetName, 19, 5, null),
						new ExpectedCellValue(sheetName, 19, 6, null),
						new ExpectedCellValue(sheetName, 19, 7, null),
						new ExpectedCellValue(sheetName, 19, 8, null),
						new ExpectedCellValue(sheetName, 19, 9, -752562.89),
						new ExpectedCellValue(sheetName, 20, 2, "11600"),
						new ExpectedCellValue(sheetName, 20, 3, null),
						new ExpectedCellValue(sheetName, 20, 4, 8277.85),
						new ExpectedCellValue(sheetName, 20, 5, null),
						new ExpectedCellValue(sheetName, 20, 6, null),
						new ExpectedCellValue(sheetName, 20, 7, null),
						new ExpectedCellValue(sheetName, 20, 8, null),
						new ExpectedCellValue(sheetName, 20, 9, 8277.85),
						new ExpectedCellValue(sheetName, 21, 2, "12100"),
						new ExpectedCellValue(sheetName, 21, 3, null),
						new ExpectedCellValue(sheetName, 21, 4, -752562.89),
						new ExpectedCellValue(sheetName, 21, 5, null),
						new ExpectedCellValue(sheetName, 21, 6, null),
						new ExpectedCellValue(sheetName, 21, 7, null),
						new ExpectedCellValue(sheetName, 21, 8, null),
						new ExpectedCellValue(sheetName, 21, 9, -752562.89),
						new ExpectedCellValue(sheetName, 22, 2, "13100"),
						new ExpectedCellValue(sheetName, 22, 3, 232.02),
						new ExpectedCellValue(sheetName, 22, 4, null),
						new ExpectedCellValue(sheetName, 22, 5, null),
						new ExpectedCellValue(sheetName, 22, 6, null),
						new ExpectedCellValue(sheetName, 22, 7, null),
						new ExpectedCellValue(sheetName, 22, 8, null),
						new ExpectedCellValue(sheetName, 22, 9, 232.02),
						new ExpectedCellValue(sheetName, 23, 2, "13200"),
						new ExpectedCellValue(sheetName, 23, 3, 232.02),
						new ExpectedCellValue(sheetName, 23, 4, null),
						new ExpectedCellValue(sheetName, 23, 5, null),
						new ExpectedCellValue(sheetName, 23, 6, null),
						new ExpectedCellValue(sheetName, 23, 7, null),
						new ExpectedCellValue(sheetName, 23, 8, null),
						new ExpectedCellValue(sheetName, 23, 9, 232.02),
						new ExpectedCellValue(sheetName, 24, 2, "13300"),
						new ExpectedCellValue(sheetName, 24, 3, 5662.08),
						new ExpectedCellValue(sheetName, 24, 4, null),
						new ExpectedCellValue(sheetName, 24, 5, null),
						new ExpectedCellValue(sheetName, 24, 6, null),
						new ExpectedCellValue(sheetName, 24, 7, null),
						new ExpectedCellValue(sheetName, 24, 8, null),
						new ExpectedCellValue(sheetName, 24, 9, 5662.08),
						new ExpectedCellValue(sheetName, 25, 2, "14400"),
						new ExpectedCellValue(sheetName, 25, 3, 232.02),
						new ExpectedCellValue(sheetName, 25, 4, null),
						new ExpectedCellValue(sheetName, 25, 5, null),
						new ExpectedCellValue(sheetName, 25, 6, null),
						new ExpectedCellValue(sheetName, 25, 7, null),
						new ExpectedCellValue(sheetName, 25, 8, null),
						new ExpectedCellValue(sheetName, 25, 9, 232.02),
						new ExpectedCellValue(sheetName, 26, 2, "17100"),
						new ExpectedCellValue(sheetName, 26, 3, null),
						new ExpectedCellValue(sheetName, 26, 4, 828.97),
						new ExpectedCellValue(sheetName, 26, 5, null),
						new ExpectedCellValue(sheetName, 26, 6, null),
						new ExpectedCellValue(sheetName, 26, 7, null),
						new ExpectedCellValue(sheetName, 26, 8, null),
						new ExpectedCellValue(sheetName, 26, 9, 828.97),
						new ExpectedCellValue(sheetName, 27, 2, "17200"),
						new ExpectedCellValue(sheetName, 27, 3, null),
						new ExpectedCellValue(sheetName, 27, 4, -558283.32),
						new ExpectedCellValue(sheetName, 27, 5, null),
						new ExpectedCellValue(sheetName, 27, 6, null),
						new ExpectedCellValue(sheetName, 27, 7, null),
						new ExpectedCellValue(sheetName, 27, 8, null),
						new ExpectedCellValue(sheetName, 27, 9, -558283.32),
						new ExpectedCellValue(sheetName, 28, 2, "18100"),
						new ExpectedCellValue(sheetName, 28, 3, null),
						new ExpectedCellValue(sheetName, 28, 4, 828.97),
						new ExpectedCellValue(sheetName, 28, 5, null),
						new ExpectedCellValue(sheetName, 28, 6, null),
						new ExpectedCellValue(sheetName, 28, 7, null),
						new ExpectedCellValue(sheetName, 28, 8, null),
						new ExpectedCellValue(sheetName, 28, 9, 828.97),
						new ExpectedCellValue(sheetName, 29, 2, "18200"),
						new ExpectedCellValue(sheetName, 29, 3, null),
						new ExpectedCellValue(sheetName, 29, 4, 828.97),
						new ExpectedCellValue(sheetName, 29, 5, null),
						new ExpectedCellValue(sheetName, 29, 6, null),
						new ExpectedCellValue(sheetName, 29, 7, null),
						new ExpectedCellValue(sheetName, 29, 8, null),
						new ExpectedCellValue(sheetName, 29, 9, 828.97),
						new ExpectedCellValue(sheetName, 30, 2, "30200"),
						new ExpectedCellValue(sheetName, 30, 3, null),
						new ExpectedCellValue(sheetName, 30, 4, -752562.89),
						new ExpectedCellValue(sheetName, 30, 5, null),
						new ExpectedCellValue(sheetName, 30, 6, null),
						new ExpectedCellValue(sheetName, 30, 7, null),
						new ExpectedCellValue(sheetName, 30, 8, null),
						new ExpectedCellValue(sheetName, 30, 9, -752562.89),
						new ExpectedCellValue(sheetName, 31, 2, "Order 106015"),
						new ExpectedCellValue(sheetName, 31, 3, null),
						new ExpectedCellValue(sheetName, 31, 4, null),
						new ExpectedCellValue(sheetName, 31, 5, null),
						new ExpectedCellValue(sheetName, 31, 6, -53800.14),
						new ExpectedCellValue(sheetName, 31, 7, 53800.14),
						new ExpectedCellValue(sheetName, 31, 8, null),
						new ExpectedCellValue(sheetName, 31, 9, 0),
						new ExpectedCellValue(sheetName, 32, 2, "11600"),
						new ExpectedCellValue(sheetName, 32, 3, null),
						new ExpectedCellValue(sheetName, 32, 4, null),
						new ExpectedCellValue(sheetName, 32, 5, null),
						new ExpectedCellValue(sheetName, 32, 6, -53800.14),
						new ExpectedCellValue(sheetName, 32, 7, null),
						new ExpectedCellValue(sheetName, 32, 8, null),
						new ExpectedCellValue(sheetName, 32, 9, -53800.14),
						new ExpectedCellValue(sheetName, 33, 2, "16210"),
						new ExpectedCellValue(sheetName, 33, 3, null),
						new ExpectedCellValue(sheetName, 33, 4, null),
						new ExpectedCellValue(sheetName, 33, 5, null),
						new ExpectedCellValue(sheetName, 33, 6, 53800.14),
						new ExpectedCellValue(sheetName, 33, 7, null),
						new ExpectedCellValue(sheetName, 33, 8, null),
						new ExpectedCellValue(sheetName, 33, 9, 53800.14),
						new ExpectedCellValue(sheetName, 34, 2, "22400"),
						new ExpectedCellValue(sheetName, 34, 3, null),
						new ExpectedCellValue(sheetName, 34, 4, null),
						new ExpectedCellValue(sheetName, 34, 5, null),
						new ExpectedCellValue(sheetName, 34, 6, -53800.14),
						new ExpectedCellValue(sheetName, 34, 7, 53800.14),
						new ExpectedCellValue(sheetName, 34, 8, null),
						new ExpectedCellValue(sheetName, 34, 9, 0),
						new ExpectedCellValue(sheetName, 35, 2, "Order 106018"),
						new ExpectedCellValue(sheetName, 35, 3, null),
						new ExpectedCellValue(sheetName, 35, 4, null),
						new ExpectedCellValue(sheetName, 35, 5, null),
						new ExpectedCellValue(sheetName, 35, 6, 23672.06),
						new ExpectedCellValue(sheetName, 35, 7, 23672.06),
						new ExpectedCellValue(sheetName, 35, 8, null),
						new ExpectedCellValue(sheetName, 35, 9, 47344.12),
						new ExpectedCellValue(sheetName, 36, 2, "11600"),
						new ExpectedCellValue(sheetName, 36, 3, null),
						new ExpectedCellValue(sheetName, 36, 4, null),
						new ExpectedCellValue(sheetName, 36, 5, null),
						new ExpectedCellValue(sheetName, 36, 6, 11836.03),
						new ExpectedCellValue(sheetName, 36, 7, null),
						new ExpectedCellValue(sheetName, 36, 8, null),
						new ExpectedCellValue(sheetName, 36, 9, 11836.03),
						new ExpectedCellValue(sheetName, 37, 2, "17110"),
						new ExpectedCellValue(sheetName, 37, 3, null),
						new ExpectedCellValue(sheetName, 37, 4, null),
						new ExpectedCellValue(sheetName, 37, 5, null),
						new ExpectedCellValue(sheetName, 37, 6, null),
						new ExpectedCellValue(sheetName, 37, 7, 11836.03),
						new ExpectedCellValue(sheetName, 37, 8, null),
						new ExpectedCellValue(sheetName, 37, 9, 11836.03),
						new ExpectedCellValue(sheetName, 38, 2, "22400"),
						new ExpectedCellValue(sheetName, 38, 3, null),
						new ExpectedCellValue(sheetName, 38, 4, null),
						new ExpectedCellValue(sheetName, 38, 5, null),
						new ExpectedCellValue(sheetName, 38, 6, 11836.03),
						new ExpectedCellValue(sheetName, 38, 7, 11836.03),
						new ExpectedCellValue(sheetName, 38, 8, null),
						new ExpectedCellValue(sheetName, 38, 9, 23672.06),
						new ExpectedCellValue(sheetName, 39, 2, "Grand Total"),
						new ExpectedCellValue(sheetName, 39, 3, 6358.14),
						new ExpectedCellValue(sheetName, 39, 4, -2804975.21),
						new ExpectedCellValue(sheetName, 39, 5, -218303.72),
						new ExpectedCellValue(sheetName, 39, 6, -30128.08),
						new ExpectedCellValue(sheetName, 39, 7, 85750.05),
						new ExpectedCellValue(sheetName, 39, 8, 22024.94),
						new ExpectedCellValue(sheetName, 39, 9, -2939273.88),
					});
				}
			}
		}

		[TestMethod]
		[DeploymentItem(@"..\..\Workbooks\PivotTables\Slicers.xlsx")]
		public void RefreshSlicerMonthDecemberSelected()
		{
			var file = new FileInfo("Slicers.xlsx");
			Assert.IsTrue(file.Exists);
			using (var newFile = new TempTestFile())
			{
				using (var package = new ExcelPackage(file))
				{
					var sheet = package.Workbook.Worksheets["Sheet3"];
					var pivotTable = sheet.PivotTables.First(p => p.Name == "PivotTable1");
					var postingDateSlicerCache = package.Workbook.SlicerCaches.First(s => s.Name == "Slicer_Posting_Date1");
					var cacheField = pivotTable.CacheDefinition.CacheFields.First(c => c.Name == postingDateSlicerCache.SourceName);
					var items = cacheField.IsGroupField ? cacheField.FieldGroup.GroupItems : cacheField.SharedItems;
					foreach (var item in postingDateSlicerCache.TabularDataNode.Items)
					{
						// Only select December for this test.
						if (items[item.AtomIndex].Value == "Dec")
							item.IsSelected = true;
						else
							item.IsSelected = false;
					}

					package.Workbook.PivotCacheDefinitions.First().UpdateData();
					package.SaveAs(newFile.File);
				}
				using (var package = new ExcelPackage(newFile.File))
				{
					var sheetName = "Sheet3";
					TestHelperUtility.ValidateWorksheet(newFile.File, sheetName, new[]
					{
						new ExpectedCellValue(sheetName, 3, 2, "Sum of Amount"),
						new ExpectedCellValue(sheetName, 3, 3, "Column Labels"),
						new ExpectedCellValue(sheetName, 4, 3, "2012"),
						new ExpectedCellValue(sheetName, 4, 5, "Grand Total"),
						new ExpectedCellValue(sheetName, 5, 2, "Row Labels"),
						new ExpectedCellValue(sheetName, 5, 3, "MIDDLE"),
						new ExpectedCellValue(sheetName, 5, 4, "START"),
						new ExpectedCellValue(sheetName, 6, 2, "Opening Entry"),
						new ExpectedCellValue(sheetName, 6, 3, 6358.14),
						new ExpectedCellValue(sheetName, 6, 4, -2804975.21),
						new ExpectedCellValue(sheetName, 6, 5, -2798617.07),
						new ExpectedCellValue(sheetName, 7, 2, "11200"),
						new ExpectedCellValue(sheetName, 7, 3, null),
						new ExpectedCellValue(sheetName, 7, 4, 232.02),
						new ExpectedCellValue(sheetName, 7, 5, 232.02),
						new ExpectedCellValue(sheetName, 8, 2, "11400"),
						new ExpectedCellValue(sheetName, 8, 3, null),
						new ExpectedCellValue(sheetName, 8, 4, -752562.89),
						new ExpectedCellValue(sheetName, 8, 5, -752562.89),
						new ExpectedCellValue(sheetName, 9, 2, "11600"),
						new ExpectedCellValue(sheetName, 9, 3, null),
						new ExpectedCellValue(sheetName, 9, 4, 8277.85),
						new ExpectedCellValue(sheetName, 9, 5, 8277.85),
						new ExpectedCellValue(sheetName, 10, 2, "12100"),
						new ExpectedCellValue(sheetName, 10, 3, null),
						new ExpectedCellValue(sheetName, 10, 4, -752562.89),
						new ExpectedCellValue(sheetName, 10, 5, -752562.89),
						new ExpectedCellValue(sheetName, 11, 2, "13100"),
						new ExpectedCellValue(sheetName, 11, 3, 232.02),
						new ExpectedCellValue(sheetName, 11, 4, null),
						new ExpectedCellValue(sheetName, 11, 5, 232.02),
						new ExpectedCellValue(sheetName, 12, 2, "13200"),
						new ExpectedCellValue(sheetName, 12, 3, 232.02),
						new ExpectedCellValue(sheetName, 12, 4, null),
						new ExpectedCellValue(sheetName, 12, 5, 232.02),
						new ExpectedCellValue(sheetName, 13, 2, "13300"),
						new ExpectedCellValue(sheetName, 13, 3, 5662.08),
						new ExpectedCellValue(sheetName, 13, 4, null),
						new ExpectedCellValue(sheetName, 13, 5, 5662.08),
						new ExpectedCellValue(sheetName, 14, 2, "14400"),
						new ExpectedCellValue(sheetName, 14, 3, 232.02),
						new ExpectedCellValue(sheetName, 14, 4, null),
						new ExpectedCellValue(sheetName, 14, 5, 232.02),
						new ExpectedCellValue(sheetName, 15, 2, "17100"),
						new ExpectedCellValue(sheetName, 15, 3, null),
						new ExpectedCellValue(sheetName, 15, 4, 828.97),
						new ExpectedCellValue(sheetName, 15, 5, 828.97),
						new ExpectedCellValue(sheetName, 16, 2, "17200"),
						new ExpectedCellValue(sheetName, 16, 3, null),
						new ExpectedCellValue(sheetName, 16, 4, -558283.32),
						new ExpectedCellValue(sheetName, 16, 5, -558283.32),
						new ExpectedCellValue(sheetName, 17, 2, "18100"),
						new ExpectedCellValue(sheetName, 17, 3, null),
						new ExpectedCellValue(sheetName, 17, 4, 828.97),
						new ExpectedCellValue(sheetName, 17, 5, 828.97),
						new ExpectedCellValue(sheetName, 18, 2, "18200"),
						new ExpectedCellValue(sheetName, 18, 3, null),
						new ExpectedCellValue(sheetName, 18, 4, 828.97),
						new ExpectedCellValue(sheetName, 18, 5, 828.97),
						new ExpectedCellValue(sheetName, 19, 2, "30200"),
						new ExpectedCellValue(sheetName, 19, 3, null),
						new ExpectedCellValue(sheetName, 19, 4, -752562.89),
						new ExpectedCellValue(sheetName, 19, 5, -752562.89),
						new ExpectedCellValue(sheetName, 20, 2, "Grand Total"),
						new ExpectedCellValue(sheetName, 20, 3, 6358.14),
						new ExpectedCellValue(sheetName, 20, 4, -2804975.21),
						new ExpectedCellValue(sheetName, 20, 5, -2798617.07),
					});
				}
			}
		}

		[TestMethod]
		[DeploymentItem(@"..\..\Workbooks\PivotTables\Slicers.xlsx")]
		public void RefreshSlicerMultiSelectMonthsSelected()
		{
			var file = new FileInfo("Slicers.xlsx");
			Assert.IsTrue(file.Exists);
			using (var newFile = new TempTestFile())
			{
				using (var package = new ExcelPackage(file))
				{
					var sheet = package.Workbook.Worksheets["Sheet3"];
					var pivotTable = sheet.PivotTables.First(p => p.Name == "PivotTable1");
					var postingDateSlicerCache = package.Workbook.SlicerCaches.First(s => s.Name == "Slicer_Posting_Date1");
					var cacheField = pivotTable.CacheDefinition.CacheFields.First(c => c.Name == postingDateSlicerCache.SourceName);
					var items = cacheField.IsGroupField ? cacheField.FieldGroup.GroupItems : cacheField.SharedItems;
					foreach (var item in postingDateSlicerCache.TabularDataNode.Items)
					{
						// Only select December, January, and April for this test.
						if (items[item.AtomIndex].Value == "Dec" || items[item.AtomIndex].Value == "Apr" || items[item.AtomIndex].Value == "Jan")
							item.IsSelected = true;
						else
							item.IsSelected = false;
					}

					package.Workbook.PivotCacheDefinitions.First().UpdateData();
					package.SaveAs(newFile.File);
				}
				using (var package = new ExcelPackage(newFile.File))
				{
					var sheetName = "Sheet3";
					TestHelperUtility.ValidateWorksheet(newFile.File, sheetName, new[]
					{
						new ExpectedCellValue(sheetName, 3, 2, "Sum of Amount"),
						new ExpectedCellValue(sheetName, 3, 3, "Column Labels"),
						new ExpectedCellValue(sheetName, 4, 3, "2012"),
						new ExpectedCellValue(sheetName, 4, 5, "2013"),
						new ExpectedCellValue(sheetName, 4, 9, "Grand Total"),
						new ExpectedCellValue(sheetName, 5, 2, "Row Labels"),
						new ExpectedCellValue(sheetName, 5, 3, "MIDDLE"),
						new ExpectedCellValue(sheetName, 5, 4, "START"),
						new ExpectedCellValue(sheetName, 5, 5, "END"),
						new ExpectedCellValue(sheetName, 5, 6, "PURCHASES"),
						new ExpectedCellValue(sheetName, 5, 7, "SALES"),
						new ExpectedCellValue(sheetName, 5, 8, "START"),
						new ExpectedCellValue(sheetName, 6, 2, "Entries, January 2013"),
						new ExpectedCellValue(sheetName, 6, 3, null),
						new ExpectedCellValue(sheetName, 6, 4, null),
						new ExpectedCellValue(sheetName, 6, 5, -218303.72),
						new ExpectedCellValue(sheetName, 6, 6, null),
						new ExpectedCellValue(sheetName, 6, 7, 8277.85),
						new ExpectedCellValue(sheetName, 6, 8, 22024.94),
						new ExpectedCellValue(sheetName, 6, 9, -188000.93),
						new ExpectedCellValue(sheetName, 7, 2, "13100"),
						new ExpectedCellValue(sheetName, 7, 3, null),
						new ExpectedCellValue(sheetName, 7, 4, null),
						new ExpectedCellValue(sheetName, 7, 5, null),
						new ExpectedCellValue(sheetName, 7, 6, null),
						new ExpectedCellValue(sheetName, 7, 7, null),
						new ExpectedCellValue(sheetName, 7, 8, 8277.85),
						new ExpectedCellValue(sheetName, 7, 9, 8277.85),
						new ExpectedCellValue(sheetName, 8, 2, "13200"),
						new ExpectedCellValue(sheetName, 8, 3, null),
						new ExpectedCellValue(sheetName, 8, 4, null),
						new ExpectedCellValue(sheetName, 8, 5, 8277.85),
						new ExpectedCellValue(sheetName, 8, 6, null),
						new ExpectedCellValue(sheetName, 8, 7, null),
						new ExpectedCellValue(sheetName, 8, 8, null),
						new ExpectedCellValue(sheetName, 8, 9, 8277.85),
						new ExpectedCellValue(sheetName, 9, 2, "22700"),
						new ExpectedCellValue(sheetName, 9, 3, null),
						new ExpectedCellValue(sheetName, 9, 4, null),
						new ExpectedCellValue(sheetName, 9, 5, null),
						new ExpectedCellValue(sheetName, 9, 6, null),
						new ExpectedCellValue(sheetName, 9, 7, null),
						new ExpectedCellValue(sheetName, 9, 8, 82.28),
						new ExpectedCellValue(sheetName, 9, 9, 82.28),
						new ExpectedCellValue(sheetName, 10, 2, "43100"),
						new ExpectedCellValue(sheetName, 10, 3, null),
						new ExpectedCellValue(sheetName, 10, 4, null),
						new ExpectedCellValue(sheetName, 10, 5, null),
						new ExpectedCellValue(sheetName, 10, 6, null),
						new ExpectedCellValue(sheetName, 10, 7, 8277.85),
						new ExpectedCellValue(sheetName, 10, 8, null),
						new ExpectedCellValue(sheetName, 10, 9, 8277.85),
						new ExpectedCellValue(sheetName, 11, 2, "43200"),
						new ExpectedCellValue(sheetName, 11, 3, null),
						new ExpectedCellValue(sheetName, 11, 4, null),
						new ExpectedCellValue(sheetName, 11, 5, -244909.87),
						new ExpectedCellValue(sheetName, 11, 6, null),
						new ExpectedCellValue(sheetName, 11, 7, null),
						new ExpectedCellValue(sheetName, 11, 8, null),
						new ExpectedCellValue(sheetName, 11, 9, -244909.87),
						new ExpectedCellValue(sheetName, 12, 2, "44100"),
						new ExpectedCellValue(sheetName, 12, 3, null),
						new ExpectedCellValue(sheetName, 12, 4, null),
						new ExpectedCellValue(sheetName, 12, 5, 8277.85),
						new ExpectedCellValue(sheetName, 12, 6, null),
						new ExpectedCellValue(sheetName, 12, 7, null),
						new ExpectedCellValue(sheetName, 12, 8, null),
						new ExpectedCellValue(sheetName, 12, 9, 8277.85),
						new ExpectedCellValue(sheetName, 13, 2, "61100"),
						new ExpectedCellValue(sheetName, 13, 3, null),
						new ExpectedCellValue(sheetName, 13, 4, null),
						new ExpectedCellValue(sheetName, 13, 5, 9336.83),
						new ExpectedCellValue(sheetName, 13, 6, null),
						new ExpectedCellValue(sheetName, 13, 7, null),
						new ExpectedCellValue(sheetName, 13, 8, 9336.83),
						new ExpectedCellValue(sheetName, 13, 9, 18673.66),
						new ExpectedCellValue(sheetName, 14, 2, "61200"),
						new ExpectedCellValue(sheetName, 14, 3, null),
						new ExpectedCellValue(sheetName, 14, 4, null),
						new ExpectedCellValue(sheetName, 14, 5, 427.31),
						new ExpectedCellValue(sheetName, 14, 6, null),
						new ExpectedCellValue(sheetName, 14, 7, null),
						new ExpectedCellValue(sheetName, 14, 8, 183.13),
						new ExpectedCellValue(sheetName, 14, 9, 610.44),
						new ExpectedCellValue(sheetName, 15, 2, "61300"),
						new ExpectedCellValue(sheetName, 15, 3, null),
						new ExpectedCellValue(sheetName, 15, 4, null),
						new ExpectedCellValue(sheetName, 15, 5, null),
						new ExpectedCellValue(sheetName, 15, 6, null),
						new ExpectedCellValue(sheetName, 15, 7, null),
						new ExpectedCellValue(sheetName, 15, 8, 4144.85),
						new ExpectedCellValue(sheetName, 15, 9, 4144.85),
						new ExpectedCellValue(sheetName, 16, 2, "64300"),
						new ExpectedCellValue(sheetName, 16, 3, null),
						new ExpectedCellValue(sheetName, 16, 4, null),
						new ExpectedCellValue(sheetName, 16, 5, 286.31),
						new ExpectedCellValue(sheetName, 16, 6, null),
						new ExpectedCellValue(sheetName, 16, 7, null),
						new ExpectedCellValue(sheetName, 16, 8, null),
						new ExpectedCellValue(sheetName, 16, 9, 286.31),
						new ExpectedCellValue(sheetName, 17, 2, "Opening Entry"),
						new ExpectedCellValue(sheetName, 17, 3, 6358.14),
						new ExpectedCellValue(sheetName, 17, 4, -2804975.21),
						new ExpectedCellValue(sheetName, 17, 5, null),
						new ExpectedCellValue(sheetName, 17, 6, null),
						new ExpectedCellValue(sheetName, 17, 7, null),
						new ExpectedCellValue(sheetName, 17, 8, null),
						new ExpectedCellValue(sheetName, 17, 9, -2798617.07),
						new ExpectedCellValue(sheetName, 18, 2, "11200"),
						new ExpectedCellValue(sheetName, 18, 3, null),
						new ExpectedCellValue(sheetName, 18, 4, 232.02),
						new ExpectedCellValue(sheetName, 18, 5, null),
						new ExpectedCellValue(sheetName, 18, 6, null),
						new ExpectedCellValue(sheetName, 18, 7, null),
						new ExpectedCellValue(sheetName, 18, 8, null),
						new ExpectedCellValue(sheetName, 18, 9, 232.02),
						new ExpectedCellValue(sheetName, 19, 2, "11400"),
						new ExpectedCellValue(sheetName, 19, 3, null),
						new ExpectedCellValue(sheetName, 19, 4, -752562.89),
						new ExpectedCellValue(sheetName, 19, 5, null),
						new ExpectedCellValue(sheetName, 19, 6, null),
						new ExpectedCellValue(sheetName, 19, 7, null),
						new ExpectedCellValue(sheetName, 19, 8, null),
						new ExpectedCellValue(sheetName, 19, 9, -752562.89),
						new ExpectedCellValue(sheetName, 20, 2, "11600"),
						new ExpectedCellValue(sheetName, 20, 3, null),
						new ExpectedCellValue(sheetName, 20, 4, 8277.85),
						new ExpectedCellValue(sheetName, 20, 5, null),
						new ExpectedCellValue(sheetName, 20, 6, null),
						new ExpectedCellValue(sheetName, 20, 7, null),
						new ExpectedCellValue(sheetName, 20, 8, null),
						new ExpectedCellValue(sheetName, 20, 9, 8277.85),
						new ExpectedCellValue(sheetName, 21, 2, "12100"),
						new ExpectedCellValue(sheetName, 21, 3, null),
						new ExpectedCellValue(sheetName, 21, 4, -752562.89),
						new ExpectedCellValue(sheetName, 21, 5, null),
						new ExpectedCellValue(sheetName, 21, 6, null),
						new ExpectedCellValue(sheetName, 21, 7, null),
						new ExpectedCellValue(sheetName, 21, 8, null),
						new ExpectedCellValue(sheetName, 21, 9, -752562.89),
						new ExpectedCellValue(sheetName, 22, 2, "13100"),
						new ExpectedCellValue(sheetName, 22, 3, 232.02),
						new ExpectedCellValue(sheetName, 22, 4, null),
						new ExpectedCellValue(sheetName, 22, 5, null),
						new ExpectedCellValue(sheetName, 22, 6, null),
						new ExpectedCellValue(sheetName, 22, 7, null),
						new ExpectedCellValue(sheetName, 22, 8, null),
						new ExpectedCellValue(sheetName, 22, 9, 232.02),
						new ExpectedCellValue(sheetName, 23, 2, "13200"),
						new ExpectedCellValue(sheetName, 23, 3, 232.02),
						new ExpectedCellValue(sheetName, 23, 4, null),
						new ExpectedCellValue(sheetName, 23, 5, null),
						new ExpectedCellValue(sheetName, 23, 6, null),
						new ExpectedCellValue(sheetName, 23, 7, null),
						new ExpectedCellValue(sheetName, 23, 8, null),
						new ExpectedCellValue(sheetName, 23, 9, 232.02),
						new ExpectedCellValue(sheetName, 24, 2, "13300"),
						new ExpectedCellValue(sheetName, 24, 3, 5662.08),
						new ExpectedCellValue(sheetName, 24, 4, null),
						new ExpectedCellValue(sheetName, 24, 5, null),
						new ExpectedCellValue(sheetName, 24, 6, null),
						new ExpectedCellValue(sheetName, 24, 7, null),
						new ExpectedCellValue(sheetName, 24, 8, null),
						new ExpectedCellValue(sheetName, 24, 9, 5662.08),
						new ExpectedCellValue(sheetName, 25, 2, "14400"),
						new ExpectedCellValue(sheetName, 25, 3, 232.02),
						new ExpectedCellValue(sheetName, 25, 4, null),
						new ExpectedCellValue(sheetName, 25, 5, null),
						new ExpectedCellValue(sheetName, 25, 6, null),
						new ExpectedCellValue(sheetName, 25, 7, null),
						new ExpectedCellValue(sheetName, 25, 8, null),
						new ExpectedCellValue(sheetName, 25, 9, 232.02),
						new ExpectedCellValue(sheetName, 26, 2, "17100"),
						new ExpectedCellValue(sheetName, 26, 3, null),
						new ExpectedCellValue(sheetName, 26, 4, 828.97),
						new ExpectedCellValue(sheetName, 26, 5, null),
						new ExpectedCellValue(sheetName, 26, 6, null),
						new ExpectedCellValue(sheetName, 26, 7, null),
						new ExpectedCellValue(sheetName, 26, 8, null),
						new ExpectedCellValue(sheetName, 26, 9, 828.97),
						new ExpectedCellValue(sheetName, 27, 2, "17200"),
						new ExpectedCellValue(sheetName, 27, 3, null),
						new ExpectedCellValue(sheetName, 27, 4, -558283.32),
						new ExpectedCellValue(sheetName, 27, 5, null),
						new ExpectedCellValue(sheetName, 27, 6, null),
						new ExpectedCellValue(sheetName, 27, 7, null),
						new ExpectedCellValue(sheetName, 27, 8, null),
						new ExpectedCellValue(sheetName, 27, 9, -558283.32),
						new ExpectedCellValue(sheetName, 28, 2, "18100"),
						new ExpectedCellValue(sheetName, 28, 3, null),
						new ExpectedCellValue(sheetName, 28, 4, 828.97),
						new ExpectedCellValue(sheetName, 28, 5, null),
						new ExpectedCellValue(sheetName, 28, 6, null),
						new ExpectedCellValue(sheetName, 28, 7, null),
						new ExpectedCellValue(sheetName, 28, 8, null),
						new ExpectedCellValue(sheetName, 28, 9, 828.97),
						new ExpectedCellValue(sheetName, 29, 2, "18200"),
						new ExpectedCellValue(sheetName, 29, 3, null),
						new ExpectedCellValue(sheetName, 29, 4, 828.97),
						new ExpectedCellValue(sheetName, 29, 5, null),
						new ExpectedCellValue(sheetName, 29, 6, null),
						new ExpectedCellValue(sheetName, 29, 7, null),
						new ExpectedCellValue(sheetName, 29, 8, null),
						new ExpectedCellValue(sheetName, 29, 9, 828.97),
						new ExpectedCellValue(sheetName, 30, 2, "30200"),
						new ExpectedCellValue(sheetName, 30, 3, null),
						new ExpectedCellValue(sheetName, 30, 4, -752562.89),
						new ExpectedCellValue(sheetName, 30, 5, null),
						new ExpectedCellValue(sheetName, 30, 6, null),
						new ExpectedCellValue(sheetName, 30, 7, null),
						new ExpectedCellValue(sheetName, 30, 8, null),
						new ExpectedCellValue(sheetName, 30, 9, -752562.89),
						new ExpectedCellValue(sheetName, 31, 2, "Order 106015"),
						new ExpectedCellValue(sheetName, 31, 3, null),
						new ExpectedCellValue(sheetName, 31, 4, null),
						new ExpectedCellValue(sheetName, 31, 5, null),
						new ExpectedCellValue(sheetName, 31, 6, -53800.14),
						new ExpectedCellValue(sheetName, 31, 7, 53800.14),
						new ExpectedCellValue(sheetName, 31, 8, null),
						new ExpectedCellValue(sheetName, 31, 9, 0),
						new ExpectedCellValue(sheetName, 32, 2, "11600"),
						new ExpectedCellValue(sheetName, 32, 3, null),
						new ExpectedCellValue(sheetName, 32, 4, null),
						new ExpectedCellValue(sheetName, 32, 5, null),
						new ExpectedCellValue(sheetName, 32, 6, -53800.14),
						new ExpectedCellValue(sheetName, 32, 7, null),
						new ExpectedCellValue(sheetName, 32, 8, null),
						new ExpectedCellValue(sheetName, 32, 9, -53800.14),
						new ExpectedCellValue(sheetName, 33, 2, "16210"),
						new ExpectedCellValue(sheetName, 33, 3, null),
						new ExpectedCellValue(sheetName, 33, 4, null),
						new ExpectedCellValue(sheetName, 33, 5, null),
						new ExpectedCellValue(sheetName, 33, 6, 53800.14),
						new ExpectedCellValue(sheetName, 33, 7, null),
						new ExpectedCellValue(sheetName, 33, 8, null),
						new ExpectedCellValue(sheetName, 33, 9, 53800.14),
						new ExpectedCellValue(sheetName, 34, 2, "22400"),
						new ExpectedCellValue(sheetName, 34, 3, null),
						new ExpectedCellValue(sheetName, 34, 4, null),
						new ExpectedCellValue(sheetName, 34, 5, null),
						new ExpectedCellValue(sheetName, 34, 6, -53800.14),
						new ExpectedCellValue(sheetName, 34, 7, 53800.14),
						new ExpectedCellValue(sheetName, 34, 8, null),
						new ExpectedCellValue(sheetName, 34, 9, 0),
						new ExpectedCellValue(sheetName, 35, 2, "Order 106018"),
						new ExpectedCellValue(sheetName, 35, 3, null),
						new ExpectedCellValue(sheetName, 35, 4, null),
						new ExpectedCellValue(sheetName, 35, 5, null),
						new ExpectedCellValue(sheetName, 35, 6, 23672.06),
						new ExpectedCellValue(sheetName, 35, 7, 23672.06),
						new ExpectedCellValue(sheetName, 35, 8, null),
						new ExpectedCellValue(sheetName, 35, 9, 47344.12),
						new ExpectedCellValue(sheetName, 36, 2, "11600"),
						new ExpectedCellValue(sheetName, 36, 3, null),
						new ExpectedCellValue(sheetName, 36, 4, null),
						new ExpectedCellValue(sheetName, 36, 5, null),
						new ExpectedCellValue(sheetName, 36, 6, 11836.03),
						new ExpectedCellValue(sheetName, 36, 7, null),
						new ExpectedCellValue(sheetName, 36, 8, null),
						new ExpectedCellValue(sheetName, 36, 9, 11836.03),
						new ExpectedCellValue(sheetName, 37, 2, "17110"),
						new ExpectedCellValue(sheetName, 37, 3, null),
						new ExpectedCellValue(sheetName, 37, 4, null),
						new ExpectedCellValue(sheetName, 37, 5, null),
						new ExpectedCellValue(sheetName, 37, 6, null),
						new ExpectedCellValue(sheetName, 37, 7, 11836.03),
						new ExpectedCellValue(sheetName, 37, 8, null),
						new ExpectedCellValue(sheetName, 37, 9, 11836.03),
						new ExpectedCellValue(sheetName, 38, 2, "22400"),
						new ExpectedCellValue(sheetName, 38, 3, null),
						new ExpectedCellValue(sheetName, 38, 4, null),
						new ExpectedCellValue(sheetName, 38, 5, null),
						new ExpectedCellValue(sheetName, 38, 6, 11836.03),
						new ExpectedCellValue(sheetName, 38, 7, 11836.03),
						new ExpectedCellValue(sheetName, 38, 8, null),
						new ExpectedCellValue(sheetName, 38, 9, 23672.06),
						new ExpectedCellValue(sheetName, 39, 2, "Grand Total"),
						new ExpectedCellValue(sheetName, 39, 3, 6358.14),
						new ExpectedCellValue(sheetName, 39, 4, -2804975.21),
						new ExpectedCellValue(sheetName, 39, 5, -218303.72),
						new ExpectedCellValue(sheetName, 39, 6, -30128.08),
						new ExpectedCellValue(sheetName, 39, 7, 85750.05),
						new ExpectedCellValue(sheetName, 39, 8, 22024.94),
						new ExpectedCellValue(sheetName, 39, 9, -2939273.88),
					});
				}
			}
		}

		[TestMethod]
		[DeploymentItem(@"..\..\Workbooks\PivotTables\Slicers.xlsx")]
		public void RefreshSlicerMultipleSlicerFilters()
		{
			var file = new FileInfo("Slicers.xlsx");
			Assert.IsTrue(file.Exists);
			using (var newFile = new TempTestFile())
			{
				using (var package = new ExcelPackage(file))
				{
					var sheet = package.Workbook.Worksheets["Sheet3"];
					var pivotTable = sheet.PivotTables.First(p => p.Name == "PivotTable1");
					var postingDateSlicerCache = package.Workbook.SlicerCaches.First(s => s.Name == "Slicer_Posting_Date1");
					var cacheField = pivotTable.CacheDefinition.CacheFields.First(c => c.Name == postingDateSlicerCache.SourceName);
					var items = cacheField.IsGroupField ? cacheField.FieldGroup.GroupItems : cacheField.SharedItems;
					foreach (var item in postingDateSlicerCache.TabularDataNode.Items)
					{
						// Only select December, January, and April for this test.
						if (items[item.AtomIndex].Value == "Dec" || items[item.AtomIndex].Value == "Apr" || items[item.AtomIndex].Value == "Jan")
							item.IsSelected = true;
						else
							item.IsSelected = false;
					}

					var amountDateSlicerCache = package.Workbook.SlicerCaches.First(s => s.Name == "Slicer_Amount1");
					cacheField = pivotTable.CacheDefinition.CacheFields.First(c => c.Name == amountDateSlicerCache.SourceName);
					items = cacheField.IsGroupField ? cacheField.FieldGroup.GroupItems : cacheField.SharedItems;
					foreach (var item in amountDateSlicerCache.TabularDataNode.Items)
					{
						if (items[item.AtomIndex].Value == "11836.03" || items[item.AtomIndex].Value == "9336.83" || items[item.AtomIndex].Value == "8277.85")
							item.IsSelected = true;
						else
							item.IsSelected = false;
					}

					package.Workbook.PivotCacheDefinitions.First().UpdateData();
					package.SaveAs(newFile.File);
				}
				using (var package = new ExcelPackage(newFile.File))
				{
					var sheetName = "Sheet3";
					TestHelperUtility.ValidateWorksheet(newFile.File, sheetName, new[]
					{
						new ExpectedCellValue(sheetName, 3, 2, "Sum of Amount"),
						new ExpectedCellValue(sheetName, 3, 3, "Column Labels"),
						new ExpectedCellValue(sheetName, 4, 3, "2012"),
						new ExpectedCellValue(sheetName, 4, 4, "2013"),
						new ExpectedCellValue(sheetName, 4, 8, "Grand Total"),
						new ExpectedCellValue(sheetName, 5, 2, "Row Labels"),
						new ExpectedCellValue(sheetName, 5, 3, "START"),
						new ExpectedCellValue(sheetName, 5, 4, "END"),
						new ExpectedCellValue(sheetName, 5, 5, "PURCHASES"),
						new ExpectedCellValue(sheetName, 5, 6, "SALES"),
						new ExpectedCellValue(sheetName, 5, 7, "START"),
						new ExpectedCellValue(sheetName, 6, 2, "Entries, January 2013"),
						new ExpectedCellValue(sheetName, 6, 3, null),
						new ExpectedCellValue(sheetName, 6, 4, 16555.7),
						new ExpectedCellValue(sheetName, 6, 5, null),
						new ExpectedCellValue(sheetName, 6, 6, 8277.85),
						new ExpectedCellValue(sheetName, 6, 7, 17614.68),
						new ExpectedCellValue(sheetName, 6, 8, 42448.23),
						new ExpectedCellValue(sheetName, 7, 2, "13100"),
						new ExpectedCellValue(sheetName, 7, 3, null),
						new ExpectedCellValue(sheetName, 7, 4, null),
						new ExpectedCellValue(sheetName, 7, 5, null),
						new ExpectedCellValue(sheetName, 7, 6, null),
						new ExpectedCellValue(sheetName, 7, 7, 8277.85),
						new ExpectedCellValue(sheetName, 7, 8, 8277.85),
						new ExpectedCellValue(sheetName, 8, 2, "13200"),
						new ExpectedCellValue(sheetName, 8, 3, null),
						new ExpectedCellValue(sheetName, 8, 4, 8277.85),
						new ExpectedCellValue(sheetName, 8, 5, null),
						new ExpectedCellValue(sheetName, 8, 6, null),
						new ExpectedCellValue(sheetName, 8, 7, null),
						new ExpectedCellValue(sheetName, 8, 8, 8277.85),
						new ExpectedCellValue(sheetName, 9, 2, "43100"),
						new ExpectedCellValue(sheetName, 9, 3, null),
						new ExpectedCellValue(sheetName, 9, 4, null),
						new ExpectedCellValue(sheetName, 9, 5, null),
						new ExpectedCellValue(sheetName, 9, 6, 8277.85),
						new ExpectedCellValue(sheetName, 9, 7, null),
						new ExpectedCellValue(sheetName, 9, 8, 8277.85),
						new ExpectedCellValue(sheetName, 10, 2, "44100"),
						new ExpectedCellValue(sheetName, 10, 3, null),
						new ExpectedCellValue(sheetName, 10, 4, 8277.85),
						new ExpectedCellValue(sheetName, 10, 5, null),
						new ExpectedCellValue(sheetName, 10, 6, null),
						new ExpectedCellValue(sheetName, 10, 7, null),
						new ExpectedCellValue(sheetName, 10, 8, 8277.85),
						new ExpectedCellValue(sheetName, 11, 2, "61100"),
						new ExpectedCellValue(sheetName, 11, 3, null),
						new ExpectedCellValue(sheetName, 11, 4, null),
						new ExpectedCellValue(sheetName, 11, 5, null),
						new ExpectedCellValue(sheetName, 11, 6, null),
						new ExpectedCellValue(sheetName, 11, 7, 9336.83),
						new ExpectedCellValue(sheetName, 11, 8, 9336.83),
						new ExpectedCellValue(sheetName, 12, 2, "Opening Entry"),
						new ExpectedCellValue(sheetName, 12, 3, 8277.85),
						new ExpectedCellValue(sheetName, 12, 4, null),
						new ExpectedCellValue(sheetName, 12, 5, null),
						new ExpectedCellValue(sheetName, 12, 6, null),
						new ExpectedCellValue(sheetName, 12, 7, null),
						new ExpectedCellValue(sheetName, 12, 8, 8277.85),
						new ExpectedCellValue(sheetName, 13, 2, "11600"),
						new ExpectedCellValue(sheetName, 13, 3, 8277.85),
						new ExpectedCellValue(sheetName, 13, 4, null),
						new ExpectedCellValue(sheetName, 13, 5, null),
						new ExpectedCellValue(sheetName, 13, 6, null),
						new ExpectedCellValue(sheetName, 13, 7, null),
						new ExpectedCellValue(sheetName, 13, 8, 8277.85),
						new ExpectedCellValue(sheetName, 14, 2, "Order 106018"),
						new ExpectedCellValue(sheetName, 14, 3, null),
						new ExpectedCellValue(sheetName, 14, 4, null),
						new ExpectedCellValue(sheetName, 14, 5, 23672.06),
						new ExpectedCellValue(sheetName, 14, 6, 23672.06),
						new ExpectedCellValue(sheetName, 14, 7, null),
						new ExpectedCellValue(sheetName, 14, 8, 47344.12),
						new ExpectedCellValue(sheetName, 15, 2, "11600"),
						new ExpectedCellValue(sheetName, 15, 3, null),
						new ExpectedCellValue(sheetName, 15, 4, null),
						new ExpectedCellValue(sheetName, 15, 5, 11836.03),
						new ExpectedCellValue(sheetName, 15, 6, null),
						new ExpectedCellValue(sheetName, 15, 7, null),
						new ExpectedCellValue(sheetName, 15, 8, 11836.03),
						new ExpectedCellValue(sheetName, 16, 2, "17110"),
						new ExpectedCellValue(sheetName, 16, 3, null),
						new ExpectedCellValue(sheetName, 16, 4, null),
						new ExpectedCellValue(sheetName, 16, 5, null),
						new ExpectedCellValue(sheetName, 16, 6, 11836.03),
						new ExpectedCellValue(sheetName, 16, 7, null),
						new ExpectedCellValue(sheetName, 16, 8, 11836.03),
						new ExpectedCellValue(sheetName, 17, 2, "22400"),
						new ExpectedCellValue(sheetName, 17, 3, null),
						new ExpectedCellValue(sheetName, 17, 4, null),
						new ExpectedCellValue(sheetName, 17, 5, 11836.03),
						new ExpectedCellValue(sheetName, 17, 6, 11836.03),
						new ExpectedCellValue(sheetName, 17, 7, null),
						new ExpectedCellValue(sheetName, 17, 8, 23672.06),
						new ExpectedCellValue(sheetName, 18, 2, "Grand Total"),
						new ExpectedCellValue(sheetName, 18, 3, 8277.85),
						new ExpectedCellValue(sheetName, 18, 4, 16555.7),
						new ExpectedCellValue(sheetName, 18, 5, 23672.06),
						new ExpectedCellValue(sheetName, 18, 6, 31949.91),
						new ExpectedCellValue(sheetName, 18, 7, 17614.68),
						new ExpectedCellValue(sheetName, 18, 8, 98070.2),
					});
				}
			}
		}
		#endregion

		[TestMethod]
		public void AutoGenerateExpectedResults()
		{
			string sheetName = "Sheet3";
			string range = "B3:H18";
			//var sourceFilePath = @"C:\Source\EPPlus\EPPlusTest\Workbooks\PivotTables\Slicers.xlsx";
			var sourceFilePath = @"C:\Users\ems\Downloads\Slicers.xlsx";
			var outputFilePath = @"C:\Users\ems\Downloads\expected.cs";

			using (var package = new OfficeOpenXml.ExcelPackage(new System.IO.FileInfo(sourceFilePath)))
			{
				var cells = package.Workbook.Worksheets[sheetName].Cells[range];
				string text = $"TestHelperUtility.ValidateWorksheet(newFile.File, sheetName, new[]{System.Environment.NewLine}{{{System.Environment.NewLine}";
				foreach (var cell in cells)
				{
					string value = null;
					if (cell.Value is string)
						value = $"\"{cell.Value}\"";
					else if (cell.Value is OfficeOpenXml.ExcelErrorValue errorValue)
						value = $"ExcelErrorValue.Create(eErrorType.{errorValue.Type})";
					else if (cell.Value is DateTime dateValue)
						value = dateValue.ToOADate().ToString();
					else if (cell.Value == null)
						value = "null";
					else
						value = cell.Value.ToString();

					text += $"	new ExpectedCellValue(sheetName, {cell._fromRow}, {cell._fromCol}, {value}),{System.Environment.NewLine}";
				}
				text += "});";
				System.IO.File.WriteAllText(outputFilePath, text);
			}
			Assert.Fail("Successfully generated results.");
		}
	}
}
