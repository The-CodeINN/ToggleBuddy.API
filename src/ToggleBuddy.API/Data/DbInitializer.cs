namespace ToggleBuddy.API.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetService<ToggleBuddyDbContext>();

        if (dbContext != null)
        {
            SeedData(dbContext);
        }
        else
        {
            // Handle the error
            throw new Exception("The DbContext is not registered");
        }
    }

    private static void SeedData(ToggleBuddyDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
