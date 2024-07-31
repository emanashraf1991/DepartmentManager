using DepartmentManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentManager.Application.Interfaces;

namespace DepartmentManager.Infrastructure.Data
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments
                .Include(d => d.ParentDepartment)
                .Include(d => d.SubDepartments)
                .ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.ParentDepartment)
                .Include(d => d.SubDepartments)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            var existingDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.Id == department.Id);

            _context.Entry(existingDepartment).CurrentValues.SetValues(department);

            _context.Entry(existingDepartment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
