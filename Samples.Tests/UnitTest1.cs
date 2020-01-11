using System;
using NUnit.Framework;
using Samples.UserManagement.Api.Controllers;
using Samples.UserManagement.Api.DataAccess;
using Samples.UserManagement.Api.Enums;
using Samples.UserManagement.Api.Extensions;
using Samples.UserManagement.Api.Models;
using Samples.UserManagement.Api.Services;

namespace Samples.Tests
{
    [TestFixture]
    public class Tests
    {
        private IUserRepository _userRepository;
        private ISubscriptionRepository _subscriptionRepository;
        private IServiceRepository _serviceRepository;
        private ISequenceProvider _sequenceProvider;

        [SetUp]
        public void Setup()
        {
            _sequenceProvider = InMemorySequenceProvider.Create();

            _userRepository = new InMemoryUserRepository(_sequenceProvider);
            _serviceRepository = new InMemoryServiceRepository(_sequenceProvider);
            _subscriptionRepository = new InMemorySubscriptionRepository(_serviceRepository, _sequenceProvider);

            var demoData = new DemoDataService(_userRepository, _serviceRepository, _subscriptionRepository, new InMemoryApiKeyRepository());
            demoData.CreateDemoData();
        }

        [Test]
        public void SequenceProviderShouldIncrementByKey()
        {
            var provider = InMemorySequenceProvider.Create();

            var key1 = "key1";
            var key2 = "key2";
            var sumI = 0;

            for (var i = 0; i <= 100; i++)
            {
                sumI += i;

                var nextVal1 = provider.Increment(key1, i);
                var nextVal2 = provider.Increment(key2, i);

                Assert.AreEqual(nextVal1, nextVal2, "1 & 2 should be equal");
                Assert.AreEqual(nextVal1, sumI, "1 & 2 should be sumi");
            }
        }

        [Test]
        public void UserPatchPopulateShouldKeepExistingWhenWithPropertyIsNull()
        {
            var existingUser = new User
                               {
                                   FirstName = "First",
                                   LastName = "Last",
                                   Title = "Dr",
                                   Suffix = "Jr",
                                   DateOfBirth = new DateTime(2010, 5, 15),
                                   SocialSecurityNumber = "123456789",
                                   Email = "sampleuser@samplething.com",
                                   Password = "password",
                                   Status = UserStatus.Active
                               };

            var patchRequest = new UserPatchReqeust
                               {
                                   LastName = "NewLast",
                                   Suffix = "NewJr",
                                   Password = "newPassword"
                               };

            existingUser.PopulateWith(patchRequest);

            Assert.AreEqual(existingUser.FirstName, "First", "First should remain");
            Assert.AreEqual(existingUser.Title, "Dr", "Title should remain");
            Assert.AreEqual(existingUser.DateOfBirth, new DateTime(2010, 5, 15), "DateOfBirth should remain");
            Assert.AreEqual(existingUser.SocialSecurityNumber, "123456789", "SocialSecurityNumber should remain");
            Assert.AreEqual(existingUser.Email, "sampleuser@samplething.com", "Email should remain");
            Assert.AreEqual(existingUser.Status, UserStatus.Active, "Status should remain");

            Assert.AreEqual(existingUser.LastName, "NewLast", "Last should change");
            Assert.AreEqual(existingUser.Suffix, "NewJr", "Suffix should change");
            Assert.AreEqual(existingUser.Password, "newPassword", "Password should change");
        }

        [Test]
        public void UserControllerGetByIdReturnsCorrectly()
        {
            var controller = new UsersController(_userRepository, _subscriptionRepository);

            var user = controller.Get(1);

            Assert.NotNull(user, "User is null");
            Assert.AreEqual(user.Value.User.Id, 1, "User result should have Id 1");
            Assert.GreaterOrEqual(user.Value.Subscriptions.Count, 1, "User 1 should have 1 or more subscriptions");
        }
    }
}
