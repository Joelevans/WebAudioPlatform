using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAudioPlatform.Data;
using WebAudioPlatform.Models;

namespace WebAudioPlatform.Controllers
{
    [Authorize]
    public class ChatAppController : Controller
    {
        private ApplicationDbContext _ctx;

        public ChatAppController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = _ctx.Chats
                .Include(x=> x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task <IActionResult> CreateMessage (int chatId, string message)
        {
            var Message = new Message
            {
                ChatID = chatId,
                Text = message,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };

            _ctx.Messages.Add(Message);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Chat", new { id = chatId });
        }

        [HttpPost]
        public async Task <IActionResult> CreateRoom(string name)
        {
            _ctx.Chats.Add(new Chat
            {
                Name = name,
                Type = ChatType.Room
            });

            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
