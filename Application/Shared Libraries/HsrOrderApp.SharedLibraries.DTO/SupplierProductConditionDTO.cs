#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO {
  [DataContract]
  public class SupplierProductConditionDTO : DTOVersionObject {
    private int? _averageLeadTime;
    private decimal _standardPrice;
    private decimal? _lastReceiptCost;
    private DateTime? _lastReceiptDate;
    private int? _minOrderQty;
    private int? _maxOrderQty;

    public SupplierProductConditionDTO() {
      this.AverageLeadTime = null;
      this.StandardPrice = default(decimal);
      this.LastReceiptCost = null;
      this.LastReceiptDate = null;
      this.MinOrderQty = null;
      this.MaxOrderQty = null;
    }


    [DataMember]
    public int? AverageLeadTime {
      get { return _averageLeadTime; }
      set {
        if (value != _averageLeadTime) {
          this._averageLeadTime = value;
          OnPropertyChanged(() => AverageLeadTime);
        }
      }
    }

    [DataMember]
    [NotNullValidator]
    public decimal StandardPrice {
      get { return _standardPrice; }
      set {
        if (value != _standardPrice) {
          this._standardPrice = value;
          OnPropertyChanged(() => StandardPrice);
        }
      }
    }

    [DataMember]
    public decimal? LastReceiptCost {
      get { return _lastReceiptCost; }
      set {
        if (value != _lastReceiptCost) {
          this._lastReceiptCost = value;
          OnPropertyChanged(() => LastReceiptCost);
        }
      }
    }

    [DataMember]
    public DateTime? LastReceiptDate {
      get { return _lastReceiptDate; }
      set {
        if (value != _lastReceiptDate) {
          this._lastReceiptDate = value;
          OnPropertyChanged(() => LastReceiptDate);
        }
      }
    }

    [DataMember]
    [NotNullValidator]
    public int? MinOrderQty {
      get { return _minOrderQty; }
      set {
        if (value != _minOrderQty) {
          this._minOrderQty = value;
          OnPropertyChanged(() => MinOrderQty);
        }
      }
    }

    [DataMember]
    [NotNullValidator]
    public int? MaxOrderQty {
      get { return _maxOrderQty; }
      set {
        if (value != _maxOrderQty) {
          this._maxOrderQty = value;
          OnPropertyChanged(() => MaxOrderQty);
        }
      }
    }
  }
}