using System;
using System.Data.Entity;

namespace QX_Frame.App.Base
{
    public interface IEntity<DataBaseEntity, TEntity> where DataBaseEntity : DbContext
    {
        Boolean Add();
        Boolean Update();
        Boolean Delete();
    }
}
