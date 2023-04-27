using Application.Abstractions.Messaging;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;

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

            var newMember = Member.Create(
                Guid.NewGuid(),
                request.Email,
                request.FirstName,
                request.LastName);

            _memberRepository.Add(newMember);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
