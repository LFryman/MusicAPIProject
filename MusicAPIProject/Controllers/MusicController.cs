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
            //var album = await _musicDAL.GetArtist(); 
            //return View(album);
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public async Task<IActionResult> SearchResult(string userInput) //also drop down selection
        {
            var search = await _musicDAL.GetArtist(userInput);
            //viewbag with drop down input
            return View(search);
        }

        public async Task<IActionResult> SearchResult()
        {
            return View();
        }
    }
}
