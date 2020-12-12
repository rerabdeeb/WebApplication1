using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AjaxController : Controller
    {
        pearprojectEntities pear = new pearprojectEntities();
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            client cli = pear.clients.FirstOrDefault(x => x.id == id);
            pear.clients.Remove(cli);
            pear.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllClient()
        {

            //List<viewModel> db = new List<viewModel>();
            var data = pear.clients.Select(z => new
            {
                id = z.id,
                Name = (z.Name != null) ? z.Name : "",
                Address = (z.Address != null) ? z.Address : "",
                Mobile = (z.Mobile != null) ? z.Mobile : "",
                DateOfBirthstr = z.DateOfBirth.ToString(),
                imageName = z.imageName

            }).ToList();
            //List<client> clients = pear.clients.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetClient(int id)
        {
            client cli = pear.clients.FirstOrDefault(x => x.id == id);
            viewModel clien = new viewModel();
            clien.DateOfBirthstr =cli.DateOfBirth.ToString();
            clien.Address = cli.Address;
            clien.imageName = cli.imageName;
            clien.Mobile = cli.Mobile;
            clien.Name = cli.Name;
            
            //obj.id = cli.id;
            //obj.address = cli.address;
            //obj.dateOfBirth = cli.dateOfBirth;
            //obj.imageName = cli.imageName;
            //obj.telephoneNumper = cli.telephoneNumper;
            //obj.Name = cli.Name;
            var client = from obj in pear.clients
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
                        pear.clients.Add(cl);
                        pear.SaveChanges();

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
                        client cli = pear.clients.FirstOrDefault(x => x.id == client.id);
                        client cl = new client();
                        cli.Name = client.Name;
                        cli.Address = client.Address;
                        cli.DateOfBirth = client.DateOfBirth;
                        cli.Mobile = client.Mobile;
                        HttpFileCollectionBase files = Request.Files;
                        HttpPostedFileBase file = files[0];
                        string fileName = file.FileName;
                        Directory.CreateDirectory(Server.MapPath("~/images/"));
                        string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                        file.SaveAs(path);
                        cli.imageName = fileName;
                       
                        pear.SaveChanges();

                    }

                    catch (Exception e)
                    {
                        return Json("error" + e.Message);
                    }
                }

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Edit(client client)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    client cli = pear.clients.FirstOrDefault(x => x.id == client.id);
                    client cl = new client();
                    cli.Name = client.Name;
                    cli.Address = client.Address;
                    cli.DateOfBirth = client.DateOfBirth;
                    cli.Mobile = client.Mobile;
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    string fileName = file.FileName;
                    Directory.CreateDirectory(Server.MapPath("~/images/"));
                    string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                    file.SaveAs(path);
                    cli.imageName = fileName;

                    pear.SaveChanges();

                }

                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
            }
            //if (Request.Files.Count > 0)
            //{
            //    client cli = pear.clients.FirstOrDefault(x => x.id == client.id);
            //    //clientViewModel obj = new clientViewModel();
            //    //obj.id = cli.id;
            //    //obj.address = cli.address;
            //    //obj.dateOfBirth = cli.dateOfBirth;
            //    //obj.imageName = cli.imageName;
            //    //obj.telephoneNumper = cli.telephoneNumper;
            //    //obj.Name = cli.Name;
            //    HttpFileCollectionBase files = Request.Files;
            //    HttpPostedFileBase file = files[0];
            //    string fileName = file.FileName;
            //    Directory.CreateDirectory(Server.MapPath("~/images/"));
            //    string path = Path.Combine(Server.MapPath("~/images/"), fileName);
            //    file.SaveAs(path);
            //    cli.imageName = fileName;
            //    cli.Name = client.Name;
            //    cli.Address = client.Address;
            //    cli.DateOfBirth = client.DateOfBirth;
            //    cli.imageName = client.imageName;
            //    cli.Mobile = client.Mobile;
            //    pear.SaveChanges();
                
            //}
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }

   
}