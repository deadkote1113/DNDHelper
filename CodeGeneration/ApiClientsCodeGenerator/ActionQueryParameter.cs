using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.ApiClientsCodeGenerator
{
	internal class ActionQueryParameter
	{
		public string Name { get; set; }

		public IEnumerable<PathItem> Path { get; set; }

		public Type Type { get; set; }

		public ActionQueryParameter(string name, IEnumerable<PathItem> path, Type type)
		{
			Name = name;
			Path = path;
			Type = type;
		}

		public ActionQueryParameter(string name, PathItem path, Type type)
		{
			Name = name;
			Path = new[] { path };
			Type = type;
		}

		public struct PathItem
		{
			public string Name { get; set; }
			public bool CanBeNull { get; set; }

			public PathItem(string name, bool canBeNull)
			{
				Name = name;
				CanBeNull = canBeNull;
			}
		}
	}
}
