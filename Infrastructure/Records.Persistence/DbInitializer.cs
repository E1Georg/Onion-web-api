namespace Records.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(RecordsDbContext context)
        {            
            context.Database.EnsureCreated();
        }
    }
}
