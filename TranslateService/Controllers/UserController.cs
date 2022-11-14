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
    public class UserController : ApiController
    {
        private ApiResponse apiResponse;
        public async Task<IHttpActionResult> Get()
        {
            var response = await Task.Run(() =>
            {
                apiResponse = new ApiResponse();
                var mng = new UserManager();
                apiResponse.Data = mng.RetrieveAll();
                return apiResponse;
            });
            return Ok(response);
        }

        public async Task<IHttpActionResult> Post(UserType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new UserManager();
                mng.Create(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        public async Task<IHttpActionResult> Get(string name)
        {
            var entity = new UserType
            {
                Name = name
            };

            var response = await Task.Run(() =>
            {
                var mng = new UserManager();
                entity = mng.RetrieveById(entity);
                apiResponse = new ApiResponse();
                apiResponse.Data = entity;
                return apiResponse;
            });
            return Ok(response);
        }

        public async Task<IHttpActionResult> Put(UserType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new UserManager();
                mng.Update(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        public async Task<IHttpActionResult> Delete(UserType entity)
        {
            var response = await Task.Run(() =>
            {
                var mng = new UserManager();
                mng.Delete(entity);

                apiResponse = new ApiResponse();
                apiResponse.Message = "Action was executed.";
                return apiResponse;
            });
            return Content(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public async Task<IHttpActionResult> RetrieveUserWithMoreTranslations()
        {
            var response = await Task.Run(() =>
            {
                var mng = new UserManager();
                var entity = mng.RetrieveUserWithMoreTranslations();
                apiResponse = new ApiResponse();
                apiResponse.Data = entity;
                return apiResponse;
            });
            return Ok(response);
        }
    }
}
