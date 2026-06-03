using EncounterForgeSSR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EncounterForgeSSR.Data;

public static class AppDataSeeder
{
    public static async Task SeedDefaultUserAsync(AppDbContext context)
    {
        if (await context.AppUsers.AnyAsync())
        {
            return;
        }

        var defaultUser = new AppUser
        {
            UserName = "dmadmin"
        };

        var hasher = new PasswordHasher<AppUser>();
        defaultUser.PasswordHash = hasher.HashPassword(defaultUser, "Encounter123!");

        context.AppUsers.Add(defaultUser);
        await context.SaveChangesAsync();
    }

    public static async Task SeedMonsterCatalogAsync(AppDbContext context)
    {
        if (await context.MonsterCatalogs.AnyAsync())
        {
            return;
        }

        context.MonsterCatalogs.AddRange(
            new MonsterCatalog { Name = "Goblin", ChallengeRating = "1/4", MonsterType = "Humanoid", SuggestedNotes = "Uses hit-and-run tactics and disengages when bloodied." },
            new MonsterCatalog { Name = "Orc", ChallengeRating = "1/2", MonsterType = "Humanoid", SuggestedNotes = "Rushes frontliners and focuses weakest armor first." },
            new MonsterCatalog { Name = "Skeleton", ChallengeRating = "1/4", MonsterType = "Undead", SuggestedNotes = "Acts in disciplined formation; ignores fear effects." },
            new MonsterCatalog { Name = "Ogre", ChallengeRating = "2", MonsterType = "Giant", SuggestedNotes = "High burst damage, low AC; telegraph heavy swings." },
            new MonsterCatalog { Name = "Cult Fanatic", ChallengeRating = "2", MonsterType = "Humanoid", SuggestedNotes = "Prioritizes concentration spells and keeps bodyguards nearby." },
            new MonsterCatalog { Name = "Young Red Dragon", ChallengeRating = "10", MonsterType = "Dragon", SuggestedNotes = "Uses mobility to line up breath weapon and pressure clustered PCs." }
        );

        await context.SaveChangesAsync();
    }
}
