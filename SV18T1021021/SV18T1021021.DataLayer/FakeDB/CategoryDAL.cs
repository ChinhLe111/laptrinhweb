//using SV18T1021021.DomainModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SV18T1021021.DataLayer.FakeDB
//{
//    public class CategoryDAL : ICategoryDAL
//    {
       
//        public int Add(Category data)
//        {
//            throw new NotImplementedException();
//        }

//        public int Count(string searchValue)
//        {
//            throw new NotImplementedException();
//        }

//        public bool Delete(int categoryID)
//        {
//            throw new NotImplementedException();
//        }

//        public Category Get(int categoryID)
//        {
//            throw new NotImplementedException();
//        }

//        public bool InUsed(int categoryID)
//        {
//            throw new NotImplementedException();
//        }

//        public IList<Category> List()
//        {
//            List<Category> data = new List<Category>();
//            data.Add(new Category()
//            {
//                CategoryID = 1,
//                CategoryName = "Mỹ phẩm",
//                Description = "Giúp các cô đẹp hơn"
//            });
//            data.Add(new Category()
//            {
//                CategoryID = 2,
//                CategoryName = "Bia rượu",
//                Description = "Bản lĩnh đàn ông"
//            });
//            return data;
//        }

//        public IList<Category> List(int page, int pageSize, string searchValue)
//        {
//            throw new NotImplementedException();
//        }

//        public bool Update(Category data)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
