using BL.DTO.Filters;
using BL.DTO.Request;

namespace BL.Services.Request
{
	public interface IRequestService
	{
		RequestListQueryResultDTO ListRequests(RequestFilter filter, int page = 0);
		RequestDTO GetRequestById(int id);

		void DeleteRequest(RequestDTO request);

		int SendRequest(RequestDTO request);
	}
}