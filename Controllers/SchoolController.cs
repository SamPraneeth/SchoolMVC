using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SchoolMVC.Models;
using System.Collections.Specialized;
using System.Text;
using System.Net;

namespace SchoolMVC.Controllers
{
    public class SchoolController : Controller
    {
        // GET: School
        public ActionResult Index()
        {
            IEnumerable<StudentView> empdata = null;

            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44310/api/";

                var json = webClient.DownloadString("Students");
                var list = JsonConvert.DeserializeObject<List<StudentView>>(json);
                empdata = list.ToList();
                return View(empdata);
            }
        }

        // GET: School/Details/5
        public ActionResult Details(int id)
        {
           StudentView supdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44310/api/";

                var json = webClient.DownloadString("Students/" + id);
                //  var list = emp 
                supdata = JsonConvert.DeserializeObject<StudentView>(json);
            }
            return View(supdata);
        }

        // GET: School/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: School/Create
        [HttpPost]
        public ActionResult Create(StudentView model)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44310/api/";
                    var url = "Students/POST";
                    //webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);
                    var response = webClient.UploadString(url, data);
                    JsonConvert.DeserializeObject<StudentView>(response);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        // GET: School/Edit/5
        public ActionResult Edit(int id)
        {
            StudentView supdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44310/api/";

                var json = webClient.DownloadString("Students/" + id);
                //  var list = emp 
                supdata = JsonConvert.DeserializeObject<StudentView>(json);
            }
            return View(supdata);
        }

        // POST: School/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, StudentView model)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44310/api/Students/" + id;
                    //var url = "Values/Put/" + id;
                    //webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);

                    var response = webClient.UploadString(webClient.BaseAddress, "PUT", data);

                    StudentView modeldata = JsonConvert.DeserializeObject<StudentView>(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        // GET: School/Delete/5
        public ActionResult Delete(int id)
        {
            StudentView empdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44310/api/";

                var json = webClient.DownloadString("Students/" + id);
                //  var list = emp 
                empdata = JsonConvert.DeserializeObject<StudentView>(json);
            }
            return View(empdata);
        }

        // POST: School/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                using (WebClient webClient = new WebClient())
                {

                    // webClient.BaseAddress = "https://localhost:44310/api/Students/" + id;

                    NameValueCollection nv = new NameValueCollection();
                    var url = "https://localhost:44310/api/Students/" + id;
                    var response = Encoding.ASCII.GetString(webClient.UploadValues(url, "DELETE", nv));

                    //webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    //webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    // string data = JsonConvert.SerializeObject(model);

                    // var response = webClient.UploadString(webClient.BaseAddress, data);

                    //SupplierView modeldata = JsonConvert.DeserializeObject<SupplierView>(response);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}
