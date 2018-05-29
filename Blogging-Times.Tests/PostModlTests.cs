using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blogging_Times.Posts;

namespace Blogging_Times.Tests
{
   
    [TestClass]
    public class PostModlTests
    {
        
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var postModel = new PostModel();

            //Act
            postModel.GetPostCountByMonthAndYear() ;
            //Assert
            PostDbContext db = new PostDbContext();
            Assert.AreEqual(0, 7);
        }
    }
}
