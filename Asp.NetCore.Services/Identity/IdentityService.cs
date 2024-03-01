namespace Asp.NetCore.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IdentityContext _context;

        public IdentityService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IdentityContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
    }
}