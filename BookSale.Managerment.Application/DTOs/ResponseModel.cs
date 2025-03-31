using System.ComponentModel.DataAnnotations;

namespace BookSale.Managerment.Application.DTOs
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public ResponseModel(bool status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
    }
}
