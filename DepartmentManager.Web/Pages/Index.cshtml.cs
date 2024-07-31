using DepartmentManager.Domain.Entities;
using DepartmentManager.Application.Services;
using DepartmentManager.Application.Interfaces;
using DepartmentManager.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepartmentManager.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDepartmentService _departmentService;

        public IndexModel(IDepartmentService departmentService, ILogger<IndexModel> logger )
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        public IList<Department> Departments { get; set; }

        public async Task OnGetAsync()
        {
            Departments = (await _departmentService.GetAllDepartmentsAsync()).ToList();
        }
    
}
}

