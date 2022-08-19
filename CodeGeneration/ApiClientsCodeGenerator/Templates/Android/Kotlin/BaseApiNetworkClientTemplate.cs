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
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    internal partial class BaseApiNetworkClientTemplate : BaseApiNetworkClientTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("package ");
            
            #line 3 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.Settings.ApiClientsPackage));
            
            #line default
            #line hidden
            this.Write(@".network

import java.io.*
import java.net.*
import java.util.*
import java.util.concurrent.*
import java.lang.reflect.*
import android.os.*
import kotlin.coroutines.*
import kotlin.reflect.full.*
import kotlinx.coroutines.*
import com.google.gson.*
import okhttp3.*
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.RequestBody.Companion.toRequestBody
import okhttp3.logging.HttpLoggingInterceptor
");
            
            #line 19 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"

	if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Reactive))
	{

            
            #line default
            #line hidden
            this.Write("import io.reactivex.rxjava3.core.Observable\r\nimport io.reactivex.rxjava3.schedule" +
                    "rs.*\r\n");
            
            #line 25 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"

	}

            
            #line default
            #line hidden
            this.Write("import ");
            
            #line 28 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.Settings.ApiClientsPackage));
            
            #line default
            #line hidden
            this.Write(".network.adapters.*\r\nimport ");
            
            #line 29 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.Settings.ApiClientsPackage));
            
            #line default
            #line hidden
            this.Write(".network.other.*\r\nimport ");
            
            #line 30 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(converter.Settings.ApiClientsPackage));
            
            #line default
            #line hidden
            this.Write(@".interfaces.*

open class BaseApiNetworkClient(val configuration: ApiNetworkClientConfiguration = ApiNetworkClientConfiguration.default) {
	companion object {
		private val commonHttpClient = OkHttpClient()

		private fun createSerializer() = GsonBuilder()
			.registerTypeAdapter(Date::class.java, UnixDateAdapter())
			.registerTypeAdapter(Date::class.createType(nullable = true).javaClass, UnixDateAdapter())
			.registerTypeAdapter(ByteArray::class.java, ByteArrayAdapter())
			.registerTypeAdapter(ByteArray::class.createType(nullable = true).javaClass, ByteArrayAdapter())
			.create()

		fun <T> serialize(value: T): String = createSerializer().toJson(value)

		fun <T> serialize(value: T, type: Type): String = createSerializer().toJson(value, type)

		fun <T> deserialize(s: String, type: Type): T = createSerializer().fromJson(s, type)
	}

");
            
            #line 50 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"

	if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Asynchronous))
	{

            
            #line default
            #line hidden
            this.Write("\tprotected suspend fun <TRequest, TResponse> makeRequest(method: String, relative" +
                    "Url: String, request: TRequest?, requestType: Type?, responseType: Type, accessT" +
                    "oken: String?): TResponse = withContext(Dispatchers.Default) {\r\n\t\tval networkReq" +
                    "uest = prepareNetworkRequest(method, relativeUrl, request, requestType, accessTo" +
                    "ken)\r\n\t\treturn@withContext suspendCoroutine<TResponse> { continuation ->\r\n\t\t\tcre" +
                    "ateHttpClient().newCall(networkRequest).enqueue(object : Callback {\r\n\t\t\t\toverrid" +
                    "e fun onFailure(call: Call, e: IOException) {\r\n\t\t\t\t\tcontinuation.resumeWithExcep" +
                    "tion(e)\r\n\t\t\t\t}\r\n\r\n\t\t\t\toverride fun onResponse(call: Call, response: Response) {\r" +
                    "\n\t\t\t\t\ttry {\r\n\t\t\t\t\t\tval result = processNetworkResponse<TResponse>(response, resp" +
                    "onseType)\r\n\t\t\t\t\t\tcontinuation.resume(result)\r\n\t\t\t\t\t} catch (ex: Exception) {\r\n\t\t" +
                    "\t\t\t\tcontinuation.resumeWithException(ex)\r\n\t\t\t\t\t}\r\n\t\t\t\t}\r\n\t\t\t})\r\n\t\t}\r\n\t}\r\n\r\n\tprot" +
                    "ected fun <TRequest, TResponse> makeRequest(method: String, relativeUrl: String," +
                    " request: TRequest?, requestType: Type?, responseType: Type, callback: RequestCa" +
                    "llback<TResponse>?, accessToken: String?) {\r\n\t\tval networkRequest = prepareNetwo" +
                    "rkRequest(method, relativeUrl, request, requestType, accessToken)\r\n\t\tcreateHttpC" +
                    "lient().newCall(networkRequest).enqueue(object : Callback {\r\n\t\t\toverride fun onF" +
                    "ailure(call: Call, e: IOException) {\r\n\t\t\t\texecuteCallbackOnMainThread(callback, " +
                    "null, e)\r\n\t\t\t}\r\n\r\n\t\t\toverride fun onResponse(call: Call, response: Response) {\r\n" +
                    "\t\t\t\ttry {\r\n\t\t\t\t\tval result = processNetworkResponse<TResponse>(response, respons" +
                    "eType)\r\n\t\t\t\t\texecuteCallbackOnMainThread(callback, result, null)\r\n\t\t\t\t} catch (e" +
                    "x: Exception) {\r\n\t\t\t\t\texecuteCallbackOnMainThread(callback, null, ex)\r\n\t\t\t\t}\r\n\t\t" +
                    "\t}\r\n\t\t})\r\n\t}\r\n\r\n");
            
            #line 92 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"

	}
	if (converter.Settings.ApiClientMethodTypes.HasFlag(ApiClientMethodType.Reactive))
	{

            
            #line default
            #line hidden
            this.Write(@"	protected fun <TRequest, TResponse> makeRequestRx(method: String, relativeUrl: String, request: TRequest?, requestType: Type?, responseType: Type, accessToken: String?): Observable<TResponse> where TResponse: Any {
		return Observable.fromCallable<TResponse> {
			makeRequestSync(method, relativeUrl, request, requestType, responseType, accessToken)
		}.subscribeOn(Schedulers.io())
	}

");
            
            #line 103 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"

	}
	if (converter.Settings.ApiClientMethodTypes != ApiClientMethodType.Asynchronous)
	{

            
            #line default
            #line hidden
            this.Write(@"	protected fun <TRequest, TResponse> makeRequestSync(method: String, relativeUrl: String, request: TRequest?, requestType: Type?, responseType: Type, accessToken: String?): TResponse {
		val networkRequest = prepareNetworkRequest(method, relativeUrl, request, requestType, accessToken)
		val networkResponse = createHttpClient().newCall(networkRequest).execute()
		return processNetworkResponse(networkResponse, responseType)
	}

");
            
            #line 114 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"

	}

            
            #line default
            #line hidden
            this.Write("\tprotected fun buildQueryAddress(pattern: String, queryParameters: List<Pair<Stri" +
                    "ng, String?>>): String {\r\n\t\tvar queryString = \"\"\r\n\t\tval segments = pattern.split" +
                    "(\'/\').toMutableList()\r\n\t\tfor ((name, value) in queryParameters) {\r\n\t\t\tif (value " +
                    "== null) {\r\n\t\t\t\tcontinue\r\n\t\t\t}\r\n\t\t\tval segmentIndex = segments.indexOfFirst { it" +
                    " == \"{${name}}\" }\r\n\t\t\tif (segmentIndex != -1) {\r\n\t\t\t\tsegments[segmentIndex] = UR" +
                    "LEncoder.encode(value, \"utf-8\")\r\n\t\t\t} else {\r\n\t\t\t\tqueryString += (if (queryStrin" +
                    "g.isEmpty()) \"?\" else \"&\") + URLEncoder.encode(name, \"utf-8\") + \"=\" + URLEncoder" +
                    ".encode(value, \"utf-8\")\r\n\t\t\t}\r\n\t\t}\r\n\t\treturn segments.joinToString(\"/\") + queryS" +
                    "tring\r\n\t}\r\n\r\n\tprivate fun <TRequest> prepareNetworkRequest(method: String, relat" +
                    "iveUrl: String, request: TRequest?, requestType: Type?, accessToken: String?): R" +
                    "equest {\r\n\t\tval mediaType = \"application/json; charset=utf-8\".toMediaType()\r\n\t\tv" +
                    "al requestBody = (if (request == null || requestType == null) \"\" else serialize(" +
                    "request, requestType)).toRequestBody(mediaType)\r\n\t\tvar builder = Request.Builder" +
                    "()\r\n\t\t\t.url(configuration.getRequestAddress(relativeUrl))\r\n\t\t\t.method(method, if" +
                    " (method == \"GET\" || method == \"DELETE\") null else requestBody)\r\n\t\t\t.cacheContro" +
                    "l(CacheControl.FORCE_NETWORK)\r\n\t\tif (accessToken != null) {\r\n\t\t\tbuilder = builde" +
                    "r.addHeader(\"Authorization\", \"Bearer $accessToken\")\r\n\t\t}\r\n\t\treturn builder.build" +
                    "()\r\n\t}\r\n\r\n\tprivate fun <TResponse> processNetworkResponse(response: Response, re" +
                    "sponseType: Type): TResponse {\r\n\t\tval responseBody = response.body?.string() ?: " +
                    "\"\"\r\n\t\ttry {\r\n\t\t\treturn deserialize(responseBody, responseType) ?: throw IllegalA" +
                    "rgumentException(\"Can not deserialize response body\")\r\n\t\t}\r\n\t\tcatch (ex: Excepti" +
                    "on) {\r\n\t\t\tif (!response.isSuccessful) {\r\n\t\t\t\tthrow HttpRequestException(response" +
                    ".request.url.toString(), response.code, responseBody)\r\n\t\t\t}\r\n\t\t\tthrow ex\r\n\t\t}\r\n\t" +
                    "}\r\n\r\n\tprivate fun <TResponse> executeCallbackOnMainThread(callback: RequestCallb" +
                    "ack<TResponse>?, response: TResponse?, ex: Exception?) {\r\n\t\tif (callback != null" +
                    ") {\r\n\t\t\tHandler(Looper.getMainLooper()).post { callback.handle(response, ex) }\r\n" +
                    "\t\t}\r\n\t}\r\n\r\n\tprivate fun createHttpClient() = commonHttpClient.newBuilder()\r\n\t\t.a" +
                    "ddInterceptor(HttpLoggingInterceptor().setLevel(if (configuration.enableLogging)" +
                    " HttpLoggingInterceptor.Level.BODY else HttpLoggingInterceptor.Level.NONE))\r\n\t\t." +
                    "connectTimeout(configuration.timeout, TimeUnit.SECONDS)\r\n\t\t.writeTimeout(configu" +
                    "ration.timeout, TimeUnit.SECONDS)\r\n\t\t.readTimeout(configuration.timeout, TimeUni" +
                    "t.SECONDS)\r\n\t\t.build()\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 173 "E:\reps\MyFfin\CodeGeneration\ApiClientsCodeGenerator\Templates\Android\Kotlin\BaseApiNetworkClientTemplate.tt"

	private KotlinAndroidConverter converter;
	internal BaseApiNetworkClientTemplate(KotlinAndroidConverter converter)
	{
		this.converter = converter;
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
    internal class BaseApiNetworkClientTemplateBase
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
