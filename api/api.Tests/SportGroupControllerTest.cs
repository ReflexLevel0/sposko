using sposko;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace api.Tests;

public class SportGroupController
{
    [Fact]
    public async Task TestGetByExistingId()
    {
        var expectedDTO = new SportGroupDTO();
        var groupServiceMock = new Mock<ISportGroupService>();
        groupServiceMock.Setup(s => s.GetSportGroupById(1)).ReturnsAsync(expectedDTO);
        var controller = new sposko.SportGroupController(groupServiceMock.Object);
        var result = await controller.Get(1);

        Assert.IsType<OkObjectResult>(result.Result);
        // Assert.IsType<SportGroupDTO>(result.Value);
        // Assert.Equal(expectedDTO, result.Value);
        groupServiceMock.Verify(s => s.GetSportGroupById(1), Times.Once());
    }

    [Fact]
    public async Task TestGetByNonExistingId()
    {
        var groupServiceMock = new Mock<ISportGroupService>();
        var controller = new sposko.SportGroupController(groupServiceMock.Object);
        var returnDTO = new SportGroupDTO();
        var result = await controller.Get(2);

        Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(null, result.Value);
        groupServiceMock.Verify(s => s.GetSportGroupById(2), Times.Once());
    }

    [Fact]
    public async Task TestPostNoConflict()
    {
        var groupServiceMock = new Mock<ISportGroupService>();
        var controller = new sposko.SportGroupController(groupServiceMock.Object);
        var insertDTO = new CreateSportGroupDTO();
        var expectedDTO = new SportGroupDTO();
        groupServiceMock.Setup(s => s.CreateSportGroup(insertDTO)).ReturnsAsync(expectedDTO);
        var result = await controller.Post(insertDTO);

        Assert.IsType<OkObjectResult>(result.Result);
        // Assert.IsType<SportGroupDTO>(result.Value);
        // Assert.Equal(expectedDTO, result.Value);
        groupServiceMock.Verify(s => s.CreateSportGroup(insertDTO), Times.Once());
    }

    [Fact]
    public async Task TestPostConflict()
    {
        var groupServiceMock = new Mock<ISportGroupService>();
        var controller = new sposko.SportGroupController(groupServiceMock.Object);
        var insertDTO = new CreateSportGroupDTO();
        var result = await controller.Post(insertDTO);

        Assert.IsType<ConflictResult>(result.Result);
        Assert.Equal(null, result.Value);
        groupServiceMock.Verify(s => s.CreateSportGroup(insertDTO), Times.Once());
    }
}
