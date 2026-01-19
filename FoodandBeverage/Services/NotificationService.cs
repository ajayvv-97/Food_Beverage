using System;
using System.Drawing; // For Color

namespace FoodandBeverage.Services
{
    public static class NotificationService
    {
        // 🔹 Delegate for notifications
        public delegate void NotificationHandler(OrderNotification note);

        // 🔹 Event triggered for notifications
        public static event NotificationHandler OnNotify;

        // 🔹 Helper to raise a notification
        public static void Raise(string message, int orderId, int tableId, Color? color = null)
        {
            OnNotify?.Invoke(new OrderNotification
            {
                Message = message,
                OrderId = orderId,
                TableId = tableId,
                NotificationColor = color ?? Color.White // default white
            });
        }

        // 🔹 Notification model
        public class OrderNotification
        {
            public string Message { get; set; }
            public int OrderId { get; set; }
            public int TableId { get; set; }

            // 🔹 Color-coded notification
            public Color NotificationColor { get; set; } = Color.White;
        }
    }
}
