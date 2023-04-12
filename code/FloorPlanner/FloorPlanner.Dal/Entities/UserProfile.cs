using Microsoft.AspNetCore.Identity;

namespace FloorPlanner.Dal.Entities;

public class UserProfile : IdentityUser
{
    public const int DomainMaxLength = 256;
    public const int UserNameMaxLength = 256;

    public string Domain { get; set; }// TODO: delete

    public string LanguageName { get; set; }

    public virtual ICollection<Plan> Plans { get; set; }
}