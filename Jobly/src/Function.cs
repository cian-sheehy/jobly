using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Jobly.Actions;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace Jobly;

public class Function
{
    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
    {
        context.Logger.Log("Starting lambda..");
        var greenhouseActions = new GreenhouseActions();
        var emailActions = new EmailActions();

        context.Logger.Log("Getting all jobs for chosen companies..");
        var jobs = await greenhouseActions.GetAllJobs();
        var additionalMessage = "";

        if (jobs.Count > 0)
        {
            context.Logger.Log($"We have found {jobs.Count} job(s) for you today..");
            await emailActions.SendPersonalJobAlertEmail(jobs);
            additionalMessage = $" We have found {jobs.Count} job(s) for you today.";
        }
        else
        {
            context.Logger.Log("We have found 0 jobs for you today.");
            await emailActions.SendPersonalJobAlertEmail();
            additionalMessage = " We have found 0 jobs for you today.";
        }

        return new APIGatewayProxyResponse
        {
            Body = JsonSerializer.Serialize(new Dictionary<string, string>
            {
                {
                    "message", $"Email has been sent successfully.{additionalMessage}"
                }
            }),
            StatusCode = 200,
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }
}