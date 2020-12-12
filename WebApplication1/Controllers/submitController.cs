using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class submitController : Controller
    {
        pearprojectEntities1 pear = new pearprojectEntities1();
        // GET: submit
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Create2(viewModel client)
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
                    pear.client.Add(cl);
                    pear.SaveChanges();
                }
                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
            }
            //List<viewModel> db = new List<viewModel>();
            //List<viewModel> db = new List<viewModel>();
            var xlist = pear.client.Select(z => new viewModel
            {
                Address = z.Address,
                DateOfBirth = z.DateOfBirth,
                Name = z.Name,
                imageName = z.imageName,
                id = z.id,
                Mobile = z.Mobile

            }).ToList();
            //foreach (var item in pear.clients)
            //{
            //    viewModel obj = new viewModel();
            //    obj.id = item.id;
            //    obj.Name = item.Name;
            //    obj.Address = item.Address;
            //    obj.Mobile = item.Mobile;
            //    obj.DateOfBirth = item.DateOfBirth;
            //    obj.imageName = item.imageName;
            //    db.Add(obj);
            //}
            return View(xlist.ToList());
        }
        public ActionResult confUpdate(viewModel client)
        {
           
            if (Request.Files.Count > 0)
            {
                try
                {
                    client cl = pear.client.FirstOrDefault(x => x.id == client.id);
                    //Client cl = new Client();
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
                    //pear.Client.Add(cl);
                    pear.SaveChanges();

                }

                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
            }
            List<viewModel> db = new List<viewModel>();

            foreach (var item in pear.client)
            {
                viewModel obj = new viewModel();
                obj.id = item.id;
                obj.Name = item.Name;
                obj.Address = item.Address;
                obj.Mobile = item.Mobile;
                obj.DateOfBirth = item.DateOfBirth;
                obj.imageName = item.imageName;
                db.Add(obj);
            }
            return RedirectToAction("Create2", db.ToList());
        }
        public ActionResult Delete(int id)
        {
            
            client cli = pear.client.FirstOrDefault(x => x.id == id);
            pear.client.Remove(cli);
            pear.SaveChanges();
            List<viewModel> db = new List<viewModel>();

            foreach (var item in pear.client)
            {
                viewModel obj = new viewModel();
                obj.id = item.id;
                obj.Name = item.Name;
                obj.Address = item.Address;
                obj.Mobile = item.Mobile;
                obj.DateOfBirth = item.DateOfBirth;
                obj.imageName = item.imageName;
                db.Add(obj);
            }
            return RedirectToAction("Create2", db.ToList());
        }
        public ActionResult Update(int id)
        {
            
            client cli = pear.client.FirstOrDefault(x => x.id == id);
            viewModel obj = new viewModel();
            obj.id = cli.id;
            obj.Address = cli.Address;
            obj.DateOfBirth = cli.DateOfBirth;
            obj.imageName = cli.imageName;
            obj.Mobile = cli.Mobile;
            obj.Name = cli.Name;

            return View(obj);
        }

    }
}