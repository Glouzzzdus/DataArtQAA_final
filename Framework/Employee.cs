using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataArtQAA_Homework04.Framework
{
    public class Employee
    {
        private string email;
        private int age;
        private long salary;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age
        {
            get => age;
            set
            {
                if (value < 0 || value > 99)
                    throw new ArgumentException("Invalid age value");

                age = value;
            }
        }
        public string Email
        {
            get => email;
            set
            {
                string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
                if (!Regex.IsMatch(value, pattern))
                    throw new ArgumentException("Invalid email format");

                email = value;
            }
        }        
        public long Salary
        {
            get => salary;
            set
            {
                if (value < 0 || value > 9999999999)
                    throw new ArgumentException("Invalid salary value");

                salary = value;
            }
        }
        public string Department { get; set; }

        public Employee(string firstName, string lastName, int age, string email, long salary, string department)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException(nameof(lastName));

            if (string.IsNullOrEmpty(department))
                throw new ArgumentNullException(nameof(department));

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
            Salary = salary;
            Department = department;
        }

        public Employee(string[] employeeData)
        {
            if (employeeData.Length != 6)
                throw new ArgumentException("Invalid employee data");

            FirstName = employeeData[0];
            LastName = employeeData[1];
            Email = employeeData[3];

            if (int.TryParse(employeeData[2], out int parsedAge))
            {
                if (parsedAge > 99)
                    parsedAge = int.Parse(employeeData[2].Substring(0, 2));

                Age = parsedAge;
            }
            else
                throw new ArgumentException("Invalid value for Age property");

            if (long.TryParse(employeeData[4], out long parsedSalary))
            {
                if (parsedSalary > 9999999999)
                    parsedSalary = long.Parse(employeeData[4].Substring(0, 10));

                Salary = parsedSalary;
            }
            else
                throw new ArgumentException("Invalid value for Salary property");

            Department = employeeData[5];
        }
        public override string ToString() => $"Employee: {FirstName}, {LastName}, {Age}, {Email}, {Salary}, {Department}.";

        public string this[int index]
        {
            get
            {
                switch (index)
                {
                    case FirstNameIndex:
                        return FirstName;
                    case LastNameIndex:
                        return LastName;
                    case AgeIndex:
                        return Age.ToString();
                    case EmailIndex:
                        return Email;                    
                    case SalaryIndex:
                        return Salary.ToString();
                    case DepartmentIndex:
                        return Department;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), $"Invalid index: {index}");
                }
            }
            set
            {
                switch (index)
                {
                    case FirstNameIndex:
                        FirstName = value;
                        break;
                    case LastNameIndex:
                        LastName = value;
                        break;
                    case EmailIndex:
                        if (!Regex.IsMatch(value, EmailPattern))
                            throw new ArgumentException("Invalid email format");
                        Email = value;
                        break;                    
                    case SalaryIndex:
                        if (long.TryParse(value, out long parsedSalary))
                            Salary = parsedSalary;
                        else
                            throw new ArgumentException("Invalid value for Salary property");
                        break;
                    case DepartmentIndex:
                        Department = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), $"Invalid index: {index}");
                }
            }
        }
        

        private const int FirstNameIndex = 0;
        private const int LastNameIndex = 1;
        private const int AgeIndex = 2;
        private const int EmailIndex = 3;
        private const int SalaryIndex = 4;
        private const int DepartmentIndex = 5;
        private const string EmailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Employee other = (Employee)obj;
            return FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   Email == other.Email &&
                   Age == other.Age &&
                   Salary == other.Salary &&
                   Department == other.Department;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (FirstName != null ? FirstName.GetHashCode() : 0);
            hash = hash * 23 + (LastName != null ? LastName.GetHashCode() : 0);
            hash = hash * 23 + (Email != null ? Email.GetHashCode() : 0);
            hash = hash * 23 + Age.GetHashCode();
            hash = hash * 23 + Salary.GetHashCode();
            hash = hash * 23 + (Department != null ? Department.GetHashCode() : 0);
            return hash;
        }
    }
}
