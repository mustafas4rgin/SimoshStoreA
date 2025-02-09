namespace SimoshStore;

public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }