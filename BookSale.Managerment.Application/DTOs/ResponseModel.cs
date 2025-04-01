using System.ComponentModel.DataAnnotations;

namespace BookSale.Managerment.Application.DTOs
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public ResponseModel(bool status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
    }
    public class ResponseModel<T> : ResponseModel where T : class
    {
        public int Total { get; set; }
        public T? Rows { get; set; }

        public ResponseModel(bool status, string message,int Total, T? rows = null) : base(status, message)
        {
            this.Total = Total;
            this.Rows = rows;
        }
    }
}
