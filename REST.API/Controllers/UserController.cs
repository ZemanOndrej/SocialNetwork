using System;
using System.Diagnostics;
using System.Net;
using System.Web.Http;
using BL.DTO.UserDTOs;
using BL.Facades;
using Newtonsoft.Json;

namespace REST.API.Controllers
{
	public class UserController : ApiController
	{
		public UserFacade UserFacade { get; set; }


		[System.Web.Mvc.Route("~/api/Account/{id}")]
		public IHttpActionResult Get(int id)
		{
			var result = id <= 0 ? null : UserFacade.GetUserById(id);

			return result == null
				? (IHttpActionResult) NotFound()
				: Content(HttpStatusCode.OK, result);
		}

		public IHttpActionResult Get()
		{
			var result = UserFacade.ListAllUsers();
			return result == null
				? NotFound()
				: (IHttpActionResult) Content(HttpStatusCode.OK, result);
		}

		public IHttpActionResult Post([FromBody] AccountDTO account)
		{
			try
			{
				var tmp = UserFacade.CreateNewUser(account);
				account = UserFacade.GetUserById(tmp);
				return Content(HttpStatusCode.Created, account);
			}
			catch (JsonException)
			{
				Debug.WriteLine($"Failed to deserialize value to GroupDto: {account}");
				return StatusCode(HttpStatusCode.BadRequest);
			}
			catch (NullReferenceException ex)
			{
				Debug.WriteLine(ex.Message);
				return StatusCode(HttpStatusCode.NotFound);
			}
		}
	}
}