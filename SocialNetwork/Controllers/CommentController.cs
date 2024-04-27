using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Models.DatabaseModels;

namespace SocialNetwork.Controllers
{
    public class CommentController : Controller
    {
        public async Task<ActionResult> Comments(int id, int idPost)
        {
            if (id > 0 && idPost > 0)
            {
                return View(Comment.GetAll().Where(c => c.IdPost == idPost && c.IsDeleted == false).ToList());
            }
            else
            {
                return View("Error", "Bad request");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(int id,int idPost,string text)
        {
            if (id > 0 && idPost > 0&&!text.IsNullOrEmpty())
            {
                Comment comment=new Comment() {IdAccount=id,IdPost=idPost,Text=text,DateTime=DateTime.Now };
                if (Comment.Create(comment))
                {
                    return RedirectToAction("Post","Post", new { id=id,idPost=idPost});
                }
                else
                {
                    return View("Error","Couldn't create");
                }
            }
            else
            {
                return View("Error", "Bad request");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, int idPost,int idComment, string text)
        {
            if (id > 0 && idPost > 0 && !text.IsNullOrEmpty())
            {
                Comment comment = new Comment() { IdAccount = id, IdPost = idPost, Text = text, DateTime = DateTime.Now };
                if (Comment.Update(comment))
                {
                    return RedirectToAction("Post", "Post", new { id = id, idPost = idPost });
                }
                else
                {
                    return View("Error", "Couldn't create");
                }
            }
            else
            {
                return View("Error", "Bad request");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id,int idPost,int idComment)
        {
            if (id > 0 && idPost > 0 && idComment>0)
            {
                if (Comment.Delete(idComment))
                {
                    return RedirectToAction("Post", "Post", new { id = id, idPost = idPost });
                }
                else
                {
                    return View("Error", "Couldn't create");
                }
            }
            else
            {
                return View("Error", "Bad request");
            }
        }
    }
}
