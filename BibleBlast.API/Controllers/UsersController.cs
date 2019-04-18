using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Dtos;
using BibleBlast.API.Helpers;
using BibleBlast.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleBlast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repo, UserManager<User> userManager, IMapper mapper)
        {
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PagedListParams queryParams)
        {
            queryParams.UserRoles = User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);

            var users = await _repo.GetUsers(queryParams);
            var dto = _mapper.Map<IEnumerable<UserDetail>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(dto);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id, true);

            var userDetail = _mapper.Map<UserDetail>(user);

            return Ok(userDetail);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateRequest updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }

            // todo check role
            var user = await _repo.GetUser(id, true);
            var currentRoles = await _userManager.GetRolesAsync(user);
            var currentRole = currentRoles.FirstOrDefault();

            _mapper.Map(updatedUser, user);

            bool roleChanged = updatedUser.UserRole != currentRole;
            if (roleChanged)
            {
                var result = await _userManager.AddToRoleAsync(user, updatedUser.UserRole);
                if (!result.Succeeded)
                {
                    return BadRequest("Failed to add to roles");
                }

                result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!result.Succeeded)
                {
                    return BadRequest("Failed to remove roles");
                }
            }

            if (await _repo.SaveAll() || roleChanged)
            {
                return NoContent();
            }

            throw new Exception("Updating user failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // todo check role
            var user = await _repo.GetUser(id, true);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
    }
}
