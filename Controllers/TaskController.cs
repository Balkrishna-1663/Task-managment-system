using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_managment_system.Data;
using Task_managment_system.Models;
using Task_managment_system.Models.DTO;

namespace Task_managment_system.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDb appDb;

        public TaskController(AppDb appDb)
        {
            this.appDb = appDb;
        }
        [Route("/Controller/Count")]
        public IActionResult Index()
        {
            var totalTasks = appDb.Tasks.Count();
            var completedTasks = appDb.Tasks.Count(t => t.Status == true);

            // Avoid division by zero
            double progress = totalTasks > 0 ? (completedTasks / (double)totalTasks) * 100 : 0;

            ViewBag.Progress = progress;

            return View(appDb.Tasks.Include(t => t.Category).ToList());
        }

        [Route("Task/Index/searchQuery")]
        public IActionResult Index(string searchQuery)
        {
            var tasks = appDb.Tasks.Include(t => t.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchQuery) || t.Description.Contains(searchQuery));
            }

            return View(tasks.ToList());
        }
        [Route("/Task/Index/Sort")]
        public IActionResult Index(string statusFilter, string sortOption, int? categoryId)
        {
            var tasks = appDb.Tasks.Include(t => t.Category).AsQueryable();

            // Filter by status (Completed / Incomplete)
            if (!string.IsNullOrEmpty(statusFilter))
            {
                bool isCompleted = statusFilter == "Completed";
                tasks = tasks.Where(t => t.Status == isCompleted);
            }

            // Filter by Category
            if (categoryId.HasValue)
            {
                tasks = tasks.Where(t => t.CategoryId == categoryId);
            }

            // Sorting Logic
            switch (sortOption)
            {
                case "DueDate":
                    tasks = tasks.OrderBy(t => t.DueDate);
                    break;
                case "Priority":
                    tasks = tasks.OrderByDescending(t => t.Priority);
                    break;
                case "CreatedAt":
                    tasks = tasks.OrderBy(t => t.CreatedAt);
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.Title);
                    break;
            }

            var categories = appDb.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");

            return View(tasks.ToList());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Addtask task)
        {
            if (ModelState.IsValid)
            {
                var NewTask = new Atask()
                {
                    Title = task.Title,
                    CategoryId = task.CategoryId,
                    Description = task.Description,
                    CreatedAt = DateTime.Now,
                    Status = task.Status,
                    DueDate = task.DueDate,
                    Priority = task.Priority
                };
                appDb.Tasks.Add(NewTask);
                await appDb.SaveChangesAsync();
                return RedirectToAction("Index", "Task");
            }

            return View(task);
        }

      
        [HttpGet]    
        public IActionResult Edit()
        {            
            return View();
        }       
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateTask task, int id)
        {
            var editdata= await appDb.Tasks.FindAsync(id);

            if (ModelState.IsValid)
            {
                if (editdata != null)
                {
                    editdata.Title = task.Title;
                    editdata.Description=task.Description;
                    editdata.Status = task.Status;
                    editdata.Priority = task.Priority;
                    editdata.DueDate = task.DueDate;
                   
                    appDb.Update(editdata);
                    appDb.SaveChanges();
                    return RedirectToAction("Index");
                }
                
                return View(editdata);

            }

             return View(editdata);
        }


        public async Task<IActionResult> Delete(int id)
        {

         var newdata= await appDb.Tasks.FindAsync(id);
            appDb.Tasks.Remove(newdata);
            appDb.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Detail(int id)
        {
            var user = appDb.Tasks.Find(id);
            return View(user);
        }


    }
}
