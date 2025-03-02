namespace WebApiWithJwtSwagger.Models.Common
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime? InsertTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public bool IsRemove { get; set; } = false;
        public DateTime? RemoveTime { get; set; } 

    }
    public abstract class BaseEntity : BaseEntity<long>
    { }
}
