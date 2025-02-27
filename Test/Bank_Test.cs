using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Banking_System.Classes;

namespace Test
{
	[TestClass]
	public class Bank_Test
	{
        #region Setup

        private Bank b;
        private string bName = "SkyBank";
        private string bID = "00000";
        private string bLocation = "Biringan, Samar";
        private string dummyId = "1234567";
        private string dummyFName = "Juan";
        private string dummyLName = "Ponce";
        private DateTime dummyDate = new DateTime(2025, 2, 27, 10, 0, 0);


        [TestInitialize]
        public void Setup()
        {
            b = new Bank(bName, bID, bLocation);

            // Adding a dummy customer
            DateTime dt = new DateTime(2025, 2, 20, 9, 0, 0);
            Customer dummy = new Customer("67894", "John", "Doe", dt); // Initial Dummy Customer
            b.AddCustomer(dummy);
        }
        #endregion

        #region Constructor Test

        [TestMethod]
        public void Constructor_Default()
        {
            Assert.AreEqual("SkyBank", b.BankName);
            Assert.AreEqual("00000", b.BankID);
            Assert.AreEqual("Biringan, Samar", b.BankLocation);
            Assert.AreEqual(1, b.Customers.Count); // Dummy customer [1]
        }
        #endregion

        [TestMethod]
        public void AddCustomer_AddsCustomer()
        {
            Customer dummy = new Customer(dummyId, dummyFName, dummyLName, dummyDate);
            b.AddCustomer(dummy);
            Assert.AreEqual(2, b.Customers.Count);
            Assert.AreEqual(dummy, b.Customers[1]);
        }

        [TestMethod]
        public void RemoveCustomer_RemovesCustomer()
        {
            Customer dummy = new Customer(dummyId, dummyFName, dummyLName, dummyDate);
            b.AddCustomer(dummy);
            b.RemoveCustomer(dummy);
            Assert.AreEqual(1, b.Customers.Count);
            Assert.AreNotEqual(dummy, b.Customers[0]);
        }

        [TestMethod]
        public void RemoveCustomer_EmptyList()
        {
            b.Customers.Clear();
            Customer dummy = new Customer(dummyId, dummyFName, dummyLName, dummyDate);
            var ex = Assert.ThrowsException<CustomerListEmptyException>(() => b.RemoveCustomer(dummy));
            Assert.AreEqual("Customer List is Empty", ex.Message);
        }

        [TestMethod]
        public void RemoveCustomer_CustomerNotFound()
        {
            Customer dummy = new Customer("2376854", dummyFName, dummyLName, dummyDate);
            var ex = Assert.ThrowsException<CustomerNotFoundException>(() => b.RemoveCustomer(dummy));
            Assert.AreEqual("Error, Customer not found", ex.Message);
        }

        [TestMethod]
        public void Transaction_CustomerNotFound()
        {
            Customer dummy = new Customer("2376854", dummyFName, dummyLName, dummyDate);
            var ex = Assert.ThrowsException<CustomerNotFoundException>(() => b.Transaction(dummy, null, 100.0, 1));
            Assert.AreEqual("Error, Customer not found", ex.Message);
        }

        [TestMethod]
        public void Transaction_AccountNotFound()
        {
            Customer dummy = new Customer(dummyId, dummyFName, dummyLName, dummyDate);
            b.Customers.Add(dummy);
            Bank_Account dummyAccount = new Bank_Account("12345678", 100.0);
            dummy.AddAccount(dummyAccount);
            Bank_Account AccounttoSearch = new Bank_Account("12875678", 100.0);
            var ex = Assert.ThrowsException<AccountNotFoundException>(() => b.Transaction(dummy, AccounttoSearch, 100.0, 1));
            Assert.AreEqual("Error, Account not found", ex.Message);
        }
    }
}