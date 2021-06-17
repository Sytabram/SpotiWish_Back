using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotiWish_back.Services;

namespace SpotiWish_test.Services
{
    [TestClass, TestCategory("Users")]
    public class UsersServiceTests
    {
        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-0)]
        [DataRow(-098765)]
        public async Task GetSingle_ThrowsArgumentOutOfRangeException_WhenIdIsLowerThan1(int userId)
        {
            //Arrange
            var sut = new UsersService(new UsersRepositoryMock());

            //Act => Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await sut.GetSingleUser(userId));
        }
        
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(098765)]
        public async Task GetSingle_ReturnUserWithId_WhenIdIsGreaterThan0(int userId)
        {
            //Arrange
            var sut = new UsersService(new UsersRepositoryMock());

            //Act
            var result = await sut.GetSingleUser(userId);

            //Assert
            Assert.AreEqual(userId, result.Id);
        }
        
    }
}