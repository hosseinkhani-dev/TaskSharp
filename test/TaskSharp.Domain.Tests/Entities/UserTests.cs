using TaskSharp.Domain.Entities;
using TaskSharp.Domain.Exceptions;

namespace TaskSharp.Domain.Tests.Entities
{
    public class UserTests
    {
        [Fact]
        public void Create_Should_CreateUser_WhenParametersAreValid()
        {
            // Arrange
            var username = "testuser";
            var passwordHash = "password123";
            var email = "test@email";

            // Act
            var user = new User(username, passwordHash, email);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(username, user.Username);
            Assert.Equal(passwordHash, user.PasswordHash);
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void Create_Should_ThrowError_WhenUsernameIsEmpty()
        {
            // Arrange
            var username = string.Empty;
            var passwordHash = "password123";
            var email = "test@email";

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new User(username, passwordHash, email));
        }

        [Fact]
        public void Create_Should_ThrowError_WhenPasswordHashIsEmpty()
        {
            // Arrange
            var username = "testuser";
            var passwordHash = string.Empty;
            var email = "test@email";

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new User(username, passwordHash, email));
        }

        [Fact]
        public void Create_Should_ThrowError_WhenEmailIsEmpty()
        {
            // Arrange
            var username = "testuser";
            var passwordHash = "password123";
            var email = string.Empty;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new User(username, passwordHash, email));
        }

        [Fact]
        public void Create_Should_ThrowError_WhenEmailIsNotValid()
        {
            // Arrange
            var username = "testuser";
            var passwordHash = "password123";
            var email = "test email";

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new User(username, passwordHash, email));
        }
    }
}
