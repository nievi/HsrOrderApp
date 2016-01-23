#region

using System;
using System.Data;
using System.Linq;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories {
  public class SupplierProductConditionRepository : RepositoryBase {
    public SupplierProductConditionRepository(HsrOrderAppEntities db)
        : base(db) {
    }

    public SupplierProductConditionRepository(string connectionString) : base(connectionString) {
    }

    public SupplierProductConditionRepository() : base() {
    }

    public int SaveSupplierProductCondition(HsrOrderApp.BL.DomainModel.SupplierProductCondition supplierProductCondition) {
      SupplierProductCondition dbSupplierProductCondition = SaveSupplierProductConditionInternal(supplierProductCondition);
      supplierProductCondition.SupplierProductConditionId = dbSupplierProductCondition.SupplierProductConditionId;
      return supplierProductCondition.SupplierProductConditionId;
    }

    public SupplierProductCondition SaveSupplierProductConditionInternal(HsrOrderApp.BL.DomainModel.SupplierProductCondition supplierProductCondition) {
      try {
        string setname = "SupplierProductConditionSet";
        SupplierProductCondition dbSupplierProductCondition;

        bool isNew = false;
        if (supplierProductCondition.SupplierProductConditionId == default(int) || supplierProductCondition.SupplierProductConditionId <= 0) {
          isNew = true;
          dbSupplierProductCondition = new SupplierProductCondition();
        }
        else {
          dbSupplierProductCondition = new SupplierProductCondition() { SupplierProductConditionId = supplierProductCondition.SupplierProductConditionId, Version = supplierProductCondition.Version.ToTimestamp() };
          dbSupplierProductCondition.EntityKey = db.CreateEntityKey(setname, dbSupplierProductCondition);
          db.AttachTo(setname, dbSupplierProductCondition);
        }

        dbSupplierProductCondition.SupplierProductConditionId = supplierProductCondition.SupplierProductConditionId;
        dbSupplierProductCondition.AverageLeadTime = supplierProductCondition.AverageLeadTime;
        dbSupplierProductCondition.StandardPrice = supplierProductCondition.StandardPrice;
        dbSupplierProductCondition.LastReceiptCost = supplierProductCondition.LastReceiptCost;
        dbSupplierProductCondition.LastReceiptDate = supplierProductCondition.LastReceiptDate;
        dbSupplierProductCondition.MinOrderQty = supplierProductCondition.MinOrderQty;
        dbSupplierProductCondition.MaxOrderQty = supplierProductCondition.MaxOrderQty;
        dbSupplierProductCondition.StandardPrice = supplierProductCondition.StandardPrice;
        if (isNew) {
          db.AddToSupplierProductConditionSet(dbSupplierProductCondition);
        }
        db.SaveChanges();

        supplierProductCondition.SupplierProductConditionId = dbSupplierProductCondition.SupplierProductConditionId;
        return dbSupplierProductCondition;
      }
      catch (OptimisticConcurrencyException ex) {
        if (ExceptionPolicy.HandleException(ex, "DA Policy"))
          throw;
        return null;
      }
    }

    public void DeleteSupplierProductCondition(int id) {
      SupplierProductCondition cu = db.SupplierProductConditionSet.First(c => c.SupplierProductConditionId == id);
      if (cu != null) {
        db.DeleteObject(cu);
        db.SaveChanges();
      }
    }
  }
}