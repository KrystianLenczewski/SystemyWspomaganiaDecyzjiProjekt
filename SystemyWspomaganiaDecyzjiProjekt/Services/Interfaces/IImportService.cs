using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces
{
    public interface IImportService
    {
        DataTable ImportTextData(IFormFile file);
        DataTable ImportExcelData(IFormFile file);
        DataTable ImportData(IFormFile file);
    }
}
