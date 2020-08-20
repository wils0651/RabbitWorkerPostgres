using System;

namespace RabbitWorker.Domain
{
    public class ProbeData
    {
        public int Id {get; set;}
        public string ProbeName { get; set; }
        public DateTime Time { get; set; }
        public string Temperature { get; set; }
    }
}
