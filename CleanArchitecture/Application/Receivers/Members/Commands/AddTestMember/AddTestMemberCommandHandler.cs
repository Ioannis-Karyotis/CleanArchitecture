using Application.Abstractions.Messaging;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Recievers.Members.Commands.AddTestMember
{
    internal sealed class AddTestMemberCommandHandler : ICommandHandler<AddTestMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTestMemberCommandHandler(
            IMemberRepository memberRepository,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddTestMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (member is not null)
            {
                return Result.Failure<GetTestMemberResponse>(DomainErrors.Member.AlreadyExists);
            }

            var newMemberResult = Member.Create(
                Guid.NewGuid(),
                request.Email,
                request.FirstName,
                request.LastName
                );

            if (newMemberResult.IsFailure)
            {
                return newMemberResult;
            }

            _memberRepository.Add(newMemberResult.Value);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                return Result.Success();
            }

            return Result.Failure<GetTestMemberResponse>(DomainErrors.Member.NotFound);

        }

        Task<Result> IRequestHandler<AddTestMemberCommand, Result>.Handle(AddTestMemberCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
