using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

/**
 * author:qixiao
 * create:2017-5-25 11:52:21
 * */
namespace QX_Frame.Bantina.Extends
{
    public static class CloneExtends
    {
        public static T DeepCloneObject<T>(this T t) where T : class
        {
            T model = System.Activator.CreateInstance<T>();                     //实例化一个T类型对象
            PropertyInfo[] propertyInfos = model.GetType().GetProperties();     //获取T对象的所有公共属性
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //判断值是否为空，如果空赋值为null见else
                if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                    NullableConverter nullableConverter = new NullableConverter(propertyInfo.PropertyType);
                    //将convertsionType转换为nullable对的基础基元类型
                    propertyInfo.SetValue(model, Convert.ChangeType(propertyInfo.GetValue(t), nullableConverter.UnderlyingType), null);
                }
                else
                {
                    propertyInfo.SetValue(model, Convert.ChangeType(propertyInfo.GetValue(t), propertyInfo.PropertyType), null);
                }
            }
            return model;
        }
        public static IList<T> DeepCloneList<T>(this IList<T> tList) where T : class
        {
            IList<T> listNew = new List<T>();
            foreach (var item in tList)
            {
                T model = System.Activator.CreateInstance<T>();                     //实例化一个T类型对象
                PropertyInfo[] propertyInfos = model.GetType().GetProperties();     //获取T对象的所有公共属性
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    //判断值是否为空，如果空赋值为null见else
                    if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                        NullableConverter nullableConverter = new NullableConverter(propertyInfo.PropertyType);
                        //将convertsionType转换为nullable对的基础基元类型
                        propertyInfo.SetValue(model, Convert.ChangeType(propertyInfo.GetValue(item), nullableConverter.UnderlyingType), null);
                    }
                    else
                    {
                        propertyInfo.SetValue(model, Convert.ChangeType(propertyInfo.GetValue(item), propertyInfo.PropertyType), null);
                    }
                }
                listNew.Add(model);
            }
            return listNew;
        }
    }
}
