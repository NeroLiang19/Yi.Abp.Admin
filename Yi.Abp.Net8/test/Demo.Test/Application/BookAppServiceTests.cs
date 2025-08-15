using Demo.Application.Contracts.Dtos.Book;
using Demo.Application.Contracts.IServices;
using Demo.Application.Services;
using Demo.Domain.Entities;
using Demo.Domain.Events;
using Demo.Domain.Shared.Enums;
using Moq;
using Shouldly;
using SqlSugar;
using System.Collections.Generic;
using System.Linq.Expressions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using Xunit;
using Yi.Framework.SqlSugarCore.Abstractions;

// 不再需要派生类，直接使用 BookAppService

namespace Demo.Test.Application
{
    /// <summary>
    /// 图书应用服务测试类
    /// </summary>
    public class BookAppServiceTests
    {
        private readonly Mock<ISqlSugarRepository<BookAggregateRoot, Guid>> _mockRepository;
        private readonly Mock<ILocalEventBus> _mockEventBus;
        private readonly Mock<IObjectMapper> _mockObjectMapper;
        private readonly Mock<IAbpLazyServiceProvider> _mockLazyServiceProvider;
        private readonly BookAppService _bookAppService;

        public BookAppServiceTests()
        {
            _mockRepository = new Mock<ISqlSugarRepository<BookAggregateRoot, Guid>>();
            _mockEventBus = new Mock<ILocalEventBus>();
            _mockObjectMapper = new Mock<IObjectMapper>();
            _mockLazyServiceProvider = new Mock<IAbpLazyServiceProvider>();

            // 创建被测服务实例
            _bookAppService = new BookAppService(
                _mockRepository.Object,
                _mockEventBus.Object)
            {
                // 设置LazyServiceProvider
                LazyServiceProvider = GetMockLazyServiceProvider()
            };

            // 配置对象映射行为
            SetupObjectMapper();
        }

        private IAbpLazyServiceProvider GetMockLazyServiceProvider()
        {
            // 配置LazyServiceProvider返回模拟对象
            _mockLazyServiceProvider
                .Setup(x => x.LazyGetService<IObjectMapper>())
                .Returns(_mockObjectMapper.Object);

            _mockLazyServiceProvider
                .Setup(x => x.LazyGetService<IGuidGenerator>())
                .Returns(SimpleGuidGenerator.Instance);

            // 添加其他必要服务的模拟
            _mockLazyServiceProvider
                .Setup(x => x.LazyGetRequiredService<IUnitOfWorkManager>())
                .Returns(Mock.Of<IUnitOfWorkManager>());

            return _mockLazyServiceProvider.Object;
        }

        private void SetupObjectMapper()
        {
            // 配置实体-DTO映射
            _mockObjectMapper
             .Setup(m => m.Map<BookCreateUpdateDto, BookAggregateRoot>(It.IsAny<BookCreateUpdateDto>()))
             .Returns((BookCreateUpdateDto dto) => new BookAggregateRoot());

            // 配置BookAggregateRoot -> BookDto映射
            _mockObjectMapper
                .Setup(m => m.Map<BookAggregateRoot, BookDto>(It.IsAny<BookAggregateRoot>()))
                .Returns((BookAggregateRoot entity) => new BookDto { Id = entity.Id });

            // 配置列表映射
            _mockObjectMapper
                .Setup(m => m.Map<List<BookAggregateRoot>, List<BookDto>>(It.IsAny<List<BookAggregateRoot>>()))
                .Returns((List<BookAggregateRoot> entities) =>
                    entities.ConvertAll(e => new BookDto { Id = e.Id }));
        }


        /// <summary>
        /// 测试获取图书列表
        /// </summary>
        [Fact]
        public async Task Should_Get_Book_List()
        {
            // Arrange
            var books = new List<BookAggregateRoot>
            {
                new BookAggregateRoot(Guid.NewGuid(), "测试图书1", BookTypeEnum.Adventure, DateTime.Now, 29.99f),
                new BookAggregateRoot(Guid.NewGuid(), "测试图书2", BookTypeEnum.ScienceFiction, DateTime.Now.AddDays(-1), 39.99f)
            };

            // 模拟查询构建器
            var mockQueryable = new Mock<ISugarQueryable<BookAggregateRoot>>();
            mockQueryable.Setup(q => q.WhereIF(It.IsAny<bool>(), It.IsAny<Expression<Func<BookAggregateRoot, bool>>>()))
                .Returns(mockQueryable.Object);
            mockQueryable.Setup(q => q.OrderBy(It.IsAny<Expression<Func<BookAggregateRoot, object>>>(), It.IsAny<OrderByType>()))
                .Returns(mockQueryable.Object);
            mockQueryable.Setup(q => q.ToListAsync())
                .ReturnsAsync(books);

            _mockRepository.Setup(r => r._DbQueryable).Returns(mockQueryable.Object);

            // 配置对象映射 - 为列表项配置映射
            _mockObjectMapper.Setup(m => m.Map<List<BookAggregateRoot>, List<BookDto>>(It.IsAny<List<BookAggregateRoot>>()))
                .Returns((List<BookAggregateRoot> sourceList) => 
                {
                    var result = new List<BookDto>();
                    foreach (var source in sourceList)
                    {
                        result.Add(new BookDto
                        {
                            Id = source.Id,
                            Name = source.Name,
                            Type = source.Type,
                            PublishDate = source.PublishDate,
                            Price = source.Price
                        });
                    }
                    return result;
                });
                
            // 配置单个实体映射
            _mockObjectMapper.Setup(m => m.Map<BookAggregateRoot,BookDto>(It.IsAny<BookAggregateRoot>()))
                .Returns((BookAggregateRoot source) => new BookDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    Type = source.Type,
                    PublishDate = source.PublishDate,
                    Price = source.Price
                });

            // Act
            var result = await _bookAppService.GetListAsync(new BookGetListInputVo());

            // Assert
            result.ShouldNotBeNull();
            result.TotalCount.ShouldBe(0); // 因为我们没有模拟RefAsync<int>的赋值
            result.Items.Count.ShouldBe(2);
            result.Items[0].Name.ShouldBe("测试图书1");
            result.Items[1].Name.ShouldBe("测试图书2");
        }

        /// <summary>
        /// 测试获取图书分页列表
        /// </summary>
        [Fact]
        public async Task Should_Get_Book_Page()
        {
            // Arrange
            var books = new List<BookAggregateRoot>
            {
                new BookAggregateRoot(Guid.NewGuid(), "测试图书1", BookTypeEnum.Adventure, DateTime.Now, 29.99f),
                new BookAggregateRoot(Guid.NewGuid(), "测试图书2", BookTypeEnum.ScienceFiction, DateTime.Now.AddDays(-1), 39.99f)
            };

            // 模拟查询构建器
            var mockQueryable = new Mock<ISugarQueryable<BookAggregateRoot>>();
            mockQueryable.Setup(q => q.WhereIF(It.IsAny<bool>(), It.IsAny<Expression<Func<BookAggregateRoot, bool>>>()))
                .Returns(mockQueryable.Object);
            mockQueryable.Setup(q => q.OrderBy(It.IsAny<Expression<Func<BookAggregateRoot, object>>>(), It.IsAny<OrderByType>()))
                .Returns(mockQueryable.Object);
            // 修复可选参数问题
            mockQueryable.Setup(q => q.ToPageListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<RefAsync<int>>()))
                .ReturnsAsync(books);

            _mockRepository.Setup(r => r._DbQueryable).Returns(mockQueryable.Object);

            // 配置对象映射 - 为列表项配置映射
            _mockObjectMapper.Setup(m => m.Map<List<BookAggregateRoot>, List<BookDto>>(It.IsAny<List<BookAggregateRoot>>()))
                .Returns((List<BookAggregateRoot> sourceList) => 
                {
                    var result = new List<BookDto>();
                    foreach (var source in sourceList)
                    {
                        result.Add(new BookDto
                        {
                            Id = source.Id,
                            Name = source.Name,
                            Type = source.Type,
                            PublishDate = source.PublishDate,
                            Price = source.Price
                        });
                    }
                    return result;
                });
                
            // 配置单个实体映射
            _mockObjectMapper.Setup(m => m.Map<BookAggregateRoot, BookDto>(It.IsAny<BookAggregateRoot>()))
                .Returns((BookAggregateRoot source) => new BookDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    Type = source.Type,
                    PublishDate = source.PublishDate,
                    Price = source.Price
                });

            // Act
            var result = await _bookAppService.GetPageAsync(new BookGetListInputVo { SkipCount = 0, MaxResultCount = 10 });

            // Assert
            result.ShouldNotBeNull();
            result.TotalCount.ShouldBe(0); // 因为我们没有模拟RefAsync<int>的赋值
            result.Items.Count.ShouldBe(2);
            result.Items[0].Name.ShouldBe("测试图书1");
            result.Items[1].Name.ShouldBe("测试图书2");
        }

        /// <summary>
        /// 测试创建图书
        /// </summary>
        [Fact]
        public async Task Should_Create_Book()
        {
            // Arrange
            var input = new BookCreateUpdateDto
            {
                Name = "新图书",
                Type = BookTypeEnum.Adventure,
                PublishDate = DateTime.Now,
                Price = 29.99f
            };

            //var createdBook = new BookAggregateRoot(Guid.NewGuid(), input.Name, input.Type, input.PublishDate, input.Price);

            //// 模拟 InsertAsync 方法（基类使用的方法）
            //_mockRepository.Setup(r => r.InsertAsync(It.IsAny<BookAggregateRoot>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(createdBook);
                
            //// 配置对象映射
            //_mockObjectMapper.Setup(m => m.Map<BookAggregateRoot,BookDto>(It.IsAny<BookAggregateRoot>()))
            //    .Returns((BookAggregateRoot source) => new BookDto
            //    {
            //        Id = source.Id,
            //        Name = source.Name,
            //        Type = source.Type,
            //        PublishDate = source.PublishDate,
            //        Price = source.Price
            //    });

            // Act
            var result = await _bookAppService.CreateAsync(input);

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe(input.Name);
            result.Type.ShouldBe(input.Type);
            result.PublishDate.ShouldBe(input.PublishDate);
            result.Price.ShouldBe(input.Price);

            // 验证事件发布
            _mockEventBus.Verify(e => e.PublishAsync(It.IsAny<BookCreatedEvent>(), It.IsAny<bool>()), Times.Once);
        }

        /// <summary>
        /// 测试更新图书
        /// </summary>
        [Fact]
        public async Task Should_Update_Book()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var existingBook = new BookAggregateRoot(bookId, "原始名称", BookTypeEnum.Adventure, DateTime.Now, 29.99f);
            var input = new BookCreateUpdateDto
            {
                Name = "更新后的名称",
                Type = BookTypeEnum.ScienceFiction,
                PublishDate = DateTime.Now.AddDays(1),
                Price = 39.99f
            };

            _mockRepository.Setup(r => r.GetAsync(It.Is<Guid>(id => id == bookId), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingBook);

            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<BookAggregateRoot>()))
                .ReturnsAsync(true);
                
            // 配置更新后的对象映射
            var updatedBook = new BookAggregateRoot(bookId, input.Name, input.Type, input.PublishDate, input.Price);
            _mockObjectMapper.Setup(m => m.Map(It.IsAny<BookCreateUpdateDto>(), It.IsAny<BookAggregateRoot>()))
                .Callback<object, object>((source, destination) => {
                    var dto = source as BookCreateUpdateDto;
                    var entity = destination as BookAggregateRoot;
                    entity.Name = dto.Name;
                    entity.Type = dto.Type;
                    entity.PublishDate = dto.PublishDate;
                    entity.Price = dto.Price;
                });
                
            _mockObjectMapper.Setup(m => m.Map<BookAggregateRoot,BookDto>(It.IsAny<BookAggregateRoot>()))
                .Returns((BookAggregateRoot source) => new BookDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    Type = source.Type,
                    PublishDate = source.PublishDate,
                    Price = source.Price
                });

            // Act
            var result = await _bookAppService.UpdateAsync(bookId, input);

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe(input.Name);
            result.Type.ShouldBe(input.Type);
            result.PublishDate.ShouldBe(input.PublishDate);
            result.Price.ShouldBe(input.Price);

            // 验证事件发布
            _mockEventBus.Verify(e => e.PublishAsync(It.IsAny<BookUpdatedEvent>(), It.IsAny<bool>()), Times.Once);
        }

        /// <summary>
        /// 测试删除图书
        /// </summary>
        [Fact]
        public async Task Should_Delete_Book()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            _mockRepository.Setup(r => r.DeleteAsync(It.Is<Guid>(id => id == bookId), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _bookAppService.DeleteAsync(bookId);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(It.Is<Guid>(id => id == bookId), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            
            // 验证事件发布 - 修复参数匹配问题
            _mockEventBus.Verify(e => e.PublishAsync(It.IsAny<BookDeletedEvent>(), It.IsAny<bool>()), Times.Once);
        }

        /// <summary>
        /// 测试更改图书状态
        /// </summary>
        [Fact]
        public async Task Should_Change_Book_Status()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var existingBook = new BookAggregateRoot(bookId, "测试图书", BookTypeEnum.Adventure);
            var newStatus = BookTypeEnum.ScienceFiction;

            _mockRepository.Setup(r => r.GetAsync(It.Is<Guid>(id => id == bookId), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingBook);

            _mockRepository.Setup(r => r.UpdateAsync(It.Is<BookAggregateRoot>(b => b.Id == bookId)))
                .ReturnsAsync(true);

            // 获取实际的BookAppService实例，因为IBookAppService接口中没有定义ChangeBookStatusAsync方法
            var bookAppService = (BookAppService)_bookAppService;

            // Act
            await bookAppService.ChangeBookStatusAsync(bookId, newStatus);

            // Assert
            existingBook.Type.ShouldBe(newStatus);
            
            // 验证事件发布 - 修复参数匹配问题
            _mockEventBus.Verify(e => e.PublishAsync(It.IsAny<BookStatusChangedEvent>(), It.IsAny<bool>()), Times.Once);
        }
    }
}