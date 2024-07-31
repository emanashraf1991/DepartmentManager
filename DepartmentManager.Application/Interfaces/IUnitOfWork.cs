using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentManager.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Departments { get; }
        Task<int> SaveChangesAsync();
    }
}
