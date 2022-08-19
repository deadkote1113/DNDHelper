using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Enums;
using Common.Attributes.ApiClientsCodeGenerator;

namespace Api.Responses
{
    [GenericArgumentsNotNull]
    public class ResponseWrapper<T>
    {
        public OperationStatus OperationStatus { get; set; }

        public T ResponseData { get; set; }

        public ResponseWrapper()
        {
        }

        public ResponseWrapper(OperationStatus operationStatus)
        {
	        OperationStatus = operationStatus;
        }
    }
}