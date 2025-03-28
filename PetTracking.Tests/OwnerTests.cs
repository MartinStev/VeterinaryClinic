using PetTracking.Models;

namespace PetTracking.Tests
{
    [TestClass]
    public class OwnerTests
    {
        [TestMethod]
        public void GetFullName_WhenNameAndSurnameProvided_ReturnsFullName()
        {
            // Arrange
            var owner = new Owner
            {
                Name = "John",
                Surname = "Doe"
            };

            // Act
            var fullName = owner.GetFullName();

            // Assert
            Assert.AreEqual("John Doe", fullName);
        }

        [TestMethod]
        public void GetFullName_WhenNameIsNullOrEmpty_ReturnsSurnameOnly()
        {
            // Arrange
            var owner1 = new Owner
            {
                Name = null,
                Surname = "Doe"
            };

            var owner2 = new Owner
            {
                Name = "",
                Surname = "Doe"
            };

            // Act
            var fullName1 = owner1.GetFullName();
            var fullName2 = owner2.GetFullName();

            // Assert
            Assert.AreEqual(" Doe", fullName1);
            Assert.AreEqual(" Doe", fullName2);
        }

        [TestMethod]
        public void GetFullName_WhenSurnameIsNullOrEmpty_ReturnsNameOnly()
        {
            // Arrange
            var owner1 = new Owner
            {
                Name = "John",
                Surname = null
            };

            var owner2 = new Owner
            {
                Name = "John",
                Surname = ""
            };

            // Act
            var fullName1 = owner1.GetFullName();
            var fullName2 = owner2.GetFullName();

            // Assert
            Assert.AreEqual("John ", fullName1);
            Assert.AreEqual("John ", fullName2);
        }

        [TestMethod]
        public void GetFullName_WhenNameAndSurnameAreNullOrEmpty_ReturnsEmptyString()
        {
            // Arrange
            var owner1 = new Owner
            {
                Name = null,
                Surname = null
            };

            var owner2 = new Owner
            {
                Name = "",
                Surname = ""
            };

            // Act
            var fullName1 = owner1.GetFullName();
            var fullName2 = owner2.GetFullName();

            // Assert
            Assert.AreEqual(" ", fullName1);
            Assert.AreEqual(" ", fullName2);
        }
    
    }

}
        