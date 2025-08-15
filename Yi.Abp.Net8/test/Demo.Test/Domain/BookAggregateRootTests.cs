using Demo.Domain.Entities;
using Demo.Domain.Shared.Enums;
using Shouldly;
using Xunit;

namespace Demo.Test.Domain
{
    /// <summary>
    /// 图书聚合根测试类
    /// </summary>
    public class BookAggregateRootTests
    {
        /// <summary>
        /// 测试创建图书
        /// </summary>
        [Fact]
        public void Should_Create_Book_With_Valid_Parameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "测试图书";
            var type = BookTypeEnum.Adventure;
            var publishDate = DateTime.Now;
            var price = 29.99f;

            // Act
            var book = new BookAggregateRoot(id, name, type, publishDate, price);

            // Assert
            book.Id.ShouldBe(id);
            book.Name.ShouldBe(name);
            book.Type.ShouldBe(type);
            book.PublishDate.ShouldBe(publishDate);
            book.Price.ShouldBe(price);
        }

        /// <summary>
        /// 测试更新图书名称
        /// </summary>
        [Fact]
        public void Should_Update_Book_Name()
        {
            // Arrange
            var book = new BookAggregateRoot(Guid.NewGuid(), "原始名称", BookTypeEnum.Adventure);
            var newName = "新名称";

            // Act
            book.UpdateName(newName);

            // Assert
            book.Name.ShouldBe(newName);
        }

        /// <summary>
        /// 测试更新图书名称为空时应抛出异常
        /// </summary>
        [Fact]
        public void Should_Throw_Exception_When_Update_Book_Name_With_Empty_String()
        {
            // Arrange
            var book = new BookAggregateRoot(Guid.NewGuid(), "原始名称", BookTypeEnum.Adventure);

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() => book.UpdateName(string.Empty));
            exception.ParamName.ShouldBe("name");
        }

        /// <summary>
        /// 测试更新图书类型
        /// </summary>
        [Fact]
        public void Should_Update_Book_Type()
        {
            // Arrange
            var book = new BookAggregateRoot(Guid.NewGuid(), "测试图书", BookTypeEnum.Adventure);
            var newType = BookTypeEnum.ScienceFiction;

            // Act
            book.UpdateType(newType);

            // Assert
            book.Type.ShouldBe(newType);
        }

        /// <summary>
        /// 测试更新图书出版日期
        /// </summary>
        [Fact]
        public void Should_Update_Book_PublishDate()
        {
            // Arrange
            var book = new BookAggregateRoot(Guid.NewGuid(), "测试图书", BookTypeEnum.Adventure);
            var newDate = new DateTime(2023, 1, 1);

            // Act
            book.UpdatePublishDate(newDate);

            // Assert
            book.PublishDate.ShouldBe(newDate);
        }

        /// <summary>
        /// 测试更新图书价格
        /// </summary>
        [Fact]
        public void Should_Update_Book_Price()
        {
            // Arrange
            var book = new BookAggregateRoot(Guid.NewGuid(), "测试图书", BookTypeEnum.Adventure);
            var newPrice = 39.99f;

            // Act
            book.UpdatePrice(newPrice);

            // Assert
            book.Price.ShouldBe(newPrice);
        }

        /// <summary>
        /// 测试更新图书价格为负数时应抛出异常
        /// </summary>
        [Fact]
        public void Should_Throw_Exception_When_Update_Book_Price_With_Negative_Value()
        {
            // Arrange
            var book = new BookAggregateRoot(Guid.NewGuid(), "测试图书", BookTypeEnum.Adventure);
            var negativePrice = -10.0f;

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() => book.UpdatePrice(negativePrice));
            exception.ParamName.ShouldBe("price");
        }
    }
}