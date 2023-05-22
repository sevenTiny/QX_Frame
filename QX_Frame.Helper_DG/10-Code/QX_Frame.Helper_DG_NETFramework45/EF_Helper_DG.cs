using LinqKit; //AsExpandable() in linqkit.dll
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using System.Transactions;
using System.Web;

namespace QX_Frame.Helper_DG
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
        //private volatile static Db db = null;   //volatile find Db in memory not in cache

        #region The Singleton to new DBEntity_DG

        //private static readonly object lockHelper = new object();
        //static EF_Helper_DG()
        //{
        //    if (db == null)
        //    {
        //        lock (lockHelper)
        //        {
        //            if (db == null)
        //                db = System.Activator.CreateInstance<Db>();
        //        }
        //    }

        //    //close the Validate of EF OnSaveEnabled
        //    db.Configuration.ValidateOnSaveEnabled = false;
        //}

        #endregion

        #region get current dbContext
        public static DbContext GetCurrentDbContext()
        {
            //method 1 : CallContext
            //CallContext：是线程内部唯一的独用的数据槽（一块内存空间）  
            //Db dbContext = CallContext.GetData("DbContext") as Db;
            //if (dbContext == null)  //线程在内存中没有此上下文  
            //{
            //    //create a dbContext to memory if dbContext has not exist
            //    dbContext = System.Activator.CreateInstance<Db>();
            //    CallContext.SetData("DbContext", dbContext);
            //}

            //method 2 :
            Db dbContext = HttpContext.Current.Items["dbContext"] as Db;
            if (dbContext == null)
            {
                //create a dbContext to memory if dbContext has not exist
                dbContext = System.Activator.CreateInstance<Db>();
                HttpContext.Current.Items["dbContext"] = dbContext;
            }
            return dbContext;
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
                    DbContext db = GetCurrentDbContext();
                    iqueryable = db.Set<T>().AsExpandable();
                    Cache_Helper_DG.Cache_Add(nameof(T), iqueryable, null, DateTime.Now.AddMinutes(CacheExpirationTime_Minutes), TimeSpan.Zero);
                }
                return iqueryable;
            }
            else
            {
                DbContext db = GetCurrentDbContext();
                return db.Set<T>().AsExpandable();
            }
        }

        #endregion

        #region Add 

        public static Boolean Add<T>(T entity) where T : class
        {
            DbContext db = GetCurrentDbContext();
            CacheChanges<T>();
            db.Entry<T>(entity).State = EntityState.Added;
            return db.SaveChanges() > 0;
        }
        public static Boolean Add<T>(T entity, out T outEntity) where T : class
        {
            DbContext db = GetCurrentDbContext();
            CacheChanges<T>();
            db.Entry<T>(entity).State = EntityState.Added;
            outEntity = entity;
            return db.SaveChanges() > 0;
        }
        public static Boolean Add<T>(IQueryable<T> entities) where T : class
        {
            DbContext db = GetCurrentDbContext();
            CacheChanges<T>();
            db.Set<T>().AddRange(entities);
            return db.SaveChanges() > 0;
        }

        #endregion

        #region Update

        public static Boolean Update<T>(T entity) where T : class
        {
            DbContext db = GetCurrentDbContext();
            CacheChanges<T>();
            if (db.Entry<T>(entity).State == EntityState.Detached)
            {
                db.Set<T>().Attach(entity);
                db.Entry<T>(entity).State = EntityState.Modified;
            }
            else
            {
                db.SaveChanges();
                return true;
            }
            return db.SaveChanges() > 0;
        }
        public static Boolean Update<T>(T entity, out T outEntity) where T : class
        {
            DbContext db = GetCurrentDbContext();
            CacheChanges<T>();
            outEntity = entity;
            if (db.Entry<T>(entity).State == EntityState.Detached)
            {
                db.Set<T>().Attach(entity);
                db.Entry<T>(entity).State = EntityState.Modified;
            }
            else
            {
                db.SaveChanges();
                return true;
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Delete

        public static Boolean Delete<T>(T entity) where T : class
        {
            DbContext db = GetCurrentDbContext();
            CacheChanges<T>();
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }
        public static Boolean Delete<T>(IQueryable<T> entities) where T : class
        {
            DbContext db = GetCurrentDbContext();
            CacheChanges<T>();
            db.Set<T>().RemoveRange(entities);
            return db.SaveChanges() > 0;
        }
        public static Boolean Delete<T>(Expression<Func<T, bool>> deleteWhere) where T : class
        {
            DbContext db = GetCurrentDbContext();
            CacheChanges<T>();
            IQueryable<T> entitys = GetIQuerybleByCache<T>().Where(deleteWhere);
            /**
             * change code 2017-5-6 11:11:19 qixiao
             * entitys.ForEach(m => db.Entry<T>(m).State = EntityState.Deleted);
             **/
            entitys.ForEachAsync(m => db.Entry<T>(m).State = EntityState.Deleted);
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

        #region Transaction
        public static void Transaction(Action action)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                action();
                trans.Complete();
            }
        }
        #endregion

        #region ExecuteSqlCommand

        public static void ExecuteSqlCommand(string sqlCommand)
        {
            DbContext db = GetCurrentDbContext();
            db.Database.ExecuteSqlCommand(sqlCommand);
        }
        public static void ExecuteSqlCommand(string sqlCommand, params object[] parameters)
        {
            DbContext db = GetCurrentDbContext();
            db.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion
    }
}
