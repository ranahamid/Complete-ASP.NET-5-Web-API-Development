using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication5.IRepository;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
       // private readonly SignInManager<ApiUser> _signInManager;

        private readonly IUnitofWork _unitOfWork;
        // private readonly ILogger _logger;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManager, /*SignInManager<ApiUser> signInManager,*/ IUnitofWork unitOfWork, ILogger<AccountController> logger, IMapper mapper)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
       // [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            _logger.LogInformation($"Registration attempt for {userDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;

                var result = await _userManager.CreateAsync(user,userDto.Password);
                if (!result.Succeeded)
                { 
                    var errStr = "";
                    foreach (var error in result.Errors)
                    {
                        errStr = errStr + error.Description + ". ";
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    // return BadRequest($"User Registration attempt failed. {errStr}");
                    return BadRequest(ModelState);
                }

                foreach (var role in userDto.Roles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
                //return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }
        //[HttpPost]
        //[Route("Login")]
        //public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        //{
        //    _logger.LogInformation($"Login attempt for {loginDto.Email}");
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {

        //        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent: false, lockoutOnFailure: false);

        //        if (result.IsLockedOut)
        //        {
        //            return Unauthorized(loginDto);
        //        }
        //        if (result.IsNotAllowed)
        //        {
        //            return Unauthorized(loginDto);
        //        }
        //        if (result.RequiresTwoFactor)
        //        {
        //            return Unauthorized(loginDto);
        //        }
        //        return Accepted();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
        //        return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
        //        //return StatusCode(500, "Internal Server Error. Please try again later.");
        //    }
        //}


    }
}
