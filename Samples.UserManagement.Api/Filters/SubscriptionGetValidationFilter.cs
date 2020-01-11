using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Samples.UserManagement.Api.DataAccess;
using Samples.UserManagement.Api.Models;

namespace Samples.UserManagement.Api.Filters
{
    public class SubscriptionGetValidationFilter : IActionFilter
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionGetValidationFilter(IUserRepository userRepository,
                                               IServiceRepository serviceRepository,
                                               ISubscriptionRepository subscriptionRepository)
        {
            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ActionArguments.TryGetValue("value", out var objValue) ||
                objValue == null ||
                !(objValue is UserSubscribeRequest userSubscribeRequest))
            {
                actionContext.Result = new BadRequestObjectResult("Invalid request body");

                return;
            }

            var user = _userRepository.GetById(userSubscribeRequest.UserId);

            if (user == null)
            {
                actionContext.Result = new NotFoundObjectResult("User or Service is not valid");

                return;
            }

            var service = _serviceRepository.GetById(userSubscribeRequest.ServiceId);

            if (service == null)
            {
                actionContext.Result = new NotFoundObjectResult("User or Service is not valid");

                return;
            }

            if (_subscriptionRepository.GetSubscriptionTo(userSubscribeRequest.UserId, userSubscribeRequest.ServiceId) != null)
            {
                actionContext.Result = new ConflictObjectResult("User is already subscribed to that service");
            }
        }
    }
}
