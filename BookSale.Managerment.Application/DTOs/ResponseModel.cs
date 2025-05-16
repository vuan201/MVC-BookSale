using BookSale.Managerment.Domain.Enums;
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
        public T? Data { get; set; }
        public ResponseModel(bool status, string message) : base(status, message) {}
        public ResponseModel(bool status, string message, T data) : base(status, message)
        {
            Data = data;
        }
        public ResponseModel(bool status, string message,int total, T data) : base(status, message)
        {
            Total = total;
            Data = data;
        }
    }
}
