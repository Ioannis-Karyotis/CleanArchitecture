using Application.Members.Queries.GetTestMember;
using Application.Recievers.Members.Commands.AddTestMember;
using Domain.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

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

            Result<GetTestMemberResponse> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response) : NotFound(response.Error);
        }

        [HttpPost("test")]
        public async Task<IActionResult> AddTestMember([FromBody] AddTestMemberCommand request, CancellationToken cancellationToken)
        {
            Result response = await Sender.Send(request, cancellationToken);
            return response.IsSuccess ? Ok(response.IsSuccess): BadRequest(response.Error);
        }
    }
}
