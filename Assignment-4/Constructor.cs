using System;

public class BankAccount
{
    public string AccountNumber { get; }
    public string AccountHolderName { get; set; }
    public double Balance { get; set; } 

    public BankAccount() //default value to obj
    {
        AccountNumber = "9999999999";
        AccountHolderName = "Sanjana";
        Balance = 32000;
    }

    public BankAccount(string accountNumber, string accountHolderName, double balance)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            throw new ArgumentException("Account number cannot be null or empty", nameof(accountNumber));
        }

        if (accountNumber.Length != 10)
        {
            throw new ArgumentException("Account number must be exactly 10 digits", nameof(accountNumber));
        }

        if (string.IsNullOrWhiteSpace(accountHolderName))
        {
            throw new ArgumentException("Account holder name cannot be null or empty", nameof(accountHolderName));
        }

        if (accountHolderName.Any(char.IsDigit))
        {
            throw new ArgumentException("Account holder name cannot contain numbers", nameof(accountHolderName));
        }

        if (balance < 0)
        {
            throw new ArgumentException("Balance cannot be negative", nameof(balance));
        }

        AccountNumber = accountNumber;
        AccountHolderName = accountHolderName;
        Balance = balance;
    }

    public BankAccount(string accountNumber) : this(accountNumber, "Biswanth Ch", 0)
    {
    }

    public void DisplayAccountInfo()
    {
        Console.WriteLine($"Account Number: {AccountNumber}");
        Console.WriteLine($"Account Holder: {AccountHolderName}");
        Console.WriteLine($"Balance: Rs {Balance:F2}"); 
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        BankAccount sanjanaAccount = new BankAccount();
        BankAccount biswanthAccount = new BankAccount("1234567890");
        BankAccount manojAccount = new BankAccount("1111111111", "Manoj", 15000.75);

        try
        {
            BankAccount Test = new BankAccount("1234567890", "User123", -234); 
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error creating account: {ex.Message}\n"); //stores actual exception obj
        }

        Console.WriteLine("Sanjana's Account:");
        sanjanaAccount.DisplayAccountInfo();

        Console.WriteLine("Biswanth's Account:");
        biswanthAccount.DisplayAccountInfo();

        Console.WriteLine("Manoj's Account:");
        manojAccount.DisplayAccountInfo();
    }
}