using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlFreamworkProjectWeb.Database.DataContext;
using MySqlFreamworkProjectWeb.Database.Entity;
using MySqlFreamworkProjectWeb.UXI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlFreamworkProjectWeb.UXI.Controllers
{
    public class HomeController : Controller
    {
        private MySqlDatabaseContext _context = new MySqlDatabaseContext();
        [Route("kullanici-listele"), Route(""), HttpGet]
        public IActionResult Index() => View();

        [Route("yeni-kullanici-ekle"),HttpGet]
        public IActionResult Create() => View();

        [HttpPost, Route("yeni-kullanici-ekle")]
        public async Task<IActionResult> Create(AdminUser admin)
        {
            var data = _context.AdminUsers.Where(x => !x.DeletionStatus && x.Email == admin.Email).ToList();
            if (data.Count() == 0)
            {
                admin.DeletionStatus = false;
                admin.CreateDateTime = DateTime.Now;
                admin.LastDateTime = DateTime.Now;
                _context.AdminUsers.Add(admin);
                await _context.SaveChangesAsync();
                return Redirect("/kullanici-listele");
            }
            else
            {
                ViewBag.Hata = admin.Email + " sistemde bu mail adresine kayıtlı mail bulunmaktadır.";
                return View(admin);
            }
        }

        [HttpGet, Route("kullanici-guncelle/{ID}")]
        public IActionResult Edit(int? ID)
        {
            if (ID == null)
            {
                return View("Index", "Home");
            }
            var data = _context.AdminUsers.Find(ID);
            if (data == null)
            {
                return View("Index", "Home");
            }
            return View(data);
        }
        [HttpPost, Route("kullanici-guncelle/{ID}")]
        public async Task<IActionResult> Edit(int ID, AdminUser admin)
        {
            var data = _context.AdminUsers.Find(ID);
            var control = _context.AdminUsers.Where(x => !x.DeletionStatus && x.Email == admin.Email && x.ID != ID).ToList();
            if (control.Count() == 0)
            {
                data.DeletionStatus = false;
                data.Email = admin.Email;
                data.LastDateTime = DateTime.Now;
                data.Name = admin.Name;
                data.Password = admin.Password;
                data.Surname = admin.Surname;
                _context.AdminUsers.Update(data);
                await _context.SaveChangesAsync();
                return Redirect("/kullanici-listele");
            }
            else
            {
                ViewBag.Hata = admin.Email + " sistemde bu mail adresine kayıtlı mail bulunmaktadır.";
                return View(admin);
            }
        }

        [Route("kullanici-sil/{ID}"),HttpGet]
        public async Task<IActionResult> Delete(int ID)
        {
            try
            {
                var data = _context.AdminUsers.Find(ID);
                _context.AdminUsers.Remove(data);
                await _context.SaveChangesAsync();
                return Redirect("/kullanici-listele");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("kullanici-getir/{searchText?}"),HttpGet]
        public JsonResult GetData(string searchText)
        {
            List<AdminUser> data = new List<AdminUser>();

            if (searchText == "" || searchText == null)
            {
                data = _context.AdminUsers.Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = _context.AdminUsers.Where(x => !x.DeletionStatus && x.Name == searchText).ToList();
            }
            var query = new
            {
                Result = (from obj in data
                         select new
                         {
                             ID = obj.ID,
                             Name = obj.Name,
                             Surname = obj.Surname,
                             Email = obj.Email,
                             Password = obj.Password,
                         }).ToList()
            };
            return Json(query);
        }
    }
}
