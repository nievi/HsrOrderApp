#region

using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.ProductCondition {
  public class ProductConditionDetailViewModel : DetailViewModelBase<SupplierProductConditionDTO> {
    public ProductConditionDetailViewModel(SupplierProductConditionDTO productCondition, bool isNew) : base(productCondition, isNew) {
      this.DisplayName = Strings.ProductConditionDetailViewModel_DisplayName;
    }
  }
}