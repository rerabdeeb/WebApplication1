using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class collectionController : Controller
    {
        pearprojectEntities1 context = new pearprojectEntities1();
        // GET: collection
        public ActionResult Index()
        {
            
            List<viewModel> db = new List<viewModel>();
            var xlist = context.client.Select(z => new viewModel
            {
                Address = z.Address,
                DateOfBirth = z.DateOfBirth,
                Name=z.Name,
                imageName=z.imageName,
                id=z.id,
                Mobile=z.Mobile,
                
        }).ToList();
          
            ViewBag.clients = xlist.ToList();
            ViewBag.types = new MultiSelectList(context.clientType.ToList(), "id", "type");


            return View();
        }
        public List<clientType> showMultiple(int id)
        {
            List<clientType> types = context.clientType.Where(x => x.clientID == id).ToList();

            return (types);
        }
        public ActionResult showImages(int id)
        {
            List<images> images = context.images.Where(x => x.clientID == id).ToList();
            
            ViewBag.imag = images;
            return View("index");
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            
            client cli = context.client.FirstOrDefault(x => x.id == id);
            context.client.Remove(cli);
            context.SaveChanges();
            if (context.SaveChanges() > 0) {
                ViewBag.alert = "deleted successfully";
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.alert = "something wrong while delete";
                return RedirectToAction("index");
            }

        }

        

        public ActionResult confirmEdit()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formCollection, HttpPostedFileBase imageName, HttpPostedFileBase[] imagesName)
        {
            if (ModelState.IsValid)
            {
               int id= Convert.ToInt32( formCollection["id"]);
                client client = new client();
                client = context.client.Where(x => x.id == id).FirstOrDefault();

                string fileName = imageName.FileName;
                Directory.CreateDirectory(Server.MapPath("~/images/"));
                string path = Path.Combine(Server.MapPath("~/images"), fileName);
                imageName.SaveAs(path);
                
                client.Name = formCollection["Name"];
                client.Address = formCollection["Address"];
                 client.DateOfBirth = Convert.ToDateTime( formCollection["DateOfBirthstr"]);
                client.Mobile = formCollection["Mobile"];
                client.imageName = fileName;
                foreach (var item in imagesName)
                {
                    images imgs = new images();
                    string fileimagesName = item.FileName;
                    Directory.CreateDirectory(Server.MapPath("~/images/"));
                    string pathimages = Path.Combine(Server.MapPath("~/images"), fileimagesName);
                    item.SaveAs(pathimages);
                    imgs.clientID = client.id;
                    imgs.images1 = fileimagesName;
                    context.images.Add(imgs);
                    context.SaveChanges();

                }

                context.SaveChanges();

                
               return RedirectToAction("Index");
                
            }
            else
            {
                return View();
            }
           
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var client = context.client.Where(x => x.id == id).FirstOrDefault();
            viewModel Client1 = Mapper.Map<client, viewModel>(client);
          
            if (Client1 != null) {
                ViewBag.client1 = Client1;
                ViewBag.check = true;
                //return RedirectToAction("Index", Client1);
                return View("Index",Client1);
            }
            else
            {
                return Content("fffff");
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(FormCollection formCollection, HttpPostedFileBase imageName, int[] ClientTypes, HttpPostedFileBase[] imagesName)
        {

            
                if (ModelState.IsValid&& formCollection["up"]==null)
                {
                    string fileName = imageName.FileName;
                    Directory.CreateDirectory(Server.MapPath("~/images/"));
                    string path = Path.Combine(Server.MapPath("~/images"), fileName);
                    imageName.SaveAs(path);
                    client client = new client();
                    client.Name = formCollection["Name"];
                    client.Address = formCollection["Address"];
                    client.DateOfBirth = Convert.ToDateTime(formCollection["DateOfBirth"]);
                    client.Mobile = formCollection["Mobile"];
                    client.imageName = fileName;
                context.client.Add(client);
                context.SaveChanges();
                foreach (var item in imagesName)
                {
                    images imgs = new images();
                    string fileimagesName = item.FileName;
                    Directory.CreateDirectory(Server.MapPath("~/images/"));
                    string pathimages = Path.Combine(Server.MapPath("~/images"), fileimagesName);
                    item.SaveAs(pathimages);
                    imgs.clientID = client.id;
                    imgs.images1 = fileimagesName;
                    context.images.Add(imgs);
                    context.SaveChanges();

                }
                foreach (var item in ClientTypes)
                {
                    clientType types = new clientType();
                    types.clientID = client.id;
                    types.typeID = item;
                    context.clientType.Add(types);
                    context.SaveChanges();

                }
                return RedirectToAction("Index");
                } 
            else if(ModelState.IsValid )
            {
                int id = Convert.ToInt32(formCollection["id"]);
                client client = new client();
                client = context.client.Where(x => x.id == id).FirstOrDefault();

                string fileName = imageName.FileName;
                Directory.CreateDirectory(Server.MapPath("~/images/"));
                string path = Path.Combine(Server.MapPath("~/images"), fileName);
                imageName.SaveAs(path);

                client.Name = formCollection["Name"];
                client.Address = formCollection["Address"];
                client.DateOfBirth = Convert.ToDateTime(formCollection["DateOfBirthstr"]);
                client.Mobile = formCollection["Mobile"];
                client.imageName = fileName;


                context.SaveChanges();


                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult update(FormCollection formCollection)
        {
            return View();
        }
        public ActionResult getAllData()
        {
            return View();
        }
    }
}