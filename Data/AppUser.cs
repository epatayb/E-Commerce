using Microsoft.AspNetCore.Identity;

namespace dotnet_store.Data;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = null!;
    
}