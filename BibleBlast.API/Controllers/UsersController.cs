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
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Coach,Admin")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateRequest updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }

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

            await _repo.SaveAll();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Coach,Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repo.GetUser(id, true);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            var currentUserRole = User.FindFirstValue(ClaimTypes.Role);
            int currentUserOrganizationId = int.Parse(User.FindFirstValue("organizationId"));

            if (currentUserRole != UserRoles.Admin && currentUserOrganizationId != user.OrganizationId)
            {
                return Unauthorized("User does not belong to your organization");
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
