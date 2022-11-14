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
    public class LanguageController : ApiController
    {
        private ApiResponse apiResponse;
        public async Task<IHttpActionResult> Get()
        {
            var response = await Task.Run(() =>
            {
                apiResponse = new ApiResponse();
                var mng = new LanguageManager();
                apiResponse.Data = mng.RetrieveAll();
                return apiResponse;
            });
            return Ok(response);
        }

        public async Task<IHttpActionResult> Post(LanguageType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new LanguageManager();
                mng.Create(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        public async Task<IHttpActionResult> Get(string name)
        {
            var entity = new LanguageType
            {
                Name = name
            };

            var response = await Task.Run(() =>
            {
                var mng = new LanguageManager();
                entity = mng.RetrieveById(entity);
                apiResponse = new ApiResponse();
                apiResponse.Data = entity;
                return apiResponse;
            });
            return Ok(response);
        }

        public async Task<IHttpActionResult> Put(LanguageType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new LanguageManager();
                mng.Update(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        public async Task<IHttpActionResult> Delete(LanguageType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new LanguageManager();
                mng.Delete(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public async Task<IHttpActionResult> PopularLanguage()
        {
            var response = await Task.Run(() =>
            {
                var mng = new LanguageManager();
                var entity = mng.RetrievePopularLanguage();
                apiResponse = new ApiResponse();
                apiResponse.Data = entity;
                return apiResponse;
            });
            return Ok(response);
        }

        [HttpGet]
        public async Task<IHttpActionResult> LanguagesOfWord(string originWord)
        {
            var response = await Task.Run(() =>
            {
                var mng = new LanguageManager();
                var entity = mng.RetrieveLanguagesOfWord(originWord);
                apiResponse = new ApiResponse();
                apiResponse.Data = entity;
                return apiResponse;
            });
            return Ok(response);
        }
    }
}
