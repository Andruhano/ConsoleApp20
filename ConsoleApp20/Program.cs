using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

abstract class Device
{
    public double Price { get; set; }
    public string Vendor { get; set; }
    public string Category { get; set; }
    public int YearOfRelease { get; set; }
    public int Warranty { get; set; } 
    public string Model { get; set; }

    public Device(string vendor, double price, string category, int yearOfRelease, int warranty, string model)
    {
        Vendor = vendor;
        Price = price;
        Category = category;
        YearOfRelease = yearOfRelease;
        Warranty = warranty;
        Model = model;
    }

    public override string ToString()
    {
        return $"Vendor: {Vendor}, Price: ${Price}, Category: {Category}, Year: {YearOfRelease}, Warranty: {Warranty} months, Model: {Model}";
    }
}

class Laptop : Device
{
    public Laptop(string vendor, double price, int yearOfRelease, int warranty, string model)
        : base(vendor, price, "Laptop", yearOfRelease, warranty, model) { }
}

class Tablet : Device
{
    public Tablet(string vendor, double price, int yearOfRelease, int warranty, string model)
        : base(vendor, price, "Tablet", yearOfRelease, warranty, model) { }
}

class MobilePhone : Device
{
    public MobilePhone(string vendor, double price, int yearOfRelease, int warranty, string model)
        : base(vendor, price, "MobilePhone", yearOfRelease, warranty, model) { }
}

class Charger : Device
{
    public Charger(string vendor, double price, int yearOfRelease, int warranty, string model)
        : base(vendor, price, "Charger", yearOfRelease, warranty, model) { }
}

class Case : Device
{
    public Case(string vendor, double price, int yearOfRelease, int warranty, string model)
        : base(vendor, price, "Case", yearOfRelease, warranty, model) { }
}

class Store
{
    private List<Device> devices = new List<Device>();

    public void AddDevice(Device device)
    {
        devices.Add(device);
    }

    public List<Device> SearchByPriceRange(double minPrice, double maxPrice)
    {
        return devices.Where(d => d.Price >= minPrice && d.Price <= maxPrice).ToList();
    }

    public List<Device> SearchByModelName(string modelNamePattern)
    {
        Regex regex = new Regex(modelNamePattern, RegexOptions.IgnoreCase);
        return devices.Where(d => regex.IsMatch(d.Model)).ToList();
    }

    public List<Device> SearchByYear(int year)
    {
        return devices.Where(d => d.YearOfRelease == year).ToList();
    }

    public List<Device> SearchByDeviceType(Type deviceType)
    {
        return devices.Where(d => d.GetType() == deviceType).ToList();
    }
}

class Program
{
    static void Main()
    {
        var store = new Store();

        store.AddDevice(new Laptop("Samsung", 3726.1, 2021, 24, "GalaxyBook"));
        store.AddDevice(new Laptop("Asus", 8073.2, 2020, 36, "ZenBook"));
        store.AddDevice(new Tablet("Apple", 2999.9, 2022, 12, "iPad Pro"));
        store.AddDevice(new MobilePhone("Apple", 1200.0, 2023, 24, "iPhone 15"));
        store.AddDevice(new Charger("Anker", 50.5, 2021, 12, "PowerPort"));
        store.AddDevice(new Case("Spigen", 29.9, 2023, 6, "RuggedArmor"));

        Console.WriteLine("Devices between $1000 and $4000:");
        var devicesByPrice = store.SearchByPriceRange(1000, 4000);
        foreach (var device in devicesByPrice)
        {
            Console.WriteLine(device);
        }

        Console.WriteLine("\nDevices with 'Book' in the model name:");
        var devicesByModel = store.SearchByModelName("Book");
        foreach (var device in devicesByModel)
        {
            Console.WriteLine(device);
        }

        Console.WriteLine("\nDevices released in 2021:");
        var devicesByYear = store.SearchByYear(2021);
        foreach (var device in devicesByYear)
        {
            Console.WriteLine(device);
        }

        Console.WriteLine("\nAll laptops:");
        var laptops = store.SearchByDeviceType(typeof(Laptop));
        foreach (var laptop in laptops)
        {
            Console.WriteLine(laptop);
        }
    }
}