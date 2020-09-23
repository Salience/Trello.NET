using System;
using System.Net;

namespace TrelloNet
{
	public class TrelloException : Exception
	{
		public TrelloException(HttpStatusCode responseStatusCode, string responseContent) : base(responseContent)
        {
            this.ResponseStatusCode = responseStatusCode;
            this.ResponseContent = responseContent;
        }

        public HttpStatusCode ResponseStatusCode { get; }
        public string ResponseContent { get; }
	}
}