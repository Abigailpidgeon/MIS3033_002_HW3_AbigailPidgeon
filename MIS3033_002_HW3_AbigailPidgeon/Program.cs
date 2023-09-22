// MIS3033 HW 3
// September 22 2023
// Abigial Pidgeon 113515288
using a;
using Microsoft.EntityFrameworkCore.Query.Internal;

Console.WriteLine("HW3");

string Menu;
Menu = @"
Option Menu
1. Add a new receipt
2. Show all receipts
3. Show receipt based on a receipt ID
4. Edit a receipt based on a receipt ID
5. Delete a receipt based on a receipt ID
6. Show the receipt with the highest total
7. Show the average total of all receipts
Press other keys to exit...";

ReceiptDB db = new ReceiptDB();

while (true)
{
    Console.WriteLine(Menu);
    Console.Write("Input your option: ");

    string userinput;
    userinput = Console.ReadLine();

    if (userinput == "1")
    {
        Console.WriteLine("Add a new receipt");
        Console.Write("Receipt ID: ");
        string input;
        input = Console.ReadLine();
        string receiptid = input;

        Console.Write("N Cogs: ");
        input = Console.ReadLine();
        int ncogs = Convert.ToInt32(input);

        Console.Write("N Gears: ");
        input = Console.ReadLine();
        int ngears = Convert.ToInt32(input);

        Receipt receipt = new Receipt() { ReceiptID = receiptid, CogQuantity = ncogs, GearQuantity = ngears };
        receipt.CalculateTotal();

        db.ReceiptTbl.Add(receipt);
        db.SaveChanges();
    }
    else if (userinput == "2")
    {
        Console.WriteLine("Show all receipts");
        var r = db.ReceiptTbl;
        foreach (Receipt receipt in r)
        {
            Console.WriteLine(receipt);
        }
    }
    else if (userinput == "3")
    {
        Console.WriteLine("Show receipt based on a receipt ID");
        Console.Write("Receipt ID: ");
        string receiptid;
        receiptid = Console.ReadLine();

        var r = db.ReceiptTbl.Where(x=>x.ReceiptID == receiptid).FirstOrDefault();
        if (r == null)
        {
            Console.WriteLine($"Receipt {receiptid} does not exist in this database!");
        }
        else
        {
            Console.WriteLine(r);
        }
    }
    else if (userinput == "4")
    {
        Console.WriteLine("Edit a receipt based on a receipt ID");
        Console.Write("Input a receipt ID: ");
        string receiptid = Console.ReadLine();

        Receipt r = db.ReceiptTbl.Where(x=>x.ReceiptID==receiptid).FirstOrDefault();
        if(r == null)
        {
            Console.WriteLine($"Receipt {receiptid} not found.");
        }
        else
        {
            Console.Write("N Cogs: ");
            string ncog = Console.ReadLine();
            int newcogs = Convert.ToInt32(ncog);

            Console.Write("N Gears: ");
            string ngear = Console.ReadLine();
            int newgears = Convert.ToInt32(ngear);

            r.CogQuantity = newcogs;
            r.GearQuantity = newgears;

            r.CalculateTotal();
            db.SaveChanges();
            Console.WriteLine("The new receipt is: ");
            Console.WriteLine(r);
        }
    }
    else if (userinput == "5")
    {
        Console.WriteLine("Delete a receipt based on a receipt ID");
        Console.Write("Input a receipt ID: ");
        string receiptid = Console.ReadLine();

        Receipt r1 = db.ReceiptTbl.Where(x=>x.ReceiptID == receiptid).FirstOrDefault();
        if(r1 == null)
        {
            Console.WriteLine($"Receipt {receiptid} not found.");
        }
        else
        {
            db.ReceiptTbl.Remove(r1);
            db.SaveChanges();

            Console.WriteLine($"The receipt {receiptid} was removed successfully!");
            Console.WriteLine(r1);
        }
    }
    else if (userinput == "6")
    {
        Console.WriteLine("Show the receipt with the highest total");
        Receipt r2 = db.ReceiptTbl.ToList().MaxBy(x => x.Total);
        Console.WriteLine(r2);
    }
    else if (userinput == "7")
    {
        Console.WriteLine("Show the average total of all receipts");
        double r3 = db.ReceiptTbl.Average(x => x.Total);
        Console.WriteLine($"{r3:C2}");

    }
    else
    {
        Console.WriteLine("Thank you. Goodbye!");
        break;
    }
}