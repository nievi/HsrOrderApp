﻿#region

using HsrOrderApp.BL.DomainModel.HelperObjects;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace HsrOrderApp.BL.DomainModel {
  public class Product : DomainObject {
    public Product() {
      this.ProductId = default(int);
      this.Name = string.Empty;
      this.ProductNumber = string.Empty;
      this.Category = string.Empty;
      this.ListUnitPrice = default(decimal);
      this.QuantityPerUnit = default(double);
      this.SupplierProductCondtions = new List<SupplierProductCondition>().AsQueryable();
    }

    public int ProductId { get; set; }

    [StringLengthValidator(1, 50)]
    public string Name { get; set; }

    [StringLengthValidator(1, 25)]
    public string ProductNumber { get; set; }

    [StringLengthValidator(1, 50)]
    public string Category { get; set; }

    [RangeValidator(0, RangeBoundaryType.Exclusive, double.MaxValue, RangeBoundaryType.Ignore)]
    public double QuantityPerUnit { get; set; }

    [RangeValidator(typeof(decimal), "0.0", RangeBoundaryType.Inclusive, "0.0", RangeBoundaryType.Ignore)]
    public decimal ListUnitPrice { get; set; }

    [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive)]
    public int UnitsOnStock { get; set; }

    public IQueryable<SupplierProductCondition> SupplierProductCondtions { get; set; }
  }
}