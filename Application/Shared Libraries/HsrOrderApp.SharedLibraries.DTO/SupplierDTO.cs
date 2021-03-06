﻿#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO {
  [DataContract]
  public class SupplierDTO : DTOParentObject {
    private string _name;
    private int _accountNumber;
    private int? _creditRating;
    private bool _preferredSupplierFlag;
    private bool _activeFlag;
    private string _purchasingWebServiceURL;
    private AddressDTO _address;
    private IList<SupplierProductConditionDTO> _productConditions;

    public SupplierDTO() {
      this.Name = string.Empty;
      this.AccountNumber = default(int);
      this.CreditRating = default(int);
      this.PreferredSupplierFlag = false;
      this.ActiveFlag = true;
      this.PurchasingWebServiceURL = string.Empty;
      this.Address = new AddressDTO();
      this.ProductConditions = new List<SupplierProductConditionDTO>();
    }

    [DataMember]
    [StringLengthValidator(1, 50)]
    public string Name {
      get { return _name; }
      set {
        if (value != _name) {
          this._name = value;
          OnPropertyChanged(() => Name);
        }
      }
    }

    [DataMember]
    [NotNullValidator]
    public int AccountNumber {
      get { return _accountNumber; }
      set {
        if (value != _accountNumber) {
          this._accountNumber = value;
          OnPropertyChanged(() => AccountNumber);
        }
      }
    }

    [DataMember]
    [IgnoreNulls]
    [RangeValidator(1, RangeBoundaryType.Inclusive, 5, RangeBoundaryType.Inclusive)]
    public int? CreditRating {
      get { return _creditRating; }
      set {
        if (value != _creditRating) {
          this._creditRating = value;
          OnPropertyChanged(() => CreditRating);
        }
      }
    }

    [DataMember]
    [NotNullValidator]
    public bool PreferredSupplierFlag {
      get { return _preferredSupplierFlag; }
      set {
        if (value != _preferredSupplierFlag) {
          this._preferredSupplierFlag = value;
          OnPropertyChanged(() => PreferredSupplierFlag);
        }
      }
    }

    [DataMember]
    [NotNullValidator]
    public bool ActiveFlag {
      get { return _activeFlag; }
      set {
        if (value != _activeFlag) {
          this._activeFlag = value;
          OnPropertyChanged(() => ActiveFlag);
        }
      }
    }

    [DataMember]
    [StringLengthValidator(1, 50)]
    public string PurchasingWebServiceURL {
      get { return _purchasingWebServiceURL; }
      set {
        if (value != _purchasingWebServiceURL) {
          this._purchasingWebServiceURL = value;
          OnPropertyChanged(() => PurchasingWebServiceURL);
        }
      }
    }

    [DataMember]
    [NotNullValidator]
    public AddressDTO Address {
      get { return _address; }
      set {
        if (value != _address) {
          this._address = value;
          OnPropertyChanged(() => Address);
        }
      }
    }


    [DataMember]
    [ObjectCollectionValidator(typeof(SupplierProductConditionDTO))]
    public IList<SupplierProductConditionDTO> ProductConditions {
      get { return _productConditions; }
      set {
        if (value != _productConditions) {
          this._productConditions = value;
          OnPropertyChanged(() => ProductConditions);
        }
      }
    }

    #region Address access

    public string AddressLine1 {
      get { return Address.AddressLine1; }
      set { Address.AddressLine1 = value; }
    }
    public string AddressLine2 {
      get { return Address.AddressLine2; }
      set { Address.AddressLine2 = value; }
    }

    public string PostalCode {
      get { return Address.PostalCode; }
      set { Address.PostalCode = value; }
    }

    public string City {
      get { return Address.City; }
      set { Address.City = value; }
    }

    public string Phone {
      get { return Address.Phone; }
      set { Address.Phone = value; }
    }

    public string Email {
      get { return Address.Email; }
      set { Address.Email = value; }
    }
    #endregion
  }
}