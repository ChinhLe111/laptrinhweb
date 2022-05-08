using SV18T1021021.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021021.DataLayer
{
    public interface IProductPhotoDAL
    {
        int Add(ProductPhoto data);

        bool Update(ProductPhoto data);

        bool Delete(int attributeID);

        IList<ProductPhoto> List();
    }
}