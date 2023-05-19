using DataArtQAA_Homework04.Framework;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataArtQAA_Homework04.Pages
{
    public class TablesPage:BasePage
    {
        protected override string Url => "https://demoqa.com/webtables";
        public TablesPage(IWebDriver driver) : base(driver) { }

        string[] emptyRecord = new string[]{" ", " ", " ", " ", " ", " "};
        //Elements
        //Add new Record section
        private IWebElement _addNewRecordBtn => driver.FindElement(By.Id("addNewRecordButton"));
        private IWebElement _registrationFormModal => driver.FindElement(By.CssSelector("div.modal-content"));
        private IWebElement _addFirstNameInput => driver.FindElement(By.XPath("//form[@id='userForm']//input[@id='firstName']"));
        private IWebElement _addLastNameInput => driver.FindElement(By.XPath("//form[@id='userForm']//input[@id='lastName']"));
        private IWebElement _addEmailInput => driver.FindElement(By.XPath("//form[@id='userForm']//input[@id='userEmail']"));
        private IWebElement _addAgeInput => driver.FindElement(By.XPath("//form[@id='userForm']//input[@id='age']"));
        private IWebElement _addSalaryInput => driver.FindElement(By.XPath("//form[@id='userForm']//input[@id='salary']"));
        private IWebElement _addDepartmentInput => driver.FindElement(By.XPath("//form[@id='userForm']//input[@id='department']"));
        private IWebElement _addSubmitBtn => driver.FindElement(By.XPath("//form[@id='userForm']//button[@id='submit']"));
        private IWebElement _addCloseBtn => driver.FindElement(By.XPath("//button[@class='close']"));
        //Search section
        private IWebElement _searchInput => driver.FindElement(By.Id("searchBox"));
        private ReadOnlyCollection<IWebElement> _noDataAlert => driver.FindElements(By.CssSelector(".rt-noData"));
        //Table body section
        private IWebElement[] _tableRows => driver.FindElements(By.CssSelector(".rt-tr-group")).ToArray();
        private IWebElement[] _tableCells => driver.FindElements(By.XPath("//div[@role='gridcell']")).ToArray();

        //Pagination section
        private IWebElement _paginationPreviousBtn => driver.FindElement(By.XPath("//button[text()='Previous']"));
        private IWebElement _paginationNextBtn => driver.FindElement(By.XPath("//button[text()='Next']"));
        private IWebElement _paginationInput => driver.FindElement(By.XPath("//input[@aria-label]"));
        private SelectElement _paginationSelect => new SelectElement(driver.FindElement(By.XPath("//select[@aria-label]")));
        private IWebElement _totalPagesIndicator => driver.FindElement(By.ClassName("-totalPages"));

        //===================================================================================================
        //Methods
        public Employee CreateRandomEmployee()
        {
            Random random = new Random();
            string rFirstName = $"F{random.Next(1, 100000)}";
            string rLastName = $"L{random.Next(1, 100000)}";
            int rAge = random.Next(1, 99);
            string rEmail = $"{random.Next(1, 100000)}@example.com";
            long rSalary = random.NextInt64(100, 999999);
            string rDepartment = $"D{random.Next(1, 1000000)}";

            return new Employee(rFirstName, rLastName, rAge, rEmail, rSalary, rDepartment);
        }
        public string[][] RowGroups()
        {
            var rowGroup = new string[_tableRows.Length][];
            int currentIndex = 0;

            for (int i = 0; i < _tableRows.Length; i++)
            {
                string[] subarray = new string[6];
                int subarrayIndex = 0;

                while (currentIndex < _tableCells.Length && subarrayIndex < 6)
                {
                    if ((currentIndex + 1) % 7 != 0)
                    {
                        subarray[subarrayIndex] = _tableCells[currentIndex].Text;
                        subarrayIndex++;
                    }
                    currentIndex++;
                }

                rowGroup[i] = subarray;
            }

            return rowGroup;
        }
        //Add new record section
        public void StartAddRecords() => _addNewRecordBtn.Click();
        public bool IsRegistrationFormDisplayed()
        {
            return _registrationFormModal.Displayed;
        }
        public void FillRegistrationFormFromObject(Employee employee)
        {
            _addFirstNameInput.SendKeys(employee.FirstName);
            _addLastNameInput.SendKeys(employee.LastName);
            _addEmailInput.SendKeys(employee.Email);
            _addAgeInput.SendKeys(employee.Age.ToString());
            _addSalaryInput.SendKeys(employee.Salary.ToString());
            _addDepartmentInput.SendKeys(employee.Department);
            _addSubmitBtn.Click();
        }
        public void FillRegistrationFormFromArray(string[] input)
        {
            _addFirstNameInput.SendKeys(input[0]);
            _addLastNameInput.SendKeys(input[1]);
            _addEmailInput.SendKeys(input[3]);
            _addAgeInput.SendKeys(input[2]);
            _addSalaryInput.SendKeys(input[4]);
            _addDepartmentInput.SendKeys(input[5]);
            _addSubmitBtn.Click();            
        }        
        public bool IsFirstNameInputValid()
        {
            return (bool)(driver as IJavaScriptExecutor).ExecuteScript("return arguments[0].validity.valid;", _addFirstNameInput);
        }
        public bool IsLastNameInputValid()
        {
            return (bool)(driver as IJavaScriptExecutor).ExecuteScript("return arguments[0].validity.valid;", _addLastNameInput);
        }
        public bool IsEmailInputValid()
        {
            return (bool)(driver as IJavaScriptExecutor).ExecuteScript("return arguments[0].validity.valid;", _addEmailInput);
        }
        public bool IsAgeInputValid()
        {
            return (bool)(driver as IJavaScriptExecutor).ExecuteScript("return arguments[0].validity.valid;", _addAgeInput);
        }
        public bool IsSalaryInputValid()
        {
            return (bool)(driver as IJavaScriptExecutor).ExecuteScript("return arguments[0].validity.valid;", _addSalaryInput);
        }
        public bool IsDepartmentInputValid()
        {
            return (bool)(driver as IJavaScriptExecutor).ExecuteScript("return arguments[0].validity.valid;", _addDepartmentInput);
        }
        public void CloseRegistrationForm() => _addCloseBtn.Click();
        public Employee GetRecord(string[][] rows, int rowNumber)
        {
            return new Employee(rows[rowNumber][0], rows[rowNumber][1], Int32.Parse(rows[rowNumber][2]), rows[rowNumber][3], long.Parse(rows[rowNumber][4]), rows[rowNumber][5]);
        }

        public bool IsRecordExist(Employee employee)
        {
            var currentTable = RowGroups();
            for (int i = 0; i < currentTable.Length; i++)
            {
                if (currentTable[i].SequenceEqual(emptyRecord)) break;
                if (employee.Equals(GetRecord(currentTable, i))) return true;
            }
            return false;
        }

        //Pagination section
        public void SetVisibleTablePage(int pageNumber) => _paginationInput.SendKeys(pageNumber.ToString());
        public void GoToNextTablePage() => _paginationNextBtn.Click();
        public void GoToPreviousTablePage() => _paginationPreviousBtn.Click();
        public void SetPagination(int rows) => _paginationSelect.SelectByValue(rows.ToString());
        public int GetPageCount() => int.Parse(_totalPagesIndicator.Text);
        

        //Search section
        public void SetSearchPhrase(string searchPhrase) => _searchInput.SendKeys(searchPhrase);
        public bool IsNoDataAlert() => _noDataAlert.Count>0;
        public Employee[] SuggesterWorks(string searchWord)
        {
            var searchResult = RowGroups();
            var result = new List<Employee>();
            foreach (var row in searchResult)
            {
                foreach (var cell in row)
                {
                    if (cell.Contains(searchWord))
                    {
                        result.Add(new Employee(row));
                        break;
                    }
                }
            }
            return result.ToArray();
        }
        public bool IsTableEmpty()
        {
            var searchResult = RowGroups();
            foreach (var row in searchResult)
            {
                foreach (var cell in row)
                {
                    if (cell != "") return false;
                }
            }
            if (_noDataAlert.Count > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("Table is empty but doesn't have an alert!");
            }
            
        }

        //Table section
        public void EditRecord(int rowNumber) => driver.FindElement(By.Id($"edit-record-{rowNumber}")).Click();
        public void DeleteRecord(int rowNumber) => driver.FindElement(By.Id($"delete-record-{rowNumber}")).Click();
        
        public Employee GetDataFromRegistrationForm()
        {
            return new Employee(_addFirstNameInput.GetAttribute("value"), _addLastNameInput.GetAttribute("value"), int.Parse(_addAgeInput.GetAttribute("value")), _addEmailInput.GetAttribute("value"), long.Parse(_addSalaryInput.GetAttribute("value")), _addDepartmentInput.GetAttribute("value"));
        }
        public void ChangeDataInRegistrationForm(Employee employee)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", _addFirstNameInput, "value", "");
            _addFirstNameInput.Clear();
            _addFirstNameInput.SendKeys(employee.FirstName);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", _addLastNameInput, "value", "");
            _addLastNameInput.Clear();
            _addLastNameInput.SendKeys(employee.LastName);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", _addEmailInput, "value", "");
            _addEmailInput.Clear();
            _addEmailInput.SendKeys(employee.Email);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", _addAgeInput, "value", "");
            _addAgeInput.Clear();
            _addAgeInput.SendKeys(employee.Age.ToString());
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", _addSalaryInput, "value", "");
            _addSalaryInput.Clear();
            _addSalaryInput.SendKeys(employee.Salary.ToString());
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", _addDepartmentInput, "value", "");
            _addDepartmentInput.Clear();
            _addDepartmentInput.SendKeys(employee.Department);

            _addSubmitBtn.Click();
        }
    }

}
