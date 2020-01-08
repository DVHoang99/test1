using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication5.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["login"] == null)

                return RedirectToAction("Login");
            else
            {
                login kq = (login)Session["login"];
                if (kq.chucnang == "admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("ThongBao", new { tenaction = "Index" });
                }
            }
        }

        public ActionResult About()
        {
            if (Session["login"] == null)

                return RedirectToAction("Login");
            else
            {
                login kq = (login)Session["login"];
                if (kq.chucnang == "admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("ThongBao", new { tenaction = "About" });
                }
            }
        }

        public ActionResult Contact()
        {
            if (Session["login"] == null)

                return RedirectToAction("Login");
            else
            {
                login kq = (login)Session["login"];
                if (kq.chucnang == "admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("ThongBao", new { tenaction = "Contact" });
                }
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(dangnhap login)
        {

            test2Entities data = new test2Entities();
            // vinhpham
            // Vinhpham
            // VINHPHAM
            //bool a = "vinh" == "Vinh";
            // .equals so sanh tuong doi chi cho string dc su dung
            var kq = data.logins.Where(x => x.userName.Equals(login.userName) && x.passWord == login.passWord).FirstOrDefault();

            if (kq != null)
            {
                if (kq.chucnang == "admin" && kq.chucnang == "admin")
                {
                    Session["login"] = kq;
                    return RedirectToAction("Index");
                }
                else if (kq.chucnang == "admin" && kq.chucnang == "member")
                {
                    Session["login"] = kq;
                    return RedirectToAction("About");
                }
                else if (kq.chucnang == "admin" && kq.chucnang == "member")
                {
                    Session["login"] = kq;
                    return RedirectToAction("Contact");
                }
                else if (kq.chucnang == "admin" && kq.chucnang == "member")
                {
                    Session["login"] = kq;
                    return RedirectToAction("SanPham");
                }

            }
            else
            {
                ViewBag.a = "sai ten tai khoan mat khau";
            }
            return View();

        }
        public ActionResult LogOut()
        {
            Session["login"] = null;
            return RedirectToAction("login");
        }
        public ActionResult ThongBao(string tenaction)
        {
            ViewBag.tenaction = tenaction;
            return View();
        }

        public ActionResult SanPham()
        {
            test2Entities data = new test2Entities();
            var d = from x in data.SanPhams select x;


            return View(d);
        }

        public ActionResult ThemSanPham()
        {

            if (Session["login"] == null)

                return RedirectToAction("Login");
            else
            {
                login kq = (login)Session["login"];
                if (kq.chucnang == "admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("ThongBao", new { tenaction = "Contact" });
                }
            }
        }
        [HttpPost]
        public ActionResult ThemSanPham(ThongTinSP sp, SanPham b)
        {

            test2Entities data = new test2Entities();
            SanPham a = new SanPham();

            a.Ten = b.Ten;
            a.SoLuong = sp.SoLuong;
            data.SanPhams.Add(a);

            string[] TenQuocGia = sp.NationalName.Trim().Split(' ');
            var ListNational = from x in data.NATIONALs select x;
            for (int i = 0; i < TenQuocGia.Length; i++)
            {
                foreach (var item in ListNational)
                {
                    if (TenQuocGia[i].ToLower().Trim() == item.NationalName.Trim().ToLower())
                    {
                        infoProduct info = new infoProduct();
                        info.NationalID = item.NationalID;
                        info.idProduct = a.ID;
                        data.infoProducts.Add(info);
                        break;
                    }
                }
            }
            data.SaveChanges();
            return RedirectToAction("SanPham");

        }

        public ActionResult XoaSP(int id, SanPham1 sp)
        {

            test2Entities data = new test2Entities();

            var result1 = data.SanPhams.Where(p => p.ID == sp.ID).FirstOrDefault();
            var result2 = data.infoProducts.Where(x => x.idProduct == sp.ID);
            var result3 = data.IMGs.Where(c => c.IDProduct == sp.ID);
            if (result1 != null)
            {
               
                    foreach (var item1 in result3)
                    {
                        data.IMGs.Remove(item1);
                    }
                
                
                foreach (var item in result2)
                {
                    data.infoProducts.Remove(item);
                }
                data.SanPhams.Remove(result1);
                data.SaveChanges();
                return RedirectToAction("SanPham");
            }
            return View(data.SanPhams.FirstOrDefault(m => m.ID.Equals(id)));
        }
        //[HttpPost]
        //public ActionResult XoaSP(SanPham1 sp)
        //{

        //    test2Entities data = new test2Entities();

        //    var result1 = data.SanPhams.Where(p => p.ID == sp.ID).FirstOrDefault();
        //    var result2 = data.infoProducts.Where(x => x.idProduct == sp.ID);
        //    if (result1 != null)
        //    {   
        //        foreach(var item in result2)
        //        {
        //            data.infoProducts.Remove(item);
        //        }
        //        data.SanPhams.Remove(result1);
        //        data.SaveChanges();
        //        return RedirectToAction("SanPham");
        //    }

        //    else
        //    {
        //        ViewBag.a = "Sản Phẩm Này Không Tồn Tại";
        //        return View();

        //    }
        //}
        public ActionResult SuaSP(int id)
        {
            test2Entities data = new test2Entities();

            var listInfoProduct = data.infoProducts;
            int[] listIDNational = new int[100];
            string nameOfNational = "";
            int i = 0;
            foreach (var item in listInfoProduct)
            {
                if (item.idProduct == id)
                {
                    listIDNational[i] = (int)item.NationalID;
                    i++;
                }
            }
            var listNational = data.NATIONALs;
            for (i = 0; i < listIDNational.Length; i++)
            {
                foreach (var item in listNational)
                {
                    if (listIDNational[i] == item.NationalID)
                    {
                        nameOfNational = nameOfNational + item.NationalName.Trim() + " ";
                    }
                }
            }

            ViewBag.a = nameOfNational;
            return View(data.SanPhams.FirstOrDefault(m => m.ID.Equals(id)));

        }
        [HttpPost]
        public ActionResult SuaSP(ThongTinSP sp, SanPham a)

        {

            test2Entities data = new test2Entities();

            var result1 = data.SanPhams.Where(x => x.ID.Equals(sp.ID)).FirstOrDefault();
            var Result2 = data.infoProducts.Where(x => x.idProduct == sp.ID);

            foreach (var item in Result2)
            {
                data.infoProducts.Remove(item);
            }
            string[] TenQuocGia = sp.NationalName.Trim().Split(' ');
            var ListNational = data.NATIONALs;
            for (int i = 0; i < TenQuocGia.Length; i++)
            {
                foreach (var item in ListNational)
                {
                    if (TenQuocGia[i].ToLower().Trim() == item.NationalName.Trim().ToLower())
                    {
                        infoProduct info = new infoProduct();
                        info.NationalID = item.NationalID;
                        info.idProduct = sp.ID;

                        data.infoProducts.Add(info);
                        break;
                    }
                }
            }
            result1.Ten = a.Ten;
            result1.SoLuong = a.SoLuong;
            data.SaveChanges();
            return RedirectToAction("SanPham");




        }
        [HttpPost]
        public JsonResult SuaSanpham(int id, string Ten, int SoLuong, string NationalName, HttpPostedFileBase IMGSua)
        {
            try
            {
                test2Entities data = new test2Entities();

                var result1 = data.SanPhams.Where(x => x.ID.Equals(id)).FirstOrDefault();
                var Result2 = data.infoProducts.Where(x => x.idProduct == id);

                foreach (var item in Result2)
                {
                    data.infoProducts.Remove(item);
                }
                string[] TenQuocGia = NationalName.Trim().Split(' ');
                var ListNational = data.NATIONALs;
                for (int i = 0; i < TenQuocGia.Length; i++)
                {
                    foreach (var item in ListNational)
                    {
                        if (TenQuocGia[i].ToLower().Trim() == item.NationalName.Trim().ToLower())
                        {
                            infoProduct info = new infoProduct();
                            info.NationalID = item.NationalID;
                            info.idProduct = id;

                            data.infoProducts.Add(info);
                            break;
                        }
                    }
                }
                result1.Ten = Ten;
                result1.SoLuong = SoLuong;

                if (IMGSua != null)
                {
                    var result = data.IMGs.Where(x => x.IDProduct == id).FirstOrDefault();
                    string imgPath = result.IMGPath.Trim();
                    imgPath = imgPath.Substring(5);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Img/"), imgPath);
                    System.IO.File.Delete(path);
                    data.IMGs.Remove(result);
                    string path1 = System.IO.Path.Combine(Server.MapPath("~/Img/"), IMGSua.FileName);
                    IMGSua.SaveAs(path1);
                    IMG img = new IMG();
                    img.IMGPath = "/Img/" + IMGSua.FileName;
                    img.IDProduct = id;
                    img.title = Ten;
                    data.IMGs.Add(img);
                }
                

                data.SaveChanges();


                return Json("sửa thành công");
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }
        [HttpPost]
        public JsonResult ThemHinhAnhJson(HttpPostedFileBase fileImg, int id, string name)
        {
            try
            {
                test2Entities data = new test2Entities();
                string path = System.IO.Path.Combine(Server.MapPath("~/img/"), fileImg.FileName);
                fileImg.SaveAs(path);
                IMG img = new IMG();
                img.IMGPath = "/Img/" + fileImg.FileName;
                img.IDProduct = id;
                img.title = name;
                data.IMGs.Add(img);
                data.SaveChanges();
                return Json(1);
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }
        [HttpPost]
        public JsonResult ThemSPJson(string ten, int soluong, string nationalname, HttpPostedFileBase fileImg)
        {
            try 
            {
                test2Entities data = new test2Entities();
                SanPham a = new SanPham();

                a.Ten = ten;
                a.SoLuong = soluong;
                data.SanPhams.Add(a);


                string[] TenQuocGia = nationalname.Trim().Split(' ');
                var ListNational = from x in data.NATIONALs select x;
                for (int i = 0; i < TenQuocGia.Length; i++)
                {
                    foreach (var item in ListNational)
                    {
                        if (TenQuocGia[i].ToLower().Trim() == item.NationalName.Trim().ToLower())
                        {
                            infoProduct info = new infoProduct();
                            info.NationalID = item.NationalID;
                            info.idProduct = a.ID;
                            data.infoProducts.Add(info);
                            break;
                        }
                    }
                }
                string path = System.IO.Path.Combine(Server.MapPath("~/img/"), fileImg.FileName);
                fileImg.SaveAs(path);
                IMG img = new IMG();
                img.IMGPath = "/Img/" + fileImg.FileName;
                img.IDProduct = a.ID;
                img.title = ten;
                data.IMGs.Add(img);
                data.SaveChanges();
                return Json(1);
            }
            catch (Exception ex)
            { return Json(0); }
            
        }
        
    }
}