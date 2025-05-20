namespace BookSale.Managerment.Ui.Areas.Admin.View.Components
{
    public class _RedirectButtonModel
    {
        public string Title { get; set; }
        public string RedirectAction { get; set; }
        public string RedirectController { get; set; }
        public _RedirectButtonModel(string title, string redirectAction, string redirectController)
        {
            this.Title = title;
            this.RedirectAction = redirectAction;
            this.RedirectController = redirectController;
        }
    }
}