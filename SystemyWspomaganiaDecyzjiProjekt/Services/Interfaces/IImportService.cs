using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Models;

namespace SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces
{
    public interface IImportService
    {
        DataTable ImportTextData(UploadFileModel model);
        DataTable ImportExcelData(IFormFile file);
        DataTable ImportData(UploadFileModel model);
    }
}
