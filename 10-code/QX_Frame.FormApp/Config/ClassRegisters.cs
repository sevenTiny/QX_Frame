using QX_Frame.App.Form;
using QX_Frame.Data.Service;

namespace QX_Frame.FormApp
{
    /**
    * author:qixiao
    * time:2017-2-21 14:48:28
    **/
    public class ClassRegisters : FormBase
    {
        public ClassRegisters()
        {
            //register region --
            //FormBase.Register(c => new UserAccountService());



            //end register region --
            FormBase.RegisterComplex();
        }
    }
}
