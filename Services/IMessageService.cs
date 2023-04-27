using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Models;

namespace AlimBio.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetAllMessagesAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task CreateMessageAsync(Message Message);
        Task UpdateMessageAsync(Message Message);
        Task DeleteMessageAsync(int id);
    }
}
