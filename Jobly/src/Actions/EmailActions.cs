using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jobly.Clients;
using Jobly.Helpers;
using Jobly.Models;

namespace Jobly.Actions;

public class EmailActions
{
    private readonly EmailClient _emailClient;

    public EmailActions()
    {
        _emailClient = new EmailClient();
    }

    public async Task SendPersonalJobAlertEmail(List<InterestedJob> interestedJobs)
    {
        var jobsText = interestedJobs.Select(j =>
            $"<a href='{j.Url}'>{j.Title}</a> ({j.Location}, {j.Company})");

        await _emailClient.Send(
            "shazamjunk@gmail.com",
            "shazamjunk@gmail.com",
            "Personal Job Alerts",
            $"Fellow Job Hunters,<br><br><p>{string.Join("<br>", jobsText)}</p><br><br><strong>Best Regards,</strong><br>JobScraper"
        );
    }
    
    public async Task SendPersonalJobAlertEmail()
    {
        var companies = Matcher.Companies;
        await _emailClient.Send(
            "shazamjunk@gmail.com",
            "shazamjunk@gmail.com",
            "Personal Job Alerts",
            $"Fellow Job Hunters,<br><br><p>Nothing to report for your interested companies ({string.Join(",", companies)})</p><br><br><strong>Best Regards,</strong><br>JobScraper"
        );
    }
}