namespace QX_Frame.App.Base
{
    public interface IWcfService
    {
        WcfQueryResult QueryAll(WcfQueryObject queryCondition);
        int QueryCount(WcfQueryObject queryCondition);
        WcfQueryResult QuerySingle(WcfQueryObject queryCondition);
    }
}
