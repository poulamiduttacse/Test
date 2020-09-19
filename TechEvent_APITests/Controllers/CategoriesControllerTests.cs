using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechEvent_API.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Repository;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Linq;
using Repository.Repositories;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TechEvent_API.Controllers.Tests
{
    [TestClass]
    public class CategoriesControllerTests
    {
        [TestMethod()]
        public async Task GetCategoryAsync_Valid_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.GetCategoryAsync()).ReturnsAsync(new List<Category> { new Category { Id = 1 , CategoryName  = "test" } });
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.GetCategoryAsync();

            //Assert
            var okResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        } 
        
        [TestMethod()]
        public async Task GetCategoryByIdAsync_Valid_With_Id_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.FindAsync(It.IsAny<int>())).ReturnsAsync(new  Category { Id = 1 , CategoryName  = "test"  });
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.GetCategoryByIdAsync(1);

            //Assert
            var okResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        } 

        [TestMethod()]
        public async Task GetCategoryByIdAsync_Invlid_Without_Id_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.GetCategoryAsync()).ReturnsAsync(new List<Category> { new Category { Id = 1 , CategoryName  = "test" } });
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.GetCategoryByIdAsync(0);

            //Assert
            mockTecheventContext.Verify(s => s.GetCategoryAsync(),Times.Never);
            var okResut = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, okResut.StatusCode);
        }

        [TestMethod()]
        public async Task PutCategory_Valid_With_Id_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.UpdateCategoryAsync(It.IsAny<int>(), It.IsAny<Category>()));
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.PutCategory(1, new Category() { Id = 1});

            //Assert
            var okResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod()]
        public async Task PutCategory_Invlid_Without_Id_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.UpdateCategoryAsync(It.IsAny<int>(), It.IsAny<Category>())).Throws(new ApplicationException());
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.PutCategory(1, new Category());

            //Assert
            var okResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, okResult.StatusCode);
        }

        [TestMethod()]
        public async Task PutCategory_Invlid_With_Exception_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.UpdateCategoryAsync(It.IsAny<int>(), It.IsAny<Category>())).Throws(new ApplicationException());
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.PutCategory(1, new Category() { Id = 1});

            //Assert
            var okResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [TestMethod()]
        public async Task PutCategory_Invlid_With_Wrong_Id_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.UpdateCategoryAsync(It.IsAny<int>(), It.IsAny<Category>())).Throws(new ApplicationException());
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.PutCategory(1, new Category() { Id = 2});

            //Assert
            var okResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, okResult.StatusCode);
        }

        [TestMethod()]
        public async Task PostCategoryAsync_Valid_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.InsertCategoryAsync(It.IsAny<Category>()));
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.PostCategoryAsync(new Category() { CategoryName = "name" });

            //Assert
            var okResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [TestMethod()]
        public async Task PostCategoryAsync_Invalid_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.InsertCategoryAsync(It.IsAny<Category>()));
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.PostCategoryAsync(null);

            //Assert
            var okResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, okResult.StatusCode);
        }

        [TestMethod()]
        public async Task DeleteCategoryAsync_Valid_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.DeleteCategoryAsync(It.IsAny<int>()));
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.DeleteCategoryAsync(1);

            //Assert
            var okResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod()]
        public async Task DeleteCategoryAsync_Invalid_Exception_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.DeleteCategoryAsync(It.IsAny<int>())).Throws(new Exception());
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.DeleteCategoryAsync(1);

            //Assert
            var okResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status404NotFound, okResult.StatusCode);
        }


        [TestMethod()]
        public async Task DeleteCategoryAsync_Invalid_Id_Test()
        {
            //Arrange
            var mockTecheventContext = new Mock<ICategoriesRepositories>();
            mockTecheventContext.Setup(s => s.DeleteCategoryAsync(It.IsAny<int>()));
            var controller = new CategoriesController(mockTecheventContext.Object);

            //Act
            var result = await controller.DeleteCategoryAsync(0);

            //Assert
            var okResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, okResult.StatusCode);
        }

    }
}