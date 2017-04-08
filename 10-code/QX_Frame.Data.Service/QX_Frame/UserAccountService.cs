using QX_Frame.App.Base;
using QX_Frame.Data.Contract.QX_Frame;
using QX_Frame.Data.Entities.QX_Frame;

namespace QX_Frame.Data.Service.QX_Frame
{
    public class UserAccountService : WcfService, IUserAccountService
    {
        private tb_UserAccount _tb_UserAccount;
        public UserAccountService()
        { }
        public UserAccountService(tb_UserAccount tb_UserAccount)
        {
            this._tb_UserAccount = tb_UserAccount;
        }
        public bool Add(tb_UserAccount tb_UserAccount)
        {
            return tb_UserAccount.Add(tb_UserAccount);
        }
        public bool Update(tb_UserAccount tb_UserAccount)
        {
            return tb_UserAccount.Update(tb_UserAccount);
        }
        public bool Delete(tb_UserAccount tb_UserAccount)
        {
            return tb_UserAccount.Delete(tb_UserAccount);
        }
    }
}
