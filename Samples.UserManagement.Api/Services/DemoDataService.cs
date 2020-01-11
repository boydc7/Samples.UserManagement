using System;
using Samples.UserManagement.Api.DataAccess;
using Samples.UserManagement.Api.Enums;
using Samples.UserManagement.Api.Interfaces;
using Samples.UserManagement.Api.Models;

namespace Samples.UserManagement.Api.Services
{
    public class DemoDataService : IDemoDataService
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IApiKeyRepository _apiKeyRepository;

        public DemoDataService(IUserRepository userRepository,
                               IServiceRepository serviceRepository,
                               ISubscriptionRepository subscriptionRepository,
                               IApiKeyRepository apiKeyRepository)
        {
            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
            _subscriptionRepository = subscriptionRepository;
            _apiKeyRepository = apiKeyRepository;
        }

        public void CreateDemoData()
        {
            _apiKeyRepository.Add(new ApiKey
                                  {
                                      Id = "9876",
                                      FriendlyName = "Default",
                                      IsAdmin = true
                                  });

            _apiKeyRepository.Add(new ApiKey
                                  {
                                      Id = "1234testnonadmin567890",
                                      FriendlyName = "NonAdminUser",
                                      IsAdmin = false
                                  });

            SeedRepo(_userRepository, i => new User
                                           {
                                               FirstName = $"First{i} User{i}",
                                               LastName = $"Last{i} User{i}",
                                               Title = $"Dr{i}",
                                               Suffix = $"Jr{i}",
                                               DateOfBirth = new DateTime(2010, 5, (i % 27) + 1),
                                               SocialSecurityNumber = i.ToString().PadLeft(9, '0'),
                                               Email = $"sampleuser{i}@samplething.com",
                                               Password = Guid.NewGuid().ToString(),
                                               Status = UserStatus.Active
                                           });

            SeedRepo(_serviceRepository, i => new Service
                                              {
                                                  Name = $"Sample Service {i}",
                                                  Price = i
                                              });

            _subscriptionRepository.Add(new Subscription
                                        {
                                            UserId = 1,
                                            ServiceId = 1
                                        });
        }

        private static void SeedRepo<T>(IBaseModelRepository<T> repo, Func<int, T> builder, double rows = 5)
            where T : BaseModel
        {
            for (var i = 0; i <= rows; i++)
            {
                repo.Add(builder(i));
            }
        }
    }
}
