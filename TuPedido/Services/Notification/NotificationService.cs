using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Helpers;
using TuPedido.Models;
using Xamarin.Forms;

namespace TuPedido.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRestClient restClient;
        private readonly IConfiguration configuration;

        public NotificationService(IRestClient restClient, IConfiguration configuration)
        {
            this.restClient = restClient;
            this.configuration = configuration;
        }

        public Task Send(Notification notification)
        {
            return ErrorHelper.TryExecuteAsync(async () =>
            {
                var request = new NotificationRequest
                {
                    Content = new NotificationContent
                    {
                        Name = notification.Title,
                        Title = notification.Title,
                        Body = notification.Message
                    },
                    Target = new DevicesTarget
                    {
                        Devices = new List<Guid> { notification.DeviceId }
                    }
                };
                
                var appOwner = notification.Platform == Device.Android ? configuration.AppCenter.AndriodAppOwner : configuration.AppCenter.iOSAppOwner;
                var appName = notification.Platform == Device.Android ? configuration.AppCenter.AndroidAppName : configuration.AppCenter.iOSAppName;

                var url = string.Format(configuration.Endpoints.PostNotificationEndpoint, appOwner, appName);
                var headers = new Dictionary<string, string> { { "X-API-Token", configuration.AppCenter.ApiToken } };

                await restClient.PostAsync<NotificationRequest, NotificationResponse>(url, request, headers);
            });
        }

        #region Serialization

        public class NotificationRequest
        {
            [JsonProperty("notification_target")]
            public DevicesTarget Target { get; set; }

            [JsonProperty("notification_content")]
            public NotificationContent Content { get; set; }
        }

        public class DevicesTarget
        {
            [JsonProperty("type")]
            public string Type => "devices_target";

            [JsonProperty("devices")]
            public IEnumerable<Guid> Devices { get; set; }
        }

        public class NotificationResponse
        {
            [JsonProperty("notification_id")]
            public string NotificationId { get; set; }
        }

        public class NotificationContent
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("body")]
            public string Body { get; set; }

            [JsonProperty("custom_data")]
            public Dictionary<string, string> CustomData { get; set; }
        }

        #endregion
    }
}
