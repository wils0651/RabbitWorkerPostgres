using RabbitWorker.Data;
using RabbitWorker.Domain;
using System;

namespace Worker
{
    public class MessageProcessor
    {
        private static WorkerContext _context;

        public MessageProcessor(WorkerContext context)
        {
            _context = context;
        }

        public void ProcessMessage(string message)
        {
            var probe = message.Substring(0, 7);
            var value = message.Substring(8);

            var time = DateTime.Now;

            var data = new ProbeData
            {
                ProbeName = probe,
                Time = time,
                Temperature = value
            };

            _context.ProbeData.Add(data);

            WriteToFile(data);

            _context.SaveChanges();
        }

        private void WriteToFile(ProbeData probeData)
        {
            string filePath;

            switch (probeData.ProbeName)
            {
                case "Probe_1":
                    //filePath = @"/home/tim/Documents/Programming/RabbitMQ/Worker/probe1.txt";
                    filePath = @"C:\repos\RabbitMqEntityFrameworkTest\probe1.txt";
                    break;

                case "Probe_2":
                    //filePath = @"/home/tim/Documents/Programming/RabbitMQ/Worker/probe2.txt";
                    filePath = @"C:\repos\RabbitMqEntityFrameworkTest\probe2.txt";
                    break;

                default:
                    //filePath = @"/home/tim/Documents/Programming/RabbitMQ/Worker/otherProbe1.txt";
                    filePath = @"C:\repos\RabbitMqEntityFrameworkTest\otherProbe.txt";
                    break;
            }

            var entry = $"{probeData.Time.ToString()}, {probeData.Temperature}";

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(filePath, true))
            {
                file.WriteLine(entry);
            }
        }
    }
}
