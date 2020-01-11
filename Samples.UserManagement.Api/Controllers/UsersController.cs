using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Samples.UserManagement.Api.DataAccess;
using Samples.UserManagement.Api.Enums;
using Samples.UserManagement.Api.Extensions;
using Samples.UserManagement.Api.Models;

namespace Samples.UserManagement.Api.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public UsersController(IUserRepository userRepository,
                               ISubscriptionRepository subscriptionRepository)
        {
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponse> Get(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserResponse
                   {
                       User = user,
                       Subscriptions = _subscriptionRepository.GetUserServices(user.Id)
                                                              .AsListReadOnly()
                   };
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get([FromQuery] UserStatus? status)
            => Ok(_userRepository.Query(u => !status.HasValue || u.Status == status));

        [HttpPost]
        public ActionResult<int> Post([FromBody] User value)
            => _userRepository.Add(value);

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] UserPatchReqeust value)
        {
            var existingUser = _userRepository.GetById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.PopulateWith(value);

            _userRepository.Update(existingUser);

            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
            => _userRepository.Delete(id);
    }
}
