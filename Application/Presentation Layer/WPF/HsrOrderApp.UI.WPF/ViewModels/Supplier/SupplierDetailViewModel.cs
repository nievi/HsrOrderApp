#region

using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;
using HsrOrderApp.UI.WPF.ViewModels.ProductCondition;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Supplier {
  public class SupplierDetailViewModel : DetailViewModelBase<SupplierDTO> {
    #region Fields

    private ProductConditionViewModel _listViewModel;

    #endregion

    public SupplierDetailViewModel(SupplierDTO customer, bool isNew) : base(customer, isNew) {
      this.DisplayName = Strings.SupplierDetailViewModel_DisplayName;
    }

    public ProductConditionViewModel ListViewModel {
      get {
        if (this._listViewModel == null) {
          this._listViewModel = new ProductConditionViewModel(this.Model);
          this._listViewModel.LoadCommand.Command.Execute(null);
        }
        return _listViewModel;
      }
    }

    protected override void SaveData() {
      Service.StoreSupplier(Model);
      SaveCommandExecuted();
    }
  }
}