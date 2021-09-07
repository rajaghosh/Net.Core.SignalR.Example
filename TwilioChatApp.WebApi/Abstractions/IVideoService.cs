using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoChatService.Models;

namespace VideoChatService.Abstractions
{
    public interface IVideoService
    {
        string GetTwilioJwt(string identity);
        Task<IEnumerable<RoomDetails>> GetAllRoomsAsync();
    }
}
