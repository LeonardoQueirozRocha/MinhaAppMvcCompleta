namespace DevIO.Business.Models.Base
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity() => Id = Guid.NewGuid();
    }
}
