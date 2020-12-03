using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAudioPlatform.Data;

namespace WebAudioPlatform.ViewComponents
{
    public class RoomViewComponent:ViewComponent
    {
        private ApplicationDbContext _ctx;

        //retrieve database
        public RoomViewComponent(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }


        //method to invoke this view component
        public IViewComponentResult Invoke()
        {
            //grabbing all chats from ctx
            var chats = _ctx.Chats.ToList();
            //passing the chats to the view
            return View(chats);
        }
    }
}
