using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private ISanPhamBusiness _SanPhamBusiness;
        private string _path;
        private IWebHostEnvironment _env;
        public SanPhamController(ISanPhamBusiness sanPhamBusiness, IConfiguration configuration, IWebHostEnvironment env)
        {
            _SanPhamBusiness = sanPhamBusiness;
            _path = configuration["AppSettings:PATH"];
            _env = env;
        }

        [Route("GetAll_SanPham")]
        [HttpGet]
        public List<SanPhamModel> getAll_SanPham()
        {
            return _SanPhamBusiness.getAll_SanPham();
        }

        [Route("get-SanPhamByID")]
        [HttpGet]
        public SanPhamModel GetDatabyID(string id)
        {
            return _SanPhamBusiness.GetDatabyID(id);
        }

        [NonAction]
        public string CreatePathFile(string RelativePathFileName)
        {
            try
            {
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                return fullPathFile;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        

        [Route("create-SanPham")]
        [HttpPost]
        public SanPhamModel CreateItem([FromBody] SanPhamModel model)
        {
            _SanPhamBusiness.Create(model);
            return model;
        }

        [Route("update-SanPham")]
        [HttpPut]
        public SanPhamModel UpdateItem([FromBody] SanPhamModel model)
        {
            _SanPhamBusiness.Update(model);
            return model;
        }

        [Route("delete-SanPham")]
        [HttpDelete]
        public int DeleteItem(int id)
        {
            _SanPhamBusiness.Delete(id);
            return id;
        }

        [Route("search-SanPham")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        { 
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string TenCafe = formData.ContainsKey("tenCafe") ? Convert.ToString(formData["tenCafe"].ToString()) : "";
                long total = 0;
                var data = _SanPhamBusiness.Search(page, pageSize, out total, TenCafe);
                return Ok(
                    new
                    {
                        TotalItems = total,
                        Data = data,
                        Page = page,
                        PageSize = pageSize
                    }
                    );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
