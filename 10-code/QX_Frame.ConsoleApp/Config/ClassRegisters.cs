using QX_Frame.App.Base;
using QX_Frame.Data.Service;

namespace QX_Frame.ConsoleApp.Config
{
    /**
     * author:qixiao
     * time:2017-2-21 14:48:28
     **/ 
    public class ClassRegisters:AppBase
    {
        public ClassRegisters()
        {
            //register region --
            AppBase.Register(c => new PeopleService());
            AppBase.Register(c => new ClassNameService());
            AppBase.Register(c => new ScoreService());



            //end register region --
            AppBase.RegisterComplex();
        }
    }
}
