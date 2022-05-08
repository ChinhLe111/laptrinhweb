using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021021.DataLayer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommonDAL<T> where T : class 
    {
        /// <summary>
        /// tìm kiếm phân trang
        /// </summary>
        /// <param name="page">trang cần xen</param>
        /// <param name="pageSize">số dòng mỗi trang(0 nếu ko phân trang)</param>
        /// <param name="searchValue">giá trị tìm kiếm ( rỗng nếu bỏ qua)</param>
        /// <returns></returns>
        IList<T> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Bổ dung dữ liệu T,hàm trả về giá trị (INDENTITY) của dữ liệu dc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(T data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        bool Update(T data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
