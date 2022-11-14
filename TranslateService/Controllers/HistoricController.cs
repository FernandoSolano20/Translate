using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Entity_POJOS;
using TranslateCoreAPI;
using TranslateService.Models;

namespace TranslateService.Controllers
{
    public class HistoricController : ApiController
    {
        private ApiResponse apiResponse;
        public async Task<IHttpActionResult> Get()
        {
            var response = await Task.Run(() =>
            {
                apiResponse = new ApiResponse();
                var mng = new HistoricManager();
                apiResponse.Data = mng.RetrieveAll();
                return apiResponse;
            });
            return Ok(response);
        }

        public async Task<IHttpActionResult> Post(HistoricType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new HistoricManager();
                mng.Create(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        public async Task<IHttpActionResult> Get(int idWord, string user, string date)
        {
            var entity = new HistoricType
            {
                Word = new WordType { Id = idWord },
                User = new UserType { Name = user },
                DateNow = DateTime.Parse(date)
            };

            var response = await Task.Run(() =>
            {
                var mng = new HistoricManager();
                entity = mng.RetrieveById(entity);
                apiResponse = new ApiResponse();
                apiResponse.Data = entity;
                return apiResponse;
            });
            return Ok(response);
        }

        public async Task<IHttpActionResult> Put(HistoricType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new HistoricManager();
                mng.Update(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        public async Task<IHttpActionResult> Delete(HistoricType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new HistoricManager();
                mng.Delete(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public async Task<IHttpActionResult> HistoricOfWord(string originWord)
        {
            var response = await Task.Run(() =>
            {
                apiResponse = new ApiResponse();
                var mng = new HistoricManager();
                apiResponse.Data = mng.HistoricOfWord(originWord);
                return apiResponse;
            });
            return Ok(response);
        }
    }
}
