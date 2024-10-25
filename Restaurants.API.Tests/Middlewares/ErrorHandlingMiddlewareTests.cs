using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Xunit;


namespace Restaurants.API.Middlewares.Tests;

public class ErrorHandlingMiddlewareTests
{
    [Fact()]
    public async Task InvokeAsync_WithNoExceptionThrown_ShouldCallNextDelegate()
    {
        //arrange 

        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middelware = new ErrorHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();

        var nextDelegateMock = new Mock<RequestDelegate>();

        //act

        await middelware.InvokeAsync(context, nextDelegateMock.Object);

        //assert 

        nextDelegateMock.Verify(next => next.Invoke(context) ,Times.Once);

    }

    [Fact()]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCode404()
    {
        //arrange 
        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middelware = new ErrorHandlingMiddleware(loggerMock.Object);
        var notFoundException = new NotFoundException(nameof(Restaurant),"1");

        //act


        // _ => throw notFoundException   IT A DELEGATE FUNCTION 
        await middelware.InvokeAsync(context, _ => throw notFoundException);

        //assert 

        context.Response.StatusCode.Should().Be(404);

    }


    [Fact()]
    public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
    {
        //arrange 
        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middelware = new ErrorHandlingMiddleware(loggerMock.Object);
        var exception = new ForbidException();

        //act


        // _ => throw exception   IT A DELEGATE FUNCTION 
        await middelware.InvokeAsync(context, _ => throw exception);

        //assert 

        context.Response.StatusCode.Should().Be(403);

    }


    [Fact()]
    public async void InvokeAsync_WhenExceptionThrown_ShouldSetStatusCode500()
    {
        //arrange 
        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middelware = new ErrorHandlingMiddleware(loggerMock.Object);
        var exception = new Exception();

        //act


        // _ => throw exception   IT A DELEGATE FUNCTION 
        await middelware.InvokeAsync(context, _ => throw exception);

        //assert 

        context.Response.StatusCode.Should().Be(500);

    }
}