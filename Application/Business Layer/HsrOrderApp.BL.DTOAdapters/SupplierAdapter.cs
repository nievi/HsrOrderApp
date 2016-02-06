#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DTOAdapters.Helper;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.BL.DtoAdapters {
  public class SupplierAdapter {
    #region SupplierToDTO

    public static IList<SupplierListDTO> SuppliersToDtos(IQueryable<Supplier> suppliers) {
      IQueryable<SupplierListDTO> supplierDtos = from c in suppliers
                                                 select new SupplierListDTO() {
                                                   Id = c.SupplierId,
                                                   AccountNumber = c.AccountNumber,
                                                   Name = c.Name,
                                                   CreditRating = c.CreditRating,
                                                   PreferredSupplierFlag = c.PreferredSupplierFlag,
                                                   ActiveFlag = c.ActiveFlag,
                                                   PurchasingWebServiceURL = c.PurchasingWebServiceURL
                                                 };
      return supplierDtos.ToList();
    }

    public static SupplierDTO SupplierToDto(Supplier supplier) {
      SupplierDTO dto = new SupplierDTO() {
        Id = supplier.SupplierId,
        AccountNumber = supplier.AccountNumber,
        Name = supplier.Name,
        CreditRating = supplier.CreditRating,
        PreferredSupplierFlag = supplier.PreferredSupplierFlag,
        ActiveFlag = supplier.ActiveFlag,
        PurchasingWebServiceURL = supplier.PurchasingWebServiceURL,
        Address = AddressAdapter.AddressToDto(supplier.Address)
      };

      return dto;
    }

    #endregion

    #region DTOToSupplier

    public static Supplier DtoToSupplier(SupplierDTO dto) {
      Supplier supplier = new Supplier() {
        SupplierId = dto.Id,
        AccountNumber = dto.AccountNumber,
        Name = dto.Name,
        CreditRating = dto.CreditRating,
        PreferredSupplierFlag = dto.PreferredSupplierFlag,
        ActiveFlag = dto.ActiveFlag,
        PurchasingWebServiceURL = dto.PurchasingWebServiceURL,
        Address = AddressAdapter.DtoToAddress(dto.Address),
        Version = dto.Version
      };
      ValidationHelper.Validate(supplier);
      return supplier;
    }

    public static IEnumerable<ChangeItem> GetChangeItems(SupplierDTO dto, Supplier supplier) {
      IEnumerable<ChangeItem> changeItems = from c in dto.Changes
                                            select new ChangeItem(c.ChangeType,
                                                                  AddressAdapter.DtoToAddress((AddressDTO)c.Object));
      return changeItems;
    }

    #endregion
  }
}