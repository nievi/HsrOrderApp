#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO {
  [DataContract]
  public class SupplierListDTO : DTOBase {
    public SupplierListDTO() {
      this.Name = string.Empty;
      this.AccountNumber = default(int);
      this.CreditRating = null;
      this.PreferredSupplierFlag = false;
      this.ActiveFlag = true;
      this.PurchasingWebServiceURL = string.Empty;
      this.Address = null;
    }

    [DataMember]
    [StringLengthValidator(1, 50)]
    public string Name { get; set; }

    [DataMember]
    [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive)]
    public int AccountNumber { get; set; }

    [DataMember]
    [RangeValidator(1, RangeBoundaryType.Inclusive, 5, RangeBoundaryType.Inclusive)]
    public int? CreditRating { get; set; }

    [DataMember]
    public bool PreferredSupplierFlag { get; set; }

    [DataMember]
    public bool ActiveFlag { get; set; }

    [DataMember]
    [StringLengthValidator(1, 100)]
    public string PurchasingWebServiceURL { get; set; }

    [DataMember]
    public AddressDTO Address { get; set; }

    public string FullName {
      get {
        return Name + " (" + AccountNumber + ")";
      }
    }
  }
}