using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicAPIProject.Models;

namespace MusicAPIProject.Controllers
{
    public class MusicController : Controller
    {
        private readonly MusicDAL _musicDAL;
        private readonly string _apiKey;
        private readonly MusicDBContext _musicDb; 

        public MusicController(IConfiguration configuration, MusicDBContext musicDB)
        {
            _musicDb = musicDB; 
            _apiKey = configuration.GetSection("ApiKeys")["MusicAPIKey"];
            _musicDAL = new MusicDAL(_apiKey);
        }

        public IActionResult MusicIndex()
        {

            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public async Task<IActionResult> SearchResult(string userInput, string searchtype) 

        {
            if (searchtype == "artist")
            {
                var search = await _musicDAL.GetInfo(userInput);
                return View("SearchResultArtist", search);
            }
            else if (searchtype == "album")
            {
                var search = await _musicDAL.GetInfo(userInput);
                return View("SearchResultAlbum", search);
            }
            else
            {
                return RedirectToAction("MusicIndex");
            }
        }

        //public IActionResult AddAlbumToList()
        //{
        //    string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    List<Album> faveAlbumList = _musicDAL.GetAlbum.Where(x => x.UserID == id).ToList();
        //    return View(faveAlbumList);
        //}

        public IActionResult SaveFavoriteAlbum(int id)
        {
            var foundAlbum = _musicDb.AlbumT.Where(x => x.Apiid == id).First();
            if (foundAlbum == null)
            {
                _musicDb.AlbumT.Add(foundAlbum);
                _musicDb.SaveChanges();
                return View("MusicIndex"); 
            }
            else
            {
                return View("MusicIndex"); 
            }
        }
    }
}
