using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.ApiClientsCodeGenerator
{
	[Flags]
	internal enum ApiClientMethodType
	{
		Asynchronous = 1,
		Synchronous = 1 << 1,
		Reactive = 1 << 2,
		All = Asynchronous | Synchronous | Reactive
	}
}
