using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Services
{
    public class ImportService : IImportService
    {

        public ImportService()
        {

        }

        public DataTable ImportData(IFormFile file)
        {
            string fileName = Path.GetExtension(file.FileName);
            if (fileName == ".xlsx")
            {
               return ImportExcelData(file);
            }
            else if (fileName == ".txt")
            {
               return ImportTextData(file);
            }
            throw new Exception($"{fileName} is not supported");
        }

        public DataTable ImportExcelData(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public DataTable ImportTextData(IFormFile file)
        {
            DataTable dt = new DataTable();

                if (file.Length > 0)
                {
                StreamReader stream = new StreamReader(file.OpenReadStream());

                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    if (!(line.StartsWith("#") || String.IsNullOrEmpty(line.Trim())))
                    {

                        if (dt.Columns.Count == 0)
                        {
                            string[] columnHeders = line.Split("\t");
                            foreach (var item in columnHeders)
                            {
                                dt.Columns.Add(item);
                            }
                        }
                        else
                        {

                            var row = dt.NewRow();
                            string[] cells = line.Split("\t");

                            for (int i = 0; i < cells.Length; i++)
                            {
                                row[i] = cells[i];
                            }
                            dt.Rows.Add(row);
                        }
                    }
                }
            }
            return dt;
        }
    }
}
