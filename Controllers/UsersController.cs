using ArtechApi.Models;
using ArtechApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtechApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var allUsers = await _userService.GetAsync();
        if (allUsers.Any()) return Ok(allUsers);

        return NotFound();
    }
    
    // Get single user
    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        var targetUser = await _userService.GetAsync(id);
        if (targetUser is null) return NotFound(); // null check

        return Ok(targetUser);
    }

    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
        await _userService.CreateAsync(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Put(string id, User user)
    {
        var targetUser = await _userService.GetAsync(id);
        if (targetUser is null) return BadRequest();

        user.Id = targetUser.Id;

        await _userService.UpdateAsync(user); // update full record

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var targetUser = await _userService.GetAsync(id);
        if (targetUser is null) return BadRequest();

        await _userService.RemoveAsync(id);

        return NoContent();
    }

}