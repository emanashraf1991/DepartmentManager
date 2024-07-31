using DepartmentManager.Domain.Entities;
using DepartmentManager.Application.Services;
using DepartmentManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DepartmentManager.Web.Pages
{
    public class EditModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IDepartmentService _departmentService;

        public EditModel(IDepartmentService departmentService, ILogger<IndexModel> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Department = await _departmentService.GetDepartmentByIdAsync(id);
            LoadParentDepartmentsAsync();

            if (Department == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadParentDepartmentsAsync();

                return Page();
            }

            await _departmentService.UpdateDepartmentAsync(Department);
            return RedirectToPage("./Index");
        }
    
   

        public SelectList ParentDepartments { get; set; }

          
        private async Task LoadParentDepartmentsAsync()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            ParentDepartments = new SelectList(departments.Where(d => d.Id != Department.Id), "Id", "Name");
        }
    }
}
