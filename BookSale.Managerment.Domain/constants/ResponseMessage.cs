using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Domain.constants
{
    public class ResponseMessage
    {
        // Thông báo thành công
        public const string GetDataSuccess = "Lấy dữ liệu thành công";
        public const string CreateSuccess = "Thêm mới dữ liệu thành công";
        public const string UpdateSuccess = "Cập nhật dữ liệu thành công";
        public const string DeleteSuccess = "Xóa dữ liệu thành công";

        // Thông báo không thành công
        public const string UpdateFail = "Cập nhật không thành công";

        // Thông báo lỗi
        public const string InvalidValue = "Giá trị không hợp lệ";
        public const string DoesNotExist = "Không tìm thấy dữ liệu";

        public static string GetInvalidMessage(string valueName) => $"{valueName} Không hợp lệ";

    }
}
