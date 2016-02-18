using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace ContinuousJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("jobqueue")] string message, TextWriter log)
        {
            Console.WriteLine("Console.Write - " + message);
            Console.Out.WriteLine("Console.Out - " + message);
            Console.Error.WriteLine("Console.Error - " + message);

            dynamic kaark = JsonConvert.DeserializeObject(message);

            log.WriteLine("Message Details");
            log.WriteLine("   Action: " + kaark.Action);
            log.WriteLine("   Email: " + kaark.Email);
            log.WriteLine("   Duration: " + kaark.Duration);

            if (kaark.ThrowError == "1")
             throw new Exception();
        }
    }
}
