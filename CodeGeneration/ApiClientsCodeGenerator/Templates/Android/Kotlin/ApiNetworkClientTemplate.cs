﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CodeGeneration.ApiClientsCodeGenerator.Templates.Android.Kotlin
{
    using CodeGeneration.ApiClientsCodeGenerator.Converters.Android;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    internal partial class ApiNetworkClientTemplate : ApiNetworkClientTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("package ");
            
            #line 5 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.GetPackage(type)));
            
            #line default
            #line hidden
            this.Write(".network\r\n\r\n");
            
            #line 7 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

	foreach (var item in imports)
	{

            
            #line default
            #line hidden
            this.Write("import ");
            
            #line 11 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(item));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 12 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

	}

            
            #line default
            #line hidden
            this.Write("\r\nclass ");
            
            #line 16 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(type.GetControllerName()));
            
            #line default
            #line hidden
            this.Write("ApiNetworkClient(configuration: ApiNetworkClientConfiguration = ApiNetworkClientC" +
                    "onfiguration.default) : BaseApiNetworkClient(configuration), ");
            
            #line 16 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(type.GetControllerName()));
            
            #line default
            #line hidden
            this.Write("ApiClient {\r\n");
            
            #line 17 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

	var methods = type.GetControllerActions();
	for (var i = 0; i < methods.Count; ++i)
	{
		var parametersList = methods[i].GetParameters().Select(item => item.Name + ": "+ converter.GetTypeName(item.ParameterType, item.CanBeNull(), item.ParameterType.CanGenericArgumentsBeNull())).ToList();
		parametersList.Add("accessToken: String" + (methods[i].IsAuthorizationRequired() ? "" : "?"));
		var bodyParameter = methods[i].GetActionBodyParameter();
		var bodyParameterTypeName = bodyParameter != null ? converter.GetTypeName(bodyParameter.ParameterType, false, bodyParameter.ParameterType.CanGenericArgumentsBeNull()) : null;
		var queryParameters = methods[i].GetActionQueryParameters();
		var responseTypeName = converter.GetTypeName(methods[i].GetActionResponseType(), false, methods[i].GetActionResponseType().CanGenericArgumentsBeNull());
		var address = queryParameters.Any() ? $"buildQueryAddress(\"{methods[i].GetActionUrl()}\", queryParameters)" : $"\"{methods[i].GetActionUrl()}\"";
		if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Asynchronous))
		{

            
            #line default
            #line hidden
            this.Write("\toverride suspend fun ");
            
            #line 31 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 31 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(", ", parametersList)));
            
            #line default
            #line hidden
            this.Write("): ");
            
            #line 31 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write(" {\r\n");
            
            #line 32 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			if (queryParameters.Any()) 
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 36 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.GetActionQueryParametersString(queryParameters).Replace("\r\n", "\r\n\t\t")));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 37 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			}
			if (bodyParameter != null) 
			{

            
            #line default
            #line hidden
            this.Write("\t\tval requestType = object : TypeToken<");
            
            #line 42 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameterTypeName));
            
            #line default
            #line hidden
            this.Write(">() { }.type\r\n");
            
            #line 43 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			}

            
            #line default
            #line hidden
            this.Write("\t\tval responseType = object : TypeToken<");
            
            #line 46 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write(">() { }.type\r\n\t\treturn makeRequest(\"");
            
            #line 47 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].GetActionHttpMethod()));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 47 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(address));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 47 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameter != null ? bodyParameter.Name + ", requestType" : "null, null"));
            
            #line default
            #line hidden
            this.Write(", responseType, accessToken)\r\n\t}\r\n\r\n\toverride fun ");
            
            #line 50 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 50 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(", ", parametersList)));
            
            #line default
            #line hidden
            this.Write(", callback: RequestCallback<");
            
            #line 50 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write(">?) {\r\n");
            
            #line 51 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			if (queryParameters.Any()) 
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 55 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.GetActionQueryParametersString(queryParameters).Replace("\r\n", "\r\n\t\t")));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 56 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			}
			if (bodyParameter != null) 
			{

            
            #line default
            #line hidden
            this.Write("\t\tval requestType = object : TypeToken<");
            
            #line 61 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameterTypeName));
            
            #line default
            #line hidden
            this.Write(">() { }.type\r\n");
            
            #line 62 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			}

            
            #line default
            #line hidden
            this.Write("\t\tval responseType = object : TypeToken<");
            
            #line 65 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write(">() { }.type\r\n\t\treturn makeRequest(\"");
            
            #line 66 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].GetActionHttpMethod()));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 66 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(address));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 66 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameter != null ? bodyParameter.Name + ", requestType" : "null, null"));
            
            #line default
            #line hidden
            this.Write(", responseType, callback, accessToken)\r\n\t}");
            
            #line 67 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.Settings.ApiClientMethodTypes != ApiClientMethodType.Asynchronous ? "\r\n" : ""));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 68 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

		}
		if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Reactive))
		{

            
            #line default
            #line hidden
            this.Write("\toverride fun ");
            
            #line 73 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Rx(");
            
            #line 73 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(", ", parametersList)));
            
            #line default
            #line hidden
            this.Write("): Observable<");
            
            #line 73 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write("> {\r\n");
            
            #line 74 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			if (queryParameters.Any()) 
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 78 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.GetActionQueryParametersString(queryParameters).Replace("\r\n", "\r\n\t\t")));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 79 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			}
			if (bodyParameter != null) 
			{

            
            #line default
            #line hidden
            this.Write("\t\tval requestType = object : TypeToken<");
            
            #line 84 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameterTypeName));
            
            #line default
            #line hidden
            this.Write(">() { }.type\r\n");
            
            #line 85 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			}

            
            #line default
            #line hidden
            this.Write("\t\tval responseType = object : TypeToken< ");
            
            #line 88 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write(">() { }.type\r\n\t\treturn makeRequestRx(\"");
            
            #line 89 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].GetActionHttpMethod()));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 89 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(address));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 89 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameter != null ? bodyParameter.Name + ", requestType" : "null, null"));
            
            #line default
            #line hidden
            this.Write(", responseType, accessToken)\r\n\t}");
            
            #line 90 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Synchronous) ? "\r\n" : ""));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 91 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

		}
		if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Synchronous))
		{

            
            #line default
            #line hidden
            this.Write("\toverride fun ");
            
            #line 96 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Sync(");
            
            #line 96 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(", ", parametersList)));
            
            #line default
            #line hidden
            this.Write("): ");
            
            #line 96 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write(" {\r\n");
            
            #line 97 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			if (queryParameters.Any()) 
			{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 101 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.GetActionQueryParametersString(queryParameters).Replace("\r\n", "\r\n\t\t")));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 102 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			}
			if (bodyParameter != null) 
			{

            
            #line default
            #line hidden
            this.Write("\t\tval requestType = object : TypeToken<");
            
            #line 107 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameterTypeName));
            
            #line default
            #line hidden
            this.Write(">() { }.type\r\n");
            
            #line 108 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

			}

            
            #line default
            #line hidden
            this.Write("\t\tval responseType = object : TypeToken< ");
            
            #line 111 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(responseTypeName));
            
            #line default
            #line hidden
            this.Write(">() { }.type\r\n\t\treturn makeRequestSync(\"");
            
            #line 112 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(methods[i].GetActionHttpMethod()));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 112 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(address));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 112 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bodyParameter != null ? bodyParameter.Name + ", requestType" : "null, null"));
            
            #line default
            #line hidden
            this.Write(", responseType, accessToken)\r\n\t}\r\n");
            
            #line 114 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

		}
		if (i < methods.Count - 1) 
		{

            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 120 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

		}
	}

            
            #line default
            #line hidden
            this.Write("}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 125 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\ApiNetworkClientTemplate.tt"

	private KotlinAndroidConverter converter;
	private Type type;
	private List<string> imports;
	internal ApiNetworkClientTemplate(KotlinAndroidConverter converter, Type type, IEnumerable<string> imports) 
	{
		this.converter = converter;
		this.type = type;
		this.imports = new List<string>
		{
			"java.util.*", "java.math.*", "java.lang.*", "java.text.*", "com.google.gson.reflect.*"
		};
		if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Reactive))
		{
			this.imports.Add("io.reactivex.rxjava3.core.Observable");
		}
		this.imports.Add(converter.Settings.ApiClientsPackage + ".interfaces.*");
		this.imports.AddRange(imports);
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
