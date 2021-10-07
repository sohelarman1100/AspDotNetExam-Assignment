using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Entities;
using DataImporter.Functionality.Exceptions;
using DataImporter.Functionality.Repositories;
using DataImporter.Functionality.Services;
using DataImporter.Functionality.UnitOfWorks;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DataImporter.Functionality.Tests
{
    
    public class GroupServiceTests
    {
        private AutoMock _mock;
        private Mock<IFunctionalityUnitOfWork> _functionalityUnitOfWork;
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IGroupService _groupService;

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
            _functionalityUnitOfWork = _mock.Mock<IFunctionalityUnitOfWork>();
            _groupRepositoryMock = _mock.Mock<IGroupRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _groupService = _mock.Create<GroupService>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _functionalityUnitOfWork.Reset();
            _groupRepositoryMock.Reset();
            _mapperMock.Reset();
        }

        [Test]
        public void CreateGroup_GroupIsNull_ThrowsException()
        {
            //Arrange
            GroupBO group = null;

            //Act & Assert
            Should.Throw<InvalidParameterException>(
                () => _groupService.CreateGroup(group)
            );
        }

        [Test]
        public void CreateGroup_GroupIsNotNull_LoadProperty()
        {
            //Arrange
            GroupBO group = new GroupBO { Id =5, GroupName = "Courses", UserId = Guid.Parse("610ca15c-d93a-4f49-ff08-08d981200880") };
            var entityGroup = new Group();

            _mapperMock.Setup(x => x.Map<Group>(group)).Returns(entityGroup).Verifiable();
            _groupRepositoryMock.Setup(x => x.Add(entityGroup)).Verifiable();

            _functionalityUnitOfWork.Setup(x => x.Groups).Returns(_groupRepositoryMock.Object);
            _functionalityUnitOfWork.Setup(x => x.Save()).Verifiable();

            //Act
            _groupService.CreateGroup(group);

            //Assert
            //_mapperMock.VerifyAll();
            this.ShouldSatisfyAllConditions(
                () => _functionalityUnitOfWork.VerifyAll(),
                () => _groupRepositoryMock.VerifyAll(),
                () => _mapperMock.VerifyAll()
            );
        }

        [Test]
        public void GetGroupById_GroupExist_ReturnEntity()
        {
            //Arrange
            const int id = 5;
            var groupEntity = new Group { Id = id, GroupName = "Courses", UserId = Guid.Parse("610ca15c-d93a-4f49-ff08-08d981200880") };
            _groupRepositoryMock.Setup(x => x.GetById(id)).Returns(groupEntity).Verifiable();

            _functionalityUnitOfWork.Setup(x => x.Groups).Returns(_groupRepositoryMock.Object).Verifiable();


            //Act
            _groupService.GetGroupById(id);

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _functionalityUnitOfWork.VerifyAll(),
                () => _groupRepositoryMock.VerifyAll()
            );
        }

    }
}