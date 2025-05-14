using BookSale.Managerment.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Managerment.Application.DTOs
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public ActionType ActionType { get; set; } = ActionType.Get;

        public ResponseModel(bool status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
        public ResponseModel(ActionType actionType, bool status, string message)
        {
            this.ActionType = actionType;
            this.Status = status;
            this.Message = message;
        }
    }
    public class ResponseModel<T> : ResponseModel where T : class
    {
        public T? Data { get; set; }
        public ResponseModel(bool status, string message) : base(status, message) {}
        public ResponseModel(bool status, string message, T data) : base(status, message)
        {
            Data = data;
        }

        public ResponseModel(ActionType actionType, bool status, string message, T data) : base(actionType, status, message)
        {
            Data = data;
        }
    }
}
