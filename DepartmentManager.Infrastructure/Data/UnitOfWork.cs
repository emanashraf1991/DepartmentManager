using DepartmentManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentManager.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDepartmentRepository _departmentRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IDepartmentRepository Departments =>
            _departmentRepository ??= new DepartmentRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}