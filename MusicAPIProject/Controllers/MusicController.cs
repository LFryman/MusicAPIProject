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

        public async Task<IActionResult> DisplayAlbumFavorites()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<AlbumT> savedFaves = _musicDb.AlbumT.Where(x => x.UserId == id).ToList();
            List<Album> favoritesList = new List<Album>();

            foreach (AlbumT a in savedFaves)
            {
                var search = await _musicDAL.GetAlbum(a.Apiid);
                favoritesList.Add(search);
            }
            return View(favoritesList);
        }

        public IActionResult SaveFavoriteAlbum(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            AlbumT foundAlbum = new AlbumT();
            try
            {
                foundAlbum = _musicDb.AlbumT.Where(x => x.Apiid == id).First();
            }
            catch
            {
                foundAlbum.UserId = userId;
                foundAlbum.Apiid = id;
                _musicDb.AlbumT.Add(foundAlbum);
                _musicDb.SaveChanges();
                return RedirectToAction("DisplayAlbumFavorites");
            }
            return View("MusicIndex");

        }
        public IActionResult DeleteAlbum(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AlbumT foundAlbum = new AlbumT();
            try
            {
                foundAlbum.UserId = userId;
                foundAlbum.Apiid = id;
                foundAlbum = _musicDb.AlbumT.Where(x => x.Apiid == id).First();
                _musicDb.AlbumT.Remove(foundAlbum);
                _musicDb.SaveChanges();
                return RedirectToAction("DisplayAlbumFavorites");
            }
            catch
            {
                return RedirectToAction("DisplayAlbumFavorites");
            }
        }
    }
}
