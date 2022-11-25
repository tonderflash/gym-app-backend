using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyGymApp.Domain.Common.BaseEntity;

namespace MyGymApp.Infrastructure.Context;

public static class EntityFrameworkModelBuilderExtensions
{
    public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
    {
        SetSoftDeleteFilterMethod.MakeGenericMethod(entityType).Invoke(null, new object[] { modelBuilder });
    }

    static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(EntityFrameworkModelBuilderExtensions)
        .GetMethods(BindingFlags.Public | BindingFlags.Static)
        .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

    public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder) where TEntity : class, IBase
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.Deleted);
    }
}