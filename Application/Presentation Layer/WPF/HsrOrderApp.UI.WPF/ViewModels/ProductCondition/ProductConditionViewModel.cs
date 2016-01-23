#region

using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.ProductCondition {
  public class ProductConditionViewModel : ListViewModelBase<SupplierProductConditionDTO> {
    public ProductConditionViewModel(SupplierDTO supplier) : base(supplier) {
    }

    protected override void LoadData() {
      foreach (SupplierProductConditionDTO productCondition in ((SupplierDTO)ParentObject).ProductConditions)
        Items.Add(productCondition);
    }

    protected override void New() {
      SupplierProductConditionDTO newProductCondition = new SupplierProductConditionDTO();
      ProductConditionDetailViewModel detailModelView = new ProductConditionDetailViewModel(newProductCondition, true);
      if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok) {
        ParentObject.MarkChildForInsertion(newProductCondition);
        Items.Add(newProductCondition);
        SelectedItem = newProductCondition;
      }
    }

    protected override void Delete() {
      ParentObject.MarkChildForDeletion(SelectedItem);
      Items.Remove(SelectedItem);
      SelectedItem = null;
    }

    protected override void Edit() {
      SupplierProductConditionDTO editProductCondition = SelectedItem.Clone();
      ProductConditionDetailViewModel detailModelView = new ProductConditionDetailViewModel(editProductCondition, false);
      if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok) {
        int index = Items.IndexOf(SelectedItem);
        Items.Remove(SelectedItem);
        Items.Insert(index, editProductCondition);
        SelectedItem = editProductCondition;
        ParentObject.MarkChildForUpdate(editProductCondition);
      }
    }
  }
}