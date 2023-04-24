using Application.Abstractions.Messaging;
using Application.Members.Queries.GetTestMember;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Recievers.Members.Commands.AddTestMember
{
    public sealed record AddTestMemberCommand(
        string Email,
        string FirstName,
        string LastName) 
    : ICommand;
}
