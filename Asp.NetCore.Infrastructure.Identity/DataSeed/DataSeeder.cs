namespace Asp.NetCore.Infrastructure.Identity.DataSeed
{
    public class DataSeeder : IDataSeeder
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IdentityContext _context;
        
        public DataSeeder(RoleManager<AppRole> roleManager
            , UserManager<AppUser> userManager
            , IdentityContext context) 
        {
            _roleManager = roleManager;
            _userManager = userManager;           
            _context = context;           
        }

        public async Task SeedAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                var adminRole = new AppRole() { Id = Guid.NewGuid(), Name = "Administrator", Description = "Administrator", CreatedAt = DateTime.Now };
                await _roleManager.CreateAsync(adminRole);                        
            }

            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = Utilities.Username.Administrator,
                    Email = "luonganh@gmail.com",
                    FullName = "Administrator",
                    PhoneNumber = string.Empty,
                    CreatedAt = DateTime.Now,
                    Gender = Gender.Male,
                    Status = AppUserStatus.Actived
                }, Utilities.Password.Administrator);
                var admin = await _userManager.FindByNameAsync(Utilities.Username.Administrator);
                var adminRole = await _roleManager.FindByNameAsync("Administrator");
                if (admin != null && adminRole != null)
                {
                    await _userManager.AddToRoleAsync(admin, adminRole.Name);
                }
            }
        }
    }
}
