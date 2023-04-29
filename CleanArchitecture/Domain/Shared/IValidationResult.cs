using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
            "ValidationError",
            "Validation error occured"
            );

        Error[] Errors {get;}
    }
}
