using DepartmentManager.Domain.Entities;
using DepartmentManager.Application.Services;
using DepartmentManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DepartmentManager.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<IndexModel> _logger;

        public DetailsModel(IDepartmentService departmentService, ILogger<IndexModel> logger )
        {
            _departmentService = departmentService;
            _logger = logger;
        }
        [BindProperty]

        public Department Department { get; set; }
        public List<Department> SubDepartments { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Department = await _departmentService.GetDepartmentByIdAsync(id);

            SubDepartments = await _departmentService.GetSubDepartmentsAsync(id);

            if (SubDepartments == null)
            {
                return NotFound();
            }

           
            return Page();
        }
    }
}
