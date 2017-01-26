using LinqKit; //AsExpandable() in linqkit.dll
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace QX_Frame.Helper_DG_Framework
{
    /*  time:   2016-10-30 15:26:05
        author: qixiao
    */
    /// <summary>
    /// EntityFramework CodeFirst Helper 
    /// </summary>
    /// <typeparam name="Db">DbContext</typeparam>
    public abstract class EF_Helper_DG<Db> where Db : DbContext
    {
        /*the singleton Db */
        private volatile static Db db = null;   //volatile find Db in memory not in cache

        #region The Singleton to new DBEntity_DG

        private static readonly object lockHelper = new object();
        static EF_Helper_DG()
        {
            if (db == null)
            {
                lock (lockHelper)
                {
                    if (db == null)
                        db = System.Activator.CreateInstance<Db>();
                }
            }
            //close the Validate of EF OnSaveEnabled
            db.Configuration.ValidateOnSaveEnabled = false;
        }

        #endregion

        #region Cache Strategy

        //read configuration
        //configuration if need cache
        private static bool IsCache
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Config_Helper_DG.AppSetting_Get("IsCache")) == 1 ? true : false;
                }
                catch { return false; }
            }
        }
        //configuration the cache expiration time unit by minutes
        private static int CacheExpirationTime_Minutes
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Config_Helper_DG.AppSetting_Get("CacheExpirationTime_Minutes"));
                }
                catch
                {
                    return 10;
                }
            }
        }

        /// <summary>
        /// edit data cache must update
        /// </summary>
        public static void CacheChanges<T>()
        {
            if (IsCache)
            {
                Cache_Helper_DG.Cache_Delete(nameof(T));
            }
        }

        /// <summary>
        /// query cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> GetIQuerybleByCache<T>() where T : class
        {
            if (IsCache)
            {
                IQueryable<T> iqueryable = Cache_Helper_DG.Cache_Get(nameof(T)) as IQueryable<T>;
                if (iqueryable == null)
                {
                    iqueryable = db.Set<T>().AsExpandable();
                    Cache_Helper_DG.Cache_Add(nameof(T), iqueryable, null, DateTime.Now.AddMinutes(CacheExpirationTime_Minutes), TimeSpan.Zero);
                }
                return iqueryable;
            }
            else
            {
                return db.Set<T>().AsExpandable();
            }
        }

        #endregion

        #region Add 

        public static Boolean Add<T>(T entity) where T : class
        {
            CacheChanges<T>();
            db.Entry<T>(entity).State = EntityState.Added;
            return db.SaveChanges() > 0;
        }
        public static Boolean Add<T>(T entity, out T outEntity) where T : class
        {
            CacheChanges<T>();
            db.Entry<T>(entity).State = EntityState.Added;
            outEntity = entity;
            return db.SaveChanges() > 0;
        }
        public static Boolean Add<T>(IQueryable<T> entities) where T : class
        {
            CacheChanges<T>();
            db.Set<T>().AddRange(entities);
            return db.SaveChanges() > 0;
        }

        #endregion

        #region Update

        public static Boolean Update<T>(T entity) where T : class
        {
            CacheChanges<T>();
            if (db.Entry<T>(entity).State == EntityState.Detached)
            {
                db.Set<T>().Attach(entity);
                db.Entry<T>(entity).State = EntityState.Modified;
            }
            return db.SaveChanges() > 0;
        }
        public static Boolean Update<T>(T entity, out T outEntity) where T : class
        {
            CacheChanges<T>();
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Modified;
            outEntity = entity;
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Delete

        public static Boolean Delete<T>(T entity) where T : class
        {
            CacheChanges<T>();
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }
        public static Boolean Delete<T>(IQueryable<T> entities) where T : class
        {
            CacheChanges<T>();
            db.Set<T>().RemoveRange(entities);
            return db.SaveChanges() > 0;
        }
        public static Boolean Delete<T>(Expression<Func<T, bool>> deleteWhere) where T : class
        {
            CacheChanges<T>();
            IQueryable<T> entitys = GetIQuerybleByCache<T>().Where(deleteWhere);
            entitys.ForEach(m => db.Entry<T>(m).State = EntityState.Deleted);
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Select 

        public static Boolean Exist<T>(Expression<Func<T, Boolean>> selectWhere) where T : class
        {
            return GetIQuerybleByCache<T>().Where(selectWhere).FirstOrDefault<T>() == null ? false : true;
        }
        public static T selectSingle<T>(Expression<Func<T, Boolean>> selectWhere) where T : class
        {
            return GetIQuerybleByCache<T>().Where(selectWhere).FirstOrDefault<T>();
        }
        public static IQueryable<T> selectAll<T>() where T : class
        {
            return GetIQuerybleByCache<T>();
        }
        public static IQueryable<T> selectAll<T>(out int Count) where T : class
        {
            Count = GetIQuerybleByCache<T>().Count();
            return GetIQuerybleByCache<T>();
        }
        public static IQueryable<T> selectAll<T, TKey>(Expression<Func<T, TKey>> orderBy, Boolean isDESC = false) where T : class
        {
            if (isDESC)
                return GetIQuerybleByCache<T>().OrderByDescending(orderBy);
            else
                return GetIQuerybleByCache<T>().OrderBy(orderBy);
        }
        public static IQueryable<T> selectAll<T, TKey>(Expression<Func<T, TKey>> orderBy, out int Count, Boolean isDESC = false) where T : class
        {
            Count = GetIQuerybleByCache<T>().Count();
            if (isDESC)
                return GetIQuerybleByCache<T>().OrderByDescending(orderBy);
            else
                return GetIQuerybleByCache<T>().OrderBy(orderBy);
        }
        public static IQueryable<T> selectAll<T>(Expression<Func<T, Boolean>> selectWhere) where T : class
        {
            return GetIQuerybleByCache<T>().Where(selectWhere);
        }
        public static IQueryable<T> selectAll<T>(Expression<Func<T, Boolean>> selectWhere, out int Count) where T : class
        {
            var IQueryable = GetIQuerybleByCache<T>().Where(selectWhere);
            Count = IQueryable.Count();
            return IQueryable;
        }
        public static IQueryable<T> selectAll<T, TKey>(Expression<Func<T, TKey>> orderBy, Expression<Func<T, Boolean>> selectWhere, Boolean isDESC = false) where T : class
        {
            if (isDESC)
                return GetIQuerybleByCache<T>().Where(selectWhere).OrderByDescending(orderBy);
            else
                return GetIQuerybleByCache<T>().Where(selectWhere).OrderBy(orderBy);
        }
        public static IQueryable<T> selectAll<T, TKey>(Expression<Func<T, TKey>> orderBy, Expression<Func<T, Boolean>> selectWhere, out int Count, Boolean isDESC = false) where T : class
        {
            var IQueryable = GetIQuerybleByCache<T>().Where(selectWhere);
            Count = IQueryable.Count();
            if (isDESC)
                return IQueryable.OrderByDescending(orderBy);
            else
                return IQueryable.OrderBy(orderBy);
        }

        public static IQueryable<T> selectAllPaging<T, TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> orderBy, Boolean isDESC = false) where T : class
        {
            var IQueryable = GetIQuerybleByCache<T>();
            if (isDESC)
                return IQueryable.OrderByDescending(orderBy).Skip((pageIndex - 1 < 0 ? 0 : pageIndex - 1) * (pageSize < 0 ? 0 : pageSize)).Take(pageSize < 0 ? 0 : pageSize);
            else
                return IQueryable.OrderBy(orderBy).Skip((pageIndex - 1 < 0 ? 0 : pageIndex - 1) * (pageSize < 0 ? 0 : pageSize)).Take(pageSize < 0 ? 0 : pageSize);
        }
        public static IQueryable<T> selectAllPaging<T, TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> orderBy, out int Count, Boolean isDESC = false) where T : class
        {
            var IQueryable = GetIQuerybleByCache<T>();
            Count = IQueryable.Count();
            if (isDESC)
                return IQueryable.OrderByDescending(orderBy).Skip((pageIndex - 1 < 0 ? 0 : pageIndex - 1) * (pageSize < 0 ? 0 : pageSize)).Take(pageSize < 0 ? 0 : pageSize);
            else
                return IQueryable.OrderBy(orderBy).Skip((pageIndex - 1 < 0 ? 0 : pageIndex - 1) * (pageSize < 0 ? 0 : pageSize)).Take(pageSize < 0 ? 0 : pageSize);
        }
        public static IQueryable<T> selectAllPaging<T, TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> orderBy, Expression<Func<T, Boolean>> selectWhere, Boolean isDESC = false) where T : class
        {
            var IQueryable = GetIQuerybleByCache<T>().Where(selectWhere);
            if (isDESC)
                return IQueryable.OrderByDescending(orderBy).Skip((pageIndex - 1 < 0 ? 0 : pageIndex - 1) * (pageSize < 0 ? 0 : pageSize)).Take(pageSize < 0 ? 0 : pageSize);
            else
                return IQueryable.OrderBy(orderBy).Skip((pageIndex - 1 < 0 ? 0 : pageIndex - 1) * (pageSize < 0 ? 0 : pageSize)).Take(pageSize < 0 ? 0 : pageSize);
        }
        public static IQueryable<T> selectAllPaging<T, TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> orderBy, Expression<Func<T, Boolean>> selectWhere, out int Count, Boolean isDESC = false) where T : class
        {
            var IQueryable = GetIQuerybleByCache<T>().Where(selectWhere);
            Count = IQueryable.Count();
            if (isDESC)
                return IQueryable.OrderByDescending(orderBy).Skip((pageIndex - 1 < 0 ? 0 : pageIndex - 1) * (pageSize < 0 ? 0 : pageSize)).Take(pageSize < 0 ? 0 : pageSize);
            else
                return IQueryable.OrderBy(orderBy).Skip((pageIndex - 1 < 0 ? 0 : pageIndex - 1) * (pageSize < 0 ? 0 : pageSize)).Take(pageSize < 0 ? 0 : pageSize);
        }

        #endregion
    }
}
