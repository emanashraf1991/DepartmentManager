using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentManager.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentDepartmentId { get; set; }
        public Department? ParentDepartment { get; set; }
        public List<Department> SubDepartments { get; set; } = new List<Department>();
    }
}
