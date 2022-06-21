using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/user
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AppUser>> GetAppUser()
        {
            //Get the identity and then get the id based on the claims
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) return BadRequest();
            var currentUserId = identity.FindFirst("Id").Value;

            var appUser = await _context.AppUsers.FindAsync(Convert.ToInt32(currentUserId));

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }

    }
}
