using DataArtQAA_Homework04.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataArtQAA_Homework04
{
    [TestFixture]
    public class TablesPageTests:BaseTest
    {
        [Test]
        [Category("Add new record tests. Correct input.")]
        [TestCaseSource(typeof(TestData),nameof(TestData.AddValidData))]

        public void AddNewCorrectRecordTest(string[] inputs, bool validation)
        {
            Pages.Tables.Open();
            Pages.Tables.StartAddRecords();
            Pages.Tables.FillRegistrationFormFromArray(inputs);

            Employee expectedEmployee = new Employee(inputs);
            
            Assert.That(validation, Is.EqualTo(Pages.Tables.IsRecordExist(expectedEmployee)));
        }
        [Test]
        [Category("Add new record tests. Incorrect input.")]
        [TestCaseSource(typeof(TestData), nameof(TestData.AddInvalidData))]
        public void AddNewIncorrectRecordTest(string[] inputs, bool[] validation)
        {
            Pages.Tables.Open();
            Pages.Tables.StartAddRecords();
            Pages.Tables.FillRegistrationFormFromArray(inputs);

            var actualValidationResponse = new bool[]
            {
                Pages.Tables.IsFirstNameInputValid(),
                Pages.Tables.IsLastNameInputValid(),
                Pages.Tables.IsEmailInputValid(),
                Pages.Tables.IsAgeInputValid(),
                Pages.Tables.IsSalaryInputValid(),
                Pages.Tables.IsDepartmentInputValid(),
            };

            Assert.True(Pages.Tables.IsRegistrationFormDisplayed());
            Assert.True(validation.SequenceEqual(actualValidationResponse));
        }

        [Test]
        [Category("Search tests.")]
        [TestCaseSource(typeof(TestData), nameof(TestData.SearchSome))]
        public void SearchSomeWordTest(string wordToSearch, bool noDataPopupDisplay, Employee[] expectedArray)
        {
            Pages.Tables.Open();
            Pages.Tables.SetSearchPhrase(wordToSearch);
            var actualArray = Pages.Tables.SuggesterWorks(wordToSearch);

            Assert.True(Pages.Tables.IsNoDataAlert() == noDataPopupDisplay);
            Assert.True(actualArray.SequenceEqual(expectedArray));
        }

        [Test]
        [Category("Pagination tests.")]
        [TestCaseSource(typeof(TestData), nameof(TestData.PaginationCount))]
        public void CountPagesAfterFillingTest(int employeesAdded,int expectedPagesQuantity)
        {
            Pages.Tables.Open();
            for (int i = 0; i < employeesAdded; i++)
            {
                Pages.Tables.StartAddRecords();                
                Pages.Tables.FillRegistrationFormFromObject(Pages.Tables.CreateRandomEmployee());
            }            
            Assert.That(Pages.Tables.GetPageCount(), Is.EqualTo(expectedPagesQuantity));
        }

        [Test]
        [Category("Pagination tests.")]
        [TestCaseSource(typeof(TestData), nameof(TestData.PaginationCountAfterChange))]
        public void CountPagesAfterRowsCountChangedTest(int employeesAdded, int rows ,int expectedPagesQuantity)
        {
            Pages.Tables.Open();
            for (int i = 0; i < employeesAdded; i++)
            {
                Pages.Tables.StartAddRecords();
                Pages.Tables.FillRegistrationFormFromObject(Pages.Tables.CreateRandomEmployee());
            }
             
            Pages.Tables.SetPagination(rows);
            Assert.That(Pages.Tables.GetPageCount(), Is.EqualTo(expectedPagesQuantity));
        }

        [Test]
        [Category("Table management tests. Deleting records.")]
        [TestCaseSource(typeof(TestData), nameof(TestData.DeleteRecords))]
        public void DeletingRecordsTest(int rowNumber, bool expectedResult)
        {
            Pages.Tables.Open();
            var recordThatWillDelete = Pages.Tables.GetRecord(Pages.Tables.RowGroups(),rowNumber - 1);
            Pages.Tables.DeleteRecord(rowNumber);

            Assert.True(Pages.Tables.IsRecordExist(recordThatWillDelete) != expectedResult);
        }

        [Test]
        [Category("Table management tests. Editing records.")]
        [TestCaseSource(typeof(TestData), nameof(TestData.EditRecords))]
        public void InitiateEditingRecordsTest(int rowNumber)
        {
            Pages.Tables.Open();
            var recordThatWilledit = Pages.Tables.GetRecord(Pages.Tables.RowGroups(), rowNumber - 1);
            Pages.Tables.EditRecord(rowNumber);
            var editingRecord = Pages.Tables.GetDataFromRegistrationForm();

            Assert.That(editingRecord.Equals(recordThatWilledit), Is.True);            
        }

        [Test]
        [Category("Table management tests. Editing records.")]
        [TestCaseSource(typeof(TestData), nameof(TestData.EditRecords))]
        public void PerformEditingRecordsTest(int rowNumber)
        {
            Pages.Tables.Open();
            var newRecord = Pages.Tables.CreateRandomEmployee();
            Pages.Tables.EditRecord(rowNumber);
            Pages.Tables.ChangeDataInRegistrationForm(newRecord);          

            Assert.That(Pages.Tables.IsRecordExist(newRecord), Is.True);
        }
    }
}
