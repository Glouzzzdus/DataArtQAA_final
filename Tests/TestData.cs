using DataArtQAA_Homework04.Framework;

namespace DataArtQAA_Homework04
{
    public static class TestData
    {
        public static IEnumerable<TestCaseData> AddValidData()
        {
            yield return new TestCaseData(new string[] { "John", "Doe", "25", "doe@example.com", "15000", "Delivery" }, true);
            yield return new TestCaseData(new string[] { "Ella", "Fitzgerald", "100000", "fitzgerald@example.com", "20003450009", "Accountant" }, true);
        }
        public static IEnumerable<TestCaseData> AddInvalidData()
        {
            yield return new TestCaseData(new string[] { "", "", "33", "nobody@example.com", "33333", "" }, new bool[] { false, false, true, true, true, false });
            yield return new TestCaseData(new string[] { "Somebody", "Else", "age", "somebody@example", "salary", "Somewhere" }, new bool[] { true, true, false, false, false, true });
        }
        public static IEnumerable<TestCaseData> SearchSome()
        {
            yield return new TestCaseData("ance", false, new Employee[] { new Employee("Cierra", "Vega", 39, "cierra@example.com", 10000, "Insurance"), new Employee("Alden", "Cantrell", 45, "alden@example.com", 12000, "Compliance") });
            yield return new TestCaseData("erra", false, new Employee[] { new Employee("Cierra", "Vega", 39, "cierra@example.com", 10000, "Insurance"), new Employee("Kierra", "Gentry", 29, "kierra@example.com", 2000, "Legal") });
            yield return new TestCaseData("obso", true, new Employee[] { });
        }
        public static IEnumerable<TestCaseData> PaginationCount()
        {
            yield return new TestCaseData(15, 2);
            yield return new TestCaseData(20, 3);
        }
        public static IEnumerable<TestCaseData> PaginationCountAfterChange()
        {
            yield return new TestCaseData(20, 5, 5);
            yield return new TestCaseData(20, 25, 1);
        }
        public static IEnumerable<TestCaseData> DeleteRecords() 
        {
            yield return new TestCaseData(1, true);
            yield return new TestCaseData(2, true);
        }
        public static IEnumerable<TestCaseData> EditRecords()
        { 
            yield return new TestCaseData(2);
            yield return new TestCaseData(3);
        }
    }
}
