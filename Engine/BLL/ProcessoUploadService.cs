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

        public async Task<Pagination> GetPagination(int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                return await processoUploadDAO.GetPagination(take, filters);
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

        public async Task<List<ProcessoUpload>> GetAll(int processoID)
        {
            try
            {
                return await processoUploadDAO.GetAll(processoID);
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

        public double GetState(ProcessoUpload processoUpload)
        {
            try
            {
                if (Directory.Exists(processoUpload.PastaZip))
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

        public bool Edit(ProcessoUpload processoUpload)
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
