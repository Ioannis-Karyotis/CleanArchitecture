using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Recievers.Members.Commands.AddTestMember
{
    internal sealed record GetTestMemberResponse(string Id, string Email);
}
