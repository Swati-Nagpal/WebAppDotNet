using InsuranceService.Infrastructure.Context;
using JWTWebAuthentication.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserLoginController : ControllerBase
    {
        SqlConnection conn;
        private readonly IConfiguration _configuration;
        private readonly IDbContextFactory<SqlServerContext> _contextFactory;
        private readonly IJWTManagerRepository _jWTManager;


        private readonly ILogger<UserLoginController> _logger;

        public UserLoginController(ILogger<UserLoginController> logger,
                                IConfiguration configuration,
                                IDbContextFactory<SqlServerContext> contextFactory,
                                IJWTManagerRepository jWTManager)
        {
            _logger = logger;
            _configuration = configuration;
            _contextFactory = contextFactory;
            _jWTManager = jWTManager;
        }

        [HttpPost, Route("/login")]
        public async Task<Tokens> LoginAsync(User user)
        {
            try
            {
                if (user != null
                    && !string.IsNullOrEmpty(user.Username)
                    && !string.IsNullOrEmpty(user.Password)
                    && !string.IsNullOrEmpty(user.Email))
                {
                    //var conn = new SqlConnection(_configuration["ConnectionStrings:sql-conn"]);
                    //using (conn)
                    //{
                        var username = new SqlParameter("@username", user.Username);
                        var password = new SqlParameter("@password", user.Password);
                        var email = new SqlParameter("@email", user.Email);

                        using var context = _contextFactory.CreateDbContext();
                        var result = (context.Database.ExecuteSqlRaw("[dbo].[SP_SaveUser] @username, @password, @email", username, password, email));

                        var token = _jWTManager.GenerateToken(user);

                        return token;
                    //}
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet, Route("/users")]
        public IActionResult GetUsers(User userdata)
        {
            var token = _jWTManager.GenerateToken(userdata);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }

}
