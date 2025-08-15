using Demo.Domain.Entities;
using Shouldly;
using Xunit;

namespace Demo.Test.Domain
{
    /// <summary>
    /// 图书馆聚合根测试类
    /// </summary>
    public class LibraryAggregateRootTests
    {
        /// <summary>
        /// 测试创建图书馆
        /// </summary>
        [Fact]
        public void Should_Create_Library_With_Valid_Parameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var name = "测试图书馆";
            var location = "测试位置";
            var stock = 10;

            // Act
            var library = new LibraryAggregateRoot(id, bookId, name, location, stock);

            // Assert
            library.Id.ShouldBe(id);
            library.BookId.ShouldBe(bookId);
            library.Name.ShouldBe(name);
            library.Location.ShouldBe(location);
            library.Stock.ShouldBe(stock);
        }

        /// <summary>
        /// 测试更新图书馆关联的图书ID
        /// </summary>
        [Fact]
        public void Should_Update_Library_BookId()
        {
            // Arrange
            var library = new LibraryAggregateRoot(Guid.NewGuid(), Guid.NewGuid(), "测试图书馆", "测试位置", 10);
            var newBookId = Guid.NewGuid();

            // Act
            library.UpdateBookId(newBookId);

            // Assert
            library.BookId.ShouldBe(newBookId);
        }

        /// <summary>
        /// 测试更新图书馆名称
        /// </summary>
        [Fact]
        public void Should_Update_Library_Name()
        {
            // Arrange
            var library = new LibraryAggregateRoot(Guid.NewGuid(), Guid.NewGuid(), "原始名称", "测试位置", 10);
            var newName = "新名称";

            // Act
            library.UpdateName(newName);

            // Assert
            library.Name.ShouldBe(newName);
        }

        /// <summary>
        /// 测试更新图书馆名称为空时应抛出异常
        /// </summary>
        [Fact]
        public void Should_Throw_Exception_When_Update_Library_Name_With_Empty_String()
        {
            // Arrange
            var library = new LibraryAggregateRoot(Guid.NewGuid(), Guid.NewGuid(), "测试图书馆", "测试位置", 10);

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() => library.UpdateName(string.Empty));
            exception.ParamName.ShouldBe("name");
        }

        /// <summary>
        /// 测试更新图书馆位置
        /// </summary>
        [Fact]
        public void Should_Update_Library_Location()
        {
            // Arrange
            var library = new LibraryAggregateRoot(Guid.NewGuid(), Guid.NewGuid(), "测试图书馆", "原始位置", 10);
            var newLocation = "新位置";

            // Act
            library.UpdateLocation(newLocation);

            // Assert
            library.Location.ShouldBe(newLocation);
        }

        /// <summary>
        /// 测试更新图书馆位置为空时应抛出异常
        /// </summary>
        [Fact]
        public void Should_Throw_Exception_When_Update_Library_Location_With_Empty_String()
        {
            // Arrange
            var library = new LibraryAggregateRoot(Guid.NewGuid(), Guid.NewGuid(), "测试图书馆", "测试位置", 10);

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() => library.UpdateLocation(string.Empty));
            exception.ParamName.ShouldBe("location");
        }

        /// <summary>
        /// 测试更新图书馆库存
        /// </summary>
        [Fact]
        public void Should_Update_Library_Stock()
        {
            // Arrange
            var library = new LibraryAggregateRoot(Guid.NewGuid(), Guid.NewGuid(), "测试图书馆", "测试位置", 10);
            var newStock = 20;

            // Act
            library.UpdateStock(newStock);

            // Assert
            library.Stock.ShouldBe(newStock);
        }

        /// <summary>
        /// 测试更新图书馆库存为负数时应抛出异常
        /// </summary>
        [Fact]
        public void Should_Throw_Exception_When_Update_Library_Stock_With_Negative_Value()
        {
            // Arrange
            var library = new LibraryAggregateRoot(Guid.NewGuid(), Guid.NewGuid(), "测试图书馆", "测试位置", 10);
            var negativeStock = -5;

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() => library.UpdateStock(negativeStock));
            exception.ParamName.ShouldBe("stock");
        }
    }
}