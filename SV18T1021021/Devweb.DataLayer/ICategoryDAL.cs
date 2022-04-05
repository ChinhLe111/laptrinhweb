using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021021.DomainModel;

namespace Devweb.DataLayer
{
    //định nghĩa các phép sử lý liên quan đến loại hàng
    public interface ICategoryDAL
    {
        //Lấy danh sách các tên mặt hàng
        IList<Category> List();

        //Lấy thông tin của 1 loại hàng theo mã loại hàng 
        Category Get(int categoryID);

        //Bổ sung loại hàng mới , hàm trả về mã của loại hàng được bổ sung
        int Add(Category data);

        //Cập nhật thông tin một mặt hàng
        bool Update(Category data);

        //Xóa thông tin của một loại hàng đưa vào mã loại hàng ,
        //Lưu ý không thể xóa nếu loại hàng đã được sử dụng ở một mặt hàng nào đó
        bool Delete(int categoryID);
    }
}
