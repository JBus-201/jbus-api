using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Services;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[CustomAuthorize("PASSENGER")]
public class FriendsController(
    IFriendsRepository friendsRepository,
    IPassengerRepository passengerRepository,
    ITokenHandlerService tokenHandlerService,
    NotificationService notificationService,
    INotisTokenRepository notisTokenRepository
        ) : BaseApiController
{
    private readonly IFriendsRepository _friendsRepository = friendsRepository;
    private readonly IPassengerRepository _passengerRepository = passengerRepository;
    private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
    private readonly NotificationService _notificationService = notificationService;
    private readonly INotisTokenRepository _notisTokenRepository = notisTokenRepository;

    [HttpPost("sendFriendRequest")]
    public async Task<ActionResult> sendFriendRequest(FriendsCreateDto friendsCreateDto)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized(new { Error = "Not authorized1" });

        var passenger = await _passengerRepository.GetPassengerDtoById(friendsCreateDto.FriendId);
        if (passenger == null)
            return NotFound(new { Error = "Friend Not Found" });

        if (passenger.Id == Id)
            return BadRequest(new { Error = "WHY??? why sending friend reqeust to yourself? Are you this lonely?" });

        bool friendReqeustExists = await _friendsRepository.FriendRequestExists(friendsCreateDto.FriendId, Id);
        if (friendReqeustExists)
            return BadRequest(new { Error = "Friend Request Exists" });

        if (await _friendsRepository.SendFriendRequest(friendsCreateDto, Id))
        {
            if (!await _friendsRepository.SaveChanges())
                return StatusCode(500, new { Error = "Friend Request not saved" });
            var FcmToken = await _notisTokenRepository.GetDeviceToken(passenger.Id);
            // TODO : check fcm token validity
            if (FcmToken != null)
            {
                await _notificationService.SendNotificationAsync(FcmToken, new NotificationDto
                {
                    Title = "Friend Request",
                    Body = "You have a new friend request",
                    Type = "friendRequest",
                    Value = Id.ToString()
                });
                return StatusCode(201, new { Success = "Friend Request Sent" });
            }
            return StatusCode(201, new { Success = "Friend Request Sent but Notification not sent cause fcm token null" });
        }

        return StatusCode(500, new { Error = "Server Error2" });
    }

    [HttpPut("confirmFriendRequest/{id}")]
    public async Task<ActionResult> confirmFriendRequest(int id)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized(new { Error = "Not authorized" });

        // var friendId = await _friendsRepository.GetFriendRequests(id, Id);

        if (await _friendsRepository.ConfirmFriendRequest(id, Id))
        {
            await _friendsRepository.SaveChanges();
            var FcmToken = await _notisTokenRepository.GetDeviceToken(id);
            // TODO : check fcm token validity
            if (FcmToken != null)
            {
                await _notificationService.SendNotificationAsync(FcmToken, new NotificationDto
                {
                    Title = "Friend Request Confirmed",
                    Body = "You have a new friend",
                    Type = "friendConfirmed",
                    Value = Id.ToString()
                });
            }
            return NoContent();
        }
        return StatusCode(500, new { Error = "Server Error" });
    }

    [HttpGet("getFriendById/{id}")]
    public async Task<ActionResult<FriendDto>> getFriendById(int id)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized(new { Error = "Not authorized1" });

        var friend = await _friendsRepository.GetFriendById(id, Id);
        if (friend == null || friend.Friend == null)
            return NotFound(new { Error = "Friend Not Found" });
        FriendDto trueFriend;

        if (friend.Passenger!.Id == Id)
            trueFriend = friend.Friend;
        else
            trueFriend = friend.Passenger;

        return Ok(trueFriend);
    }

    [HttpGet("getFriends")]
    public async Task<ActionResult<IEnumerable<FriendDto>>> getFriends()
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized(new { Error = "Not authorized1" });

        var friends = await _friendsRepository.GetFriends(Id);
        List<FriendDto> friendss = [];
        foreach (var fri in friends)
        {
            if (fri!.Passenger!.Id == Id)
                friendss.Add(fri.Friend!);
            if (fri.Friend!.Id == Id)
                friendss.Add(fri.Passenger!);
        }
        return Ok(friendss);
    }

    [HttpGet("getFriendRequests")]
    public async Task<ActionResult<IEnumerable<FriendsDto>>> GetFriendsRequests()
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized(new { Error = "Not authorized1" });

        var friendsRequests = await _friendsRepository.GetFriendRequests(Id);

        return Ok(friendsRequests);
    }

    [HttpDelete("deleteFriend/{id}")]
    public async Task<ActionResult> deleteFriend(int id)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized(new { Error = "Not authorized1" });

        bool result = await _friendsRepository.DeleteFriend(id, Id);
        if (!result)
            return NotFound(new { Error = "Friend Not Found" });
        if (!await _friendsRepository.SaveChanges())
            return StatusCode(500, new { Error = "Server Error" });
        return NoContent();
    }
}
