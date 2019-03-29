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
            var user = await _repo.GetUser(id, true);

            _mapper.Map(updatedUser, user);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating user failed on save");
        }
    }
}
