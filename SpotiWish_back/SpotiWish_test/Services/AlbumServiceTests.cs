using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotiWish_back.Services;

namespace SpotiWish_test.Services
{
    [TestClass, TestCategory("Album")]
    public class AlbumServiceTests
    {
        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-0)]
        [DataRow(-098765)]
        public async Task GetSingle_ThrowsArgumentOutOfRangeException_WhenIdIsLowerThan1(int albumId)
        {
            //Arrange
            var sut = new AlbumService(new AlbumRepositoryMock());

            //Act => Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await sut.GetSingleAlbum(albumId));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(098765)]
        public async Task GetSingle_ReturnAlbumtWithId_WhenIdIsGreaterThan0(int albumId)
        {
            //Arrange
            var sut = new AlbumService(new AlbumRepositoryMock());

            //Act
            var result = await sut.GetSingleAlbum(albumId);

            //Assert
            Assert.AreEqual(albumId, result.Id);
        }
    }
}