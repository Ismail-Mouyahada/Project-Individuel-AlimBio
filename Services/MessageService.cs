using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.EntityFrameworkCore;
namespace AlimBio.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task CreateMessageAsync(Message Message)
        {
            _context.Add(Message);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMessageAsync(Message Message)
        {
            _context.Update(Message);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(int id)
        {
            var Message = await _context.Messages.FindAsync(id);
            if (Message != null)
            {
                _context.Messages.Remove(Message);
                await _context.SaveChangesAsync();
            }
        }
    }

}
