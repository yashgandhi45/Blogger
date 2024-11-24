using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogger.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })

            };
            //get tags from Repository
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //Map viewModel to Domain Model before passing
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageURL = addBlogPostRequest.FeaturedImageURL,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,

            };

            //Map Tags from Selected Tags
            var selectedTags = new List<Tags>();
            foreach(var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var exisitngTag = await tagRepository.GetAsync(selectedTagIdAsGuid);
                if (exisitngTag != null)
                {
                    selectedTags.Add(exisitngTag);
                }
            }
            blogPost.Tags = selectedTags;
            await blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //Call the repository
            var blogPosts = await blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Retrieve the result from the repository
            var blogpost = await blogPostRepository.GetAsync(id);
            var tags = await tagRepository.GetAllAsync();
            //Map Domain Model into the View Model
            if (blogpost != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogpost.Id,
                    Heading = blogpost.Heading,
                    PageTitle = blogpost.PageTitle,
                    Content = blogpost.Content,
                    Author = blogpost.Author,
                    FeaturedImageURL = blogpost.FeaturedImageURL,
                    UrlHandle = blogpost.UrlHandle,
                    ShortDescription = blogpost.ShortDescription,
                    PublishedDate = blogpost.PublishedDate,
                    Visible = blogpost.Visible,
                    Tags = tags.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogpost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }
            //pass data to View
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit (EditBlogPostRequest editBlogPostRequest)
        {
            //mapview model to DOmain Model
            var blogpostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                FeaturedImageURL = editBlogPostRequest.FeaturedImageURL,
                UrlHandle = editBlogPostRequest.UrlHandle,
                ShortDescription = editBlogPostRequest.ShortDescription,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Visible = editBlogPostRequest.Visible,
            };
            //Map tags into domain Model
            var selectedTags = new List<Tags>();
            foreach(var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if(Guid.TryParse(selectedTag, out var tags))
                {
                    var foundTag = await tagRepository.GetAsync(tags);
                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                    
                }
            }
            blogpostDomainModel.Tags = selectedTags;
            //Submit infor to repo to update
            var updatedBlog = await blogPostRepository.UpdateAsync(blogpostDomainModel);
            if (updatedBlog != null)
            {
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit", new { id = updatedBlog.Id });
            //Redirect to Get Method
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            //Talk to Repo to delete blogposts and tags
            var deletedBlogPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
            if(deletedBlogPost!=null)
            {
                return RedirectToAction("List");
            }
            //Display the response
            return RedirectToAction("Edit", new {id=editBlogPostRequest.Id});
        }

    }
}
