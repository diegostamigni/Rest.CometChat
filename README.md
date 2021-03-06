# Rest.CometChat
This is an unofficial .NET (standard) library for [CometChat](https://prodocs.cometchat.com/reference) REST APIs supporting v2 and v3 API versions.

[![.NET](https://github.com/diegostamigni/Rest.CometChat/actions/workflows/dotnet.yml/badge.svg)](https://github.com/diegostamigni/Rest.CometChat/actions/workflows/dotnet.yml)

## Description
All APIs are grouped in services:
 * User service
 * Friends service
 * Messages service
 * Groups service
 * Roles service
 * API Keys service
 * Auth Tokens service

All concrete classes respect a contract (ex. `UserService` -> `IUserService`) making things easier for testing/mocking and people that wants to use dependency injection. All services need at least the `ICometChatConfig` which exposes the following properties:
 * AppId
 * ApiKey
 * Region

Because this config is strictly dependant on your project, you are supposed to inherit from this contract and provide an implementation upon service construction.


### Examples

#### Create user
```csharp
var config = MyConfig(); // inherits from `ICometChatConfig`
var userService = new UserService(config);
var result = await userService.CreateAsync(new(userUid, userName)
{
   Avatar = "https://example.com/avatar.jpg",
   Link  = "https://userwebsite.example"
});
...
```

#### List users
```csharp
var config = MyConfig(); // inherits from `ICometChatConfig`
var userService = new UserService(config);
var result = await userService.ListAsync(); // supports pagination via `ListUserOptions`
...
```

### Supported APIs
- [x] User service
- [x] Friends service
- [ ] Messages service
- [ ] Groups service
- [ ] Roles service
- [ ] API Keys service
- [ ] Auth Tokens service

Feel free to contribute! Support for missing APIs and tests are underway.
