namespace BuildingBlocks.Messaging.Events
{
    public record IntegrationEvent
    {
        public Guid Guid => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
