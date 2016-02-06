#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel {
  public class Supplier : DomainObject {
    public Supplier() {
      this.SupplierId = default(int);
      this.Name = string.Empty;
      this.AccountNumber = default(int);
      this.CreditRating = null;
      this.PreferredSupplierFlag = false;
      this.ActiveFlag = true;
      this.PurchasingWebServiceURL = string.Empty;
      this.Address = null;
      this.SupplierProductConditions = new List<SupplierProductCondition>().AsQueryable();
    }

    public int SupplierId { get; set; }

    [StringLengthValidator(1, 50)]
    public string Name { get; set; }

    [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive)]
    public int AccountNumber { get; set; }

    [IgnoreNulls]
    [RangeValidator(1, RangeBoundaryType.Inclusive, 5, RangeBoundaryType.Inclusive)]
    public int? CreditRating { get; set; }

    public bool PreferredSupplierFlag { get; set; }

    public bool ActiveFlag { get; set; }

    [StringLengthValidator(1, 100)]
    public string PurchasingWebServiceURL { get; set; }

    public Address Address { get; set; }

    public IQueryable<SupplierProductCondition> SupplierProductConditions { get; set; }

    public override string ToString() {
      return this.Name;
    }
  }
}