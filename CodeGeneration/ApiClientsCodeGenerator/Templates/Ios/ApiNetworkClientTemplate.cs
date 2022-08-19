﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CodeGeneration.ApiClientsCodeGenerator.Templates.Ios
{
    using CodeGeneration.ApiClientsCodeGenerator.Converters.Ios;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    internal partial class ApiNetworkClientTemplate : ApiNetworkClientTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 5 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

	if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Reactive))
	{

            
            #line default
            #line hidden
            this.Write("import RxSwift\r\n\r\n");
            
            #line 11 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

	}

            
            #line default
            #line hidden
            this.Write("public class ");
            
            #line 14 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(type.GetControllerName()));
            
            #line default
            #line hidden
            this.Write("ApiNetworkClient: BaseApiNetworkClient, ");
            
            #line 14 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(type.GetControllerName()));
            
            #line default
            #line hidden
            this.Write("ApiClient {\r\n");
            
            #line 15 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

	var methods = type.GetControllerActions();
	for (var i = 0; i < methods.Count; ++i)
	{
		var parametersList = methods[i].GetParameters().Select(item => item.Name + ": "+ converter.GetTypeName(item.ParameterType, item.CanBeNull(), item.ParameterType.CanGenericArgumentsBeNull())).ToList();
		parametersList.Add("accessToken: String" + (methods[i].IsAuthorizationRequired() ? "" : "?"));
		var bodyParameter = methods[i].GetActionBodyParameter();
		var bodyParameterTypeName = bodyParameter != null ? converter.GetTypeName(bodyParameter.ParameterType, false, bodyParameter.ParameterType.CanGenericArgumentsBeNull()) : null;
		var queryParameters = methods[i].GetActionQueryParameters();
		var responseTypeName = converter.GetTypeName(methods[i].GetActionResponseType(), true, methods[i].GetActionResponseType().CanGenericArgumentsBeNull());
		var address = queryParameters.Any() ? $"buildQueryAddress(pattern: \"{methods[i].GetActionUrl()}\", queryParameters: queryParameters)" : $"\"{methods[i].GetActionUrl()}\"";
		if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Asynchronous))
		{

            
            #line default
            #line hidden
            this.Write("\tpublic func ");
            
            #line 29 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Async(");
            
            #line 29 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(", ", parametersList)));
            
            #line default
            #line hidden
            this.Write(", callback: ((");
            
            #line 29 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write(", Error?) -> Void)?) {\r\n");
            
            #line 30 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

			if (queryParameters.Any()) 
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 34 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.GetActionQueryParametersString(queryParameters).Replace("\r\n", "\r\n\t\t")));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 35 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

			}

            
            #line default
            #line hidden
            this.Write("\t\tmakeRequestAsync(method: \"");
            
            #line 38 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].GetActionHttpMethod()));
            
            #line default
            #line hidden
            this.Write("\", address: ");
            
            #line 38 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(address));
            
            #line default
            #line hidden
            this.Write(", request: ");
            
            #line 38 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameter != null ? bodyParameter.Name : "Optional<Int>.none"));
            
            #line default
            #line hidden
            this.Write(", accessToken: accessToken, callback: callback)\r\n\t}");
            
            #line 39 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.Settings.ApiClientMethodTypes != ApiClientMethodType.Asynchronous ? "\r\n" : ""));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 40 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

		}
		if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Reactive))
		{

            
            #line default
            #line hidden
            this.Write("\tpublic func ");
            
            #line 45 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Rx(");
            
            #line 45 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(", ", parametersList)));
            
            #line default
            #line hidden
            this.Write(") -> Observable<");
            
            #line 45 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write("> {\r\n");
            
            #line 46 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

			if (queryParameters.Any()) 
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 50 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.GetActionQueryParametersString(queryParameters).Replace("\r\n", "\r\n\t\t")));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 51 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

			}

            
            #line default
            #line hidden
            this.Write("\t\treturn makeRequestRx(method: \"");
            
            #line 54 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].GetActionHttpMethod()));
            
            #line default
            #line hidden
            this.Write("\", address: ");
            
            #line 54 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(address));
            
            #line default
            #line hidden
            this.Write(", request: ");
            
            #line 54 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameter != null ? bodyParameter.Name : "Optional<Int>.none"));
            
            #line default
            #line hidden
            this.Write(", accessToken: accessToken)\r\n\t}");
            
            #line 55 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Synchronous) ? "\r\n" : ""));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 56 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

		}
		if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Synchronous))
		{

            
            #line default
            #line hidden
            this.Write("\tpublic func ");
            
            #line 61 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Sync(");
            
            #line 61 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(", ", parametersList)));
            
            #line default
            #line hidden
            this.Write(") throws -> ");
            
            #line 61 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write(" {\r\n");
            
            #line 62 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

			if (queryParameters.Any()) 
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 66 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.GetActionQueryParametersString(queryParameters).Replace("\r\n", "\r\n\t\t")));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 67 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

			}

            
            #line default
            #line hidden
            this.Write("\t\treturn try makeRequestSync(method: \"");
            
            #line 70 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].GetActionHttpMethod()));
            
            #line default
            #line hidden
            this.Write("\", address: ");
            
            #line 70 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(address));
            
            #line default
            #line hidden
            this.Write(", request: ");
            
            #line 70 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameter != null ? bodyParameter.Name : "Optional<Int>.none"));
            
            #line default
            #line hidden
            this.Write(", accessToken: accessToken)\r\n\t}\r\n");
            
            #line 72 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

		}
		if (i < methods.Count - 1) 
		{

            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 78 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

		}

            
            #line default
            #line hidden
            
            #line 81 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

	}

            
            #line default
            #line hidden
            this.Write("}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 85 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Ios\ApiNetworkClientTemplate.tt"

	private IosConverter converter;
	private Type type;
	internal ApiNetworkClientTemplate(IosConverter converter, Type type)
	{
		this.converter = converter;
		this.type = type;
	}

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    internal class ApiNetworkClientTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
