using System;

namespace FoodandBeverage.Services
{
    public static class NotificationService
    {
        // 🔹 Delegate for notifications
        public delegate void NotificationHandler(OrderNotification note);

        // 🔹 Event triggered for notifications
        public static event NotificationHandler OnNotify;

        // 🔹 Helper to raise a notification
        public static void Raise(string message, int orderId, int tableId)
        {
            OnNotify?.Invoke(new OrderNotification
            {
                Message = message,
                OrderId = orderId,
                TableId = tableId
            });
        }

        // 🔹 Notification model
        public class OrderNotification
        {
            public string Message { get; set; }
            public int OrderId { get; set; }
            public int TableId { get; set; }
        }
    }
}
