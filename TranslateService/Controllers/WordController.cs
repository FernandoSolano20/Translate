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
    public class WordController : ApiController
    {
        private ApiResponse apiResponse;
        public async Task<IHttpActionResult> Get()
        {
            var response = await Task.Run(() =>
            {
                apiResponse = new ApiResponse();
                var mng = new WordManager();
                apiResponse.Data = mng.RetrieveAll();
                return apiResponse;
            });
            return Ok(response);
        }

        public async Task<IHttpActionResult> Post(WordType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new WordManager();
                mng.Create(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        public async Task<IHttpActionResult> Get(string originWord, string originLang, string destLang, string userName)
        {
            var entity = new WordType
            {
                OriginWord = originWord,
                OriginLanguage = new LanguageType { Name = originLang },
                DestinationLanguage = new LanguageType { Name = destLang }
            };

            var response = await Task.Run(() =>
            {
                var mng = new WordManager();
                entity = mng.RetrieveById(entity);
                apiResponse = new ApiResponse();
                apiResponse.Data = entity;
                return apiResponse;
            });

            if (entity != null)
            {
                var apiTasks = new List<Task>();

                var historic = new HistoricType();
                historic.User = new UserType{Name = userName};
                historic.DateNow = DateTime.Now;
                historic.Word = entity;

                var updateWordTask = Task.Run(() =>
                {
                    var mng = new WordManager();
                    entity.Popularity++;
                    mng.Update(entity);
                    apiResponse = new ApiResponse();
                    apiResponse.Message = "Action was executed.";
                    return apiResponse;
                });
                apiTasks.Add(updateWordTask);


                var createHistoricTask = Task.Run(() =>
                {
                    var mng = new HistoricManager();
                    mng.Create(historic);
                    apiResponse = new ApiResponse();
                    apiResponse.Message = "Action was executed.";
                    return apiResponse;
                });
                apiTasks.Add(createHistoricTask);

                Task.WaitAll(apiTasks.ToArray());
            }
            return Ok(response);
        }

        public async Task<IHttpActionResult> Put(WordType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new WordManager();
                mng.Update(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        public async Task<IHttpActionResult> Delete(WordType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new WordManager();
                mng.Delete(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public async Task<IHttpActionResult> PopularWord()
        {
            var response = await Task.Run(() =>
            {
                var mng = new WordManager();
                var entity = mng.RetrievePopularWord();
                apiResponse = new ApiResponse();
                apiResponse.Data = entity;
                return apiResponse;
            });
            return Ok(response);
        }

        [HttpGet]
        public async Task<IHttpActionResult> PopularWordOfWeek(string day)
        {
            var response = await Task.Run(() =>
            {
                var mng = new WordManager();
                var entity = mng.RetrievePopularWordOfDay(day);
                apiResponse = new ApiResponse();
                apiResponse.Data = entity;
                return apiResponse;
            });
            return Ok(response);
        }

        [HttpGet]
        public async Task<IHttpActionResult> DictionaryByLanguage(string destLang)
        {
            var response = await Task.Run(() =>
            {
                var mng = new WordManager();
                var entity = mng.RetrieveDictionaryByLanguage(destLang);
                apiResponse = new ApiResponse();
                apiResponse.Data = entity;
                return apiResponse;
            });
            return Ok(response);
        }
    }
}
