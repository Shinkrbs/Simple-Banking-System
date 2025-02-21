using System;
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
        }

        [TestMethod]
        public void Deposit_IncreaseAmount()
        {
            Bank_Account b = new Bank_Account();
            b.Deposit(100.0);
            Assert.AreEqual(100.0, b.GetBalance());
        }
    }
}
