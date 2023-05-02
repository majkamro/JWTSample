using JWTSample.Database.Context;
using JWTSample.Database.Entity;
using JWTSample.Models;
using JWTSample.Services;
using JWTSample.Utilities.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWTSample.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HomeController : ControllerBase
	{
		private readonly JWTSampleContext db;

		public HomeController(JWTSampleContext db)
		{
			this.db = db;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (await db.Users.AnyAsync(x => x.Username == model.Username))
			{
				return Conflict();
			}

			var user = new User
			{
				Username = model.Username,
				passwordHash = model.Password.GetHash(),
				FullName = model.FullName,
				Role = model.Role,
				UId = Guid.NewGuid().ToString("00000000"),
			};

			await db.Users.AddAsync(user);
			await db.SaveChangesAsync();


			return Ok("Success");
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> Login(string username, string password)
		{
			var user = await db.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
			if (user == null)
			{
				return NotFound();
			}

			if (user.passwordHash != password.GetHash())
			{
				return Conflict();
			}

			var result = "AccessToken: " + TokenService.GenerateToken(user) + " " + "RefreshToken: " + TokenService.GenerateRefreshToken(user);
			return Ok(result);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetCategory()
		{
			var categories = await db.Categories.ToListAsync();

			return Ok(categories);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> AddCategory(string title)
		{
			if (await db.Categories.AnyAsync(x => x.Title == title))
			{
				return Conflict();
			}

			var category = new Category { Title = title };

			await db.Categories.AddAsync(category);
			await db.SaveChangesAsync();
			return Ok();
		}
	}
}