namespace TrelloNet
{
	public class NewAttachment
	{
        //TODO Add File attribute to match Trello API
        //see https://trello.com/docs/api/card/index.html#post-1-cards-card-id-or-shortlink-attachments

		/// <summary>
		/// A string with a length from 1 to 256 (optional).
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A string with a length from 1 to 16384 (optional).
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// A string with a length from 1 to 256 (optional).
		/// </summary>
		public string MimeType { get; set; }

		/// <param name="name">A string with a length from 1 to 256.</param>
		/// <param name="url">A string with a length from 1 to 16384.</param>
		/// <param name="mimeType">A string with a length from 1 to 256.</param>
		public NewAttachment(string name, string url, string mimeType)
		{
			Name = name;
            Url = url;
		    MimeType = mimeType;
		}
	}
}