using System;

namespace QX_Frame.Data.DTO
{
    [Serializable]
    public class UserViewModel
    {
        public Guid UserUid { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string LimitCode { get; set; }
        public string DisplayCode { get; set; }
    }
}
