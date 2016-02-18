using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NetAtNoonAzure101.Models;
using Newtonsoft.Json;

namespace NetAtNoonAzure101.Controllers
{
    public class QueueJobController : Controller
    {
        private CloudQueue queue;

        public QueueJobController()
        {
            InitQueue();
        }

        private void InitQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            queue = queueClient.GetQueueReference("jobqueue");
            queue.CreateIfNotExists();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var defaultMessage = new { Action = "ReportA", Email = "agschwantes@gmail.com", Duration = "10", ThrowError = "0" };
            return View(new SendQueueMessageViewModel { Message = JsonConvert.SerializeObject(defaultMessage) });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(SendQueueMessageViewModel model)
        {
            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(model.Message);
            await queue.AddMessageAsync(cloudQueueMessage);

            return RedirectToAction("Index", "QueueJob");
        }

    }
}