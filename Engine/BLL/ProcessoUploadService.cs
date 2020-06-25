using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static CrossCutting.LOG;

namespace BLL
{
    public class ProcessoUploadService
    {
        private static readonly ProcessoUploadDAO dao = new ProcessoUploadDAO();

        public async Task<long> GetCount()
        {
            try
            {
                return await dao.GetCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pagination> GetPagination(int page, int take, Dictionary<string, string> filters)
        {
            try
            {
                return await dao.GetPagination(page, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProcessoUpload>> GetAll(int page = 1, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                int skip = (page - 1) * take;

                return await dao.GetAll(skip, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProcessoUpload>> GetAll(int processoID, int page = 1, int take = 30)
        {
            try
            {
                int skip = (page - 1) * take;

                return await dao.GetAll(processoID, skip, take);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProcessoUpload> Get(int id)
        {
            try
            {
                return await dao.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProcessoUpload> Get(int processoID, string zipPath)
        {
            try
            {
                return await dao.Get(processoID, zipPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double GetState(ProcessoUpload processoUpload)
        {
            try
            {
                if (!processoUpload.Ativo)
                {
                    return 100.0;
                }
                else if (File.Exists(processoUpload.PastaZip + ".zip"))
                {
                    return 0.0;
                }
                else if(Directory.Exists(processoUpload.PastaZip) && Directory.GetFiles(processoUpload.PastaZip).Count() > 0)
                {
                    double restFiles = Directory.GetFiles(processoUpload.PastaZip).Count();

                    double processedFiles = processoUpload.QntArq - restFiles;

                    double rest = (double)(processedFiles / processoUpload.QntArq);

                    double percent = (double)(rest * 100.0);

                    return percent;
                }
                else
                {
                    processoUpload.Ativo = false;

                    processoUpload = Edit(processoUpload);

                    if (processoUpload is null)
                    {
                        Log(func: $"ProcessoUploadService.{ MethodBase.GetCurrentMethod().Name }",
                            message: "Erro ao realizar update",
                            parameters: new
                            {
                                ID = processoUpload.ID,
                                ProcessoID = processoUpload.ProcessoID,
                                PastaZip = processoUpload.PastaZip,
                            });
                    }

                    return 100.0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetErrorFiles(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    var errorPath = Path.Combine(path, "Error");

                    if (Directory.Exists(errorPath))
                    {
                        return Directory.GetFiles(errorPath).Count();
                    }
                }
                    
                
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Exists(int id)
        {
            try
            {
                return await dao.Exists(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcessoUpload Insert(ProcessoUpload processoUpload)
        {
            try
            {
                return dao.Insert(processoUpload);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcessoUpload Edit(ProcessoUpload processoUpload)
        {
            try
            {
                return dao.Edit(processoUpload);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return dao.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
