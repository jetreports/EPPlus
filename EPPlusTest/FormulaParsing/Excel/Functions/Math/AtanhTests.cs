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
using EPPlusTest.FormulaParsing.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace EPPlusTest.FormulaParsing.Excel.Functions.Math
{
	[TestClass]
	public class AtanhTests : MathFunctionsTestBase
	{
		#region AtanhTests Function(Execute) Tests
		[TestMethod]
		public void AtanhIsGivenAStringAsInput()
		{
			var function = new Atanh();

			var input1 = "string";
			var input2 = "0";
			var input3 = "1";
			var input4 = "1.5";

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);
			var result4 = function.Execute(FunctionsHelper.CreateArgs(input4), this.ParsingContext);

			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result1.Result).Type);
			Assert.AreEqual(0, result2.ResultNumeric, .00001);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result3.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result4.Result).Type);
		}

		[TestMethod]
		public void AtanhIsGivenValuesRanginFromNegative10to10()
		{
			var function = new Atanh();

			var input1 = -10;
			var input2 = -1;
			var input3 = 0;
			var input4 = 1;
			var input5 = 10;

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);
			var result4 = function.Execute(FunctionsHelper.CreateArgs(input4), this.ParsingContext);
			var result5 = function.Execute(FunctionsHelper.CreateArgs(input5), this.ParsingContext);

			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result1.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result2.Result).Type);
			Assert.AreEqual(0, result3.ResultNumeric, .00001);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result4.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result5.Result).Type);
		}

		[TestMethod]
		public void AtanhIntAndDoublesAsInputs()
		{
			var function = new Atanh();

			var input1 = 20;
			var input2 = 100;
			var input3 = 1;
			var input4 = 1.0;
			var input5 = 1.5;
			var input6 = 1000;

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);
			var result4 = function.Execute(FunctionsHelper.CreateArgs(input4), this.ParsingContext);
			var result5 = function.Execute(FunctionsHelper.CreateArgs(input5), this.ParsingContext);
			var result6 = function.Execute(FunctionsHelper.CreateArgs(input6), this.ParsingContext);

			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result1.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result2.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result3.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result4.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result5.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result6.Result).Type);
		}

		[TestMethod]
		public void AtanhHandlesPi()
		{
			var function = new Atanh();
			var Pi = System.Math.PI;

			var input1 = Pi;
			var input2 = Pi / 2;
			var input3 = 2 * Pi;
			var input4 = 60 * Pi / 180;

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);
			var result4 = function.Execute(FunctionsHelper.CreateArgs(input4), this.ParsingContext);

			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result1.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result2.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result3.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result4.Result).Type);
		}

		[TestMethod]
		public void AtanhHandlesMilitaryTime()
		{
			var function = new Atanh();

			var input1 = "00:00";
			var input2 = "00:01";
			var input3 = "23:59:59";

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);

			Assert.AreEqual(0, result1.ResultNumeric, .00001);
			Assert.AreEqual(0.000694445, result2.ResultNumeric, .00001);
			Assert.AreEqual(6.029942174, result3.ResultNumeric, .00001);
		}

		[TestMethod]
		public void AtanhHandlesMilitaryTimesPast2400()
		{
			var function = new Atanh();

			var input1 = "01:00";
			var input2 = "02:00";

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);

			Assert.AreEqual(0.041690804, result1.ResultNumeric, .00001);
			Assert.AreEqual(0.083527042, result2.ResultNumeric, .00001);
		}

		[TestMethod]
		public void AtanhHandlesDateTimeInputs()
		{
			var function = new Atanh();

			var input1 = "1/17/2011 2:00";
			var input2 = "1/17/2011 2:00 AM";
			var input3 = "17/1/2011 2:00 AM";
			var input4 = "17/Jan/2011 2:00 AM";

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);
			var result4 = function.Execute(FunctionsHelper.CreateArgs(input4), this.ParsingContext);

			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result1.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result2.Result).Type);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result3.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result4.Result).Type);
		}

		[TestMethod]
		public void AtanhHandlesNormal12HourClockInputs()
		{
			var function = new Atanh();

			var input1 = "00:00:00 AM";
			var input2 = "00:01:32 AM";
			var input3 = "12:00 PM";
			var input4 = "12:00 AM";
			var input5 = "1:00 PM";
			var input6 = "1:10:32 am";
			var input7 = "3:42:32 pm";

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);
			var result4 = function.Execute(FunctionsHelper.CreateArgs(input4), this.ParsingContext);
			var result5 = function.Execute(FunctionsHelper.CreateArgs(input5), this.ParsingContext);
			var result6 = function.Execute(FunctionsHelper.CreateArgs(input6), this.ParsingContext);
			var result7 = function.Execute(FunctionsHelper.CreateArgs(input7), this.ParsingContext);

			Assert.AreEqual(0, result1.ResultNumeric, .00001);
			Assert.AreEqual(0.001064815, result2.ResultNumeric, .00001);
			Assert.AreEqual(0.549306144, result3.ResultNumeric, .00001);
			Assert.AreEqual(0, result4.ResultNumeric, .00001);
			Assert.AreEqual(0.60651132, result5.ResultNumeric, .00001);
			Assert.AreEqual(0.04902071, result6.ResultNumeric, .00001);
			Assert.AreEqual(0.783195537, result7.ResultNumeric, .00001);
		}

		[TestMethod]
		public void AtanhTestMilitaryTimeAndNormalTimeComparisions()
		{
			var function = new Atanh();

			var input1 = "16:30";
			var input2 = "04:30 pm";
			var input3 = "02:30";
			var input4 = "2:30 am";

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);
			var result4 = function.Execute(FunctionsHelper.CreateArgs(input4), this.ParsingContext);

			Assert.AreEqual(0.843199477, result1.ResultNumeric, .00001);
			Assert.AreEqual(0.843199477, result2.ResultNumeric, .00001);
			Assert.AreEqual(0.104545899, result3.ResultNumeric, .00001);
			Assert.AreEqual(0.104545899, result4.ResultNumeric, .00001);
		}

		[TestMethod]
		public void AtanhTestInputsWithDatesThatHaveSlashesInThem()
		{
			var function = new Atanh();

			var input1 = "1/17/2011 2:00 am";
			var input2 = "17/01/2011 2:00 AM";
			var input3 = "17/Jan/2011 2:00 AM";
			var input4 = "17/January/2011 2:00 am";
			var input5 = "1/17/2011 2:00:00 am";
			var input6 = "17/01/2011 2:00:00 AM";
			var input7 = "17/Jan/2011 2:00:00 AM";
			var input8 = "17/January/2011 2:00:00 am";

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);
			var result4 = function.Execute(FunctionsHelper.CreateArgs(input4), this.ParsingContext);
			var result5 = function.Execute(FunctionsHelper.CreateArgs(input5), this.ParsingContext);
			var result6 = function.Execute(FunctionsHelper.CreateArgs(input6), this.ParsingContext);
			var result7 = function.Execute(FunctionsHelper.CreateArgs(input7), this.ParsingContext);
			var result8 = function.Execute(FunctionsHelper.CreateArgs(input8), this.ParsingContext);

			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result1.Result).Type);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result2.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result3.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result4.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result5.Result).Type);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result6.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result7.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result8.Result).Type);
		}

		[TestMethod]
		public void AtanhHandlesInputsWithDatesInTheFormMonthDateCommaYearTime()
		{
			var function = new Atanh();

			var input1 = "Jan 17, 2011 2:00 am";
			var input2 = "June 5, 2017 11:00 pm";
			var input3 = "Jan 17, 2011 2:00:00 am";
			var input4 = "June 5, 2017 11:00:00 pm";

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);
			var result4 = function.Execute(FunctionsHelper.CreateArgs(input4), this.ParsingContext);

			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result1.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result2.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result3.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result4.Result).Type);
		}

		[TestMethod]
		public void AtanhHandlesInputDatesAreSeperatedByDashes()
		{
			var function = new Atanh();

			var input1 = "1-17-2017 2:00";
			var input2 = "1-17-2017 2:00 am";

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);

			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result1.Result).Type);
			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result2.Result).Type);
		}

		[TestMethod]
		public void AtanhHandlesDoublesCorrectly()
		{
			var function = new Atanh();

			var input1 = 0.5;
			var input2 = 0.25;
			var input3 = 0.9;
			var input4 = -0.9;
			var input5 = ".5";

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);
			var result3 = function.Execute(FunctionsHelper.CreateArgs(input3), this.ParsingContext);
			var result4 = function.Execute(FunctionsHelper.CreateArgs(input4), this.ParsingContext);
			var result5 = function.Execute(FunctionsHelper.CreateArgs(input5), this.ParsingContext);

			Assert.AreEqual(0.549306144, System.Math.Round(result1.ResultNumeric, 9));
			Assert.AreEqual(0.255412812, System.Math.Round(result2.ResultNumeric, 9));
			Assert.AreEqual(1.47221949, System.Math.Round(result3.ResultNumeric, 9));
			Assert.AreEqual(-1.47221949, System.Math.Round(result4.ResultNumeric, 9));
			Assert.AreEqual(0.549306144, System.Math.Round(result5.ResultNumeric, 9));
		}

		[TestMethod]
		public void AtanhHandlesTrueOrFalse()
		{
			var function = new Atanh();

			var input1 = true;
			var input2 = false;

			var result1 = function.Execute(FunctionsHelper.CreateArgs(input1), this.ParsingContext);
			var result2 = function.Execute(FunctionsHelper.CreateArgs(input2), this.ParsingContext);

			Assert.AreEqual(eErrorType.Num, ((ExcelErrorValue)result1.Result).Type);
			Assert.AreEqual(0, System.Math.Round(result2.ResultNumeric, 9));
		}

		#endregion
	}
}
