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
      var suppliers = from c in this.db.SupplierSet.Include("Address").Include("SupplierProductConditions").AsEnumerable()
                      select SupplierAdapter.AdaptSupplier(c);

      return suppliers.AsQueryable();
    }

    public HsrOrderApp.BL.DomainModel.Supplier GetById(int id) {
      try {
        var suppliers = from c in this.db.SupplierSet.Include("Address").Include("SupplierProductConditions").AsEnumerable()
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
        string supplierSetname = "SupplierSet";
        string addressSetname = "AddressSet";
        Supplier dbSupplier;
        Address dbAddress = null;

        bool isNew = false;
        if (supplier.SupplierId == default(int) || supplier.SupplierId <= 0) {
          isNew = true;
          dbSupplier = new Supplier();
          dbAddress = new Address();
        }
        else {
          dbSupplier = new Supplier() { SupplierId = supplier.SupplierId, Version = supplier.Version.ToTimestamp() };
          dbSupplier.EntityKey = db.CreateEntityKey(supplierSetname, dbSupplier);
          db.AttachTo(supplierSetname, dbSupplier);
          dbAddress = new Address() { AddressId = supplier.Address.AddressId, Version = supplier.Address.Version.ToTimestamp() };
          dbAddress.EntityKey = db.CreateEntityKey(addressSetname, dbAddress);
          db.AttachTo(addressSetname, dbAddress);
        }

        dbSupplier.SupplierId = supplier.SupplierId;
        dbSupplier.AddressId = supplier.Address.AddressId;
        dbSupplier.AccountNumber = supplier.AccountNumber;
        dbSupplier.PurchasingWebServiceURL = supplier.PurchasingWebServiceURL;
        dbSupplier.CreditRating = (short) supplier.CreditRating;
        dbSupplier.Name = supplier.Name;
        dbSupplier.PreferredSupplierFlag = supplier.PreferredSupplierFlag;
        dbSupplier.ActiveFlag = supplier.ActiveFlag;

        dbAddress.AddressId = supplier.Address.AddressId;
        dbAddress.AddressLine1 = supplier.Address.AddressLine1;
        dbAddress.AddressLine2 = supplier.Address.AddressLine2;
        dbAddress.City = supplier.Address.City;
        dbAddress.Email = supplier.Address.Email;
        dbAddress.Phone = supplier.Address.Phone;
        dbAddress.PostalCode = supplier.Address.PostalCode;

        if (isNew) {
          db.AddToSupplierSet(dbSupplier);
          db.AddToAddressSet(dbAddress);
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

    public void SaveAddress(BL.DomainModel.Address address) {
      AddressRepository rep = new AddressRepository(db);
      Address dbAddress = rep.SaveAddressInternal(address);
    }
  }
}