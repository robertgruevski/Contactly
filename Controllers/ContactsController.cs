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
		private readonly ContactlyDbContext dbContext;
		public ContactsController(ContactlyDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult GetAllContacts() => Ok(dbContext.Contacts.ToList());

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

			dbContext.Contacts.Add(domainModelContact);
			dbContext.SaveChanges();

			return Ok(domainModelContact);
		}

		[HttpDelete]
		[Route("{id:guid}")]
		public IActionResult DeleteContact(Guid id)
		{
			var contact = dbContext.Contacts.Find(id);
			if(contact is not null)
			{
				dbContext.Contacts.Remove(contact);
				dbContext.SaveChanges();
			}
			
			return Ok();
		}
	}
}
