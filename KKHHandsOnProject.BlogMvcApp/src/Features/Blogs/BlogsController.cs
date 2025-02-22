using Microsoft.AspNetCore.Mvc;

namespace KKHHandsOnProject.BlogMvcApp.src.Features.Blogs
{
    public class BlogsController : Controller
    {
        private readonly BlogService _blogService;

        public BlogsController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        public IActionResult GetBlogDataTable()
        {
            var _dt = _blogService.GetBlogDataTable(Request);
            return Json(new
            {
                draw            = _dt.Draw,
                recordsTotal    = _dt.RecordsTotal,
                recordsFiltered = _dt.RecordsFiltered,
                data            = _dt.Data,
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
