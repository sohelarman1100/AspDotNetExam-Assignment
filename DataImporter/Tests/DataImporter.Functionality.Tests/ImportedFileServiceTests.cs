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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Tests
{
    public class ImportedFileServiceTests
    {
        private AutoMock _mock;
        private Mock<IFunctionalityUnitOfWork> _functionalityUnitOfWork;
        private Mock<IImportedFileRepository> _importedFileRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IImportedFileService _importedFileService;

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
            _importedFileRepositoryMock = _mock.Mock<IImportedFileRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _importedFileService = _mock.Create<ImportedFileService>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _functionalityUnitOfWork.Reset();
            _importedFileRepositoryMock.Reset();
            _mapperMock.Reset();
        }

        [Test]
        public void UpdateStatus_FileIsNotNull_StatusUpdated()
        {
            //Arrange
            const int id = 5;
            var fileEntity = new ImportedFiles { Id = id, GroupName = "Courses", FileName = "file_1"};
            _importedFileRepositoryMock.Setup(x => x.GetById(id)).Returns(fileEntity).Verifiable();

            _functionalityUnitOfWork.Setup(x => x.ImFiles).Returns(_importedFileRepositoryMock.Object).Verifiable();
            _functionalityUnitOfWork.Setup(x => x.Save()).Verifiable();

            //Act
            _importedFileService.UpdateStatus(id);

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _functionalityUnitOfWork.VerifyAll(),
                () => _importedFileRepositoryMock.VerifyAll()
            );
        }

        [Test]
        public void UpdateStatus_FileIsNull_ThrowException()
        {
            //Arrange
            const int id = 5;
            ImportedFiles fileEntity = null;
            _importedFileRepositoryMock.Setup(x => x.GetById(id)).Returns(fileEntity).Verifiable();

            _functionalityUnitOfWork.Setup(x => x.ImFiles).Returns(_importedFileRepositoryMock.Object).Verifiable();


            //Act & Assert
            Should.Throw<InvalidOperationException>(
                () => _importedFileService.UpdateStatus(id)
            );
        }

        [Test]
        public void GetFile_GetFileId_ReturnFileId()
        {
            //Arrange
            const int grpId = 5;
            Guid usrId = Guid.Parse("610ca15c-d93a-4f49-ff08-08d981200880");
            string fileName = "file_1";
            ImportedFiles entityFile = new ImportedFiles { FileName = fileName, GroupId = grpId, UserId = usrId };

            var BOFile = new ImportedFileBO { FileName = fileName, GroupId = grpId, UserId = usrId };

            _importedFileRepositoryMock.Setup(x => x.GetFirstMatchingRecord(t => fileName == BOFile.FileName && usrId == BOFile.UserId
            && grpId == BOFile.GroupId)).Returns(entityFile).Verifiable();

            _functionalityUnitOfWork.Setup(x => x.ImFiles).Returns(_importedFileRepositoryMock.Object).Verifiable();

            //Act
            _importedFileService.GetFile(BOFile);

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _functionalityUnitOfWork.VerifyAll(),
                () => _importedFileRepositoryMock.VerifyAll()
            );
        }

        [Test]
        public void GetFileById_FileExist_ReturnEntity()
        {
            //Arrange
            const int grpId = 5;
            Guid usrId = Guid.Parse("610ca15c-d93a-4f49-ff08-08d981200880");
            string fileName = "file_1";
            var fileEntity = new ImportedFiles { FileName = fileName, GroupId = grpId, UserId = usrId };
            _importedFileRepositoryMock.Setup(x => x.GetById(grpId)).Returns(fileEntity).Verifiable();

            _functionalityUnitOfWork.Setup(x => x.ImFiles).Returns(_importedFileRepositoryMock.Object).Verifiable();


            //Act
            _importedFileService.GetFileById(grpId);

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _functionalityUnitOfWork.VerifyAll(),
                () => _importedFileRepositoryMock.VerifyAll()
            );
        }
    }
}
