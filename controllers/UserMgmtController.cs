using Microsoft.AspNetCore.Mvc;
using UserManagement.MockData;
using UserManagement.Models;

namespace UserManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserMgmtController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UserMgmtController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet(Name = "GetAllUsers")]
    [Produces("application/json")]
    public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 50)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return BadRequest("Page number and page size must be greater than zero.");
        }
        var users = await _userRepository.GetAllUsers();
        if (users == null)
        {
            return NotFound($"Users List is Empty.");
        }

        var totalCount = users.Count;
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        var pagedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        var response = new
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Users = pagedUsers,
        };
        return Ok(response); //Task.FromResult<IActionResult>(Ok(result));
        // return Task.FromResult<ActionResult<IEnumerable<User>>>(_userRepository.Users);
    }

    [HttpGet("{id}", Name = "GetUserById")]
    [Produces("application/json")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound($"User with Id {id} not found.");
        }
        return Ok(user);
    }

    [HttpPost(Name = "CreateUser")]
    [Produces("application/json")]
    public async Task<ActionResult<User>> CreateUser([FromBody] User user)
    {
        var createdUser = await _userRepository.AddUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, createdUser);
    }

    [HttpPut("{id}", Name = "UpdateUser")]
    [Produces("application/json")]
    public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest("User ID in the URL does not match User ID in the body.");
        }

        var existingUser = await _userRepository.GetUserById(id);
        if (existingUser == null)
        {
            return NotFound($"User with Id {id} not found.");
        }

        var updatedUser = await _userRepository.UpdateUser(id, user);
        return Ok(updatedUser);
    }

    [HttpDelete("{id}", Name = "DeleteUser")]
    [Produces("application/json")]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
        var deletedUser = await _userRepository.DeleteUser(id);
        if (deletedUser == null)
        {
            return NotFound($"User with Id {id} not found.");
        }
        return Ok(deletedUser);
    }
}
