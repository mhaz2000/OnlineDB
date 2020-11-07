using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace TableProject.Context
{
    
    class OnlineDB : DbContext
    {
        
        public Type Type;
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] entityTypes = { Type };

                try
                {
                    foreach (var type in entityTypes)
                    {
                        entityMethod.MakeGenericMethod(type)
                          .Invoke(modelBuilder, new object[] { });
                    }
                }
                catch
                {
                    return;
                }
            }
        }
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class PersistentAttribute : Attribute
    {

    }
}