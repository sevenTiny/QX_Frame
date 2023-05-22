using QX_Frame.App.Web;
using QX_Frame.Data.Service;

/**
 * author:qixiao 
 * create：2017-2-21 14:21:41
 **/
namespace QX_Frame.Web.Config
{
    //class registers
    public class ClassRegisters : WebControllerBase
    {
        public ClassRegisters()
        {
            //register region --
            WebControllerBase.Register(c => new PeopleService());
            WebControllerBase.Register(c => new ClassNameService());
            WebControllerBase.Register(c => new ScoreService());



            //end register region --
            WebControllerBase.RegisterComplex();
        }
    }
}