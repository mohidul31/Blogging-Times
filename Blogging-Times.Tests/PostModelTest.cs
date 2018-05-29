using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blogging_Times.Data;
using System.Collections.Generic;
using Blogging_Times.Posts;
using Moq;
using System.Linq;
using System.Data.Entity;

namespace Blogging_Times.Tests
{
    [TestClass]
    public class PostModelTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var data = new List<Post>
            {
                new Post{
                    PostDescription="aaa",
                    PostTitle ="Testing",
                    CreatedAt =DateTime.ParseExact("2009-05-08", "yyyy-MM-dd",
															System.Globalization.CultureInfo.InvariantCulture)
                },
                new Post{
                    PostTitle ="Testing",
                    CreatedAt =DateTime.ParseExact("2009-06-08", "yyyy-MM-dd",
                                                            System.Globalization.CultureInfo.InvariantCulture)
                },

                new Post{
                    PostTitle ="Testing",
                    CreatedAt =DateTime.ParseExact("2009-06-08 00:00:01", "yyyy-MM-dd HH:mm:ss",
                                                            System.Globalization.CultureInfo.InvariantCulture)
                },

                new Post{
                    PostTitle ="Testing",
                    CreatedAt =DateTime.ParseExact("2009-06-30 23:59:59", "yyyy-MM-dd HH:mm:ss",
                                                            System.Globalization.CultureInfo.InvariantCulture)
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Post>>();
            mockSet.As<IQueryable<Post>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Post>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Post>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Post>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PostDbContext>();
            mockContext.Setup(c => c.Post).Returns(mockSet.Object);

            //Act
            var service = new PostModel(mockContext.Object); 

            var results = service.GetPostCountByMonthAndYear(0,2009);
            var results1 = service.GetPostCountByMonthAndYear(6,2009);
            var results2 = service.GetPostCountByMonthAndYear(5,2009);
            var results3 = service.GetPostCountByMonthAndYear(6, 2008);

            //Assert 
            Assert.AreEqual(4, results);
            Assert.AreEqual(3, results1);
            Assert.AreEqual(1, results2);
            Assert.AreEqual(0, results3);

        }
    }
}
