#region

using System.Data.Objects.DataClasses;
using System.Linq;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters {
  internal static class SupplierProductConditionAdapter {
    internal static IQueryable<BL.DomainModel.SupplierProductCondition> AdaptSupplierProductConditions(EntityCollection<SupplierProductCondition> productConditionCollection) {
      if (productConditionCollection.IsLoaded == false)
        return null;

      var productConditions = from a in productConditionCollection.AsEnumerable()
                      select AdaptSupplierProductCondition(a);
      return productConditions.AsQueryable();
    }

    internal static BL.DomainModel.SupplierProductCondition AdaptSupplierProductCondition(SupplierProductCondition spc) {
      return new BL.DomainModel.SupplierProductCondition() {
        SupplierProductConditionId = spc.SupplierProductConditionId,
        AverageLeadTime = spc.AverageLeadTime,
        StandardPrice = spc.StandardPrice,
        LastReceiptCost = spc.LastReceiptCost,
        LastReceiptDate = spc.LastReceiptDate,
        MinOrderQty = spc.MinOrderQty,
        MaxOrderQty = spc.MaxOrderQty,
        Version = spc.Version.ToUlong(),
      };
    }
  }
}