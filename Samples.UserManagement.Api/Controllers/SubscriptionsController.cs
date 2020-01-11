using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Samples.UserManagement.Api.DataAccess;
using Samples.UserManagement.Api.Filters;
using Samples.UserManagement.Api.Models;

namespace Samples.UserManagement.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionsController(IUserRepository userRepository,
                                       IServiceRepository serviceRepository,
                                       ISubscriptionRepository subscriptionRepository)
        {
            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet("users/{userid}")]
        public ActionResult<IEnumerable<Service>> Get(int userId)
            => Ok(_subscriptionRepository.GetUserServices(userId));

        [HttpPost]
        [ServiceFilter(typeof(SubscriptionGetValidationFilter))]
        public ActionResult<int> Post([FromBody] UserSubscribeRequest value)
        {
            var subId = _subscriptionRepository.Add(new Subscription
                                                    {
                                                        UserId = value.UserId,
                                                        ServiceId = value.ServiceId
                                                    });

            return Ok(subId);
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] UserSubscribeRequest value)
        {
            var subscription = _subscriptionRepository.GetSubscriptionTo(value.UserId, value.ServiceId);

            if (subscription == null)
            {
                return NotFound();
            }

            _serviceRepository.Delete(subscription.Id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
            => _subscriptionRepository.Delete(id);
    }
}
