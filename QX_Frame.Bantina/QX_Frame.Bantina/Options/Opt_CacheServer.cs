/**
 * author:qixiao
 * create:2017-6-6 11:44:45
 * */
namespace QX_Frame.Bantina.Options
{
    public enum Opt_CacheServer
    {
        /// <summary>
        /// HttpRuntime.Cache
        /// </summary>
        HttpRuntimeCache,
        /// <summary>
        /// Redis Server
        /// </summary>
        Redis,
        /// <summary>
        /// Memcached
        /// </summary>
        Memcached,
        /// <summary>
        /// SqlLite
        /// </summary>
        SqlLite
    }
}
