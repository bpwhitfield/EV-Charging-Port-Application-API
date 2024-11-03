using AutoMapper;
using Castle.Core.Logging;
using EVApplicationAPI.Controllers;
using EVApplicationAPI.Entities;
using EVApplicationAPI.Models;
using EVApplicationAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace EVApplicationsAPI.Controllers;

[TestClass]
public class UserApplicationsControllerTests
{
    private Mock<IApplicationInfoRepository> _applicationInfoRepositoryMock;
    private Mock<ILogger<UserApplicationsController>> _loggerMock;
    private Mock<IMapper> _autoMapperMock;
    private UserApplicationsController _userApplicationsController;

    [TestInitialize]
    public void Setup()
    {
        _applicationInfoRepositoryMock = new Mock<IApplicationInfoRepository>();
        _loggerMock = new Mock<ILogger<UserApplicationsController>>();
        _autoMapperMock = new Mock<IMapper>();
        _userApplicationsController = new UserApplicationsController(_loggerMock.Object,
            _applicationInfoRepositoryMock.Object, _autoMapperMock.Object);
    }

    [TestMethod]
    public async Task GetApplicationTestSuccessful()
    {
        var applicationId = '1';
        var applicationData = new Application("Ben", "test@tester.com", "", "", "", "") {
            ApplicationId = 1,
            Name = "Ben",
            EmailAddress = "test@tester.com",
            AddressLine1 = "",
            AddressLine2 = "Brixton",
            City = "",
            Postcode = "",
            County = "Greater London",
            Vrn = ""
        };
        var mappedData = new UserApplicationDto
        {
            ApplicationId = 1,
            Name = "Ben",
            EmailAddress = "test@tester.com",
            AddressLine1 = "",
            AddressLine2 = "Brixton",
            City = "",
            Postcode = "",
            County = "Greater London",
            Vrn = ""
        };
        _applicationInfoRepositoryMock.Setup(repository => repository.GetApplicationAsync(applicationId).Result).Returns(applicationData);
        _autoMapperMock.Setup(m => m.Map<UserApplicationDto>(applicationData)).Returns(mappedData);

        var result = await _userApplicationsController.GetApplication(applicationId);

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(mappedData, okResult.Value);
    }

    [TestMethod]
    public async Task GetApplicationTestNull()
    {
        var applicationId = '1';
        _applicationInfoRepositoryMock.Setup(repository => repository.GetApplicationAsync(applicationId).Result).Returns((Application)null);

        var result = await _userApplicationsController.GetApplication(applicationId);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task GetApplicationTestException()
    {
        var applicationId = '1';
        _applicationInfoRepositoryMock.Setup(repository => repository.GetApplicationAsync(applicationId).Result).Throws(new Exception());

        var exception = Assert.ThrowsExceptionAsync<SystemException>(async () => await _userApplicationsController.GetApplication(applicationId));
    }

    [TestMethod]
    public async Task DeleteUserApplicationNotFound()
    {
        var applicationId = '1';
        _applicationInfoRepositoryMock.Setup(repository => repository.GetApplicationAsync(applicationId).Result).Returns((Application)null);

        var result = await _userApplicationsController.DeleteUserApplication(applicationId);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteUserApplicationNoContent()
    {
        var applicationId = '1';
        var applicationData = new Application("Ben", "test@tester.com", "", "", "", "") {
            ApplicationId = 1,
            Name = "Ben",
            EmailAddress = "test@tester.com",
            AddressLine1 = "",
            AddressLine2 = "Brixton",
            City = "",
            Postcode = "",
            County = "Greater London",
            Vrn = ""
        };
        _applicationInfoRepositoryMock.Setup(repository => repository.GetApplicationAsync(applicationId).Result).Returns(applicationData);

        var result = await _userApplicationsController.DeleteUserApplication(applicationId);

        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }
}