using DepartmentManager.Domain.Entities;
using DepartmentManager.Application.Services;
using DepartmentManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DepartmentManager.Web.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<IndexModel> _logger;

        public CreateModel(IDepartmentService departmentService, ILogger<IndexModel> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [BindProperty]
        public Department Department { get; set; }

        public SelectList ParentDepartments { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadParentDepartmentsAsync();
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadParentDepartmentsAsync();
                return Page();
            }

            await _departmentService.CreateDepartmentAsync(Department);
            return RedirectToPage("./Index");
        }

        private async Task LoadParentDepartmentsAsync()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            ParentDepartments = new SelectList(departments, "Id", "Name");
         
        }
    }
  
    
}
