using System;
using System.Collections.Generic;
using System.IO;

public class MenuItem
{
    public string Description { get; set; }
    public decimal Price { get; set; }

    public MenuItem(string description, decimal price)
    {
        Description = description;
        Price = price;
    }
}

public static class Menu
{
    public static int ShowMenu(string title, List<MenuItem> menuItems)
    {
        Console.Clear();
        Console.WriteLine(title);

        int selectedItemIndex = 0;

        while (true)
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{(i == selectedItemIndex ? "-> " : "   ")}{menuItems[i].Description}");
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow)
            {
                selectedItemIndex--;
                if (selectedItemIndex < 0)
                    selectedItemIndex = menuItems.Count - 1;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                selectedItemIndex++;
                if (selectedItemIndex == menuItems.Count)
                    selectedItemIndex = 0;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                return selectedItemIndex;
            }
        }
    }
}


public class Order
{
    private List<MenuItem> cakes;
    private List<MenuItem> sizes;
    private List<MenuItem> flavors;
    private List<MenuItem> quantities;
    private List<MenuItem> glazings;
    private List<MenuItem> decorations;

    private MenuItem selectedCake;
    private MenuItem selectedSize;
    private MenuItem selectedFlavor;
    private MenuItem selectedQuantity;
    private MenuItem selectedGlazing;
    private MenuItem selectedDecoration;

    private decimal totalPrice;

    public Order()
    {
        // cakes = LoadMenuItemsFromFile("cakes.txt");
        // sizes = LoadMenuItemsFromFile("sizes.txt");
        // flavors = LoadMenuItemsFromFile("flavors.txt");
        // и так далее...

        cakes = new List<MenuItem>()
        {
            new MenuItem("Тортик А", 100),
            new MenuItem("Тортик Б", 150),
            new MenuItem("Тортик с", 200)
        };

        sizes = new List<MenuItem>()
        {
            new MenuItem("Маленький", 0),
            new MenuItem("Средний", 50),
            new MenuItem("Большой", 100)
        };

        flavors = new List<MenuItem>()
        {
            new MenuItem("Шоколадный", 0),
            new MenuItem("Ванильный", 0),
            new MenuItem("Фруктовый", 0)
        };

        quantities = new List<MenuItem>()
        {
            new MenuItem("1 шт.", 0),
            new MenuItem("2 шт.", 50),
            new MenuItem("3 шт.", 100)
        };

        glazings = new List<MenuItem>()
        {
            new MenuItem("Шоколадная", 0),
            new MenuItem("Карамельная", 0),
            new MenuItem("Фруктовая", 0)
        };

        decorations = new List<MenuItem>()
        {
            new MenuItem("Цветы", 10),
            new MenuItem("Фигурки", 20),
            new MenuItem("Шоколадные украшения", 15)
        };
    }

    public void PlaceOrder()
    {
        MenuItem selectedCake = cakes[Menu.ShowMenu("Выберите торт:", cakes)];
        MenuItem selectedSize = sizes[Menu.ShowMenu("Выберите размер:", sizes)];
        MenuItem selectedFlavor = flavors[Menu.ShowMenu("Выберите вкус:", flavors)];
        MenuItem selectedQuantity = quantities[Menu.ShowMenu("Выберите количество:", quantities)];
        MenuItem selectedGlazing = glazings[Menu.ShowMenu("Выберите глазурь:", glazings)];

        MenuItem selectedDecoration = decorations[Menu.ShowMenu("Выберите декор:", decorations)];

        decimal totalPrice = selectedCake.Price + selectedSize.Price + selectedFlavor.Price +
            selectedQuantity.Price + selectedGlazing.Price + selectedDecoration.Price;

        Console.Clear();
        Console.WriteLine("Ваш заказ:");
        Console.WriteLine($"Торт: {selectedCake.Description}");
        Console.WriteLine($"Размер: {selectedSize.Description}");
        Console.WriteLine($"Вкус: {selectedFlavor.Description}");
        Console.WriteLine($"Количество: {selectedQuantity.Description}");
        Console.WriteLine($"Глазурь: {selectedGlazing.Description}");
        Console.WriteLine($"Декор: {selectedDecoration.Description}");
        Console.WriteLine($"Итого: ${totalPrice}");
    }

    private void SaveOrderToFile()
    {
        string order = $"{DateTime.Now}: Торт: {selectedCake.Description}, Размер: {selectedSize.Description}, Вкус: {selectedFlavor.Description}, " +
            $"Количество: {selectedQuantity.Description}, Глазурь: {selectedGlazing.Description}, Декор: {selectedDecoration.Description}, " +
            $"Сумма заказа: {totalPrice}";

        using (StreamWriter sw = File.AppendText(@"C:\Новая папка\заказы.txt"))
        {
            sw.WriteLine(order);
        }
    }



    public class Program
    {
        static void Main(string[] args)
        {
            bool continueOrdering = true;

            while (continueOrdering)
            {
                Order order = new Order();
                order.PlaceOrder();

                Console.WriteLine("Хотите сделать еще один заказ? (y/n)");
                string userInput = Console.ReadLine();
                continueOrdering = (userInput == "y" || userInput == "Y");
            }
        }
    }
}
