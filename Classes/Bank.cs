using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_System.Classes
{
    public class Bank
    {
        public string BankName { get; private set; }
        public string BankID { get; private set; }
        public string BankLocation { get; private set; }
        public List<Customer> Customers { get; private set; }

        public Bank(string bankname, string bankid,string banklocation)
        {
            this.BankName = bankname;
            this.BankID = bankid;
            this.BankLocation = banklocation;
            this.Customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            this.Customers.Add(customer);
        }

        public void RemoveCustomer(Customer customer)
        {
            if (this.Customers.Count == 0)
                throw new CustomerListEmptyException("Customer List is Empty");
            for(int i = 0; i < this.Customers.Count; i++)
            {
                if (this.Customers[i].CustomerID == customer.CustomerID)
                {
                    this.Customers.RemoveAt(i);
                    return;
                }
            }
            throw new CustomerNotFoundException(customer, "Error, Customer not found");
        }

        public void Transaction(Customer customer, Bank_Account account, double amount, int choice)
        {
            // Validate Customer
            if (!Customers.Exists(c => c.CustomerID == customer.CustomerID))
                throw new CustomerNotFoundException(customer, "Error, Customer not found");

            // Validate Account
            if (!customer.Accounts.Exists(a => a.AccountNumber == account.AccountNumber))
                throw new AccountNotFoundException(account.AccountNumber, "Error, Account not found");

            // Perform Transaction
            switch (choice)
            {
                case 1:
                    account.Deposit(amount);
                    break;
                case 2:
                    account.GetBalance();
                    break;
                case 3:
                    account.Withdraw(amount);
                    break;
                case 4:
                    return; // Exit
                default:
                    throw new ArgumentException("Invalid transaction choice");
            }
        }
    }

    #region Exceptions
    public class CustomerNotFoundException : Exception
    {
        public Customer Customer { get; private set; }
        public CustomerNotFoundException(Customer customer, string message) : base(message)
        {
            this.Customer = customer;
        }
    }

    public class CustomerListEmptyException : Exception
    {
        public CustomerListEmptyException(string message) : base(message) {  }
    }
    #endregion
}