using Microsoft.WindowsAzure.Mobile.Service;

namespace UniversalTodoAppService.DataObjects
{
    public class UserInfo : EntityData
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string PictureUrl { get; set; }
    }
}