namespace QX_Frame.App.Base.Options
{
    /**
     * author:qixiao
     * time:2017-3-2 22:58:56
     * desc:Sql Execute Type
     * */
    public enum ExecuteType
    {
        ExecuteNonQuery,
        ExecuteScalar,
        ExecuteReader,
        ExecuteDataTable,
        ExecuteDataSet,
        Execute_Model_T,
        Execute_List_T,
        _ChooseOthers_IfYouChooseThisYouWillGetAnException
    }
}
