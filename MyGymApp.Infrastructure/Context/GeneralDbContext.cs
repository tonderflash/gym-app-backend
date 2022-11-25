using Microsoft.EntityFrameworkCore;

namespace MyGymApp.Infrastructure.Context;

public interface IGeneralDbContext : IDbContext
{

}
public class GeneralDbContext : BaseDbContext, IGeneralDbContext
{
    public GeneralDbContext(DbContextOptions<GeneralDbContext> options) 
        : base(options)
    {
    }

   
   
}