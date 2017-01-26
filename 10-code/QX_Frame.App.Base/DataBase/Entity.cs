using QX_Frame.Helper_DG_Framework;
using System;
using System.Data.Entity;
using System.Reflection;

namespace QX_Frame.App.Base
{
    [Serializable]
    public class Entity<DataBaseEntity, TEntity>:IEntity<DataBaseEntity, TEntity> where DataBaseEntity : DbContext
    {
        //New Entity Instance
        public static TEntity Build()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public static TEntity Build(params dynamic[] valueParms)
        {
            TEntity entity = System.Activator.CreateInstance<TEntity>();        // new instance of TEntity
            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();    //get the all public Properties
            if (propertyInfos.Length != valueParms.Length)
                throw new ArgumentException("arguments count not matching --qixiao");    //if arguments`s count not matching throw an exception
            for (int i = 0; i < propertyInfos.Length; i++)
                propertyInfos[i].SetValue(entity, valueParms[i]);               //set value for properties
            return entity;
        }

        public Type DataBaseType { get { return typeof(DataBaseEntity); } }
        public Type EntityType { get { return typeof(TEntity); } }

        //Entity to SqlServer DataBase
        public Boolean Add()
        {
            if (this == null)
            {
                throw new ArgumentNullException(nameof(TEntity));
            }
            return EF_Helper_DG<DataBaseEntity>.Add(this);
        }
        public Boolean Update()
        {
            if (this == null)
            {
                throw new ArgumentNullException(nameof(TEntity));
            }
            return EF_Helper_DG<DataBaseEntity>.Update(this);
        }
        public Boolean Delete()
        {
            if (this == null)
            {
                throw new ArgumentNullException(nameof(TEntity));
            }
            return EF_Helper_DG<DataBaseEntity>.Delete(this);
        }
    }
}
