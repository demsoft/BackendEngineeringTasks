using AutoMapper;
using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Exceptions;
using BackendEngineeringTasks.Domain.Repositories;
using BackendEngineeringTasks.Infrastructure.Repositories;

namespace BackendEngineeringTasks.Application.Services
{
    public class NotficationAppService : INotificationAppService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper; // AutoMapper for mapping DTOs
        public NotficationAppService(INotificationRepository notificationRepository, IMapper mapper)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }
        public async Task<NotificationDto> CreateNotificationAsync(NotificationDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);
            await _notificationRepository.AddAsync(notification);
            return _mapper.Map<NotificationDto>(notification);
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification == null)
            {
                throw new NotFoundException("Notification not found");
            }

            await _notificationRepository.DeleteAsync(notificationId);
        }

        public async Task<IEnumerable<NotificationDto>> GetByUserIdAsync(int userId)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
            return _mapper.Map< IEnumerable<NotificationDto>>(notifications);
        }

        public async Task<NotificationDto> GetNotificationByIdAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            return _mapper.Map<NotificationDto>(notification);
        }

        public async Task<IEnumerable<NotificationDto>> GetUnreadByUserIdAsync(int userId)
        {
            var notifications = await _notificationRepository.GetUnreadByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            await _notificationRepository.MarkNotificationAsReadAsync(notificationId);
        }

        public async Task MarkNotificationAsUnreadAsync(int notificationId)
        {
            await _notificationRepository.MarkNotificationAsUnreadAsync(notificationId);
        }

        public async Task UpdateNotificationAsync(int notificationId, NotificationDto notificationDto)
        {
            var existingNotification = await _notificationRepository.GetByIdAsync(notificationId);
            if (existingNotification == null)
            {
                throw new NotFoundException("Notification not found");
            }

            _mapper.Map(notificationDto, existingNotification);
            await _notificationRepository.UpdateAsync(existingNotification);
        }
    }
}
