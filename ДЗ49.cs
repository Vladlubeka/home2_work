using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp57
{
    public class MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class Menu
    {
        private List<MenuItem> menuItems = new List<MenuItem>();

        public void AddMenuItem(MenuItem menuItem)
        {
            menuItems.Add(menuItem);
        }

        public void RemoveMenuItem(MenuItem menuItem)
        {
            menuItems.Remove(menuItem);
        }

        public List<MenuItem> GetMenuItems()
        {
            return menuItems;
        }
    }

    public enum OrderStatus
    {
        Pending,
        Preparing,
        Ready,
        Delivering,
        Completed
    }

    public interface IOrderStatusObserver
    {
        void Update(OrderStatus status);
    }

    public class Order
    {
        private List<MenuItem> orderedItems = new List<MenuItem>();
        private List<IOrderStatusObserver> observers = new List<IOrderStatusObserver>();

        public void AddMenuItem(MenuItem menuItem)
        {
            orderedItems.Add(menuItem);
        }

        public void AddObserver(IOrderStatusObserver observer)
        {
            observers.Add(observer);
        }

        public void ChangeStatus(OrderStatus status)
        {
            foreach (var observer in observers)
            {
                observer.Update(status);
            }
        }
    }

    public abstract class IngredientDecorator : MenuItem
    {
        protected MenuItem menuItem;

        public IngredientDecorator(MenuItem menuItem)
        {
            this.menuItem = menuItem;
        }

        public override string Description => menuItem.Description;
    }

    public class ExtraIngredientDecorator : IngredientDecorator
    {
        private string extraIngredient;
        private decimal extraPrice;

        public ExtraIngredientDecorator(MenuItem menuItem, string extraIngredient, decimal extraPrice) : base(menuItem)
        {
            this.extraIngredient = extraIngredient;
            this.extraPrice = extraPrice;
        }

        public override decimal Price => menuItem.Price + extraPrice;
    }

    public abstract class OrderFactory
    {
        public abstract Order CreateOrder();
    }

    public class TakeawayOrderFactory : OrderFactory
    {
        public override Order CreateOrder()
        {
            return new TakeawayOrder();
        }
    }

    public class DeliveryOrderFactory : OrderFactory
    {
        public override Order CreateOrder()
        {
            return new DeliveryOrder();
        }
    }

    public class TakeawayOrder : Order
    {
    }

    public class DeliveryOrder : Order
    {
    }

    class Client : IOrderStatusObserver
    {
        public void Update(OrderStatus status)
        {
            Console.WriteLine($"Статус заказа обновлен: {status}");
        }
    }
}
