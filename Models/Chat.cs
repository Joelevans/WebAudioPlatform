using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebAudioPlatform.Models
{
    public class Chat
    {
        public Chat()
        {
            Messages = new List<Message>();
            Users = new List<IdentityUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages  { get; set; }

        public ICollection<IdentityUser> Users { get; set; }
        public ChatType Type { get; set; }
        
    }
}