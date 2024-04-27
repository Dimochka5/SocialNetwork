using SocialNetwork.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSociaNetwork
{
    public class TestsViews
    {
        private Views views;

        [Test]
        public void TestGetAll()
        {
            Assert.IsEmpty(Views.GetAll().ToList());
        }
        [Test]
        public void TestCreate()
        {
            Views views2 = new Views() { IdAccount=1,IdPost=1};
            Assert.IsTrue(Views.Create(views2));
        }
    }
}
