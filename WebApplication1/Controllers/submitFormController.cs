using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class submitFormController : Controller
    {
        pearprojectEntities1 context = new pearprojectEntities1();
        // GET: submitForm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {


            client cli = context.client.FirstOrDefault(x => x.id == id);
            context.client.Remove(cli);
            context.SaveChanges();


            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllClient()
        {
          
            List<viewModel> db = new List<viewModel>();

            foreach (var item in context.client)
            {
                viewModel obj = new viewModel();
                obj.id = item.id;
                obj.Name = item.Name;
                obj.Address = item.Address;
                obj.Mobile = item.Mobile;
                obj.DateOfBirthstr = item.DateOfBirth.ToString();
                obj.imageName = item.imageName;
                db.Add(obj);
            }
            return Json(db.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetClient(int id)
        {
            var cli = context.client.FirstOrDefault(x => x.id == id);
            viewModel clien = new viewModel();
            clien.DateOfBirthstr = cli.DateOfBirth.ToString();
            clien.Address = cli.Address;
            clien.imageName = cli.imageName;
            clien.Mobile = cli.Mobile;
            clien.Name = cli.Name;
            clien.DateOfBirth = cli.DateOfBirth;
            var client = from obj in context.client
                         where obj.id == id
                         select new
                         {
                             Name = obj.Name,
                             address = obj.Address,
                             dateOfBirth = obj.DateOfBirth,
                             telephoneNumper = obj.Mobile,
                             imageName = obj.imageName
                         };

            return Json(clien, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(viewModel client, bool ISAdd = true)
        {
            
            if (ISAdd)
            {
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        client cl = new client();
                        cl.Name = client.Name;
                        cl.Address = client.Address;
                        cl.DateOfBirth = client.DateOfBirth;
                        cl.Mobile = client.Mobile;
                        HttpFileCollectionBase files = Request.Files;
                        HttpPostedFileBase file = files[0];
                        string fileName = file.FileName;
                        Directory.CreateDirectory(Server.MapPath("~/images/"));
                        string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                        file.SaveAs(path);
                        cl.imageName = fileName;
                        context.client.Add(cl);
                        context.SaveChanges();

                    }

                    catch (Exception e)
                    {
                        return Json("error" + e.Message);
                    }
                }

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        var clien = context.client.FirstOrDefault(x => x.id == client.id);
                        clien.Name = client.Name;
                        clien.Address = client.Address;
                        clien.DateOfBirth = Convert.ToDateTime(client.DateOfBirth);
                        clien.Mobile = client.Mobile;

                        HttpFileCollectionBase files = Request.Files;
                        HttpPostedFileBase file = files[0];
                        string fileName = file.FileName;
                        Directory.CreateDirectory(Server.MapPath("~/images/"));
                        string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                        file.SaveAs(path);
                        clien.imageName = fileName;
                        context.SaveChanges();
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        return Json("error" + e.Message);
                    }
                }
                return Json("Success", JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult Edit(viewModel client, int id)
        {
            var cli = context.client.FirstOrDefault(x => x.id == id);
            //clientViewModel obj = new clientViewModel();
            //obj.id = cli.id;
            //obj.address = cli.address;
            //obj.dateOfBirth = cli.dateOfBirth;
            //obj.imageName = cli.imageName;
            //obj.telephoneNumper = cli.telephoneNumper;
            //obj.Name = cli.Name;
            cli.Name = client.Name;
            cli.Address = client.Address;
            cli.DateOfBirth = client.DateOfBirth;
            cli.imageName = client.imageName;
            cli.Mobile = client.Mobile;
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}