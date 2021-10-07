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

        

    }
}