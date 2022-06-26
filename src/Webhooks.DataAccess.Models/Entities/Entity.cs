using MongoDB.Bson.Serialization.Attributes;

namespace Webhooks.DataAccess.Models.Entities
{
    public class Entity
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsActive { get; set; }
    }
}
