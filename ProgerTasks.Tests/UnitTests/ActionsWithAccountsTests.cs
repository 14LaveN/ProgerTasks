using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using ProgerTasks.DAL;
using ProgerTasks.DAL.Repositories.Account;
using ProgerTasks.DAL.Repositories.Tasks;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Enum;

namespace ProgerTasks.Tests.UnitTests;

public class ActionsWithAccountsTests
{
    private readonly AccountRepository _accountRepository;

    public ActionsWithAccountsTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            //var mockDbContext = fixture.Create<AppDbContext>();
            var appDbContext = new AppDbContext();
            _accountRepository = new AccountRepository(appDbContext);
        }

        [Fact]
        public async Task GetAllTasks_ReturnsAllTasks()
        {
            // Arrange

            // Act
            var actualTaskEntities = await _accountRepository.GetAllAccounts();

            // Assert
            actualTaskEntities.Should().NotBeNull();
        }

        [Fact]
        public async Task GetByTeamNameAndTitle_ReturnsTask()
        {
            // Arrange
            var accountTeamName = "Team 1";
            var accountAuthor = "Task 1";
            
            // Act
            var actualTaskEntity = await _accountRepository.GetByTeamNameAndAuthor(accountTeamName, accountAuthor);

            // Assert
            Assert.Equal(accountTeamName, actualTaskEntity.TeamName);
            Assert.Equal(accountAuthor, actualTaskEntity.Author);
        }

        [Fact]
        public async Task CreateTask_CreatesTask()
        {
            // Arrange
            var accountEntity = new AccountEntity() { Author = "Alice", Description = "Description 1", TeamName = "Team 1", CreationDate = DateTime.Now, TasksCount = 0, TeamRole = TeamRoles.Programmer, Role = Roles.User, Password = "fdjhgbdjfg"};

            // Act
            await _accountRepository.CreateAccount(accountEntity);
            var account =  await _accountRepository.GetByTeamNameAndAuthor(accountEntity.TeamName, accountEntity.Author);

            // Assert
            Assert.Equal(accountEntity.TeamName, account.TeamName);
            Assert.Equal(accountEntity.Author, account.Author);
        }

        [Fact]
        public async Task UpdateTask_UpdatesTask()
        {
            // Arrange
            var accountEntity = new AccountEntity() { Id = 7, Author = "Alice", Description = "Description 1", TeamName = "Team 1", CreationDate = DateTime.Now, TasksCount = 0, TeamRole = TeamRoles.Programmer, Role = Roles.User, Password = "fdjhgbdjfg"};

            // Act
            await _accountRepository.UpdateAccount(accountEntity.Id, accountEntity);

            // Assert
            // TODO: Assert that the account was updated in the database.
        }

        [Fact]
        public async Task DeleteTask_DeletesTask()
        {
            // Arrange
            var accountEntityId = 1;

            // Act
            await _accountRepository.DeleteAccount(accountEntityId);

            // Assert
            // TODO: Assert that the account was deleted from the database.
        }

        [Fact]
        public async Task GetById_ReturnsTask()
        {
            // Arrange
            int id = 5;

            // Act
            var actualTaskEntity = await _accountRepository.GetById(id);

            // Assert
            actualTaskEntity.Should().NotBeNull();
        }
}