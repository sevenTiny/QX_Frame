/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-6-6 11:54:07
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * Personal WebSit: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:-.
 * Thx , Best Regards ~
 *********************************************************/
using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
/**
* author:qixiao
* create:2017-6-6 11:54:07
* */
namespace QX_Frame.Bantina
{
    public class HttpRuntimeCache_Helper_DG
    {
        /// <summary>
        /// Cache_Get
        /// </summary>
        /// <param name="cacheKey">cacheKey</param>
        /// <returns></returns>
        public static object Cache_Get(string cacheKey)
        {
            return HttpRuntime.Cache[cacheKey];
        }

        #region Cache Add

        /// <summary>
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
            HttpRuntime.Cache.Insert(cacheKey, cacheValue, dependencies, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(keepMinutes), CacheItemPriority.NotRemovable, cacheItemRemovedCallback);
            return true;
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
            HttpRuntime.Cache.Insert(cacheKey, cacheValue, dependencies, expireTime, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, cacheItemRemovedCallback);
            return true;
        }

        /// <summary>
        /// Cache_Add
        /// </summary>
        /// <param name="cacheKey">key</param>
        /// <param name="cacheValue">object value</param>
        /// <param name="dependencies">缓存的依赖项，也就是此项的更改意味着缓存内容已经过期。如果没有依赖项，可将此值设置为NULL。</param>
        /// <param name="absoluteExpiration">如果设置slidingExpiration，则该项必须设置为DateTime.MaxValue。是日期型数据，表示缓存过期的时间，.NET 2.0提供的缓存在过期后是可以使用的，能使用多长时间，就看这个参数的设置。</param>
        /// <param name="slidingExpiration">如果设置absoluteExpiration，则该项必须设置为TimeSpan.Zero。表示一段时间间隔，表示缓存参数将在多长时间以后被删除，此参数与absoluteExpiration参数相关联。</param>
        /// <param name="cacheItemPriority">表示撤销缓存的优先值，此参数的值取自枚举变量“CacheItemPriority”，优先级低的数据项将先被删除。此参数主要用在缓存退出对象时。</param>
        /// <param name="cacheItemRemovedCallback">表示缓存删除数据对象时调用的事件，一般用做通知程序。</param>
        //public static Boolean Cache_Add(string cacheKey, object cacheValue, CacheDependency dependencies = null, DateTime absoluteExpiration = default(DateTime), TimeSpan slidingExpiration = default(TimeSpan), CacheItemPriority cacheItemPriority = CacheItemPriority.NotRemovable, CacheItemRemovedCallback cacheItemRemovedCallback = null)
        //{
        //    DateTime absoluteExpirationTime = default(DateTime);
        //    if (!DateTime.TryParse(absoluteExpiration.ToString(), out absoluteExpirationTime) || absoluteExpiration.Equals(default(DateTime)))
        //        absoluteExpirationTime = DateTime.MaxValue;
        //    else
        //        slidingExpiration = TimeSpan.Zero;

        //    TimeSpan slidingExpirationTime = default(TimeSpan);
        //    if (!TimeSpan.TryParse(slidingExpiration.ToString(), out slidingExpirationTime) || slidingExpiration.Equals(default(TimeSpan)))
        //        slidingExpirationTime = TimeSpan.Zero;

        //    HttpRuntime.Cache.Insert(cacheKey, cacheValue, dependencies, absoluteExpirationTime, slidingExpirationTime, cacheItemPriority, cacheItemRemovedCallback);
        //    return true;
        //}

        #endregion

        /// <summary>
        /// Cache_Delete
        /// </summary>
        /// <param name="cacheKey">cacheKey</param>
        public static Boolean Cache_Delete(string cacheKey)
        {
            HttpRuntime.Cache.Remove(cacheKey);
            return true;
        }

        /// <summary>
        /// Cache_DeleteAll
        /// </summary>
        public static Boolean Cache_DeleteAll()
        {
            Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
            return true;
        }

        /// <summary>
        /// Cache Count
        /// </summary>
        public static int CacheCount
        {
            get { return HttpRuntime.Cache.Count; }
        }

        #region Cache Support
        /// <summary>
        /// TableConstructionCache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T CacheChannel<T>(string cacheHashKey, Func<T> func) where T : class
        {
            string hashKey = cacheHashKey.GetHashCode().ToString();
            object cacheValue = HttpRuntimeCache_Helper_DG.Cache_Get(hashKey);
            if (cacheValue != null)
                return cacheValue as T;
            cacheValue = func();
            HttpRuntimeCache_Helper_DG.Cache_Add(hashKey, cacheValue);
            return cacheValue as T;
        }

        public static T CacheChannel<T>(string cacheHashKey,int keepMinutes, Func<T> func) where T : class
        {
            string hashKey = cacheHashKey.GetHashCode().ToString();
            object cacheValue = HttpRuntimeCache_Helper_DG.Cache_Get(hashKey);
            if (cacheValue != null)
                return cacheValue as T;
            cacheValue = func();
            HttpRuntimeCache_Helper_DG.Cache_Add(hashKey, cacheValue, keepMinutes);
            return cacheValue as T;
        }
        #endregion
    }
}
