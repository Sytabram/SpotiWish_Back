using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotiWish_back.Services;

namespace SpotiWish_test.Services
{
    [TestClass, TestCategory("Playlist")]
    public class PlaylistServiceTests
    {

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-0)]
        [DataRow(-098765)]
        public async Task GetSingle_ThrowsArgumentOutOfRangeException_WhenIdIsLowerThan1(int playlistId)
        {
            //Arrange
            var sut = new PlayListService(new PlaylistRepositoryMock());

            //Act => Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await sut.GetSinglePlayList(playlistId));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(098765)]
        public async Task GetSingle_ReturnPlaylistWithId_WhenIdIsGreaterThan0(int playlistId)
        {
            //Arrange
            var sut = new PlayListService(new PlaylistRepositoryMock());

            //Act
            var result = await sut.GetSinglePlayList(playlistId);

            //Assert
            Assert.AreEqual(playlistId, result.Id);
        }
        
    }
}