using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface ISanPhamRepository
    {
        List<SanPhamModel> getAll_SanPham();
        SanPhamModel GetDatabyID(string id);

        bool Create(SanPhamModel model);

        bool Update(SanPhamModel model);

        bool Delete(int id);

        public List<SanPhamModel> Search(int pageIndex, int pageSize, out long total, string TenCafe);
    }
}
