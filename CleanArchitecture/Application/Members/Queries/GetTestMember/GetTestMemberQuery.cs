using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.Queries.GetTestMember
{
    public sealed record GetTestMemberQuery() : IQuery<TestMemberResponse>;
}
