using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Models
{
    public class UploadFileModel
    {
        public IFormFile File { get; set; }
        public bool IsHeader { get; set; }
    }
}
