using KKHHandsOnProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KKHHandsOnProject.BlogMvcApp.Features.Blogs
{
    public class BlogsController : Controller
    {
        private readonly ILogger<BlogsController> _logger;
        private readonly BlogService _blogService;

        public BlogsController(BlogService blogService, ILogger<BlogsController> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult GetBlogDataTable()
        {
            LogInformation log = new LogInformation();
            log.Message = $"Retrieve Data Table List at {DateTime.Now.ToString("yyyy-MMM-dd HH:mm")}";
            LogData logData = new LogData();
            logData.Status = "R";
            log.Data = logData;
            _logger.LogInformation("{@Object}", JsonConvert.SerializeObject(log));
            var dt = _blogService.GetBlogDataTable(Request);
            return Json(new
            {
                draw            = dt.Draw,
                recordsTotal    = dt.RecordsTotal,
                recordsFiltered = dt.RecordsFiltered,
                data            = dt.Data,
            });
        }

        [ActionName("Index")]
        public IActionResult BlogList()
        {
            ViewBag.Heading1 = "Blog List";
            ViewBag.Heading2 = "Blog Table";
            return View("BlogList");
        }

        [HttpGet]
        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult BlogCreate(BlogRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return View("BlogCreate", requestModel);
            }
            var item = _blogService.CreateBlog(requestModel);
            TempData["IsInitial"] = false;
            TempData["IsSuccess"] = item.IsSuccess;
            TempData["Message"] = item.Message;

            return RedirectToAction("Index");
        }

        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            return View("BlogEdit", _blogService.GetBlog(id));
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogRequestModel requestModel)
        {

            var item = _blogService.UpdateBlog(id, requestModel);
            TempData["IsInitial"] = false;
            TempData["IsSuccess"] = item.IsSuccess;
            TempData["Message"] = item.Message;

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public IActionResult BlogDelete(int id)
        {

            var item = _blogService.DeleteBlog(id);
            TempData["IsInitial"] = false;
            TempData["IsSuccess"] = item.IsSuccess;
            TempData["Message"] = item.Message;
            return RedirectToAction("Index");
        }

        [HttpGet("/TestActionFilter")]
        [TypeFilter(typeof(BlogsActionFilter),Arguments = new object[]
        {
            "x-custom-header", "Custom Value"
        })]
        public IActionResult TestActionFilter(string? fname, string? lname)
        {
            return Json(new { firstName = fname, lastName = lname});
           
        }
    }


}
