using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using TodoApp.Application.Services;
using Xunit;

namespace TodoApp.Tests
{
    public class TodoServiceTests
    {
        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenTitleIsEmpty()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            var service = new TodoService(mockRepo.Object);
            var item = new TodoItem { Title = "", IsCompleted = false };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(item));
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepository_WhenValidItem()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            var service = new TodoService(mockRepo.Object);
            var item = new TodoItem { Title = "Test", IsCompleted = false };

            mockRepo.Setup(repo => repo.AddAsync(item)).ReturnsAsync(item);

            // Act
            var result = await service.AddAsync(item);

            // Assert
            Assert.Equal("Test", result.Title);
            mockRepo.Verify(repo => repo.AddAsync(item), Times.Once);
        }
    }
}
