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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

        // GET: InformationController/Upsert/5
        //public IActionResult Upsert(int? id)
        //{
        //    InformationView informationView = new InformationView()
        //    {
        //        Information = new Information(),
        //        Player = _db.Players.Select(i => new SelectListItem
        //        {
        //            Text = i.Name,
        //            Value = i.Id.ToString()
        //        })
        //    };

        //    if(id == null)
        //    {
        //        return View(null);
        //    }
        //    else
        //    {
        //        informationView.Information = _db.Information.Find(id);
                
        //        if(informationView == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(informationView);
        //    }
        //}

        // POST: InformationController/Upsert
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(InformationView informationView)
        {
            int? PlayerId = 1;
            if (ModelState.IsValid)
            {
                if(informationView.Information == null)
                {
                    //Creating
                    var text = WikiParser.GetExtract(PlayerId.Value);
                    informationView.Information= new Information
                    {
                        BaseInfoText = text,
                        Image = WikiParser.GetPhotoAsync(PlayerId.Value),
                    Player = _db.Players.Find(PlayerId),
                };
                    //informationView.Player = _db.Players.Find(PlayerId);
                    //informationView.Information.Image = WikiParser.GetPhotoAsync(PlayerId.Value);

                    _db.Information.Add(informationView.Information);

                }
                else
                {
                    //Updating
                    //TODO
                    var objFromDb = _db.Information.AsNoTracking().FirstOrDefault(x => x.Id == informationView.Information.Id);

                    if (objFromDb != null)
                    {
                        informationView.Information.BaseInfoText = objFromDb.BaseInfoText;
                        informationView.Information.Image = objFromDb.Image;

                        _db.Information.Update(informationView.Information);
                    }
                }
                _db.SaveChanges();
                //informationView.Player = (Player)_db.Players.Select(x => x.InformationId == informationView.Information.Id);

                return View(informationView);
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: InformationController/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            Information information = _db.Information.FirstOrDefault(x => x.Id == id);

            return View(information);
        }

        // POST: InformationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
