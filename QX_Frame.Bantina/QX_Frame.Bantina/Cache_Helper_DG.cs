using QX_Frame.Bantina.Configs;
using QX_Frame.Bantina.Options;
using ServiceStack.Redis;
using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

/*
 * author:qixiao
 * create:2016-11-12 22:58:25 
 * update:2017-6-2 15:23:35
 */
namespace QX_Frame.Bantina
{

    public abstract class Cache_Helper_DG
    {
        /// <summary>
        /// Cache_Get
        /// </summary>
        /// <param name="cacheKey">cacheKey</param>
        /// <returns></returns>
        public static object Cache_Get(string cacheKey)
        {
            switch (QX_Frame_Helper_DG_Config.Cache_CacheServer)
            {
                case Opt_CacheServer.HttpRuntimeCache:
                    return HttpRuntime.Cache[cacheKey];
                case Opt_CacheServer.Redis:
                    using (Redis_Helper_DG redis = new Redis_Helper_DG())
                    {
                        using (IRedisClient client = redis.GetClient())
                        {
                            return client.Get<object>(cacheKey);
                        }
                    }
                case Opt_CacheServer.Memcached:
                    throw new Exception("Opt_CacheServer Error !");
                case Opt_CacheServer.SqlLite:
                    throw new Exception("Opt_CacheServer Error !");
                default:
                    throw new Exception("Opt_CacheServer Error !");
            }
        }


        /// <summary>nani 
        /// Cache_Add
        /// </summary>
        /// <param name="cacheKey">key</param>
        /// <param name="cacheValue">object value</param>
        /// <param name="keepMinutes"></param>
        /// <param name="dependencies">缓存的依赖项，也就是此项的更改意味着缓存内容已经过期。如果没有依赖项，可将此值设置为NULL。</param>
        /// <param name="cacheItemRemovedCallback">表示缓存删除数据对象时调用的事件，一般用做通知程序。</param>
        /// <returns></returns>
        public static Boolean Cache_Add(string cacheKey, object cacheValue, int keepMinutes = 10, CacheDependency dependencies = null, CacheItemRemovedCallback cacheItemRemovedCallback = null)
        {
            switch (QX_Frame_Helper_DG_Config.Cache_CacheServer)
            {
                case Opt_CacheServer.HttpRuntimeCache:
                    HttpRuntime.Cache.Insert(cacheKey, cacheValue, dependencies, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(keepMinutes), CacheItemPriority.NotRemovable, cacheItemRemovedCallback);
                    return true;
                case Opt_CacheServer.Redis:
                    using (Redis_Helper_DG redis = new Redis_Helper_DG())
                    {
                        using (IRedisClient client = redis.GetClient())
                        {
                            return client.Set(cacheKey, cacheValue, TimeSpan.FromMinutes(keepMinutes));
                        }
                    }
                case Opt_CacheServer.Memcached:
                    throw new Exception("Opt_CacheServer Error !");
                case Opt_CacheServer.SqlLite:
                    throw new Exception("Opt_CacheServer Error !");
                default:
                    throw new Exception("Opt_CacheServer Error !");
            }
        }

        /// <summary>
        /// Cache_Add
        /// </summary>
        /// <param name="cacheKey">key</param>
        /// <param name="cacheValue">object value</param>
        /// <param name="keepMinutes"></param>
        /// <param name="dependencies">缓存的依赖项，也就是此项的更改意味着缓存内容已经过期。如果没有依赖项，可将此值设置为NULL。</param>
        /// <param name="cacheItemRemovedCallback">表示缓存删除数据对象时调用的事件，一般用做通知程序。</param>
        /// <returns></returns>
        public static Boolean Cache_Add(string cacheKey, object cacheValue, DateTime expireTime, CacheDependency dependencies = null, CacheItemRemovedCallback cacheItemRemovedCallback = null)
        {
            switch (QX_Frame_Helper_DG_Config.Cache_CacheServer)
            {
                case Opt_CacheServer.HttpRuntimeCache:
                    HttpRuntime.Cache.Insert(cacheKey, cacheValue, dependencies, expireTime, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, cacheItemRemovedCallback);
                    return true;
                case Opt_CacheServer.Redis:
                    using (Redis_Helper_DG redis = new Redis_Helper_DG())
                    {
                        using (IRedisClient client = redis.GetClient())
                        {
                            return client.Set(cacheKey, cacheValue, expireTime);
                        }
                    }
                case Opt_CacheServer.Memcached:
                    throw new Exception("Opt_CacheServer Error !");
                case Opt_CacheServer.SqlLite:
                    throw new Exception("Opt_CacheServer Error !");
                default:
                    throw new Exception("Opt_CacheServer Error !");
            }
        }


        /// <summary>
        /// Cache_Delete
        /// </summary>
        /// <param name="cacheKey">cacheKey</param>
        public static Boolean Cache_Delete(string cacheKey)
        {
            switch (QX_Frame_Helper_DG_Config.Cache_CacheServer)
            {
                case Opt_CacheServer.HttpRuntimeCache:
                    HttpRuntime.Cache.Remove(cacheKey);
                    return true;
                case Opt_CacheServer.Redis:
                    using (Redis_Helper_DG redis = new Redis_Helper_DG())
                    {
                        using (IRedisClient client = redis.GetClient())
                        {
                            return client.Remove(cacheKey);
                        }
                    }
                case Opt_CacheServer.Memcached:
                    throw new Exception("Opt_CacheServer Error !");
                case Opt_CacheServer.SqlLite:
                    throw new Exception("Opt_CacheServer Error !");
                default:
                    throw new Exception("Opt_CacheServer Error !");
            }
        }

        /// <summary>
        /// Cache_DeleteAll
        /// </summary>
        public static Boolean Cache_DeleteAll()
        {
            if (QX_Frame_Helper_DG_Config.Cache_CacheServer == Opt_CacheServer.HttpRuntimeCache)
            {
                Cache _cache = HttpRuntime.Cache;
                IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
                while (CacheEnum.MoveNext())
                {
                    _cache.Remove(CacheEnum.Key.ToString());
                }
            }
            else if (QX_Frame_Helper_DG_Config.Cache_CacheServer == Opt_CacheServer.Redis)
            {
                using (Redis_Helper_DG redis = new Redis_Helper_DG())
                {
                    using (IRedisClient client = redis.GetClient())
                    {
                        client.FlushAll();
                        return true;
                    }
                }
            }
            else
            {
                throw new Exception("Opt_CacheServer Error !");
            }
            return true;
        }

        /// <summary>
        /// Cache Count
        /// </summary>
        public static int CacheCount
        {
            get
            {
                switch (QX_Frame_Helper_DG_Config.Cache_CacheServer)
                {
                    case Opt_CacheServer.HttpRuntimeCache:
                        return HttpRuntime.Cache.Count;
                    case Opt_CacheServer.Redis:
                        using (Redis_Helper_DG redis = new Redis_Helper_DG())
                        {
                            using (IRedisClient client = redis.GetClient())
                            {
                                return client.GetAllKeys().Count;
                            }
                        }
                    case Opt_CacheServer.Memcached:
                        throw new Exception("Opt_CacheServer Error !");
                    case Opt_CacheServer.SqlLite:
                        throw new Exception("Opt_CacheServer Error !");
                    default:
                        throw new Exception("Opt_CacheServer Error !");
                }
            }
        }
    }
}
