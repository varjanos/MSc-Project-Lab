using Core.Context.CurrentUserIdContext;
using Core.Extensions;
using Core.Time;
using Core.Translation.Dal.Context;
using Core.Translation.Dal.Entities;
using Core.Translation.Dal.Seed;
using Core.Translation.Extensions;
using FloorPlanner.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using TranslationEntity = Core.Translation.Dal.Entities.Translation;

namespace FloorPlanner.Dal.Context;

public class FloorPlannerDbContext : DbContext, ITranslationDbContext
{
    private readonly ICurrentUserIdContext _currentUserIdContext;
    private readonly ITimeService _timeService;

    public FloorPlannerDbContext(
        DbContextOptions<FloorPlannerDbContext> options,
        ICurrentUserIdContext currentUserIdContext,
        ITimeService timeService)
        : base(options)
    {
        _currentUserIdContext = currentUserIdContext;
        _timeService = timeService;
    }

    public virtual DbSet<TranslationEntity> Translation { get; set; }
    public virtual DbSet<Language> Language { get; set; }
    public virtual DbSet<UserProfile> UserProfile { get; set; }



    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        SaveChangesCore();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected virtual void SaveChangesCore()
    {
        this.UpdateDeletedEntries();
        this.UpdateModifiedEntries(_timeService, _currentUserIdContext);
        this.UpdateAddedEntries(_timeService, _currentUserIdContext);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureEntities(modelBuilder);

        modelBuilder.RegisterSoftDeleteQueryFilters();

        var languages = new List<Language>
    {
            new Language
            {
                Name = "en",
                TranslationFilePath = "Translations/en-US.json",
                DateTimeFormat = "yyyy.MM.dd. HH:mm",
                DecimalSeparator = '.',
                IconFilePath = "flags/en-US.svg",
                DisplayName = "English",
            },
            new Language
            {
                Name = "hu",
                DateTimeFormat = "yyyy.MM.dd. HH:mm",
                TranslationFilePath = "Translations/hu-HU.json",
                DecimalSeparator = '.',
                IconFilePath = "flags/hu-HU.svg",
                DisplayName = "Hungarian",
            },
    };
        modelBuilder.SeedLanguages(languages);
    }

    private static void ConfigureEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyTranslationConfigurations();
    }
}