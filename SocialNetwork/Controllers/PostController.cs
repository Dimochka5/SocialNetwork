using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SocialNetwork.Models.DatabaseModels;
using System.Runtime.Intrinsics.X86;

namespace SocialNetwork.Controllers
{
    public class PostController : Controller
    {
        //TODO:з'ясувати чого з токеном дає 400
        public async Task<IActionResult> Post(int id, int idPost)
        {
            if (id>0&&idPost>0)
            {
                var post = SocialNetwork.Models.DatabaseModels.Post.Read(idPost);
                if (post != null&&post.IsDeleted==false)
                {
                    SocialNetwork.Models.DatabaseModels.Views view=new SocialNetwork.Models.DatabaseModels.Views() {IdAccount=id,IdPost=idPost};
                    SocialNetwork.Models.DatabaseModels.Views.Create(view);
                    return View(SocialNetwork.Models.DatabaseModels.Post.Read(idPost));
                }
                else
                {
                    return View("Error", "Post isn't exist");
                }
            }
            else
            {
                return View("Error", "Bad request");
            }
        }

        public async Task<IActionResult> Posts(int id){
            if (id>0)
            {
                return View(SocialNetwork.Models.DatabaseModels.Post.GetAll().Where(p => p.IsDeleted == false).ToList());
            }
            else
            {
                return View("Error", "Bad request");
            }
        }

        public async Task<IActionResult> EditPost(int id, int idPost)
        {
            if (id > 0 && idPost > 0)
            {
                var post = SocialNetwork.Models.DatabaseModels.Post.Read(idPost);
                if (post != null && post.IsDeleted == false)
                {
                    return View(post);
                }
                else
                {
                    return View("Error", "Post isn't exist");
                }
            }
            else
            {
                return View("Error", "Bad request");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(int id, int idPost,string NewText,IFormFile? NewImage)
        {

            if (id > 0 && idPost > 0)
            {
                var post = SocialNetwork.Models.DatabaseModels.Post.Read(idPost);
                if (post != null)
                {
                    if (NewText != null)
                    {
                        post.Text = NewText;
                    }
                    if (SocialNetwork.Models.DatabaseModels.Post.Update(post))
                    {
                        if (NewImage != null)
                        {
                            PostImage newImage = new PostImage() { IdPost = post.Id, IsDeleted = false };
                            using (var memoryStream = new MemoryStream())
                            {
                                NewImage.CopyToAsync(memoryStream);
                                newImage.Image = memoryStream.ToArray();
                            }
                            if (!PostImage.Create(newImage))
                            {
                                return View("Error", "Image couldn't load");
                            }
                            else
                            {
                                return RedirectToAction("Posts", new { id = id });
                            }
                        }
                        else
                        {
                            return RedirectToAction("Posts", new { id = id });
                        }
                    }
                    else
                    {
                        return View("Error", "Couldn't update");
                    }
                }
                else
                {
                    return View("Error", "Couldn't update");
                }
            }
            else
            {
                return View("Error", "Bad request");
            }
        }

        public async Task<IActionResult> CreatePost(int id)
        {
            if (id > 0)
            {
                Exception ex;
                Account account = Account.Read(id);
                if (account != null)
                {
                    return View();
                }
                else
                {
                    return View("Error", "User isn't exist");
                }
            }
            else
            {
                return View("Error", "User isn't exist");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(int id,string text, IFormFile? Image) {
            if (id > 0)
            {
                SocialNetwork.Models.DatabaseModels.Post post = new SocialNetwork.Models.DatabaseModels.Post() {Text=text,IdAccount=id,DateTime=DateTime.Now };
                if (SocialNetwork.Models.DatabaseModels.Post.Create(post))
                {
                    if (Image != null)
                    {
                        PostImage newImage = new PostImage() { IdPost = Models.DatabaseModels.Post.GetAll().Count()-1, IsDeleted = false };
                        using (var memoryStream = new MemoryStream())
                        {
                            Image.CopyToAsync(memoryStream);
                            newImage.Image = memoryStream.ToArray();
                        }
                        if (!PostImage.Create(newImage))
                        {
                            return View("Error", "Image couldn't load");
                        }
                        else
                        {
                            return RedirectToAction("Posts", new { id = id });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Posts", new { id = id });
                    }
                }
                else
                {
                    return View("Error", "Post couldn't create");
                }
            }
            else
            {
                return View("Error", "Post couldn't create");
            }
        }

        public async Task<IActionResult> DeletePost(int id,int idPost)
        {
            if (id > 0 && idPost > 0)
            {
                SocialNetwork.Models.DatabaseModels.Post post = SocialNetwork.Models.DatabaseModels.Post.Read(idPost);
                if(post != null)
                {
                    if (SocialNetwork.Models.DatabaseModels.Post.Delete(idPost))
                    {
                        return RedirectToAction("Posts", new { id = id });
                    }
                    else
                    {
                        return View("Error","Couldn't delete post");
                    }
                }
                else
                {
                    return View("Error", "Post isn't exist");
                }
            }
            else
            {
                return View("Error", "Bad request");
            }
        }

        


    }
}
