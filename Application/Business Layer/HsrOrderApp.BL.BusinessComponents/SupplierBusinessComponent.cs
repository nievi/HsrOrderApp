#region

using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.SharedLibraries.SharedEnums;
using System;

#endregion

namespace HsrOrderApp.BL.BusinessComponents {
  public class SupplierBusinessComponent {
    private ISupplierRepository rep;

    public SupplierBusinessComponent() {
    }

    public SupplierBusinessComponent(ISupplierRepository unitOfWork) {
      this.rep = unitOfWork;
    }

    public ISupplierRepository Repository {
      get { return this.rep; }
      set { this.rep = value; }
    }

    public Supplier GetSupplierById(int supplierId) {
      Supplier supplier = rep.GetById(supplierId);
      return supplier;
    }

    public IQueryable<Supplier> GetSuppliersByCriteria(SupplierSearchType searchType, int accountNumber, string supplierName) {
      IQueryable<Supplier> suppliers = new List<Supplier>().AsQueryable();

      switch (searchType) {
        case SupplierSearchType.None:
          suppliers = rep.GetAll();
          break;
        case SupplierSearchType.ByAccountNumber:
          suppliers = rep.GetAll().Where(supplier => supplier.AccountNumber == accountNumber);
          break;
        case SupplierSearchType.ByActiveSupplier:
          suppliers = rep.GetAll().Where(supplier => supplier.ActiveFlag);
          break;
        case SupplierSearchType.ByName:
          suppliers = rep.GetAll().Where(supplier => supplier.Name == supplierName);
          break;
      }
      return suppliers;
    }

    public int StoreSupplier(Supplier supplier, IEnumerable<ChangeItem> changeItems) {
      int supplierId = default(int);
      using (TransactionScope transaction = new TransactionScope()) {
        supplierId = rep.SaveSupplier(supplier);
        foreach (ChangeItem change in changeItems) {
          if (change.Object is Address) {
            Address address = (Address)change.Object;
            switch (change.ChangeType) {
              case ChangeType.ChildInsert:
                supplier.Address = address;
                continue;
              case ChangeType.ChildUpate:
                rep.SaveAddress(address);
                break;
              case ChangeType.ChildDelete:
                throw new NotImplementedException();
            }
          }
        }
        transaction.Complete();
      }

      return supplierId;
    }

    public void DeleteSupplier(int supplierId) {
      rep.DeleteSupplier(supplierId);
    }
  }
}