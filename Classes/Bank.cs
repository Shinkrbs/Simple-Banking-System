using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Banking_System.Classes;

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

        public void Transaction(Customer customer, Bank_Account account, double amount)
        {
            // Validate Customer and Account
            bool flag = false;
            for(int i = 0; i < Customers.Count; i++)
            {
                if (Customers[i].CustomerID == customer.CustomerID)
                {
                    flag = true;
                    for(int j = 0; j < customer.Accounts.Count;j++)
                    {
                        if (customer.Accounts[j].AccountNumber == account.AccountNumber)
                        {
                            flag = true;
                            break;
                        }
                        throw new AccountNotFoundException(account.AccountNumber, "Error, Account not found");
                    }
                }
                throw new CustomerNotFoundException(customer, "Error, Customer not found");
            }

            // Perform Transaction
            if(flag)
            {
                int choice = 0;
                while(choice != 4)
                {
                    System.Console.WriteLine($"1. Deposit\n" +
                        $"2. Get Balance\n" +
                        $"3. Withdraw\n" +
                        $"4. Exit\n)" +
                        $"Input Choice Here (1 - 3): ");

                    // Get input and validate
                    bool isValid = int.TryParse(Console.ReadLine(), out choice);

                    if (!isValid || choice < 1 || choice > 4)
                    {
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Performing Deposit...");
                            account.Deposit(amount);

                            break;
                        case 2:
                            Console.WriteLine("Checking Balance...");
                            account.GetBalance();
                            break;
                        case 3:
                            Console.WriteLine("Withdrawing Funds...");
                            account.Withdraw(amount);
                            break;
                        case 4:
                            Console.WriteLine("Exiting...");
                            break;
                    }
                }

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
    #endregion
}