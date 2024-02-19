using Tools.Finances.Sberbank.Responses.Models;

namespace Tools.Finances.Sberbank.Responses
{
	/// <summary>
	/// Базовый класс для всех классов, описывающих ответы платежного шлюза на стандартные запросы к API Сбербанка.
	/// </summary>
	/// <inheritdoc />
	public abstract class DefaultBaseResponse : BaseResponse
	{
		/// <summary>
		/// Код ошибки.
		/// </summary>
		public int ErrorCode { get; set; }

		/// <summary>
		/// Описание ошибки.
		/// </summary>
		public string ErrorMessage { get; set; }

		public override ErrorInfo GetErrorInfo()
		{
			return new ErrorInfo
			{
				Code = ErrorCode,
				Description = null,
				Message = ErrorMessage
			};
		}
	}
}
