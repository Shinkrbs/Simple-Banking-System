using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking_System.Enums;

namespace Banking_System.Classes
{
    public class Bank_Account
    {
        #region Mandatory Fields
        public string AccountNumber { get; private set; }
        public double Balance { get; private set; }
        #endregion

        #region Optional Field
        public Account_Type AccountType { get; private set; }
        #endregion

        #region Constructors
        // Default Constructor
        public Bank_Account()
        {
            this.AccountNumber = "0000000000";
            this.Balance = 0.0;
            this.AccountType = Account_Type.Savings;
        }
        // Mandatory Fields Constructor

        public Bank_Account(string accountnumber, double balance)
        {
            this.AccountNumber = accountnumber;
            this.Balance = balance;
            this.AccountType = Account_Type.Savings;
        }
        // All Fields Constructor
        public Bank_Account(string accountnumber, double balance, Account_Type accounttype)
        {
            this.AccountNumber = accountnumber;
            this.Balance = balance;
            this.AccountType = accounttype;
        }
        #endregion

        public void Deposit(double amount)
        {
            if (amount < 0)
                throw new NegativeAmountException(amount, "Amount cannot be negative");
            this.Balance += amount;
        }

        public double GetBalance()
        {
            return this.Balance;
        }

        public void Withdraw(double amount)
        {
            if (amount < 0)
                throw new NegativeAmountException(amount, "Amount cannot be negative");
            if (amount > this.Balance)
                throw new InsufficientFundsException(amount, "Amount is greater than available balance");

            this.Balance -= amount;
            System.Console.WriteLine("Amount withdrawn successfully");
        }
    }

    #region Exceptions
    public class InsufficientFundsException : Exception
    {
        public double AttemptedAmount { get; private set; }

        public InsufficientFundsException(double attemptedamount, string message) : base(message)
        {
            this.AttemptedAmount = attemptedamount;
        }
    }
    public class NegativeAmountException : Exception
    {
        public double AttemptedAmount { get; private set; }
        public NegativeAmountException(double attemptedamount, string message) : base(message)
        {
            this.AttemptedAmount = attemptedamount;
        }
    }
    #endregion
}