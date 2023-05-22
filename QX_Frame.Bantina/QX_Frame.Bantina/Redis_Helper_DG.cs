using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using QX_Frame.Bantina.Configs;

/**
 * author:qixiao
 * create:2017-6-5 10:47:01
 * update:2017-7-28 23:26:13
 * */
namespace QX_Frame.Bantina
{
    public class Redis_Helper_DG : IDisposable
    {
        /// <summary>
        /// client
        /// </summary>
        private static IRedisClient client;

        /// <summary>
        /// pooled redis lient manager
        /// </summary>
        private static PooledRedisClientManager _prcm;

        public Redis_Helper_DG()
        {
            CreateManager();
        }

        /// <summary>  
        /// 创建链接池管理对象  
        /// </summary>  
        private void CreateManager()
        {
            _prcm = CreateManager(QX_Frame_Helper_DG_Config.Cache_Redis_Host_ReadWrite_Array, QX_Frame_Helper_DG_Config.Cache_Redis_Host_OnlyRead_Array);
        }

        private PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            //WriteServerList：可写的Redis链接地址。  
            //ReadServerList：可读的Redis链接地址。  
            //MaxWritePoolSize：最大写链接数。  
            //MaxReadPoolSize：最大读链接数。  
            //AutoStart：自动重启。  
            //LocalCacheTime：本地缓存到期时间，单位:秒。  
            //RecordeLog：是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项。  
            //RedisConfigInfo类是记录redis连接信息，此信息和配置文件中的RedisConfig相呼应  

            // 支持读写分离，均衡负载   
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = 20, // “写”链接池链接数   
                MaxReadPoolSize = 20, // “读”链接池链接数   
                AutoStart = true,
            });
        }

        /// <summary>  
        /// Client
        /// </summary>  
        public IRedisClient GetClient()
        {
            if (_prcm == null)
            {
                CreateManager();
            }
            return _prcm.GetClient();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if (client != null)
                    {
                        client = null;
                        client.Dispose();
                    }
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>  
        /// 保存数据DB文件到硬盘  
        /// </summary>  
        public void Save()
        {
            client.Save();
        }
        /// <summary>  
        /// 异步保存数据DB文件到硬盘  
        /// </summary>  
        public void SaveAsync()
        {
            client.SaveAsync();
        }
    }
}
