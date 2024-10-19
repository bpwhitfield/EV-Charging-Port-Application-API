using EVApplicationAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EVApplicationAPI.Controllers;

[ApiController]
[Route("/applications")]
public class UserApplicationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<UserApplicationDto>> GetUserApplications()
    {
        return Ok(ApplicationsDataStore.Current.UserApplications);
    }

    [HttpGet("{id}", Name = "GetApplication")]
    public ActionResult<UserApplicationDto> GetApplication(int id)
    {
        var application = ApplicationsDataStore.Current.UserApplications.FirstOrDefault(c => c.ApplicationId == id);

        if (application == null)
        {
            return NotFound();
        }

        return Ok(application);
    }

    [HttpPost]
    public ActionResult<UserApplicationDto> CreateUserApplication(UserApplicationCreationDto userApplicationCreation)
    {
        var maxId = ApplicationsDataStore.Current.UserApplications.Max(p => p.ApplicationId);

        var userApplication = new UserApplicationDto()
        {
            ApplicationId = ++maxId,
            Name = userApplicationCreation.Name,
            EmailAddress = userApplicationCreation.EmailAddress
        };

        var applications = ApplicationsDataStore.Current.UserApplications;

        applications.Add(userApplication);

        return CreatedAtRoute("GetApplication",
            new
            {
                id = userApplication.ApplicationId
            },
            userApplication);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUserApplication(int id, UserApplicationUpdateDto userApplicationUpdate)
    {
        var userApplicationFromStore = ApplicationsDataStore.Current.UserApplications.FirstOrDefault(c => c.ApplicationId == id);
        if (userApplicationFromStore == null)
        {
            return NotFound();
        }

        userApplicationFromStore.Name = userApplicationUpdate.Name;
        userApplicationFromStore.EmailAddress = userApplicationUpdate.EmailAddress;

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateUserApplication(int id, JsonPatchDocument<UserApplicationUpdateDto> patchApplication)
    {
        var userApplicationFromStore = ApplicationsDataStore.Current.UserApplications.FirstOrDefault(c => c.ApplicationId == id);
        if (userApplicationFromStore == null)
        {
            return NotFound();
        }

        var applicationToPatch = new UserApplicationUpdateDto()
            {
                Name = userApplicationFromStore.Name,
                EmailAddress = userApplicationFromStore.EmailAddress
            };

        patchApplication.ApplyTo(applicationToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!TryValidateModel(applicationToPatch))
        {
            return BadRequest(ModelState);
        }

        userApplicationFromStore.Name = applicationToPatch.Name;
        userApplicationFromStore.EmailAddress = applicationToPatch.EmailAddress;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUserApplication(int id)
    {
        var userApplicationFromStore = ApplicationsDataStore.Current.UserApplications.FirstOrDefault(c => c.ApplicationId == id);
        if (userApplicationFromStore == null)
        {
            return NotFound();
        }

        var applications = ApplicationsDataStore.Current.UserApplications;

        applications.Remove(userApplicationFromStore);

        return NoContent();
    }
}