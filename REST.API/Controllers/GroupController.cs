using System;
using System.Diagnostics;
using System.Net;
using System.Web.Http;
using BL.DTO;
using BL.DTO.GroupDTOs;
using BL.Facades;
using Newtonsoft.Json;

namespace REST.API.Controllers
{
    public class GroupController :ApiController
    {
        public GroupFacade GroupFacade { get; set; }


        [System.Web.Mvc.Route("~/api/Group/{id}")]
        public IHttpActionResult Get(int id)
        {
            var result = id <= 0 ? null : GroupFacade.GetGroupById(id);

            return result == null
                ? (IHttpActionResult) NotFound()
                : Content(HttpStatusCode.OK, result);
        }

        public IHttpActionResult Get()
        {
            var result = GroupFacade.ListAllGroups();
            return result == null
                ? NotFound()
                : (IHttpActionResult) Content(HttpStatusCode.OK,result);
        }


        public IHttpActionResult Post([FromBody] GroupDTO group)
        {
            try
            {
                var tmp=GroupFacade.CreateNewGroup(group);
                group = GroupFacade.GetGroupById(tmp);
                return Content(HttpStatusCode.Created, group);
            }
            catch (JsonException)
            {
                Debug.WriteLine($"Failed to deserialize value to GroupDto: {group}");
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