using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Jobly.Clients;
using Jobly.Helpers;
using Jobly.Models;

namespace Jobly.Actions;

public class GreenhouseActions
{
    private GreenhouseClient _greenhouseClient;

    public GreenhouseActions()
    {
        _greenhouseClient = new GreenhouseClient(new HttpClient());
    }

    public async Task<List<InterestedJob>> GetAllJobs()
    {
        var interestedJobs = new List<InterestedJob>();
        foreach (var company in Matcher.Companies)
        {
            var jobs = await GetCompanyJobs(company);
            if (jobs == null)
            {
                continue;
            }

            interestedJobs.AddRange(jobs);
        }

        return interestedJobs;
    }

    private async Task<List<InterestedJob>?> GetCompanyJobs(string company)
    {
        var interestedJobs = new List<InterestedJob>();
        var jobs = (await _greenhouseClient.GetJobsFromCompany(company.ToLower()))?.Jobs;
        if (jobs == null)
        {
            return null;
        }

        foreach (var job in jobs)
        {
            var titleMatch = Matcher.JobTitle.Any(j => job.Title.Contains(j, StringComparison.OrdinalIgnoreCase));
            var locationMatch = Matcher.JobLocations.Any(l => job.Location.Name.Contains(l, StringComparison.OrdinalIgnoreCase));

            if (!titleMatch || !locationMatch)
            {
                continue;
            }

            interestedJobs.Add(new InterestedJob
            {
                Company = company,
                Location = job.Location.Name,
                Title = job.Title,
                Url = job.absolute_url
            });
        }

        return interestedJobs;
    }
}