﻿using Application.Abstractions.Messaging;
using Application.Models.Configuration;
using Domain.Models.DTOs;
using Domain.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.Queries.GetTestMember
{
    internal sealed class GetTestMemberQueryHandler
    : IQueryHandler<GetTestMemberQuery, GetTestMemberResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly TestConfiguration _test;

        public GetTestMemberQueryHandler(
            IMemberRepository memberRepository,
            IOptions<TestConfiguration> test)
        {
            _memberRepository = memberRepository;
            _test = test.Value;
        }

        public async Task<Result<GetTestMemberResponse>> Handle(
            GetTestMemberQuery request,
            CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(
                new Guid("c381d663-2240-4efd-8a29-84765f16a88d"),
                cancellationToken);

            if (member is null)
            {
                return Result.Failure<GetTestMemberResponse>(new Error(
                    "Member.NotFound",
                    $"The member with Id {new Guid("c381d663-2240-4efd-8a29-84765f16a88d")} was not found"));
            }

            return Result.Success<GetTestMemberResponse>(new GetTestMemberResponse(_test.Test, "ioannis.karyotis@gmail.com"));
        }
    }
}