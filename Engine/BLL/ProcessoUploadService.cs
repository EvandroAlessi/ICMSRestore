using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class ProcessoUploadService
    {
        private static readonly ProcessoUploadDAO processoUploadDAO = new ProcessoUploadDAO();

        public async Task<Pagination> GetPagination(int page, int take, Dictionary<string, string> filters)
        {
            try
            {
                return await processoUploadDAO.GetPagination("ProcessosUpload", page, take, filters);
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

                return await processoUploadDAO.GetAll(skip, take, filters);
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

                return await processoUploadDAO.GetAll(processoID, skip, take);
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
                return await processoUploadDAO.Get(id);
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
                return await processoUploadDAO.Get(processoID, zipPath);
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
                if (processoUpload.Ativo && Directory.Exists(processoUpload.PastaZip))
                {
                    double restFiles = Directory.GetFiles(processoUpload.PastaZip).Count();

                    double processedFiles = processoUpload.QntArq - restFiles;

                    double rest = (double)(processedFiles / processoUpload.QntArq);

                    double percent = (double)(rest * 100.0);

                    return percent;
                }
                else
                {
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
                var errorPath = Path.Combine(path, "Error");

                if (Directory.Exists(errorPath))
                {
                    return Directory.GetFiles(errorPath).Count();
                }
                else
                {
                    return 0;
                }
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
                return await processoUploadDAO.Exists(id);
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
                return processoUploadDAO.Insert(processoUpload);
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
                return processoUploadDAO.Edit(processoUpload);
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
                return processoUploadDAO.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
