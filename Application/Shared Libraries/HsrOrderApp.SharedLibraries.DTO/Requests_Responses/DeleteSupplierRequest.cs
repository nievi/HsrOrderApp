﻿#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    [DataContract]
    public class DeleteSupplierRequest : RequestType
    {
        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Exclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int SupplierId { get; set; }
    }
}