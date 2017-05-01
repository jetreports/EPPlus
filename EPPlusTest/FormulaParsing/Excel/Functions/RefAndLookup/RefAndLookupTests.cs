﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using Rhino.Mocks;
using EPPlusTest.FormulaParsing.TestHelpers;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.FormulaParsing;
using OfficeOpenXml.FormulaParsing.Exceptions;
using OfficeOpenXml.FormulaParsing.ExcelUtilities;
using OfficeOpenXml.FormulaParsing.Excel.Functions;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using AddressFunction = OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup.Address;

namespace EPPlusTest.Excel.Functions
{
    [TestClass]
    public class RefAndLookupTests
    {
        const string WorksheetName = null;

		[TestMethod]
		public void AddressWithTooFewArgumentsReturnsPoundValue()
		{
			var function = new OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup.Address();
			var args = FunctionsHelper.CreateArgs("One Arg Only");
			var result = function.Execute(args, null);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}

        [TestMethod]
        public void LookupArgumentsShouldSetSearchedValue()
        {
            var args = FunctionsHelper.CreateArgs(1, "A:B", 2);
            var lookupArgs = new LookupArguments(args, ParsingContext.Create());
            Assert.AreEqual(1, lookupArgs.SearchedValue);
        }

        [TestMethod]
        public void LookupArgumentsShouldSetRangeAddress()
        {
            var args = FunctionsHelper.CreateArgs(1, "A:B", 2);
            var lookupArgs = new LookupArguments(args, ParsingContext.Create());
            Assert.AreEqual("A:B", lookupArgs.RangeAddress);
        }

        [TestMethod]
        public void LookupArgumentsShouldSetColIndex()
        {
            var args = FunctionsHelper.CreateArgs(1, "A:B", 2);
            var lookupArgs = new LookupArguments(args, ParsingContext.Create());
            Assert.AreEqual(2, lookupArgs.LookupIndex);
        }

        [TestMethod]
        public void LookupArgumentsShouldSetColIndexFromReferenceSameSheet()
        {
            // This test addresses a bug fix where under certain cases a faulty lookup is created
            // that always references the first sheet. That's why there is an unused Worksheet1.
            const int expectedIndex = 9;
            using (var excelPackage = new ExcelPackage())
            using (var worksheet1 = excelPackage.Workbook.Worksheets.Add("Worksheet1"))
            using (var worksheet2 = excelPackage.Workbook.Worksheets.Add("Worksheet2"))
            {
                worksheet1.Cells["C3"].Value = expectedIndex + 1;
                worksheet2.Cells["C3"].Value = expectedIndex;
                var parsingContext = this.BuildParsingContext(excelPackage);
                var scopeAddress = parsingContext.RangeAddressFactory.Create("Worksheet2!D4");
                using (parsingContext.Scopes.NewScope(scopeAddress))
                {
                    var args = new[]
                    {
                        new FunctionArgument(1),
                        new FunctionArgument("A:B", DataType.ExcelAddress),
                        new FunctionArgument("C3", DataType.ExcelAddress)
                    };
                    var lookupArgs = new LookupArguments(args, parsingContext);
                    Assert.AreEqual(expectedIndex, lookupArgs.LookupIndex);
                }
            }
        }

        [TestMethod]
        public void LookupArgumentsShouldSetColIndexFromReferenceDifferentSheet()
        {
            // This test addresses a bug fix where under certain cases a faulty lookup is created
            // that always references the first sheet. That's why there is an unused Worksheet1.
            const int expectedIndex = 9;
            using (var excelPackage = new ExcelPackage())
            using (var worksheet1 = excelPackage.Workbook.Worksheets.Add("Worksheet1"))
            using (var worksheet2 = excelPackage.Workbook.Worksheets.Add("Worksheet2"))
            using (var worksheet3 = excelPackage.Workbook.Worksheets.Add("Worksheet3"))
            {
                worksheet1.Cells["C3"].Value = expectedIndex + 1;
                worksheet2.Cells["C3"].Value = expectedIndex + 2;
                worksheet3.Cells["C3"].Value = expectedIndex;
                var parsingContext = this.BuildParsingContext(excelPackage);
                var scopeAddress = parsingContext.RangeAddressFactory.Create("Worksheet2!D4");
                using (parsingContext.Scopes.NewScope(scopeAddress))
                {
                    var args = new[]
                    {
                        new FunctionArgument(1),
                        new FunctionArgument("A:B", DataType.ExcelAddress),
                        new FunctionArgument("Worksheet3!C3", DataType.ExcelAddress)
                    };
                    var lookupArgs = new LookupArguments(args, parsingContext);
                    Assert.AreEqual(expectedIndex, lookupArgs.LookupIndex);
                }
            }
        }

        [TestMethod]
        public void LookupArgumentsShouldSetRangeLookupToTrueAsDefaultValue()
        {
            var args = FunctionsHelper.CreateArgs(1, "A:B", 2);
            var lookupArgs = new LookupArguments(args, ParsingContext.Create());
            Assert.IsTrue(lookupArgs.RangeLookup);
        }

        [TestMethod]
        public void LookupArgumentsShouldSetRangeLookupToTrueWhenTrueIsSupplied()
        {
            var args = FunctionsHelper.CreateArgs(1, "A:B", 2, true);
            var lookupArgs = new LookupArguments(args, ParsingContext.Create());
            Assert.IsTrue(lookupArgs.RangeLookup);
        }

        [TestMethod]
        public void VLookupShouldReturnResultFromMatchingRow()
        {
            var func = new VLookup();
            var args = FunctionsHelper.CreateArgs(2, "A1:B2", 2);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);
            
            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 2)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName,2, 1)).Return(2);
            provider.Stub(x => x.GetCellValue(WorksheetName,2, 2)).Return(5);

            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual(5, result.Result);
        }

        [TestMethod]
        public void VLookupShouldReturnClosestValueBelowWhenRangeLookupIsTrue()
        {
            var func = new VLookup();
            var args = FunctionsHelper.CreateArgs(4, "A1:B2", 2, true);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 2)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName,2, 1)).Return(5);
            provider.Stub(x => x.GetCellValue(WorksheetName,2, 2)).Return(4);

            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual(1, result.Result);
        }

        [TestMethod]
        public void VLookupShouldReturnClosestStringValueBelowWhenRangeLookupIsTrue()
        {
            var func = new VLookup();
            var args = FunctionsHelper.CreateArgs("B", "A1:B2", 2, true);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            //provider.Stub(x => x.GetCellValue(WorksheetName,0, 0)).Return(new ExcelCell("A", null, 0, 0));
            //provider.Stub(x => x.GetCellValue(WorksheetName,0, 1)).Return(new ExcelCell(1, null, 0, 0));
            //provider.Stub(x => x.GetCellValue(WorksheetName,1, 0)).Return(new ExcelCell("C", null, 0, 0));
            //provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(new ExcelCell(4, null, 0, 0));

            provider.Stub(x => x.GetCellValue(WorksheetName, 1, 1)).Return("A");
            provider.Stub(x => x.GetCellValue(WorksheetName, 1, 2)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName, 2, 1)).Return("C");
            provider.Stub(x => x.GetCellValue(WorksheetName, 2, 2)).Return(4);

            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual(1, result.Result);
        }

        [TestMethod]
        public void HLookupShouldReturnResultFromMatchingRow()
        {
            var func = new HLookup();
            var args = FunctionsHelper.CreateArgs(2, "A1:B2", 2);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            //provider.Stub(x => x.GetCellValue(WorksheetName,0, 0)).Return(new ExcelCell(3, null, 0, 0));
            //provider.Stub(x => x.GetCellValue(WorksheetName,0, 1)).Return(new ExcelCell(1, null, 0, 0));
            //provider.Stub(x => x.GetCellValue(WorksheetName,1, 0)).Return(new ExcelCell(2, null, 0, 0));
            //provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(new ExcelCell(5, null, 0, 0));

            provider.Stub(x => x.GetCellValue(WorksheetName, 1, 1)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName, 1, 2)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName, 2, 1)).Return(2);
            provider.Stub(x => x.GetCellValue(WorksheetName, 2, 2)).Return(5);

            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual(5, result.Result);
        }

        [TestMethod]
        public void HLookupShouldReturnNaErrorIfNoMatchingRecordIsFoundWhenRangeLookupIsFalse()
        {
            var func = new HLookup();
            var args = FunctionsHelper.CreateArgs(2, "A1:B2", 2, false);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();

            provider.Stub(x => x.GetCellValue(WorksheetName, 1, 1)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName, 1, 2)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName, 2, 1)).Return(2);
            provider.Stub(x => x.GetCellValue(WorksheetName, 2, 2)).Return(5);

            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            var expectedResult = ExcelErrorValue.Create(eErrorType.NA);
            Assert.AreEqual(expectedResult, result.Result);
        }

        [TestMethod]
        public void HLookupShouldReturnErrorIfNoMatchingRecordIsFoundWhenRangeLookupIsTrue()
        {
            var func = new HLookup();
            var args = FunctionsHelper.CreateArgs(1, "A1:B2", 2, true);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();

            provider.Stub(x => x.GetCellValue(WorksheetName, 1, 1)).Return(2);
            provider.Stub(x => x.GetCellValue(WorksheetName, 1, 2)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName, 2, 1)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName, 2, 2)).Return(5);

            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual(result.DataType, DataType.ExcelError);
        }

        [TestMethod]
        public void LookupShouldReturnResultFromMatchingRowArrayVertical()
        {
            var func = new Lookup();
            var args = FunctionsHelper.CreateArgs(4, "A1:B3", 2);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 2)).Return("A");
            provider.Stub(x => x.GetCellValue(WorksheetName,2, 1)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName,2, 2)).Return("B");
            provider.Stub(x => x.GetCellValue(WorksheetName,3, 1)).Return(5);
            provider.Stub(x => x.GetCellValue(WorksheetName,3, 2)).Return("C");

            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual("B", result.Result);
        }

        [TestMethod]
        public void LookupShouldReturnResultFromMatchingRowArrayHorizontal()
        {
            var func = new Lookup();
            var args = FunctionsHelper.CreateArgs(4, "A1:C2", 2);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 2)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 3)).Return(5);
            provider.Stub(x => x.GetCellValue(WorksheetName,2, 1)).Return("A");
            provider.Stub(x => x.GetCellValue(WorksheetName,2, 2)).Return("B");
            provider.Stub(x => x.GetCellValue(WorksheetName,2, 3)).Return("C");

            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual("B", result.Result);
        }

        [TestMethod]
        public void LookupShouldReturnResultFromMatchingSecondArrayHorizontal()
        {
            var func = new Lookup();
            var args = FunctionsHelper.CreateArgs(4, "A1:C1", "A3:C3");
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 2)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 3)).Return(5);
            provider.Stub(x => x.GetCellValue(WorksheetName,3, 1)).Return("A");
            provider.Stub(x => x.GetCellValue(WorksheetName,3, 2)).Return("B");
            provider.Stub(x => x.GetCellValue(WorksheetName,3, 3)).Return("C");

            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual("B", result.Result);
        }

        [TestMethod]
        public void LookupShouldReturnResultFromMatchingSecondArrayHorizontalWithOffset()
        {
            var func = new Lookup();
            var args = FunctionsHelper.CreateArgs(4, "A1:C1", "B3:D3");
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 2)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 3)).Return(5);
            provider.Stub(x => x.GetCellValue(WorksheetName,3, 2)).Return("A");
            provider.Stub(x => x.GetCellValue(WorksheetName,3, 3)).Return("B");
            provider.Stub(x => x.GetCellValue(WorksheetName,3, 4)).Return("C");

            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual("B", result.Result);
        }

        [TestMethod]
        public void MatchShouldReturnIndexOfMatchingValHorizontal_MatchTypeExact()
        {
            var func = new Match();
            var args = FunctionsHelper.CreateArgs(3, "A1:C1", 0);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 2)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 3)).Return(5);
            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual(2, result.Result);
        }

        [TestMethod]
        public void MatchShouldReturnIndexOfMatchingValVertical_MatchTypeExact()
        {
            var func = new Match();
            var args = FunctionsHelper.CreateArgs(3, "A1:A3", 0);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName,2, 1)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName,3, 1)).Return(5);
            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual(2, result.Result);
        }

        [TestMethod]
        public void MatchShouldReturnIndexOfMatchingValHorizontal_MatchTypeClosestBelow()
        {
            var func = new Match();
            var args = FunctionsHelper.CreateArgs(4, "A1:C1", 1);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(1);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 2)).Return(3);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 3)).Return(5);
            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual(2, result.Result);
        }

        [TestMethod]
        public void MatchShouldReturnIndexOfMatchingValHorizontal_MatchTypeClosestAbove()
        {
            var func = new Match();
            var args = FunctionsHelper.CreateArgs(6, "A1:C1", -1);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(10);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 2)).Return(8);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 3)).Return(5);
            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual(2, result.Result);
        }

        [TestMethod]
        public void MatchShouldReturnFirstItemWhenExactMatch_MatchTypeClosestAbove()
        {
            var func = new Match();
            var args = FunctionsHelper.CreateArgs(10, "A1:C1", -1);
            var parsingContext = ParsingContext.Create();
            parsingContext.Scopes.NewScope(RangeAddress.Empty);

            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 1)).Return(10);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 2)).Return(8);
            provider.Stub(x => x.GetCellValue(WorksheetName,1, 3)).Return(5);
            parsingContext.ExcelDataProvider = provider;
            var result = func.Execute(args, parsingContext);
            Assert.AreEqual(1, result.Result);
        }

        [TestMethod, Ignore]
        public void MatchShouldHandleAddressOnOtherSheet()
        {
            using (var package = new ExcelPackage())
            {
                var sheet1 = package.Workbook.Worksheets.Add("Sheet1");
                var sheet2 = package.Workbook.Worksheets.Add("Sheet2");
                sheet1.Cells["A1"].Formula = "Match(10, Sheet2!A1:Sheet2!A3, 0)";
                sheet2.Cells["A1"].Value = 9;
                sheet2.Cells["A2"].Value = 10;
                sheet2.Cells["A3"].Value = 11;
                sheet1.Calculate();
                Assert.AreEqual(2, sheet1.Cells["A1"].Value);
            }    
        }

        [TestMethod]
        public void RowShouldReturnRowFromCurrentScopeIfNoAddressIsSupplied()
        {
            var func = new Row();
            var parsingContext = ParsingContext.Create();
            var rangeAddressFactory = new RangeAddressFactory(MockRepository.GenerateStub<ExcelDataProvider>());
            parsingContext.Scopes.NewScope(rangeAddressFactory.Create("A2"));
            var result = func.Execute(Enumerable.Empty<FunctionArgument>(), parsingContext);
            Assert.AreEqual(2, result.Result);
        }

        [TestMethod]
        public void RowShouldReturnRowSuppliedAddress()
        {
            var func = new Row();
            var parsingContext = ParsingContext.Create();
            parsingContext.ExcelDataProvider = MockRepository.GenerateStub<ExcelDataProvider>();
            var result = func.Execute(FunctionsHelper.CreateArgs("A3"), parsingContext);
            Assert.AreEqual(3, result.Result);
        }

        [TestMethod]
        public void ColumnShouldReturnRowFromCurrentScopeIfNoAddressIsSupplied()
        {
            var func = new Column();
            var parsingContext = ParsingContext.Create();
            var rangeAddressFactory = new RangeAddressFactory(MockRepository.GenerateStub<ExcelDataProvider>());
            parsingContext.Scopes.NewScope(rangeAddressFactory.Create("B2"));
            var result = func.Execute(Enumerable.Empty<FunctionArgument>(), parsingContext);
            Assert.AreEqual(2, result.Result);
        }

        [TestMethod]
        public void ColumnShouldReturnRowSuppliedAddress()
        {
            var func = new Column();
            var parsingContext = ParsingContext.Create();
            parsingContext.ExcelDataProvider = MockRepository.GenerateStub<ExcelDataProvider>();
            var result = func.Execute(FunctionsHelper.CreateArgs("E3"), parsingContext);
            Assert.AreEqual(5, result.Result);
        }

        [TestMethod]
        public void RowsShouldReturnNbrOfRowsSuppliedRange()
        {
            var func = new Rows();
            var parsingContext = ParsingContext.Create();
            parsingContext.ExcelDataProvider = MockRepository.GenerateStub<ExcelDataProvider>();
            var result = func.Execute(FunctionsHelper.CreateArgs("A1:B3"), parsingContext);
            Assert.AreEqual(3, result.Result);
        }

        [TestMethod]
        public void RowsShouldReturnNbrOfRowsForEntireColumn()
        {
            var func = new Rows();
            var parsingContext = ParsingContext.Create();
            parsingContext.ExcelDataProvider = MockRepository.GenerateStub<ExcelDataProvider>();
            var result = func.Execute(FunctionsHelper.CreateArgs("A:B"), parsingContext);
            Assert.AreEqual(1048576, result.Result);
        }

        [TestMethod]
        public void ColumnssShouldReturnNbrOfRowsSuppliedRange()
        {
            var func = new Columns();
            var parsingContext = ParsingContext.Create();
            parsingContext.ExcelDataProvider = MockRepository.GenerateStub<ExcelDataProvider>();
            var result = func.Execute(FunctionsHelper.CreateArgs("A1:E3"), parsingContext);
            Assert.AreEqual(5, result.Result);
        }

        [TestMethod]
        public void ChooseShouldReturnItemByIndex()
        {
            var func = new Choose();
            var parsingContext = ParsingContext.Create();
            var result = func.Execute(FunctionsHelper.CreateArgs(1, "A", "B"), parsingContext);
            Assert.AreEqual("A", result.Result);
        }

        [TestMethod]
        public void AddressShouldReturnAddressByIndexWithDefaultRefType()
        {
            var func = new AddressFunction();
            var parsingContext = ParsingContext.Create();
            parsingContext.ExcelDataProvider = MockRepository.GenerateStub<ExcelDataProvider>();
            parsingContext.ExcelDataProvider.Stub(x => x.ExcelMaxRows).Return(10);
            var result = func.Execute(FunctionsHelper.CreateArgs(1, 2), parsingContext);
            Assert.AreEqual("$B$1", result.Result);
        }

        [TestMethod]
        public void AddressShouldReturnAddressByIndexWithRelativeType()
        {
            var func = new AddressFunction();
            var parsingContext = ParsingContext.Create();
            parsingContext.ExcelDataProvider = MockRepository.GenerateStub<ExcelDataProvider>();
            parsingContext.ExcelDataProvider.Stub(x => x.ExcelMaxRows).Return(10);
            var result = func.Execute(FunctionsHelper.CreateArgs(1, 2, (int)ExcelReferenceType.RelativeRowAndColumn), parsingContext);
            Assert.AreEqual("B1", result.Result);
        }

        [TestMethod]
        public void AddressShouldReturnAddressByWithSpecifiedWorksheet()
        {
            var func = new AddressFunction();
            var parsingContext = ParsingContext.Create();
            parsingContext.ExcelDataProvider = MockRepository.GenerateStub<ExcelDataProvider>();
            parsingContext.ExcelDataProvider.Stub(x => x.ExcelMaxRows).Return(10);
            var result = func.Execute(FunctionsHelper.CreateArgs(1, 2, (int)ExcelReferenceType.RelativeRowAndColumn, true, "Worksheet1"), parsingContext);
            Assert.AreEqual("Worksheet1!B1", result.Result);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void AddressShouldThrowIfR1C1FormatIsSpecified()
        {
            var func = new AddressFunction();
            var parsingContext = ParsingContext.Create();
            parsingContext.ExcelDataProvider = MockRepository.GenerateStub<ExcelDataProvider>();
            parsingContext.ExcelDataProvider.Stub(x => x.ExcelMaxRows).Return(10);
            var result = func.Execute(FunctionsHelper.CreateArgs(1, 2, (int)ExcelReferenceType.RelativeRowAndColumn, false), parsingContext);
        }

		[TestMethod]
		public void OffsetReturnsPoundValueIfTooFewArgumentsAreSupplied()
		{
			var func = new Offset();
			var parsingContext = ParsingContext.Create();
			var args = FunctionsHelper.CreateArgs("B2", 2);
			var result = func.Execute(args, parsingContext);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}

		[TestMethod]
		public void OffsetReturnsPoundRefIfInvalidArgumentsAreSupplied()
		{
			var func = new Offset();
			var parsingContext = ParsingContext.Create();
			var args = FunctionsHelper.CreateArgs("B2", 0, 0, 0, 0);
			var result = func.Execute(args, parsingContext);
			Assert.AreEqual(eErrorType.Ref, ((ExcelErrorValue)result.Result).Type);
		}

		[TestMethod]
		public void ColumnsWithInvalidArgumentReturnsPoundValue()
		{
			var func = new Columns();
			var parsingContext = ParsingContext.Create();
			var args = FunctionsHelper.CreateArgs();
			var result = func.Execute(args, parsingContext);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}

		[TestMethod]
		public void HLookupWithInvalidArgumentReturnsPoundValue()
		{
			var func = new HLookup();
			var parsingContext = ParsingContext.Create();
			var args = FunctionsHelper.CreateArgs();
			var result = func.Execute(args, parsingContext);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}

		[TestMethod]
		public void LookupWithInvalidArgumentReturnsPoundValue()
		{
			var func = new Lookup();
			var parsingContext = ParsingContext.Create();
			var args = FunctionsHelper.CreateArgs();
			var result = func.Execute(args, parsingContext);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}

		[TestMethod]
		public void MatchWithInvalidArgumentReturnsPoundValue()
		{
			var func = new Match();
			var parsingContext = ParsingContext.Create();
			var args = FunctionsHelper.CreateArgs();
			var result = func.Execute(args, parsingContext);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}
 
		[TestMethod]
		public void OffsetWithInvalidArgumentReturnsPoundValue()
		{
			var func = new Offset();
			var parsingContext = ParsingContext.Create();
			var args = FunctionsHelper.CreateArgs();
			var result = func.Execute(args, parsingContext);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}

		[TestMethod]
		public void RowsWithInvalidArgumentReturnsPoundValue()
		{
			var func = new Rows();
			var parsingContext = ParsingContext.Create();
			var args = FunctionsHelper.CreateArgs();
			var result = func.Execute(args, parsingContext);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}

		[TestMethod]
		public void VLookupWithInvalidArgumentReturnsPoundValue()
		{
			var func = new VLookup();
			var parsingContext = ParsingContext.Create();
			var args = FunctionsHelper.CreateArgs();
			var result = func.Execute(args, parsingContext);
			Assert.AreEqual(eErrorType.Value, ((ExcelErrorValue)result.Result).Type);
		}

        #region Helper Methods
        private ParsingContext BuildParsingContext(ExcelPackage excelPackage)
        {
            var parsingContext = ParsingContext.Create();
            parsingContext.ExcelDataProvider = new EpplusExcelDataProvider(excelPackage);
            parsingContext.RangeAddressFactory = new RangeAddressFactory(parsingContext.ExcelDataProvider);
            return parsingContext;
        }
        #endregion
    }
}