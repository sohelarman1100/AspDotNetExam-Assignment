using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Areas.DataControlArea.Models;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Services;
using Moq;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace DataImporter.Tests
{
    [ExcludeFromCodeCoverage]
    public class EditGroupModelTests
    {
        private AutoMock _mock;
        private Mock<IMapper> _mapperMock;
        private Mock<IGroupService> _groupServiceMock;
        private EditGroupModel _model;

        [OneTimeSetUp]
        public void ClassSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void TestSetup()
        {
            _groupServiceMock = _mock.Mock<IGroupService>();
            _mapperMock = _mock.Mock<IMapper>();
            _model = _mock.Create<EditGroupModel>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _groupServiceMock?.Reset();
            _mapperMock?.Reset();
        }

        [Test]
        public void EditGroup_GroupExists_LoadProperties()
        {
            //Arrange
            const int id = 2;

            var group = new GroupBO
            {
                Id = 2,
                GroupName = "Courses"
            };

            _groupServiceMock.Setup(x => x.EditGroup(id)).Returns(group).Verifiable();

            _mapperMock.Setup(x => x.Map(
                group, It.IsAny<EditGroupModel>()
            )).Verifiable();


            //Act
            _model.EditGroup(id);


            //Assert
            _groupServiceMock.VerifyAll();
            _mapperMock.VerifyAll();
        }

        [Test]
        public void DeleteGroup_GroupDeletedOrNot_CallDeletMethod()
        {
            //Arrange
            const int id = 5;

            _groupServiceMock.Setup(x => x.DeleteGroup(id)).Verifiable();

            //Act
            _model.DeleteGroup(id);

            //Assert
            _groupServiceMock.VerifyAll();
        }
    }
}