namespace PocServiceBus.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public int Id { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
