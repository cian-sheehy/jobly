using System.Threading.Tasks;
using Jobly.Actions;
using Xunit;

namespace Jobly.Tests;

public class GreenhouseActionTests
{
    [Fact]
    public async Task TestToUpperFunction()
    {
        // Arrange
        var greenhouseActions = new GreenhouseActions();
        var emailActions = new EmailActions();
        var jobs = await greenhouseActions.GetAllJobs();

        // Act
        await emailActions.SendPersonalJobAlertEmail(jobs);
    }
}