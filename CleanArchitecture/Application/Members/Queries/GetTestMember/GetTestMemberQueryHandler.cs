using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.Queries.GetTestMember
{
    internal sealed class GetTestMemberQueryHandler
    : IQueryHandler<GetTestMemberQuery, TestMemberResponse>
    {
        private readonly IMemberRepository _memberRepository;
        public GetTestMemberQueryHandler(
            IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Result<TestMemberResponse>> Handle(
            GetTestMemberQuery request,
            CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(
                new Guid("c381d663-2240-4efd-8a29-84765f16a88d"),
                cancellationToken);

            if (member is null)
            {
                return Result.Failure<TestMemberResponse>(new Error(
                    "Member.NotFound",
                    $"The member with Id {new Guid("c381d663-2240-4efd-8a29-84765f16a88d")} was not found"));
            }

            return Result.Success<TestMemberResponse>(new TestMemberResponse("lol", "ioannis.karyotis@gmail.com"));
        }
    }
}
