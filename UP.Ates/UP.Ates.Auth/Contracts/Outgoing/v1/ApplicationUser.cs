namespace UP.Ates.Auth.Contracts.Outgoing.v1
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public static ApplicationUser FromDomain(UP.Ates.Auth.Models.ApplicationUser user) =>
            new ApplicationUser { Id = user.Id };
    }
}