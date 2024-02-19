namespace Tools.Finances.Ckassa.Responses
{
	public class ResponseWrapper<TResponseData, TError> where TError: Error, new()
	{
		public TResponseData Data { get; set; }

		public TError Error { get; set; }
	}
}
