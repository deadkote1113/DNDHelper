using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace CodeGeneration.ApiClientsCodeGenerator.Converters
{
	internal abstract class BaseConverter
	{
		internal ILogger Logger { get; }

		protected BaseConverter(ILogger logger)
		{
			Logger = logger;
		}

		public abstract void CreateAuxiliaryFiles(IEnumerable<Type> types);

		public abstract void CreateResources(IEnumerable<Type> types);

		protected abstract void ConvertEnum(Type type);

		protected abstract void ConvertClass(Type type);

		protected abstract void ConvertController(Type type);

		public virtual void Convert(IEnumerable<Type> types)
		{
			var typesList = types.ToList();
			CreateAuxiliaryFiles(typesList);
			CreateResources(typesList);
			foreach (var type in typesList)
			{
				if (type.IsEnum)
				{
					ConvertEnum(type);
					continue;
				}
				if (type.IsApiController())
				{
					ConvertController(type);
					continue;
				}
				ConvertClass(type);
			}
		}
	}

	internal abstract class BaseConverter<T> : BaseConverter
	{
		public T Settings { get; set; }

		protected BaseConverter(T settings, ILogger logger) : base(logger)
		{
			Settings = settings;
		}
	}
}
