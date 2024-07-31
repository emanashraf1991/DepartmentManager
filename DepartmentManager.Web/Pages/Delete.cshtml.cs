using DepartmentManager.Domain.Entities;
using DepartmentManager.Application.Services;
using DepartmentManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepartmentManager.Web.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IDepartmentService departmentService;
        private readonly ILogger<IndexModel> _logger;

        public DeleteModel(IDepartmentService _departmentService, ILogger<IndexModel> logger)
        {
            departmentService = _departmentService;
            _logger = logger;
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Department = await departmentService.GetDepartmentByIdAsync(id);

            if (Department == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await departmentService.DeleteDepartmentAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
