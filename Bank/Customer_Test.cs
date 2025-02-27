using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple_Banking_System.Classes;
using Simple_Banking_System.Enums;

namespace Bank
{
	[TestClass]
	public class Customer_Test
	{
        #region Test Setup
        private Customer c;
        private Bank_Account dummy;
        private DateTime Fixed_Account_CreationTime;
        private string cID;
        private string fName;
        private string lName;
        private string Fixed_Email;
        private string mName;
        private string address;
        private int age;
        
        [TestInitialize]
        public void Setup()
        {
            Fixed_Account_CreationTime = new DateTime(2025, 2, 27, 10, 0, 0); // Feb. 27, 2025 10:00
            c = new Customer("123456", "John", "Doe", Fixed_Account_CreationTime, "Cruz", "1234 Artikulo Burgos St.", 21);
            dummy = new Bank_Account();
            c.AddAccount(dummy);

            cID = c.CustomerID;
            fName = c.FirstName;
            lName = c.LastName;
            Fixed_Email = "example@gmail.com";
            mName = c.MiddleName;
            address = c.Address;
            age = c.Age;
        }
        #endregion

        #region Constructor Tests

        [TestMethod]
        public void Constructor_Mandatory()
        {
            Assert.AreEqual(cID, c.CustomerID);
            Assert.AreEqual(fName, c.FirstName);
            Assert.AreEqual(lName, c.LastName);
            Assert.AreEqual("", c.Email);
            Assert.AreEqual(Fixed_Account_CreationTime, c.AccountCreationDate);
        }

        [TestMethod]
        public void Constructor_Optional()
        {
            Assert.AreEqual(cID, c.CustomerID);
            Assert.AreEqual(fName, c.FirstName);
            Assert.AreEqual(lName, c.LastName);
            Assert.AreEqual("", c.Email);
            Assert.AreEqual(Fixed_Account_CreationTime, c.AccountCreationDate);
            Assert.AreEqual(mName, c.MiddleName);
            Assert.AreEqual(address, c.Address);
            Assert.AreEqual(age, c.Age);
        }
        #endregion

        [TestMethod]
        public void AddEmail_ValidEmail()
        {
            c.AddEmail(Fixed_Email);
            Assert.AreEqual(Fixed_Email, c.Email);
        }

        [TestMethod]
        public void AddEmail_InvalidEmail()
        {
            var ex = Assert.ThrowsException<InvalidEmailException>(() => c.AddEmail("randomgibbersih.com"));
            Assert.AreEqual("", c.Email);
        }

        [TestMethod]
        public void AddAccount_AddsAccount()
        {
            Bank_Account newDummy = new Bank_Account("12345678", 100.0);
            c.AddAccount(newDummy);
            Assert.AreEqual(2, c.Accounts.Count);
            Assert.AreEqual(newDummy, c.Accounts[1]);
        }

        [TestMethod]
        public void RetrieveBankAccount_ReturnsAccount()
        {
            var ex = c.RetrieveBankAccount(dummy.AccountNumber);
            Assert.AreEqual(dummy, ex);
        }

        [TestMethod]
        public void RetrieveBankAccount_EmptyList()
        {
            c.Accounts.Clear();
            var ex = Assert.ThrowsException<EmptyAccountListException>(() => c.RetrieveBankAccount(dummy.AccountNumber));
            Assert.AreEqual("No Accounts Found", ex.Message);
        }

        [TestMethod]
        public void RetrieveBankAccount_AccountNotFound()
        {
            Bank_Account newDummy = new Bank_Account("12345678", 100.0);
            var ex = Assert.ThrowsException<AccountNotFoundException>(() => c.RetrieveBankAccount(newDummy.AccountNumber));
            Assert.AreEqual("Account Not Found", ex.Message);
        }

        [TestMethod]
        public void RemoveBankAccount_RemovesAccount()
        {
            Bank_Account newDummy = new Bank_Account("12345678", 100.0);
            c.AddAccount(newDummy);
            c.RemoveBankAccount(dummy.AccountNumber);
            Assert.AreEqual(1, c.Accounts.Count);
            Assert.AreEqual(newDummy, c.Accounts[0]);
        }

        [TestMethod]
        public void RemoveBankAccount_EmptyList()
        {
            c.Accounts.Clear();
            var ex = Assert.ThrowsException<EmptyAccountListException>(() => c.RemoveBankAccount(dummy.AccountNumber));
            Assert.AreEqual("No Accounts Found", ex.Message);
        }

        [TestMethod]
        public void RemoveBankAccount_AccountNotFound()
        {
            Bank_Account newDummy = new Bank_Account("12345678", 100.0);
            var ex = Assert.ThrowsException<AccountNotFoundException>(() => c.RetrieveBankAccount(newDummy.AccountNumber));
            Assert.AreEqual("Account Not Found", ex.Message);
        }

        [TestMethod]
        public void Check_toString()
        {
            string ex = $"Customer ID: {cID}" +
                $"\nCustomer Name: {c.FirstName} {c.MiddleName} {c.LastName}" +
                $"\nEmail: {c.Email}" +
                $"\nAddress: {c.Address}" +
                $"\nAge: {c.Age}" +
                $"\nAccount Creation Date: {c.AccountCreationDate}";
            Assert.AreEqual(ex, c.ToString());
        }
    }
}