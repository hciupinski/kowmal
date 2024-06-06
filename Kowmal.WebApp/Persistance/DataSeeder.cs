using Kowmal.WebApp.Models;

namespace Kowmal.WebApp.Persistance;

public class DataSeeder
{
    private static Random _random = new Random();
    public async Task SeedDatabaseWithInitialDataAsync(AppDbContext context, int totalCount)
    {
        var count = 0;
        var currentCycle = 0;
        while (count < totalCount)
        {
            var list = new List<Post>();
            while (currentCycle++ < 100 && count++ < totalCount)
            {
                list.Add(MakePost());
            }
            if (list.Count > 0)
            {
                context.Posts?.AddRange(list);
                await context.SaveChangesAsync();
            }
            currentCycle = 0;
        }
    }

    private Post MakePost()
    {
        var post = new Post().SetDetails(RandomString(6), RandomString(20));
        for (var i = 0; i < 10; i++)
        {
            post.AddPhoto( new byte[1024],RandomString(5) + ".jpg");
        }

        return post;
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}