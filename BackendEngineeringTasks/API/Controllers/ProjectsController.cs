using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Application.Services;
using BackendEngineeringTasks.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BackendEngineeringTasks.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectAppService _projectAppService;

        public ProjectsController(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectAppService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            var project = await _projectAppService.GetProjectByIdAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectDto projectDto)
        {
            try
            {
                var createdProject = await _projectAppService.CreateProjectAsync(projectDto);
                return CreatedAtAction(nameof(GetProjectById), new { projectId = createdProject.Id }, createdProject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject(int projectId, ProjectDto projectDto)
        {
            try
            {
                await _projectAppService.UpdateProjectAsync(projectId, projectDto);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            try
            {
                await _projectAppService.DeleteProjectAsync(projectId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
