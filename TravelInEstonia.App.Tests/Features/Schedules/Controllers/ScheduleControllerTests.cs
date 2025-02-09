using Marten;
using Moq;
using TravelInEstonia.App.Features.Schedules.Controllers;
using TravelInEstonia.Domain.Features.Schedules;

namespace TravelInEstonia.App.Tests.Features.Schedules.Controllers;

[TestFixture]
public class ScheduleControllerTests
{
    private ScheduleController _sut;
    private Mock<IDocumentSession> _documentSessionMock;
    [SetUp]
    public void Setup()
    {
        _documentSessionMock = new Mock<IDocumentSession>();
        _sut = new ScheduleController();
    }
    

    [Test]
    public async Task Locations_Returns_BadRequest_If_No_Schedules_Are_Found()
    {
        // Act
        var result = await _sut.Locations(_documentSessionMock.Object);
    }
}