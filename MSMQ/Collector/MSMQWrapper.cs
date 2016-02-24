using System;
using System.Messaging;

namespace Collector
{
    internal class MSMQWrapper
    {
        private readonly MessageQueue queue;

        public MSMQWrapper(string MessageQueueName)
        {
            if (MessageQueue.Exists(MessageQueueName))
                queue = new MessageQueue(MessageQueueName);
            else
                queue = MessageQueue.Create(MessageQueueName);

            queue.Formatter = new XmlMessageFormatter(new[] {typeof (FileChangeDTO), typeof (string)});
        }

        public void Clean()
        {
            queue.Close();
        }

        public void SendObject(string fileName)
        {
            var fileDTO = new FileChangeDTO(fileName);
            queue.Send(fileDTO);
        }

        public string RecieveMessage()
        {
            
            var res = queue.Receive();
            //var res = queue.Peek();

            //if (res.Body is string)
            //    Console.WriteLine(res.Id + " " + (string) res.Body);
            //else if (res.Body is ErrorNotification)
            //    Console.WriteLine(res.Id + " " + (FileChangeDTO) res.Body);
            return res.Id + " " + (FileChangeDTO) res.Body;
        }

        public class ErrorNotification
        {
            public int ErrorId { get; set; }
            public string Message { get; set; }

            public override string ToString()
            {
                return ErrorId + " - " + Message;
            }
        }
    }
}