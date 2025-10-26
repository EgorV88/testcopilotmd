namespace CoPilodMD.Core.pipes
{
    public class ServiceMessage
    {
        public string Sender { get; set; }
        public string To { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
    }
}
