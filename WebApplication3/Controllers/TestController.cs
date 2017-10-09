
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList(test model)
        {
            

            using (Models.DocomPlatformEntities db = new Models.DocomPlatformEntities())
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT TOP {0} *", model.length);
                sb.AppendFormat(" FROM (SELECT ROW_NUMBER() OVER(ORDER BY id) AS RowNumber, * FROM tb_test) as A WHERE RowNumber >{0} *({1} - {2})  ", model.length, model.page, 1);

                var total = db.Tb_Test.Count();
                var data = db.Database.SqlQuery<Models.Tb_Test>(sb.ToString()).ToList();
                var obj = new
                {
                    total = total,
                    data = data
                  

                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }


        }

        public class test
        {

            public int page { get; set; }

            public int length { get; set; }
        }
    }
}