using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Errors
{
    public static class DomainErrors
    {
        public static class Member
        {
            public static readonly Error AlreadyExists = new (
                "Member.AlreadyExists",
                "The member with this Email already exists.");

            public static readonly Error NotFound = new(
                "Member.NotFound",
                "Member was not found");
        }
    }
}
