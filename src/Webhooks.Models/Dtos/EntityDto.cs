namespace Webhooks.Models.Dtos
{
    public class EntityDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
