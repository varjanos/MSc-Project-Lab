namespace FloorPlanner.Dal.Entities;

public class UserProfile
{
    public const int DomainMaxLength = 256;
    public const int UserNameMaxLength = 256;

    public int Id { get; set; }

    public string Domain { get; set; }// TODO: delete

    public string UserName { get; set; }
    public string LanguageName { get; set; }

    public virtual ICollection<Plan> Plans { get; set; }
}