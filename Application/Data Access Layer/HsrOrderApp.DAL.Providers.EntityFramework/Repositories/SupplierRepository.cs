#region

using System;
using System.Data;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories {
  public class SupplierRepository : RepositoryBase, ISupplierRepository {
    public SupplierRepository(HsrOrderAppEntities db)
        : base(db) {
    }

    public SupplierRepository(string connectionString) : base(connectionString) {
    }

    public SupplierRepository() : base() {
    }

    public IQueryable<HsrOrderApp.BL.DomainModel.Supplier> GetAll() {
      var suppliers = from c in this.db.SupplierSet.Include("SupplierProductConditions").AsEnumerable()
                      select SupplierAdapter.AdaptSupplier(c);

      return suppliers.AsQueryable();
    }

    public HsrOrderApp.BL.DomainModel.Supplier GetById(int id) {
      try {
        var suppliers = from c in this.db.SupplierSet.Include("Addresses").AsEnumerable()
                        where c.SupplierId == id
                        select SupplierAdapter.AdaptSupplier(c);

        return suppliers.First();
      }
      catch (ArgumentNullException ex) {
        if (ExceptionPolicy.HandleException(ex, "DA Policy"))
          throw;
        return new MissingSupplier();
      }
    }

    public int SaveSupplier(HsrOrderApp.BL.DomainModel.Supplier supplier) {
      try {
        string setname = "SupplierSet";
        Supplier dbSupplier;

        bool isNew = false;
        if (supplier.SupplierId == default(int) || supplier.SupplierId <= 0) {
          isNew = true;
          dbSupplier = new Supplier();
        }
        else {
          dbSupplier = new Supplier() { SupplierId = supplier.SupplierId, Version = supplier.Version.ToTimestamp() };
          dbSupplier.EntityKey = db.CreateEntityKey(setname, dbSupplier);
          db.AttachTo(setname, dbSupplier);
        }

        dbSupplier.SupplierId = supplier.SupplierId;
        dbSupplier.AddressId = supplier.Address.AddressId;
        dbSupplier.AccountNumber = supplier.AccountNumber;
        dbSupplier.Name = supplier.Name;
        dbSupplier.PreferredSupplierFlag = supplier.PreferredSupplierFlag;
        dbSupplier.ActiveFlag = supplier.ActiveFlag;

        if (isNew) {
          db.AddToSupplierSet(dbSupplier);
        }
        db.SaveChanges();

        supplier.SupplierId = dbSupplier.SupplierId;
        return dbSupplier.SupplierId;
      }
      catch (OptimisticConcurrencyException ex) {
        if (ExceptionPolicy.HandleException(ex, "DA Policy"))
          throw;
        return default(int);
      }
    }

    public void DeleteSupplier(int id) {
      Supplier cu = db.SupplierSet.First(c => c.SupplierId == id);
      if (cu != null) {
        db.DeleteObject(cu);
        db.SaveChanges();
      }
    }

    public int SaveProductCondition(BL.DomainModel.SupplierProductCondition productCondition, BL.DomainModel.Supplier forThisSupplier) {
      SupplierProductConditionRepository rep = new SupplierProductConditionRepository(db);
      SupplierProductCondition dbProductCondition = rep.SaveSupplierProductConditionInternal(productCondition);
      if (productCondition.IsNew) {
        Supplier supplier = db.SupplierSet.First(c => c.SupplierId == forThisSupplier.SupplierId);
        supplier.SupplierProductConditions.Add(dbProductCondition);
        db.SaveChanges();
      }
      return dbProductCondition.SupplierProductConditionId;
    }

    public void DeleteProductCondition(int id) {
      SupplierProductConditionRepository rep = new SupplierProductConditionRepository(db);
      rep.DeleteSupplierProductCondition(id);
    }
  }
}