using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SocialNetwork.Models.DatabaseModels;

namespace TestSociaNetwork
{
    public class TestsPost
    {
        private Post post;

        [Test]
        public void TestGetAll()
        {
            Assert.IsEmpty(Post.GetAll().ToList());
        }

        [Test]
        public void TestCreate()
        {
            Post newpost = new Post() { Text = "test222", DateTime = DateTime.Now, IdAccount = 1 };
            Assert.DoesNotThrow(() => Post.Create(newpost));
        }
        [Test]
        public void TestUpdate()
        {
            Post newpost = new Post() {Id=1, Text = "updateText", DateTime = DateTime.Now, IdAccount = 1 };
            Assert.IsTrue(Post.Update(newpost));
        }
        [Test]
        public void TestRead()
        {
            Assert.IsNotNull(Post.Read(1));
        }
        [Test]
        public void TestDelete()
        {
            Assert.IsTrue(Post.Delete(0));
        }
    }
}
