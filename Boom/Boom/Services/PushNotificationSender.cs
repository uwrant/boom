using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Boom
{
    public class PushNotificationSender
    {
        public void Send(PushNotification notification)
        {
            const string ApplicationUrl = "https://arts-push.azure-mobile.net/";
            const string ApplicationKey = "gDabIJgwJZhPFohnwPlsiWvCnQeXgF73";
            const string NotificationTableName = "notifications";

            var client = new MobileServiceClient(ApplicationUrl, ApplicationKey);

            var notificationsTable = client.GetTable(NotificationTableName);
            var serializedNotification = JsonConvert.SerializeObject(notification);

            Task.WaitAll(notificationsTable.InsertAsync(JObject.Parse(serializedNotification)));
        }
    }
}