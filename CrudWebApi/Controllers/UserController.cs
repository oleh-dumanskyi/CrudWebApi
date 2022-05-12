using CrudWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UserController(UserDbContext context)
        {
            _context = context;

            //if (!_context.Users.Any())
            //{
            //    _context.Users.Add(new User() { Name = "Pavel", Age = 19 });
            //    _context.Users.Add(new User() { Name = "John", Age = 56 });
            //    _context.Users.Add(new User() { Name = "Bjorn", Age = 32 });
            //    _context.Users.Add(new User() { Name = "Javier", Age = 27 });
            //    _context.Users.Add(new User() { Name = "Josh", Age = 10 });
            //    _context.SaveChanges();
            //}
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get(CancellationToken token)
        {
            if (!_context.Users.Any())
                return NoContent();
            return await _context.Users.ToListAsync(token);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(long id, CancellationToken token)
        {
            try
            {
                return await _context.Users.FirstAsync(u => u.Id == id, token);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user, CancellationToken token)
        {
            if (user is null)
                return BadRequest();
            if (_context.Users.Contains(user))
                return Conflict();
            _context.Users.Add(user);
            await _context.SaveChangesAsync(token);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<User>> Put(User user, CancellationToken token)
        {
            try
            {
                var existingUser = await _context.Users.FirstAsync(u => u.Id == user.Id, token);
                _context.Users.Remove(existingUser);
                _context.Users.Add(user);
                await _context.SaveChangesAsync(token);
                return Ok(user);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(long id, CancellationToken token)
        {
            try
            {
                var userToDelete = await _context.Users.FirstAsync(u => u.Id == id, token);
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync(token);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NoContent();
            }
        }
    }
}