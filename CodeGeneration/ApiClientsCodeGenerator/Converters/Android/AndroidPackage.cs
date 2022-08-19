using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.ApiClientsCodeGenerator.Converters.Android
{
	internal class AndroidPackage
	{
		private List<string> pathItems;

		public AndroidPackage(IEnumerable<string> pathItems)
		{
			this.pathItems = pathItems.Select(item => item.ToLowerInvariant()).ToList();
		}

		public AndroidPackage(string package)
		{
			pathItems = package.ToLowerInvariant().Split(new[] {"."}, StringSplitOptions.RemoveEmptyEntries).ToList();
		}

		public override string ToString()
		{
			return string.Join(".", pathItems);
		}

		public IList<string> GetItems()
		{
			return pathItems.ToList();
		}

		public AndroidPackage Append(AndroidPackage trailingPackage)
		{
			var result = new AndroidPackage(pathItems);
			result.pathItems.AddRange(trailingPackage.pathItems);
			return result;
		}
	}
}
