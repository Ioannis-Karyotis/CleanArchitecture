using Application.Members.Queries.GetTestMember;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using System.Threading;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class HomeController : ApiController
    {

        public HomeController(ISender sender)
        : base(sender)
        {}

        [HttpGet("test")]
        public async Task<IActionResult> GetTestMember(CancellationToken cancellationToken)
        {
            var query = new GetTestMemberQuery();

            Result<TestMemberResponse> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }
    }
}
