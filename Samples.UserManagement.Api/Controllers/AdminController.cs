using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Samples.UserManagement.Api.DataAccess;
using Samples.UserManagement.Api.Extensions;
using Samples.UserManagement.Api.Models;

namespace Samples.UserManagement.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] AdminUserPatchReqeust value)
        {
            var existingUser = _userRepository.GetById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.PopulateWith(value);

            _userRepository.Update(existingUser);

            return NoContent();
        }
    }
}
