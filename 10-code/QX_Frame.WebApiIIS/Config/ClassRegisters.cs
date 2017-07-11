using QX_Frame.App.WebApi;

/**
 * author:qixiao 
 * create：2017-2-21 14:21:41
 **/
namespace QX_Frame.WebApiIIS.Config
{
    //class registers
    public class ClassRegisters : WebApiControllerBase
    {
        public ClassRegisters()
        {
            //register region --
            //WebApiControllerBase.Register(c => new UserAccountService());



            //end register region --
            WebApiControllerBase.RegisterComplex();
        }
    }
}