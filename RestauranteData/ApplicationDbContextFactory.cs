using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RestauranteData;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseOracle("User Id=rm99134;Password=020405;Data Source=oracle.fiap.com.br:1521/ORCL");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
