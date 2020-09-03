using System;
using System.Collections.Generic;
using System.Linq;
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

        public MusicController(IConfiguration configuration)
        {
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

    }
}
