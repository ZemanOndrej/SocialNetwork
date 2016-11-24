using System;
using System.Diagnostics;
using System.Net;
using System.Web.Http;
using BL.DTO;
using BL.Facades;
using Newtonsoft.Json;

namespace REST.API.Controllers
{
	public class UserController : ApiController
	{
		public UserFacade UserFacade { get; set; }


		[System.Web.Mvc.Route("~/api/User/{id}")]
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
				: (IHttpActionResult) Content(HttpStatusCode.OK,result);
		}

	    public IHttpActionResult Post([FromBody] UserDTO user)
	    {
	        try
	        {
	            var tmp=UserFacade.CreateNewUser(user);
	            user = UserFacade.GetUserById(tmp);
	            return Content(HttpStatusCode.Created, user);
	        }
	        catch (JsonException)
	        {
	            Debug.WriteLine($"Failed to deserialize value to GroupDto: {user}");
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
