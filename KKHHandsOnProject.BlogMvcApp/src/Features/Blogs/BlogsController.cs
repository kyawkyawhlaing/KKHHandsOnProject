using Microsoft.AspNetCore.Mvc;

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
            _logger.LogInformation("Custom Logs: Get Data Table at {NOW}", DateTime.Now);
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
    }
}
