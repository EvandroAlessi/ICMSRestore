﻿using BLL;
using CrossCutting;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/mva")]
    public class MVAController : ControllerBase
    {
        private static readonly MVAService service = new MVAService();

        // GET: api/<MVAController>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> Get(int page = 1, int take = 30, [FromQuery] Dictionary<string, string> filters = null)
        {
            try
            {
                var response = new
                {
                    MVAs = await service.GetAll(page, take, filters),
                    Pagination = await service.GetPagination(page, take, filters)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<MVAController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var mva = await service.Get(id);

                if (mva is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(mva);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                return Ok(await service.GetCount());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<MVAController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mva"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] MVA mva)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<string> errors = new List<string>();

                    foreach (var item in ModelState.Values)
                    {
                        errors.AddRange(item.Errors.Select(x => x.ErrorMessage).Where(x => !string.IsNullOrEmpty(x)));
                    }

                    return BadRequest(errors);
                }
                else
                {
                    mva = service.Insert(mva);

                    if (mva is null)
                    {
                        return BadRequest("Can't complete the insert process, please verify the data send.");
                    }
                    else
                    {
                        return Ok(mva);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //[HttpPost("import")]
        //public async Task<IActionResult> Post([FromForm] IFormFile file)
        //{
        //    try
        //    {
        //        if (file == null || file.Length == 0)
        //            return Content("File Not Selected");

        //        string fileExtension = Path.GetExtension(file.FileName);

        //        if (fileExtension == ".xls" || fileExtension == ".xlsx")
        //        {
        //            var rootFolder = AppSettings.RootPath;
        //            var fileName = file.FileName;
        //            var filePath = Path.Combine(rootFolder, fileName);
        //            var fileLocation = new FileInfo(filePath);

        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await file.CopyToAsync(fileStream);
        //            }

        //            if (file.Length <= 0)
        //                return BadRequest("FileNotFound");

        //            string con = @"Provider=Microsoft.ACE.OLEDB;Data Source=" + fileLocation + @"Extended Properties='Excel 12.0 Xml;HDR=Yes;'";

        //            using (OleDbConnection connection = new OleDbConnection(con))
        //            {
        //                connection.Open();

        //                OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);

        //                using (OleDbDataReader dr = command.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        var row1Col0 = dr[0];
        //                        Console.WriteLine(row1Col0);
        //                    }
        //                }
        //            }



        //            //using (ExcelPackage package = new ExcelPackage(fileLocation))
        //            //{
        //            //    ExcelWorksheet workSheet = package.Workbook.Worksheets["Table1"];
        //            //    //var workSheet = package.Workbook.Worksheets.First();
        //            //    int totalRows = workSheet.Dimension.Rows;

        //            //    //var DataList = new List<Customers>();

        //            //    for (int i = 2; i <= totalRows; i++)
        //            //    {
        //            //        //DataList.Add(new Customers
        //            //        // {
        //            //        var CustomerName = workSheet.Cells[i, 1].Value.ToString();
        //            //        var CustomerEmail = workSheet.Cells[i, 2].Value.ToString();
        //            //        var CustomerCountry = workSheet.Cells[i, 3].Value.ToString();
        //            //        //});
        //            //    }

        //            //    //_db.Customers.AddRange(customerList);
        //            //    //_db.SaveChanges();
        //            //}
        //        }

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        // PUT api/<MVAController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mva"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MVA mva)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<string> errors = new List<string>();

                    foreach (var item in ModelState.Values)
                    {
                        errors.AddRange(item.Errors.Select(x => x.ErrorMessage).Where(x => !string.IsNullOrEmpty(x)));
                    }

                    return BadRequest(errors);
                }
                else if (mva.ID != id)
                {
                    return BadRequest("The ID in the object is different from the indicates in the URL.");
                }
                else
                {
                    bool exists = service.Exists(id).Result;

                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var editedMVA = service.Edit(mva);

                        if (editedMVA is null)
                        {
                            return BadRequest("Can't complete the edit process, please verify the data send.");
                        }
                        else
                        {
                            return Ok(editedMVA);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<MVAController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool exists = service.Exists(id).Result;

                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    bool deleted = service.Delete(id);

                    if (deleted)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("Can't complete the delete process, please verify the data send.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
