using System.ComponentModel.DataAnnotations;

namespace BookSale.Managerment.Ui.Areas.Admin.View.Components
{
    public class _BootstrapModal
    {
        public string Action { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Controller { get; set; }
        public _BootstrapModal(string action, string controller)
        {
            Action = action;
            Controller = controller;
        }
    }
}