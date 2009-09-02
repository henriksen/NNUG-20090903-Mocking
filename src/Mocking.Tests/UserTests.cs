using NUnit.Framework;
using Moq;

namespace Mocking.Tests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void DeleteUser_Id_DeletesPosts()
        {
            int userID = 1;
            var userRepositoryMock = new Mock<IUserRepository>();

            var userService = new UserService(userRepositoryMock.Object);
            userService.DeleteUser(userID);

            userRepositoryMock.Verify(repository => repository.DeletePostsForUser(userID));
        }
    }

    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeletePostsForUser(id);
            _userRepository.DeleteUser(id);
        }
    }

    public interface IUserRepository
    {
        bool DeletePostsForUser(int id);
        bool DeleteUser(int id);
    }
}