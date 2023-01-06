using ChessPlayersRatingApp.Data;
using ChessPlayersRatingApp.Models;
using ChessPlayersRatingApp.Models.ViewModel;
using ChessPlayersRatingApp.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ChessPlayersRatingApp.Controllers
{
    public class InformationController : Controller
    {
        private readonly AppDbContext _db;

        public InformationController(AppDbContext db)
        {
            _db = db;
        }


        // GET: InformationController
        public ActionResult Index()
        {
            IEnumerable<Information> objList = _db.Information;

            return View(objList);
        }

        //GET - UPSERT
        public async Task<IActionResult> Upsert(int? id)
        {
            var information = _db.Information.Where(x => x.PlayerId == id).FirstOrDefault();
            var informationView = new InformationView();

            if (information != null)
            {
                informationView.Information = information;
                return View(informationView);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (informationView.Information == null)
                    {
                        //Creating
                        informationView.Information = new Information
                        {
                            BaseInfoText = WikiParser.GetExtract(id.Value),
                            Image = WikiParser.GetPhoto(id.Value),
                            Player = _db.Players.Find(id),
                        };

                        _db.Information.Add(informationView.Information);

                    }
                    else
                    {
                        var objFromDb = _db.Information.AsNoTracking().FirstOrDefault(x => x.Id == informationView.Information.Id);

                        if (objFromDb != null)
                        {
                            informationView.Information.BaseInfoText = objFromDb.BaseInfoText;
                            informationView.Information.Image = objFromDb.Image;

                            _db.Information.Update(informationView.Information);
                        }
                    }
                    _db.SaveChanges();

                    return View(informationView);
                }
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //POST - UPSERT
        [HttpPost]
        public async Task<IActionResult> Upsert(InformationView informationView)
        {
            var objFromDb = _db.Information.AsNoTracking().FirstOrDefault(u => u.Id == informationView.Information.Id);
            if (objFromDb != null)
            {
                objFromDb.BaseInfoText = informationView.Information.BaseInfoText;
            }
            else
            {
                return RedirectToAction("Upsert");
            }
            _db.Information.Update(objFromDb);
            _db.SaveChanges();
            return RedirectToAction("Upsert");
        }


        // GET: InformationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var obj = _db.Information.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Information.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Information/GetImageFromByteArray  
        public ActionResult GetImageFromByteArray(byte[] images)
        {
            string imreBase64Data = Convert.ToBase64String(images);
            string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            ViewBag.ImageData = imgDataURL;
            return View();
        }
    }
}
