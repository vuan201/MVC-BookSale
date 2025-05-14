namespace BookSale.Managerment.Ui.Models
{
    public class TableViewModel<T> 
    {
        public int total {get;set;}
        public List<T>? Rows { get; set;}
    }
}