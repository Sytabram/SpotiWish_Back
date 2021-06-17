using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotiWish_back.Model;

namespace SpotiWish_test.Controllers
{
    [TestClass]
    public class UsersControllerTests: ApiControllerTestBase
    {
        [TestMethod, TestCategory("Ex1")]
        public async Task GetAllUsers()
        {
            // Act
            var response = await GetAsync<IList<User>>("/User");

            // Assert
            Assert.AreEqual(3, response.Count);
        }

        [TestMethod, TestCategory("Ex1")]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task UserGetSingleId(int id)
        {
            // Act
            var response = await GetAsync<User>($"/User/{id}");

            // Assert
            Assert.AreEqual(id, response.Id);
        }

        
    }
}