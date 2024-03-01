namespace Asp.NetCore.Infrastructure.Identity.Enums
{
    public enum AppUserStatus
    {
        [Display(Name = "Chưa kích hoạt")]
        Unactived = 0,

        [Display(Name = "Hoạt động")]
        Actived = 1,

        [Display(Name = "Vô hiệu hóa")]
        Disabled = 2
    }
}