using Demo.Application.EventHandlers;
using Demo.Domain.Entities;
using Demo.Domain.Events;
using Demo.Domain.Shared.Enums;
using Moq;
using System.Linq.Expressions;
using Volo.Abp.Uow;
using Xunit;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Demo.Test.EventHandlers
{
    /// <summary>
    /// 图书创建事件处理程序测试类
    /// </summary>
    public class BookCreatedEventHandlerTests
    {
        private readonly Mock<ISqlSugarRepository<LibraryAggregateRoot, Guid>> _mockLibraryRepository;
        private readonly BookCreatedEventHandler _eventHandler;

        public BookCreatedEventHandlerTests()
        {
            _mockLibraryRepository = new Mock<ISqlSugarRepository<LibraryAggregateRoot, Guid>>();
            _eventHandler = new BookCreatedEventHandler(_mockLibraryRepository.Object);
        }

        /// <summary>
        /// 测试处理图书创建事件 - 当图书馆不存在时应创建新的图书馆
        /// </summary>
        [Fact]
        public async Task Should_Create_New_Library_When_Not_Exists()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var bookName = "测试图书";
            var bookType = BookTypeEnum.Adventure;
            var creationTime = DateTime.Now;
            
            var eventData = new BookCreatedEvent(bookId, bookName, bookType, creationTime);
            
            // 模拟图书馆不存在
            _mockLibraryRepository.Setup(r => r.GetListAsync(It.Is<Expression<Func<LibraryAggregateRoot, bool>>>(
                expr => true))) // 使用It.Is代替It.IsAny来避免可选参数问题
                .ReturnsAsync(new List<LibraryAggregateRoot>());

            // Act
            await _eventHandler.HandleEventAsync(eventData);

            // Assert
            _mockLibraryRepository.Verify(r => r.InsertAsync(
               It.Is<LibraryAggregateRoot>(l =>
                   l.BookId == bookId &&
                   l.Name == $"{bookName}默认图书馆" &&
                   l.Location == "默认位置" &&
                   l.Stock == 1
               )));
        }

        /// <summary>
        /// 测试处理图书创建事件 - 当图书馆已存在时应增加库存
        /// </summary>
        [Fact]
        public async Task Should_Increase_Stock_When_Library_Exists()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var bookName = "测试图书";
            var bookType = BookTypeEnum.Adventure;
            var creationTime = DateTime.Now;
            
            var eventData = new BookCreatedEvent(bookId, bookName, bookType, creationTime);
            
            // 模拟图书馆已存在
            var existingLibrary = new LibraryAggregateRoot(
                Guid.NewGuid(),
                bookId,
                "现有图书馆",
                "现有位置",
                5
            );
            
            _mockLibraryRepository.Setup(r => r.GetListAsync(It.Is<Expression<Func<LibraryAggregateRoot, bool>>>(
                expr => true))) // 使用It.Is代替It.IsAny来避免可选参数问题
                .ReturnsAsync(new List<LibraryAggregateRoot> { existingLibrary });

            // Act
            await _eventHandler.HandleEventAsync(eventData);

            // Assert
            _mockLibraryRepository.Verify(r => r.UpdateAsync(
                It.Is<LibraryAggregateRoot>(l => 
                    l.Id == existingLibrary.Id && 
                    l.Stock == 6
                )), 
                Times.Once);
        }

        /// <summary>
        /// 测试处理空事件数据
        /// </summary>
        [Fact]
        public async Task Should_Do_Nothing_When_EventData_Is_Null()
        {
            // Arrange
            BookCreatedEvent eventData = null;

            // Act
            await _eventHandler.HandleEventAsync(eventData);

            // Assert
            _mockLibraryRepository.Verify(r => r.GetListAsync(It.IsAny<Expression<Func<LibraryAggregateRoot, bool>>>()), Times.Never);
            _mockLibraryRepository.Verify(r => r.InsertAsync(It.IsAny<LibraryAggregateRoot>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockLibraryRepository.Verify(r => r.UpdateAsync(It.IsAny<LibraryAggregateRoot>()), Times.Never);
        }

        /// <summary>
        /// 测试处理图书创建事件 - 当发生异常时应正常处理
        /// </summary>
        [Fact]
        public async Task Should_Handle_Exception_Gracefully()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var bookName = "测试图书";
            var bookType = BookTypeEnum.Adventure;
            var creationTime = DateTime.Now;
            
            var eventData = new BookCreatedEvent(bookId, bookName, bookType, creationTime);
            
            // 模拟抛出异常
            _mockLibraryRepository.Setup(r => r.GetListAsync(It.Is<Expression<Func<LibraryAggregateRoot, bool>>>(
                expr => true))) // 使用It.Is代替It.IsAny来避免可选参数问题
                .ThrowsAsync(new Exception("测试异常"));

            // Act & Assert
            // 不应抛出异常
            await _eventHandler.HandleEventAsync(eventData);
        }
    }
}