using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlobStorageApi.Controllers
{
    [Route("/api/blob")]
    public class BlobController : ControllerBase
    {
        static Dictionary<int, byte[]> _blobs = new Dictionary<int, byte[]>();

        [HttpGet]
        [Route("{blobId}")]
        public IActionResult GetBinaryDataManual(int blobId)
        {
            byte[] blobValue;
            if (_blobs.TryGetValue(blobId, out blobValue))
            {
                return File(blobValue, "application/octet-stream");
            }

            return NotFound();
         }

        [HttpPost]
        [Route("PostBinaryDataManual")]
        public async Task<int> PostBinaryDataManual()
        {
            using (var ms = new MemoryStream(2048))
            {
                await Request.Body.CopyToAsync(ms);
                byte[] blobValue = ms.ToArray();
                int id = _blobs.Keys.OrderBy(ky => ky).Last() + 1;
                _blobs.Add(id, blobValue);
                return id;
            }
        }
    }
}
