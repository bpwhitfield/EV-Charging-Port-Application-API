using AutoMapper;
using EVApplicationAPI.Models;
using EVApplicationAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EVApplicationAPI.Controllers;

[ApiController]
[Route("/applications")]
public class UserApplicationsController : ControllerBase
{
    private readonly ILogger<UserApplicationsController> _logger;
    private readonly IApplicationInfoRepository _applicationInfoRepository;
    private readonly IMapper _mapper;

    public UserApplicationsController(ILogger<UserApplicationsController> logger, IApplicationInfoRepository applicationInfoRepository, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentException(nameof(logger));
        _applicationInfoRepository = applicationInfoRepository ?? throw new ArgumentException(nameof(applicationInfoRepository));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserApplicationDto>>> GetUserApplications()
    {
        var applicationEntities = await _applicationInfoRepository.GetApplicationsAsync();
        return Ok(_mapper.Map<IEnumerable<UserApplicationDto>>(applicationEntities));
    }

    [HttpGet("{id}", Name = "GetApplication")]
    public async Task<IActionResult> GetApplication(int id)
    {
        try
        {
            var application = await _applicationInfoRepository.GetApplicationAsync(id);

            if (application == null)
            {
                _logger.LogInformation("Application with id {id} wasn't found", id);
                return NotFound();
            }

            return Ok(_mapper.Map<UserApplicationDto>(application));
        }
        catch (Exception ex)
        {
            _logger.LogCritical(
                $"Exception while getting application with id {id}", ex
            );
            return StatusCode(500, "A problem happened when handling your request");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserApplicationDto>> CreateUserApplication(UserApplicationCreationDto userApplicationCreation)
    {
        
        var userApplication = _mapper.Map<Entities.Application>(userApplicationCreation);

        _applicationInfoRepository.AddApplication(userApplication);

        await _applicationInfoRepository.SaveChangesAsync();

        var createdApplication = _mapper.Map<Models.UserApplicationDto>(userApplication);

        return CreatedAtRoute("GetApplication",
            new
            {
                id = createdApplication.ApplicationId
            },
            createdApplication);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserApplication(int id, UserApplicationUpdateDto userApplicationUpdate)
    {
        var userApplicationEntity = await _applicationInfoRepository.GetApplicationAsync(id);
        if (userApplicationEntity == null)
        {
            return NotFound();
        }

        _mapper.Map(userApplicationUpdate, userApplicationEntity);

        await _applicationInfoRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateUserApplication(int id, JsonPatchDocument<UserApplicationUpdateDto> patchApplication)
    {
        var userApplicationEntity = await _applicationInfoRepository.GetApplicationAsync(id);
        if (userApplicationEntity == null)
        {
            return NotFound();
        }

        var applicationToPatch = _mapper.Map<UserApplicationUpdateDto>(userApplicationEntity);

        patchApplication.ApplyTo(applicationToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!TryValidateModel(applicationToPatch))
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(applicationToPatch, userApplicationEntity);
        await _applicationInfoRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserApplication(int id)
    {
        var userApplicationEntity = await _applicationInfoRepository.GetApplicationAsync(id);
        if (userApplicationEntity == null)
        {
            return NotFound();
        }

        _applicationInfoRepository.DeleteApplication(userApplicationEntity);

        await _applicationInfoRepository.SaveChangesAsync();

        return NoContent();
    }
}