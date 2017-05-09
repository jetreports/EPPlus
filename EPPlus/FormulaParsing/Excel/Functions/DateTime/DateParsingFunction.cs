﻿using System.Collections.Generic;

namespace OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime
{
	public abstract class DateParsingFunction : ExcelFunction
	{
		protected System.DateTime ParseDate(IEnumerable<FunctionArgument> arguments, object dateObj)
		{
			System.DateTime date = System.DateTime.MinValue;
			if (dateObj is string)
			{
				date = System.DateTime.Parse(dateObj.ToString());
			}
			else
			{
				var d = ArgToDecimal(arguments, 0);
				date = System.DateTime.FromOADate(d);
			}
			return date;
		}
	}
}
