﻿using System;
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
            c = new Customer("123456", "John", "Doe", Fixed_Account_CreationTime);
            dummy = new Bank_Account();
            c.AddAccount(dummy);

            cID = c.CustomerID;
            fName = c.FirstName;
            lName = c.LastName;
            Fixed_Email = "example@gmail.com";
            mName = "Cruz";
            address = "1234 Artikulo Burgos St.";
            age = 21;
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
            c = new Customer(cID, fName, lName, Fixed_Account_CreationTime, mName, address, age);
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
            Assert.AreEqual(1, c.Accounts.Count);
            Assert.AreEqual(dummy, c.Accounts[0]);
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
            var ex = Assert.ThrowsException<EmptyAccountList>(() => c.RetrieveBankAccount(dummy.AccountNumber));
            Assert.AreEqual("No Accounts Found", ex.Message);
        }
    }
}