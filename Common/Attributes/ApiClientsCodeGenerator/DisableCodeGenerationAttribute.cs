using System;

namespace Common.Attributes.ApiClientsCodeGenerator
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
	public class DisableCodeGenerationAttribute : Attribute
	{
	}
}
