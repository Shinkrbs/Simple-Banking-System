using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simple_Banking_System.Classes
{
    public class Customer
    {
        // List of Customers Bank Accounts // Can Make Multiple Bank Accounts
        public List<Bank_Account> Accounts { get; private set; }

        #region Mandatory Fields
        public string CustomerID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime AccountCreationDate { get; private set; }
        #endregion

        #region Optional Fields
        public string MiddleName { get; private set; }
        public string Address { get; private set; }
        public int Age { get; private set; }
        #endregion

        #region Constructors
        // Mandatory Constructor
        public Customer(string customerid, string firstname, string lastname, DateTime accountcreationdate)
        {
            this.CustomerID = customerid;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = "";
            this.AccountCreationDate = accountcreationdate;
            this.Accounts = new List<Bank_Account>();
        }
        // Optional Fields Constructor
        public Customer(string customerid, string firstname, string lastname, DateTime accountcreationdate, string middlename = "", string address = "", int age = 0)
        {
            this.CustomerID = customerid;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = "";
            this.AccountCreationDate = accountcreationdate;
            this.MiddleName = middlename;
            this.Address = address;
            this.Age = age;
            this.Accounts = new List<Bank_Account>();
        }
        #endregion

        public void AddEmail(string email)
        {
            Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (regex.IsMatch(email))
            {
                this.Email = email;
                System.Console.WriteLine("Email Added Successfully");
            }
            else
                throw new InvalidEmailException(email, "Invalid Email Address");
        }

        public void AddAccount(Bank_Account account)
        {
            this.Accounts.Add(account);
        }

        public Bank_Account RetrieveBankAccount(string accountnumber)
        {
            if (this.Accounts == null)
                throw new AccountNotFoundException(accountnumber, "Account Not Found");
            for (int i = 0; i < this.Accounts.Count; i++)
            {
                if (this.Accounts[i].AccountNumber == accountnumber)
                    return this.Accounts[i];
            }
            throw new AccountNotFoundException(accountnumber, "Account Not Found");
        }

        public void RemoveBankAccount(string accountnumber)
        {
            if (this.Accounts == null)
                throw new AccountNotFoundException(accountnumber, "Account Not Found");
            for (int i = 0; i < this.Accounts.Count; i++)
            {
                if (this.Accounts[i].AccountNumber == accountnumber)
                {
                    this.Accounts.RemoveAt(i);
                    return;
                }
            }
            throw new AccountNotFoundException(accountnumber, "Account Not Found");
        }

        public override string ToString()
        {
            return $"Customer ID: {this.CustomerID}\n" +
                $"Customer Name: {this.FirstName} {this.MiddleName} {this.LastName}\n" +
                $"Email: {this.Email}\n" +
                $"Address: {this.Address}\n" +
                $"Age: {this.Age}\n" +
                $"Account Creation Date: {this.AccountCreationDate}";
        }
    }
    #region Exceptions
    public class InvalidEmailException : Exception
    {
        public string Email { get; private set; }
        public InvalidEmailException(string email, string message) : base(message)
        {
            this.Email = email;
        }
    }

    public class AccountNotFoundException : Exception
    {
        public string AccountNumber { get; private set; }
        public AccountNotFoundException(string accountnumber, string message) : base(message)
        {
            this.AccountNumber = accountnumber;
        }
    }
    #endregion
}