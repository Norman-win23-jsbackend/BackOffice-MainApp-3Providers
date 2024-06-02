using Microsoft.EntityFrameworkCore;
using Norman.Common.Services.Models;

namespace Data.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
    	public DbSet<Files> Files { get; set; }
    }
}
