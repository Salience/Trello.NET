using System.Net;

namespace TrelloNet
{
	public class TrelloUnauthorizedException : TrelloException
	{
		public TrelloUnauthorizedException(string message) : base(HttpStatusCode.Unauthorized, message)
		{			
		}
	}
}