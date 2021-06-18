using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotiWish_back.Services;

namespace SpotiWish_test.Services
{
    [TestClass, TestCategory("Music")]
    public class MusicServiceTests
    {
        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-0)]
        [DataRow(-098765)]
        public async Task GetSingle_ThrowsArgumentOutOfRangeException_WhenIdIsLowerThan1(int musicId)
        {
            //Arrange
            var sut = new MusicService(new MusicRepositoryMock());

            //Act => Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await sut.GetSingleMusic(musicId));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(098765)]
        public async Task GetSingle_ReturnMusicWithId_WhenIdIsGreaterThan0(int musicId)
        {
            //Arrange
            var sut = new MusicService(new MusicRepositoryMock());

            //Act
            var result = await sut.GetSingleMusic(musicId);

            //Assert
            Assert.AreEqual(musicId, result.Id);
        }
    }
}