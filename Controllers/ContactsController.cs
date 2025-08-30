using Contactly.Data;
using Contactly.Models;
using Contactly.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Contactly.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController : ControllerBase
	{
		private readonly ContactlyDbContext _dbContext;
		public ContactsController(ContactlyDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult GetAllContacts() => Ok(_dbContext.Contacts.ToList());

		[HttpPost]
		public IActionResult AddContact(AddContactDto contactDto)
		{
			Contact domainModelContact = new()
			{
				Id = Guid.NewGuid(),
				Name = contactDto.Name,
				Email = contactDto.Email,
				Phone = contactDto.Phone,
				Favorite = contactDto.Favorite
			};

			_dbContext.Contacts.Add(domainModelContact);
			_dbContext.SaveChanges();

			return Ok(domainModelContact);
		}
	}
}
