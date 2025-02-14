namespace CurrencyConverter.Core.DomainEntities
{
    public class EntitySaveResult
    {
        public EntitySaveResult(int changes = 0, int savedId = 0)
        {
            Changes = changes;
            SavedId = savedId;
        }
        public int Changes { get; set; }
        public int SavedId { get; set; }
    }
}
