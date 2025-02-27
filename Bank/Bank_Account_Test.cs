using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple_Banking_System.Classes;

namespace Test
{
    [TestClass]
    public class Bank_Account_Test
    {
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
            var ex = Assert.ThrowsException<InsufficientFundsException>(() => b.Withdraw(0.0));
            Assert.AreEqual("Amount is greater than available balance", ex.Message);
        }
    }
}
