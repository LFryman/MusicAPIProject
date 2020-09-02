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

        public async Task<IActionResult> MusicIndex()
        {
            var album = await _musicDAL.GetArtist(); 
            return View(album); 
        }
        //public IActionResult Index()
        //{
        //    var AlbumList = _musicDAL.ToList();
        //    return View(AlbumList);
        //}

    }
}
