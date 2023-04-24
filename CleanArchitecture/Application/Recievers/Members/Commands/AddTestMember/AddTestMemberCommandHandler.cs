using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Models.DTOs;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return Result.Failure(new Error(
                    "Member.AlreadyExists",
                    $"The member with Email {request.Email} already exists"));
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
