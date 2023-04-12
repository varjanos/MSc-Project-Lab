using FloorPlanner.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace FloorPlanner.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
        => await CreateResponseAsync(context);

    private static async Task CreateResponseAsync(HttpContext context)
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception != null)
        {
            var problem = new ProblemDetails
            {
                Title = "Errors.InternalServerError",
            };

            switch (exception)
            {
                case BaseException baseException:
                    problem.Status = (int)HttpStatusCode.InternalServerError;
                    problem.Extensions["message"] = baseException.Message;
                    break;
                default:
                    problem.Status = (int)HttpStatusCode.InternalServerError;
                    problem.Extensions["message"] = problem.Title;
                    break;
            }

            problem.Extensions["serverTime"] = DateTime.Now;
            context.Response.StatusCode = problem.Status.Value;
            context.Response.ContentType = "application/problem+json";
            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problem);
        }
    }
}