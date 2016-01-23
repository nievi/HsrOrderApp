#region

using System.Data.Objects.DataClasses;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters {
  internal static class SupplierAdapter {
    internal static BL.DomainModel.Supplier AdaptSupplier(EntityReference<Supplier> c) {
      if (c == null || c.Value == null)
        return null;
      return AdaptSupplier(c.Value, null);
    }

    internal static BL.DomainModel.Supplier AdaptSupplier(EntityReference<Supplier> dbSupplier, BL.DomainModel.User user) {
      if (dbSupplier == null || dbSupplier.Value == null)
        return null;
      return AdaptSupplier(dbSupplier.Value, user);
    }

    internal static BL.DomainModel.Supplier AdaptSupplier(Supplier dbSupplier) {
      return AdaptSupplier(dbSupplier, null);
    }

    internal static BL.DomainModel.Supplier AdaptSupplier(Supplier dbSupplier, BL.DomainModel.User user) {
      BL.DomainModel.Supplier supplier = new BL.DomainModel.Supplier() {
        SupplierId = dbSupplier.SupplierId,
        Name = dbSupplier.Name,
        AccountNumber = dbSupplier.AccountNumber,
        CreditRating = dbSupplier.CreditRating,
        PreferredSupplierFlag = dbSupplier.PreferredSupplierFlag,
        ActiveFlag = dbSupplier.ActiveFlag,
        PurchasingWebServiceURL = dbSupplier.PurchasingWebServiceURL,
        Version = dbSupplier.Version.ToUlong()
      };
      supplier.SupplierProductConditions = SupplierProductConditionAdapter.AdaptSupplierProductConditions(dbSupplier.SupplierProductConditions);
      supplier.Address = AddressAdapter.AdaptAddress(dbSupplier.Address);
      return supplier;
    }
  }
}