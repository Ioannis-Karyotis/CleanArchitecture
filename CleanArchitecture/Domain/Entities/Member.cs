﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Member
    {

        public Member(
            Guid id,
            string email,
            string firstName,
            string lastName
            )
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }


    }
}