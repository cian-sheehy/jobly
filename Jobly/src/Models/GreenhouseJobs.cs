using System;
using System.Collections.Generic;

namespace Jobly.Models;

public class GreenhouseJobs
{
    public List<Job> Jobs { get; set; }
    public Meta Meta { get; set; }
}

public class DataCompliance
{
    public string Type { get; set; }
    public bool RequiresConsent { get; set; }
    public object RetentionPeriod { get; set; }
}

public class Job
{
    public string absolute_url { get; set; }
    public List<DataCompliance> DataCompliance { get; set; }
    public double InternalJobId { get; set; }
    public Location Location { get; set; }
    public List<Metadata> Metadata { get; set; }
    public double Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string RequisitionId { get; set; }
    public string Title { get; set; }
}

public class Location
{
    public string Name { get; set; }
}

public class Meta
{
    public int Total { get; set; }
}

public class Metadata
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool? Value { get; set; }
    public string ValueType { get; set; }
}