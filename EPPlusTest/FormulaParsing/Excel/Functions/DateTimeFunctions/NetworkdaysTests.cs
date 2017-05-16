﻿/*******************************************************************************
* You may amend and distribute as you like, but don't remove this header!
*
* EPPlus provides server-side generation of Excel 2007/2010 spreadsheets.
* See http://www.codeplex.com/EPPlus for details.
*
* Copyright (C) 2011-2017 Jan Källman, Matt Delaney, and others as noted in the source history.
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Lesser General Public
* License as published by the Free Software Foundation; either
* version 2.1 of the License, or (at your option) any later version.

* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
* See the GNU Lesser General Public License for more details.
*
* The GNU Lesser General Public License can be viewed at http://www.opensource.org/licenses/lgpl-license.php
* If you unfamiliar with this license or have questions about it, here is an http://www.gnu.org/licenses/gpl-faq.html
*
* All code and executables are provided "as is" with no warranty either express or implied. 
* The author accepts no liability for any damage or loss of business that this product may cause.
*
* For code change notes, see the source control history.
*******************************************************************************/
using System.IO;
using EPPlusTest.Excel.Functions.DateTimeFunctions;
using EPPlusTest.FormulaParsing.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace EPPlusTest.FormulaParsing.Excel.Functions.DateTimeFunctions
{
	class NetworkdaysTests : DateTimeFunctionsTestBase
	{
		#region Networkdays Function (Execute) Tests
		[TestMethod]
		public void NetworkdaysShouldReturnNumberOfDays()
		{
			using (var package = new ExcelPackage())
			{
				var ws = package.Workbook.Worksheets.Add("test");
				ws.Cells["A1"].Formula = "NETWORKDAYS(DATE(2016,1,1), DATE(2016,1,20))";
				ws.Calculate();
				Assert.AreEqual(14, ws.Cells["A1"].Value);
			}
		}

		[TestMethod]
		public void NetworkdaysShouldReturnNumberOfDaysWithHolidayRange()
		{
			using (MemoryStream ms = new MemoryStream())
			{
				// do something...
				using (var package = new ExcelPackage())
				{
					package.Load(ms);
				}
			}
			using (var package = new ExcelPackage())
			{
				var ws = package.Workbook.Worksheets.Add("test");
				ws.Cells["A1"].Formula = "NETWORKDAYS(DATE(2016,1,1), DATE(2016,1,20),B1)";
				ws.Cells["B1"].Formula = "DATE(2016,1,15)";
				ws.Calculate();
				Assert.AreEqual(13, ws.Cells["A1"].Value);
			}
		}

		[TestMethod]
		public void NetworkdaysNegativeShouldReturnNumberOfDays()
		{
			using (var package = new ExcelPackage())
			{
				var ws = package.Workbook.Worksheets.Add("test");
				ws.Cells["A1"].Formula = "NETWORKDAYS(DATE(2016,1,1), DATE(2015,12,20))";
				ws.Calculate();
				Assert.AreEqual(10, ws.Cells["A1"].Value);
			}
		}

		[TestMethod]
		public void NetworkdaysWithInvalidArgumentReturnsPoundValue()
		{
			var func = new Networkdays();

			var args = FunctionsHelper.CreateArgs();
			var result = func.Execute(args, this.ParsingContext);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}
		#endregion

		#region Networkdays.INTL Function (Execute) Tests
		[TestMethod]
		public void NetworkdayIntlShouldUseWeekendArg()
		{
			using (var package = new ExcelPackage())
			{
				var ws = package.Workbook.Worksheets.Add("test");
				ws.Cells["A1"].Formula = "NETWORKDAYS.INTL(DATE(2016,1,1), DATE(2016,1,20), 11)";
				ws.Calculate();
				Assert.AreEqual(17, ws.Cells["A1"].Value);
			}
		}

		[TestMethod]
		public void NetworkdayIntlShouldUseWeekendStringArg()
		{
			using (var package = new ExcelPackage())
			{
				var ws = package.Workbook.Worksheets.Add("test");
				ws.Cells["A1"].Formula = "NETWORKDAYS.INTL(DATE(2016,1,1), DATE(2016,1,20), \"0000011\")";
				ws.Calculate();
				Assert.AreEqual(14, ws.Cells["A1"].Value);
			}
		}

		[TestMethod]
		public void NetworkdayIntlShouldReduceHoliday()
		{
			using (var package = new ExcelPackage())
			{
				var ws = package.Workbook.Worksheets.Add("test");
				ws.Cells["A1"].Formula = "NETWORKDAYS.INTL(DATE(2016,1,1), DATE(2016,1,20), \"0000011\", DATE(2016,1,4))";
				ws.Calculate();
				Assert.AreEqual(13, ws.Cells["A1"].Value);
			}
		}

		[TestMethod]
		public void NetworkdaysIntlWithInvalidArgumentReturnsPoundValue()
		{
			var func = new NetworkdaysIntl();

			var args = FunctionsHelper.CreateArgs();
			var result = func.Execute(args, this.ParsingContext);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}
		#endregion
	}
}