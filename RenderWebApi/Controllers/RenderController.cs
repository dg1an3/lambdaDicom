using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using RenderWebApi.Model;

namespace RenderWebApi.Controllers
{
    [Route("/api/render")]
    public class RenderController : Controller
    {
        [HttpGet("volume/{volumeId}")]
        IActionResult RenderMpr(Guid volumeId, [FromBody] SceneModel forScene)
        {
            return this.File(new byte[100], "application/jpeg");
        }
    }
}
