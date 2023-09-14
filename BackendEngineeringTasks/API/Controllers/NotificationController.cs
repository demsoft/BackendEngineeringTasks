using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Application.Services;
using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BackendEngineeringTasks.API.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationAppService _notifcationAppService;
        public NotificationController(INotificationAppService notifcationAppService)
        {
            _notifcationAppService = notifcationAppService;
        }

        [HttpGet("get/notifications/by/userId/{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(int userId)
        {
            var notifications = await _notifcationAppService.GetByUserIdAsync(userId);
            return Ok(notifications);
        }

        [HttpGet("get/unread/notification/by/userId/{userId}")]
        public async Task<IActionResult> GetUnreadByUserIdAsync(int userId)
        {
            var notifications = await _notifcationAppService.GetUnreadByUserIdAsync(userId);
            return Ok(notifications);
        }

        // Mark a notification as read
        [HttpPut("{notificationId}/mark-as-read")]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            try
            {
                await _notifcationAppService.MarkNotificationAsReadAsync(notificationId);
                return NoContent(); 
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Mark a notification as unread
        [HttpPut("{notificationId}/mark-as-unread")]
        public async Task<IActionResult> MarkNotificationAsUnread(int notificationId)
        {
            try
            {
                await _notifcationAppService.MarkNotificationAsUnreadAsync(notificationId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{notificationId}")]
        public async Task<IActionResult> GetNotificationByIdAsync(int notificationId)
        {
            var notification = await _notifcationAppService.GetNotificationByIdAsync(notificationId);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetNotificationAsync()
        {
            var notification = await _notifcationAppService.GetNotificationAsync();
            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotificationAsync(NotificationDto notificationDto)
        {
            try
            {
                var createdNotification = await _notifcationAppService.CreateNotificationAsync(notificationDto);
                return CreatedAtAction(nameof(GetNotificationByIdAsync), new { notificationId = createdNotification.Id }, createdNotification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{notificationId}")]
        public async Task<IActionResult> UpdateNotificationAsync(int notificationId, NotificationDto notificationDto)
        {
            try
            {
                await _notifcationAppService.UpdateNotificationAsync(notificationId, notificationDto);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotificationAsync(int notificationId)
        {
            try
            {
                await _notifcationAppService.DeleteNotificationAsync(notificationId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
