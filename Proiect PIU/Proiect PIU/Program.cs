using System;
using System.Collections.Generic;

class Program
{
    static List<Product> products = new List<Product>();
    static List<Order> orders = new List<Order>(); 

    static void Main(string[] args)
    {
        bool exit = false;
        do
        {
            Console.WriteLine("Meniu:");
            Console.WriteLine("1. Editare produse stoc");
            Console.WriteLine("2. Cautare produse");
            Console.WriteLine("3. Creare comanda");
            Console.WriteLine("4. Afisare Stoc");
            Console.WriteLine("5. Iesire program");
            Console.Write("Alegeti o optiune: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    EditareProduseStoc();
                    break;
                case "2":
                    CautareProduse();
                    break;
                case "3":
                    CreareComanda();
                    break;
                case "4":
                    AfisareStoc();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Optiune invalida. Va rugam sa alegeti o optiune valida.");
                    break;
            }

        } while (!exit);
    }
    static void AfisareStoc()
    {
        Console.WriteLine("Stocul de produse:");

        foreach (Product product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Nume: {product.Name}, Pret: {product.Price}, Cantitate in stoc: {product.QuantityInStock}");
        }
    }

    static void EditareProduseStoc()
    {
        Console.WriteLine("Editare produse stoc:");
        Console.WriteLine("1. Adaugare produs");
        Console.WriteLine("2. Stergere produs");
        Console.Write("Alegeti o optiune: ");
        string editOption = Console.ReadLine();

        switch (editOption)
        {
            case "1":
                AdaugareProdus();
                break;
            case "2":
                StergereProdus();
                break;
            default:
                Console.WriteLine("Optiune invalida. Va rugam sa alegeti o optiune valida.");
                break;
        }
    }

    static void AdaugareProdus()
    {
        Console.Write("Numele produsului: ");
        string productName = Console.ReadLine();

        Console.Write("Pretul produsului: ");
        decimal productPrice;
        if (!decimal.TryParse(Console.ReadLine(), out productPrice))
        {
            Console.WriteLine("Pretul introdus nu este valid.");
            return;
        }

        Console.Write("Cantitatea de produse disponibile: ");
        int productQuantity;
        if (!int.TryParse(Console.ReadLine(), out productQuantity))
        {
            Console.WriteLine("Cantitatea introdusa nu este valida.");
            return;
        }
        products.Add(new Product()
        {
            Id = products.Count + 1,
            Name = productName,
            Price = productPrice,
            QuantityInStock = productQuantity
        });

        Console.WriteLine("Produsul a fost adaugat cu succes in stoc.");
    }

    static void StergereProdus()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Nu exista produse in stoc.");
            return;
        }

        Console.WriteLine("Produse disponibile pentru stergere:");

        foreach (Product product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Nume: {product.Name}, Pret: {product.Price}, Cantitate in stoc: {product.QuantityInStock}");
        }

        Console.Write("Introduceti ID-ul produsului de sters: ");
        int productId;
        if (!int.TryParse(Console.ReadLine(), out productId))
        {
            Console.WriteLine("ID-ul produsului introdus nu este valid.");
            return;
        }

        Product productToRemove = products.Find(p => p.Id == productId);
        if (productToRemove == null)
        {
            Console.WriteLine("Produsul cu ID-ul introdus nu exista in stoc.");
            return;
        }

        products.Remove(productToRemove);
        Console.WriteLine("Produsul a fost sters cu succes din stoc.");
    }

   
    
        static void CautareProduse()
        {
            Console.WriteLine("Cautare produse:");
            Console.WriteLine("1. Cautare dupa nume");
            Console.WriteLine("2. Cautare dupa pret");
            Console.Write("Alegeti o optiune: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CautareDupaNume();
                    break;
                case "2":
                    CautareDupaPret();
                    break;
                default:
                    Console.WriteLine("Optiune invalida. Va rugam sa alegeti o optiune valida.");
                    break;
            }
        }

        static void CautareDupaNume()
        {
            Console.Write("Introduceti numele produsului: ");
            string searchName = Console.ReadLine();

            List<Product> foundProducts = products.FindAll(p => p.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

            if (foundProducts.Count == 0)
            {
                Console.WriteLine("Produsul nu a fost gasit.");
            }
            else
            {
                Console.WriteLine("Produse gasite:");

                foreach (Product product in foundProducts)
                {
                    Console.WriteLine($"ID: {product.Id}, Nume: {product.Name}, Pret: {product.Price}, Cantitate in stoc: {product.QuantityInStock}");
                }
            }
        }

        static void CautareDupaPret()
        {
            Console.Write("Introduceti pretul maxim: ");
            decimal maxPrice;
            if (!decimal.TryParse(Console.ReadLine(), out maxPrice))
            {
                Console.WriteLine("Pretul introdus nu este valid.");
                return;
            }

            List<Product> foundProducts = products.FindAll(p => p.Price <= maxPrice);

            if (foundProducts.Count == 0)
            {
                Console.WriteLine("Nu s-au gasit produse cu pretul mai mic sau egal cu cel introdus.");
            }
            else
            {
                Console.WriteLine("Produse gasite:");

                foreach (Product product in foundProducts)
                {
                    Console.WriteLine($"ID: {product.Id}, Nume: {product.Name}, Pret: {product.Price}, Cantitate in stoc: {product.QuantityInStock}");
                }
            }
        }
    

    static void CreareComanda()
    {
        Console.WriteLine("Creare comanda:");

        if (products.Count == 0)
        {
            Console.WriteLine("Stoc de produse complet epuizat.");
            return;
        }

        Order order = new Order();

        bool addMoreProducts = true;
        do
        {
            Console.Write("Introduceti ID-ul produsului pentru adaugare in comanda: ");
            int productId;
            if (!int.TryParse(Console.ReadLine(), out productId))
            {
                Console.WriteLine("ID-ul produsului introdus nu este valid.");
                continue;
            }

            Product productToAdd = products.Find(p => p.Id == productId);
            if (productToAdd == null)
            {
                Console.WriteLine("Produsul cu ID-ul introdus nu exista in stoc.");
                continue;
            }

            Console.Write($"Introduceti cantitatea pentru produsul '{productToAdd.Name}': ");
            int quantity;
            if (!int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("Cantitatea introdusa nu este valida.");
                continue;
            }

            if (quantity <= 0 || quantity > productToAdd.QuantityInStock)
            {
                Console.WriteLine("Cantitatea introdusa este invalida sau depaseste stocul disponibil.");
                continue;
            }
            order.Products.Add(new OrderItem { Product = productToAdd, Quantity = quantity });

            Console.WriteLine($"Produsul '{productToAdd.Name}' a fost adaugat in comanda.");

            Console.WriteLine("Doriti sa adaugati un alt produs? (da/nu)");
            string response = Console.ReadLine();
            addMoreProducts = response.ToLower() == "da";

        } while (addMoreProducts);

        
        orders.Add(order);

        Console.WriteLine("Comanda a fost creata cu succes!");
        CreareFisierComanda(order);
    }
    static void CreareFisierComanda(Order order)
    {
        string fileName = $"Comanda_{order.Id}.txt";

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine($"ID Comanda: {order.Id}");
            writer.WriteLine($"Data: {DateTime.Now}");
            writer.WriteLine("Produse:");

            foreach (OrderItem orderItem in order.Products)
            {
                writer.WriteLine($"  - Nume: {orderItem.Product.Name}, Pret: {orderItem.Product.Price}, Cantitate: {orderItem.Quantity}");
            }

            writer.WriteLine($"Total: {order.CalculateTotal()}");
        }

        Console.WriteLine($"Fisierul cu datele comenzii a fost creat: {fileName}");
    }
}


class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
}

class OrderItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}

class Order
{
    private static int nextId = 1;

    public int Id { get; private set; }
    public List<OrderItem> Products { get; private set; }

    public Order()
    {
        Id = nextId++;
        Products = new List<OrderItem>();
    }
    public decimal CalculateTotal()
    {
        decimal total = 0;

        foreach (OrderItem orderItem in Products)
        {
            total += orderItem.Product.Price * orderItem.Quantity;
        }

        return total;
    }
}
