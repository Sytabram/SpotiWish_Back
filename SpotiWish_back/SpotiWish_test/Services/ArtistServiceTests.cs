using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotiWish_back.Services;

namespace SpotiWish_test.Services
{
    [TestClass, TestCategory("Artist")]
    public class ArtistServiceTests
    {
        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-0)]
        [DataRow(-098765)]
        public async Task GetSingle_ThrowsArgumentOutOfRangeException_WhenIdIsLowerThan1(int artistId)
        {
            //Arrange
            var sut = new ArtistService(new ArtistRepositoryMock());

            //Act => Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await sut.GetSingleArtist(artistId));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(098765)]
        public async Task GetSingle_ReturnArtistWithId_WhenIdIsGreaterThan0(int artistId)
        {
            //Arrange
            var sut = new ArtistService(new ArtistRepositoryMock());

            //Act
            var result = await sut.GetSingleArtist(artistId);

            //Assert
            Assert.AreEqual(artistId, result.Id);
        }
    }
}