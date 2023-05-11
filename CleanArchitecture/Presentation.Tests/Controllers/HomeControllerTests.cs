using Application.Members.Queries.GetTestMember;
using Domain.Shared;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using Xunit;

namespace Presentation.Tests.Controllers
{
    public  class HomeControllerTests
    {
        private readonly ISender Sender;
        private readonly HomeController _sut;

        public HomeControllerTests()
        {
            Sender = A.Fake<ISender>();

            //System Under Test
            _sut = new HomeController(Sender);
        }

        [Fact]
        public void GetTestMember_ShouldReturnOk()
        {
            //Arrange
            var query = new GetTestMemberQuery();
            var response = A.Fake<GetTestMemberResponse>();
            var successResult = Result.Success(response);
            CancellationToken cancelationToken = new CancellationToken();
            A.CallTo(() => Sender.Send(query, cancelationToken)).Returns(successResult);

            //Act
            var result = _sut.GetTestMember(cancelationToken).Result;

            //Assert
            result.Should().NotBeNull().And.BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void GetTestMember_ShouldReturnNotFound()
        {
            //Arrange
            var query = new GetTestMemberQuery();
            var response = A.Fake<GetTestMemberResponse>();
            var error = A.Fake<Error>();
            var failureResult = Result.Failure<GetTestMemberResponse>(error);
            CancellationToken cancelationToken = new CancellationToken();
            A.CallTo(() => Sender.Send(query, cancelationToken)).Returns(failureResult);

            //Act
            var result = _sut.GetTestMember(cancelationToken).Result;

            //Assert
            result.Should().NotBeNull().And.BeOfType(typeof(NotFoundObjectResult));
        }

        // Should Implement for rest of endpoints
    }

}

