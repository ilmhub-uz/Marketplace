using MongoDB.Bson.Serialization.Attributes;

namespace Categories.Entites;

public class Category
{
   [BsonId]
   public Guid Id { get; set; } = Guid.NewGuid();

   public required string Name { get; set; }
   public required string Key { get; set; }
   public Guid? ParentId { get; set; }
}