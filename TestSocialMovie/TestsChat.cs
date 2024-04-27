using SocialNetwork.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSociaNetwork
{
    public class TestsChat
    {
        [Test]
        public void TestCreate()
        {
            Chat chat = new Chat() { Name = "Chat1" };
            int id;
            Assert.IsTrue(Chat.Create(chat,out id));
        }
        [Test]
        public void TestUpdate()
        {
            Chat chat = new Chat() { Name = "Chat2", Id = 0 };
            Assert.IsTrue(Chat.Update(chat));
        }
        [Test]
        public void TestRead() {
            Assert.IsNull(Chat.Read(0));
        }
        [Test]
        public void TestDelete()
        {
            Assert.IsTrue(Chat.Delete(0));
        }
    }
}
