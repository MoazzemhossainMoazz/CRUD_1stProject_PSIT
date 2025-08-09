using FirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Data
{
    public class EmployeeData : DbContext
    { 
        public DbSet<EmployeeModels > Employees { get; set; }
        public EmployeeData(DbContextOptions<EmployeeData> op) :base(op) 
        {
            
        }
    }
}
