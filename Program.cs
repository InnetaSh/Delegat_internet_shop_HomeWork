

OrderProcessor processor = new OrderProcessor();

Warehouse wh = new Warehouse();
processor.OnOrderPlaced += wh.CheckInventory;

NotificationService notification = new NotificationService();
processor.OnOrderProcessed += notification.NotifyCustomer;
processor.OnOrderFailed += notification.NotifyCustomer;

Order order1 = new Order(1, "Iphone", 3);
processor.PlaceOrder(order1);
processor.ProcessOrder(order1);

Order order2 = new Order(2, "Samsung", 0);
processor.PlaceOrder(order2);
processor.ProcessOrder(order2);

public class Order
{
    public int OrderId;
    public string Product;
    public int Quantity;

    public Order(int Id, string product, int quantity)
    {
        OrderId = Id;
        Product = product;
        Quantity = quantity;
    }
}

delegate void OrderHandler(Order order);

class OrderProcessor
{
    public event OrderHandler OnOrderPlaced;
    public event OrderHandler OnOrderProcessed;
    public event OrderHandler OnOrderFailed;

    public void PlaceOrder(Order order)
    {
        Console.WriteLine($"Заказ {order.Product} создан");
        OnOrderPlaced?.Invoke(order);
    }
    public void ProcessOrder(Order order)
    {
        if (order.Quantity > 0)
            OnOrderProcessed?.Invoke(order);
        else
            OnOrderFailed?.Invoke(order);
    }
}

public class Warehouse
{
    public void CheckInventory(Order order)
    {
        if(order.Quantity > 0)
            Console.WriteLine($"Товар {order.Product} в наличии") ;
        else
            Console.WriteLine($"Товара {order.Product} нет в наличии");
    }
}

public class NotificationService
{
    public void NotifyCustomer(Order order)
    {
        if (order.Quantity > 0)
            Console.WriteLine($"Заказ {order.Product} успешно обработан");
        else
            Console.WriteLine($"Заказ {order.Product} отменён");
    }
}

