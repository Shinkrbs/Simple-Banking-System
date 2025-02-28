﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Banking_System.Classes;
using Banking_System.Enums;

namespace Test
{
	[TestClass]
	public class Customer_Test
	{
        #region Test Setup

        private Customer c;
        private Bank_Account dummy;
        private DateTime Fixed_Account_CreationTime;
        private string cID = "123456";
        private string fName = "John";
        private string lName = "Doe";
        private string Fixed_Email = "example@gmail.com";
        private string mName = "Cruz";
        private string address = "1234 Artikulo Burgos St.";
        private int age = 21;
        
        [TestInitialize]
        public void Setup()
        {
            Fixed_Account_CreationTime = new DateTime(2025, 2, 27, 10, 0, 0); // Feb. 27, 2025 10:00
            c = new Customer(cID, fName, lName, Fixed_Account_CreationTime, mName, address, age);
            dummy = new Bank_Account();
            c.AddAccount(dummy);
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