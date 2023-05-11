using Application.Interfaces.Repositories;
using Application.Recievers.Members.Commands.AddTestMember;
using Domain.Entities;
using Domain.Shared;
using FakeItEasy;
using FakeItEasy.Sdk;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Receivers.Members.CommandHandlers
{
    public class AddTestMemberCommandHandlerTests
    {
        private readonly AddTestMemberCommandHandler _sut;
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddTestMemberCommandHandlerTests()
        {
            _memberRepository = A.Fake<IMemberRepository>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            //System Under Test
            _sut = new AddTestMemberCommandHandler(_memberRepository, _unitOfWork);
        }

        [Fact]
        public async void AddTestMemberCommandHandler_ShouldReturnSuccessResult()
        {
            //Arrange
            var request = new AddTestMemberCommand(
                "whatever@gmail.com",
                "What",
                "Ever");

            var cancelationToken = new CancellationToken();
            Member member = null;

            A.CallTo(() => _memberRepository.GetByEmailAsync(request.Email, cancelationToken)).Returns(member);

            var newMemberResult = Member.Create(
                    A.Dummy<Guid>(),
                    request.Email,
                    request.FirstName,
                    request.LastName
                    );

            if (newMemberResult.IsFailure)
            {
                throw new InvalidOperationException("Member Repository tests failed");
            };
            
            A.CallTo(() => _memberRepository.Add(newMemberResult.Value));

            var saveChangesResult = 1;

            A.CallTo(() => _unitOfWork.SaveChangesAsync(cancelationToken)).Returns(saveChangesResult);

            //Act
            var finalResult = await _sut.Handle(request, cancelationToken);

            //Assert
            finalResult.Should().NotBeNull().And.BeEquivalentTo(Result.Success());
        }

        //Should implement the rest of the scenarios

    }
}
