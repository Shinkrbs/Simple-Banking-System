using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple_Banking_System.Classes;
using Simple_Banking_System.Enums;

namespace Test
{
    [TestClass]
    public class Bank_Account_Test
    {
        #region Constructor Tests
        [TestMethod]
        public void Constructor_Default()
        {
            Bank_Account b = new Bank_Account();
            Assert.AreEqual("0000000000", b.AccountNumber);
            Assert.AreEqual(0.0, b.Balance);
            Assert.AreEqual(Account_Type.Savings, b.AccountType);
        }

        [TestMethod]
        public void Constructor_Mandatory()
        {
            Bank_Account b = new Bank_Account("123459987", 123.00);
            Assert.AreEqual("123459987", b.AccountNumber);
            Assert.AreEqual(123.00, b.Balance);
        }
        [TestMethod]
        public void Constructor_Mandatory_AllFields()
        {
            // Assuming Account Type is Business Type
            Bank_Account b = new Bank_Account("123459987", 123.00, Account_Type.Business);
            Assert.AreEqual("123459987", b.AccountNumber);
            Assert.AreEqual(123.00, b.Balance);
            Assert.AreEqual(Account_Type.Business, b.AccountType);
        }
        #endregion

        [TestMethod]
        public void Deposit_Negative_throwException()
        {
            Bank_Account b = new Bank_Account();
            var ex = Assert.ThrowsException<NegativeAmountException>(() => b.Deposit(-100.0));
            Assert.AreEqual("Amount cannot be negative", ex.Message);
        }

        [TestMethod]
        public void Deposit_IncreaseAmount()
        {
            Bank_Account b = new Bank_Account();
            b.Deposit(100.0);
            Assert.AreEqual(100.0, b.GetBalance());
        }

        [TestMethod]
        public void GetBalance_ReturnsBalance()
        {
            Bank_Account b = new Bank_Account();
            b.Deposit(100.0);
            Assert.AreEqual(100.0, b.GetBalance());
        }

        [TestMethod]
        public void Withdraw_DecreaseAmount()
        {
            Bank_Account b = new Bank_Account();
            b.Deposit(100.0);
            b.Withdraw(50.0);
            Assert.AreEqual(50.0, b.GetBalance());
        }

        [TestMethod]
        public void Withdraw_NegativeAmount()
        {
            Bank_Account b = new Bank_Account();
            var ex = Assert.ThrowsException<NegativeAmountException>(() => b.Withdraw(-100.0));
            Assert.AreEqual("Amount cannot be negative", ex.Message);
        }

        [TestMethod]
        public void Withdraw_InsuficcientFunds()
        {
            Bank_Account b = new Bank_Account();
            var ex = Assert.ThrowsException<InsufficientFundsException>(() => b.Withdraw(10.0));
            Assert.AreEqual("Amount is greater than available balance", ex.Message);
        }
    }
}
