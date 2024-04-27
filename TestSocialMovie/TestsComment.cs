using SocialNetwork.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSociaNetwork
{
    public class TestsComment
    {
        [Test] 
        public void TestCreate() {
            Comment comment = new Comment() { Text="hahaha",DateTime=DateTime.Now,IdAccount=2,IdPost=1};
            Assert.DoesNotThrow(() => Comment.Create(comment));
        }
        [Test]
        public void TestGetAll()
        {

        }

        [Test]
        public void TestDelete()
        {
            Assert.IsTrue(Comment.Delete(0));
        }
    }
}
