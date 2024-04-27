using SocialNetwork.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSociaNetwork
{
    public class TestsMessage
    {
        [Test]
        public void TestCreate()
        {
            Message message= new Message() { IdAccount=4,IdChat=0,Text="Hello",Time=DateTime.Now};
            Assert.IsTrue(Message.Create(message));
        }
        [Test]
        public void TestUpdate()
        {
            Message message = new Message() { Text="It's the first message", Id = 2 };
            Assert.IsTrue(Message.Update(message));
        }
        [Test]
        public void TestRead()
        {
            Assert.IsNull(Message.Read(0));
        }
        [Test]
        public void TestDelete()
        {
            Assert.IsTrue(Message.Delete(0));
        }
    }
}
