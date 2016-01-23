#region

using HsrOrderApp.BL.DomainModel.HelperObjects;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;

#endregion

namespace HsrOrderApp.BL.DomainModel {
  public class SupplierProductCondition : DomainObject {
    public SupplierProductCondition() {
      this.SupplierProductConditionId = default(int);
      this.AverageLeadTime = null;
      this.StandardPrice = default(decimal);
      this.LastReceiptCost = null;
      this.LastReceiptDate = null;
      this.MinOrderQty = null;
      this.MaxOrderQty = null;
    }

    public int SupplierProductConditionId { get; set; }

    [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive)]
    public int? AverageLeadTime { get; set; }

    [RangeValidator(typeof(decimal), "0.0", RangeBoundaryType.Inclusive, "0.0", RangeBoundaryType.Ignore)]
    public decimal StandardPrice { get; set; }

    [RangeValidator(typeof(decimal), "0.0", RangeBoundaryType.Inclusive, "0.0", RangeBoundaryType.Ignore)]
    public decimal? LastReceiptCost { get; set; }

    public DateTime? LastReceiptDate { get; set; }

    [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive)]
    public int? MinOrderQty { get; set; }

    [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive)]
    public int? MaxOrderQty { get; set; }
  }
}