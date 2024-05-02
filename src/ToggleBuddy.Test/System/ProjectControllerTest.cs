using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToggleBuddy.API.Controllers;
using ToggleBuddy.API.Helpers;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;
using ToggleBuddy.API.Services.ProjectServices;

namespace ToggleBuddy.API.Tests.Controllers
{
    public class ProjectControllerTests
    {
        //private readonly IMapper _mapper;
        private readonly Mock<IProjectService> _projectServiceMock;
        private readonly ProjectController _controller;

        public ProjectControllerTests()
        {
            //_mapper = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<ProjectRequestDto, Project>();
            //    cfg.CreateMap<Project, ProjectResponseDto>();
            //}).CreateMapper();

            _projectServiceMock = new Mock<IProjectService>();
            _controller = new ProjectController(_projectServiceMock.Object);
        }

        [Fact]
        public async Task CreateProject_ValidInput_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var projectRequestDto = new ProjectRequestDto { Name = "Test Project", Description = "Test Description" };

            var userIdentity = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            ]);

            var userPrincipal = new ClaimsPrincipal(userIdentity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            var projectResponse = new ServiceResponse<ProjectResponseDto>
            {
                Status = ResponseStatus.Success,
                Result = new ProjectResponseDto { Id = Guid.NewGuid() }
            };

            _projectServiceMock.Setup(x => x.CreateProjectAsync(projectRequestDto, userPrincipal))
                               .ReturnsAsync(projectResponse);

            // Act
            var result = await _controller.CreateProject(projectRequestDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.ActionName.Should().Be(nameof(ProjectController.GetProjectById));
            result.As<CreatedAtActionResult>().RouteValues?.ContainsKey("id").Should().BeTrue();
            result.As<CreatedAtActionResult>().Value.Should().BeEquivalentTo(projectResponse);
        }

        // Add similar tests for GetProjects, GetProjectById, UpdateProjectById, and DeleteProjectById actions
        [Fact]
        public async Task GetProjects_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var userIdentity = new ClaimsIdentity(
                           [
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            ]);

            var userPrincipal = new ClaimsPrincipal(userIdentity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            var projectResponse = new ServiceResponse<List<ProjectResponseDto>>
            {
                Status = ResponseStatus.Success,
                Result = new List<ProjectResponseDto>
                {
                    new ProjectResponseDto { Id = Guid.NewGuid() },
                    new ProjectResponseDto { Id = Guid.NewGuid() }
                }
            };

            _projectServiceMock.Setup(x => x.GetProjectsAsync(userPrincipal))
                               .ReturnsAsync(projectResponse);

            // Act
            var result = await _controller.GetProjects();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(projectResponse);
        }

        [Fact]
        public async Task GetProjectById_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var userIdentity = new ClaimsIdentity(
                                          [
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            ]);

            var userPrincipal = new ClaimsPrincipal(userIdentity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            var projectId = Guid.NewGuid();
            var projectResponse = new ServiceResponse<ProjectResponseDto>
            {
                Status = ResponseStatus.Success,
                Result = new ProjectResponseDto { Id = projectId }
            };

            _projectServiceMock.Setup(x => x.GetProjectByIdAsync(projectId, userPrincipal))
                               .ReturnsAsync(projectResponse);

            // Act
            var result = await _controller.GetProjectById(projectId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(projectResponse);
        }

        [Fact]
        public async Task UpdateProjectById_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var projectRequestDto = new ProjectRequestDto { Name = "Test Project", Description = "Test Description" };
            var userIdentity = new ClaimsIdentity(
                                                         [
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            ]);

            var userPrincipal = new ClaimsPrincipal(userIdentity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            var projectId = Guid.NewGuid();
            var projectResponse = new ServiceResponse<ProjectResponseDto>
            {
                Status = ResponseStatus.Success,
                Result = new ProjectResponseDto { Id = projectId }
            };

            _projectServiceMock.Setup(x => x.UpdateProjectAsync(projectId, projectRequestDto, userPrincipal))
                               .ReturnsAsync(projectResponse);

            // Act
            var result = await _controller.UpdateProjectById(projectId, projectRequestDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(projectResponse);
        }

        [Fact]
        public async Task DeleteProjectById_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var userIdentity = new ClaimsIdentity(
                                                                        [
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            ]);

            var userPrincipal = new ClaimsPrincipal(userIdentity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            var projectId = Guid.NewGuid();
            var projectResponse = new ServiceResponse<ProjectResponseDto>
            {
                Status = ResponseStatus.Success,
                Result = new ProjectResponseDto { Id = projectId }
            };

            _projectServiceMock.Setup(x => x.DeleteProjectAsync(projectId, userPrincipal))
                               .ReturnsAsync(projectResponse);

            // Act
            var result = await _controller.DeleteProjectById(projectId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(projectResponse);
        }
    }
}
