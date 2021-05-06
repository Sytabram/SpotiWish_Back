using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotiWish_back.Controllers.Exception;
using SpotiWish_back.Model;
using SpotiWish_back.Services.Interface;

namespace SpotiWish_back.Controllers
{
    [ApiController]
    public class musicController : ControllerBase
    {
        private readonly IPlayListService _playListService;
        private readonly IMapper _mapper;
        public musicController(IPlayListService playListService, IMapper mapper)
        {
            _playListService = playListService;
            _mapper = mapper;
        }
        
    }
}