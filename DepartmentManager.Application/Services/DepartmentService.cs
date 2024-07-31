using DepartmentManager.Application.Interfaces;
using DepartmentManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentManager.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _unitOfWork.Departments.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _unitOfWork.Departments.GetAllAsync();
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.SaveChangesAsync();
            return department;
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            await _unitOfWork.Departments.UpdateAsync(department);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _unitOfWork.Departments.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<List<Department>> GetSubDepartmentsAsync(int parentId)
        {
            var departments = await _unitOfWork.Departments.GetAllAsync(); // Await the asynchronous method
            var subDepartments = departments
                .Where(x => x.ParentDepartmentId == parentId) // Filter the results
                .ToList();
            if (subDepartments == null)
            {
                throw new KeyNotFoundException($"Parent department with ID {parentId} not found.");
            }

            return subDepartments;
        }

        
    }
}
