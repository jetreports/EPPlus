﻿/* Copyright (C) 2011  Jan Källman
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
 * Code change notes:
 * 
 * Author							Change						Date
 *******************************************************************************
 * Max Ackley		                Added		                2017-05-05
 *******************************************************************************/
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;

namespace EPPlusTest.FormulaParsing.Excel.Functions.RefAndLookup
{
	[TestClass]
	public class MatchTests
	{
		#region Properties
		private ExcelPackage Package { get; set; }
		private ExcelWorksheet Worksheet { get; set; }
		#endregion

		#region TestInitialize/TestCleanup
		[TestInitialize]
		public void Initialize()
		{
			this.Package = new ExcelPackage(new MemoryStream());
			this.Worksheet = this.Package.Workbook.Worksheets.Add("test");
		}

		[TestCleanup]
		public void Cleanup()
		{
			this.Package.Dispose();
		}

		[TestMethod]
		public void MatchInvalidParameterCount()
		{
			this.Worksheet.Cells["A4"].Formula = "MATCH(3)";
			this.Worksheet.Calculate();
			Assert.AreEqual(ExcelErrorValue.Create(eErrorType.Value), this.Worksheet.Cells["A4"].Value);
		}
		#endregion

		#region Match Tests
		[TestMethod]
		public void MatchExact()
		{
			this.Worksheet.Cells["A1"].Value = 5d;
			this.Worksheet.Cells["A2"].Value = 1d;
			this.Worksheet.Cells["A3"].Value = 3d;
			this.Worksheet.Cells["A4"].Formula = "MATCH(3,A1:A3,0)";
			this.Worksheet.Calculate();
			Assert.AreEqual(3, this.Worksheet.Cells["A4"].Value);
		}

		[TestMethod]
		public void MatchExactNotFound()
		{
			this.Worksheet.Cells["A1"].Value = 5d;
			this.Worksheet.Cells["A2"].Value = 1d;
			this.Worksheet.Cells["A3"].Value = 3d;
			this.Worksheet.Cells["A4"].Formula = "MATCH(2,A1:A3,0)";
			this.Worksheet.Calculate();
			Assert.AreEqual(ExcelErrorValue.Create(eErrorType.NA), this.Worksheet.Cells["A4"].Value);
		}

		[TestMethod]
		public void MatchLessThanExact()
		{
			// Match for "less than" requires values be in acending order, 
			// in order to find the closest value to the search value.
			// Otherwise, the index of the value immediately preceding 
			// the first value greater than the search value will be found.
			this.Worksheet.Cells["A1"].Value = 1d;
			this.Worksheet.Cells["A2"].Value = 3d;
			this.Worksheet.Cells["A3"].Value = 5d;
			this.Worksheet.Cells["A4"].Formula = "MATCH(3,A1:A3,1)";
			this.Worksheet.Calculate();
			Assert.AreEqual(2, this.Worksheet.Cells["A4"].Value);
		}

		[TestMethod]
		public void MatchLessThanNotExact()
		{
			// Match for "less than" requires values be in acending order, 
			// in order to find the closest value to the search value.
			// Otherwise, the index of the value immediately preceding 
			// the first value greater than the search value will be found.
			this.Worksheet.Cells["A1"].Value = 1d;
			this.Worksheet.Cells["A2"].Value = 3d;
			this.Worksheet.Cells["A3"].Value = 5d;
			this.Worksheet.Cells["A4"].Formula = "MATCH(4,A1:A3,1)";
			this.Worksheet.Calculate();
			Assert.AreEqual(2, this.Worksheet.Cells["A4"].Value);
		}

		[TestMethod]
		public void MatchLessThanNotFound()
		{
			// Match for "less than" requires values be in acending order, 
			// in order to find the closest value to the search value.
			// Otherwise, the index of the value immediately preceding 
			// the first value greater than the search value will be found.
			this.Worksheet.Cells["A1"].Value = 2d;
			this.Worksheet.Cells["A2"].Value = 3d;
			this.Worksheet.Cells["A3"].Value = 5d;
			this.Worksheet.Cells["A4"].Formula = "MATCH(1,A1:A3,1)";
			this.Worksheet.Calculate();
			Assert.AreEqual(ExcelErrorValue.Create(eErrorType.NA), this.Worksheet.Cells["A4"].Value);
		}

		[TestMethod]
		public void MatchLessThanNotAcending()
		{
			// NOTE: this is not how MATCH is intended to be used, but how Excel behaves.

			// Match for "less than" requires values be in acending order, 
			// in order to find the closest value to the search value.
			// Otherwise, the index of the value immediately preceding 
			// the first value greater than the search value will be found.
			this.Worksheet.Cells["A1"].Value = 1d;
			this.Worksheet.Cells["A2"].Value = 7d;
			this.Worksheet.Cells["A3"].Value = 5d;
			this.Worksheet.Cells["A4"].Formula = "MATCH(6,A1:A3,1)";
			this.Worksheet.Calculate();
			Assert.AreEqual(1, this.Worksheet.Cells["A4"].Value);
		}

		[TestMethod]
		public void MatchGreaterThanExact()
		{
			// Match for "greater than" requires values be in descending order, 
			// in order to find the closest value to the search value.
			// Otherwise, the index of the value immediately preceding 
			// the first value less than the search value will be found.
			this.Worksheet.Cells["A1"].Value = 5d;
			this.Worksheet.Cells["A2"].Value = 3d;
			this.Worksheet.Cells["A3"].Value = 1d;
			this.Worksheet.Cells["A4"].Formula = "MATCH(3,A1:A3,-1)";
			this.Worksheet.Calculate();
			Assert.AreEqual(2, this.Worksheet.Cells["A4"].Value);
		}

		[TestMethod]
		public void MatchGreaterThanNotExact()
		{
			// Match for "greater than" requires values be in descending order, 
			// in order to find the closest value to the search value.
			// Otherwise, the index of the value immediately preceding 
			// the first value less than the search value will be found.
			this.Worksheet.Cells["A1"].Value = 5d;
			this.Worksheet.Cells["A2"].Value = 3d;
			this.Worksheet.Cells["A3"].Value = 1d;
			this.Worksheet.Cells["A4"].Formula = "MATCH(2,A1:A3,-1)";
			this.Worksheet.Calculate();
			Assert.AreEqual(2, this.Worksheet.Cells["A4"].Value);
		}

		[TestMethod]
		public void MatchGreaterThanNotFound()
		{
			// Match for "greater than" requires values be in descending order, 
			// in order to find the closest value to the search value.
			// Otherwise, the index of the value immediately preceding 
			// the first value less than the search value will be found.
			this.Worksheet.Cells["A1"].Value = 5d;
			this.Worksheet.Cells["A2"].Value = 3d;
			this.Worksheet.Cells["A3"].Value = 1d;
			this.Worksheet.Cells["A4"].Formula = "MATCH(6,A1:A3,-1)";
			this.Worksheet.Calculate();
			Assert.AreEqual(ExcelErrorValue.Create(eErrorType.NA), this.Worksheet.Cells["A4"].Value);
		}

		[TestMethod]
		public void MatchGreaterThanNotDescending()
		{
			// NOTE: this is not how MATCH is intended to be used, 
			// but how Excel behaves in this case.

			// Match for "greater than" requires values be in descending order, 
			// in order to find the closest value to the search value.
			// Otherwise, the index of the value immediately preceding 
			// the first value less than the search value will be found.
			this.Worksheet.Cells["A1"].Value = 7d;
			this.Worksheet.Cells["A2"].Value = 3d;
			this.Worksheet.Cells["A3"].Value = 5d;
			this.Worksheet.Cells["A4"].Formula = "MATCH(4,A1:A3,-1)";
			this.Worksheet.Calculate();
			Assert.AreEqual(1, this.Worksheet.Cells["A4"].Value);
		}
		#endregion
	}
}
