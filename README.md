# Samples.UserManagement
Sample user management api

## [Build Test Run](#build-test-run)

* Download and install dotnet core 3.1 LTS
  * Mac: <https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.100-macos-x64-installer>

  * Linux instructions: <https://docs.microsoft.com/dotnet/core/install/linux-package-managers>

  * Windows: <https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.100-windows-x64-installer>

* Clone code and build:
```bash
git clone https://github.com/boydc7/Samples.UserManagement
cd Samples.UserManagement
dotnet publish -c Release -o publish Samples.UserManagement.Api/Samples.UserManagement.Api.csproj
```
* Run test suites (from textpipe folder from above)
```bash
dotnet test Samples.Tests/Samples.Tests.csproj
```
* Run (from Samples.UserManagement folder from above)
```bash
# Show usage:
cd publish
./tmcum
```

The api will start and by default listen for requests on localhost:8084.


## [Authentication](#authentication)

Using a simple x-api-key header based auth method.  Set an "x-api-key" request header value to any of the following valid keys:

2 users are created by default:

* API Key = 9876
  * Default user, is an admin

* API Key = 1234testnonadmin567890
  * Non admin user - using this key should result in forbidden results to admin endpoints (admin controller)

## [Endpoints](#endpoints)

* Admin
  * HTTP PATCH /admin/{id}
    * Send an AdminUserPatchReqeust object as an admin to change a user status

* Services
  * HTTP GET /services
    * Get all services that exist on the api

* Users
  * HTTP GET /users/{userid}
    * Get a user and subscriptions for the user by id
  * HTTP GET /users
    * Get all users
    * OR
    * Include a query string param of "status=<UserStatus enum value>" to get users in a particular status (any status)
 * HTTP POST /users
    * Post a User object to create a new user
 * HTTP PATCH /users/{id}
    * Patch a user with some/all values in a UserPatchReqeust object
 * HTTP DELETE /users/{id}
    * Delete a user with the given id

* Subscriptions
  * HTTP GET /subscriptions/users/{userid}
    * Get all services a given user is subscribed to
   HTTP POST /subscriptions
    * Post a new subscription to a service for a user with a UserSubscribeRequest object
 * HTTP DELETE /subscriptions
    * Unsubscribe/remove a subscription to a given service for a given user with a UserSubscribeRequest object
 * HTTP DELETE /subscriptions/{id}
    * Delete a subscription with the given id
