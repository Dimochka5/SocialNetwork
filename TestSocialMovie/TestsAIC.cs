using SocialNetwork.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSociaNetwork
{
    public class TestsAIC
    {
        [Test]
        public void TestCreate()
        {
            AccountInChat aic=new AccountInChat() {IdAccount=3,IdChat=0};
            Assert.IsTrue(AccountInChat.Create(aic));
        }
        [Test]
        public void TestUpdate() { 
           AccountInChat aic=new AccountInChat() { Id=0,IdAccount=4,IdChat=0};
           Assert.IsTrue(AccountInChat.Update(aic));
        }
        [Test]
        public void TestRead()
        {
            Assert.IsNotNull(AccountInChat.Read(0));
        }
        [Test]
        public void TestDelete()
        {
            Assert.IsTrue(AccountInChat.Delete(0));
        }
    }
}
