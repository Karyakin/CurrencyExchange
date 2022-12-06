using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApi.Entities.Entities;
using TestApi.Utils.Helpers.Interfaces;

namespace TestApi.Controllers;
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class UserController: ControllerBase
{
    private readonly IUserHelper _userHelper;//(Repository pattern)

    public UserController( IUserHelper userHelper)
    {
        _userHelper = userHelper;
    }

    /// <summary>
    /// Получение пользователей по диапазону записей. Всего 100
    /// </summary>
    /// <remarks>
    /// Simple parameters:
    /// 
    ///     minRecord: 5(enable 0-99)
    ///     maxRecord: 100(enable 1-100)
    /// Simple Request:
    /// 
    ///     http://localhost:7000/api/User/getUsers
    ///     or
    ///     http://localhost:7000/api/User/getUsers?minRecord=11&amp;maxRecord=3
    /// Remark:
    /// 
    ///     AllowAnonymous
    /// </remarks>
    /// <param name="minRecord">Минимальная запись</param>
    /// <param name="maxRecord">Максимальная запись</param>
    /// <response code="200">Return list users</response>
    /// <response code="400">BadRequest. incorrect input parameters range</response>
    /// <response code="500">Threw exception</response>
    [HttpGet("getUsers")]
    [AllowAnonymous]
    public async Task<ActionResult<List<User>>> GetUsers(
       [FromQuery] [Range(0,99, ErrorMessage = "Недопустимый диапазон значений(доступно: 0-99)")]int minRecord, 
       [FromQuery] [Range(1,100, ErrorMessage = "Недопустимый диапазон значений(доступно: 1-100)")]int maxRecord)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(await _userHelper.GetUsers(new Range(minRecord,maxRecord)));
    }
}