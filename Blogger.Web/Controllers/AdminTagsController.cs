using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag domain Model
            var tag = new Tags
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            await tagRepository.AddAsync(tag);
            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //dbContext to read the tags
            var alltags = await tagRepository.GetAllAsync();
            return View(alltags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)//Name has to match
        {
            //var tag = _bloggerDbContext.Tags.Find(id);
            var tag = await tagRepository.GetAsync(id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tags
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var updatedTag = await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {

            }
            else
            {

            }
                
            
            return RedirectToAction("Edit", new {id=editTagRequest.Id});
            
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if(deletedTag != null)
            {
                return RedirectToAction("List");
            }
            //Show a success
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
       
    }
}
