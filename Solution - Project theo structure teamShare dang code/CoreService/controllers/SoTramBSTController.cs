using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreService.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary.area.respond;
using ModelClassLibrary.model;

namespace CoreService.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SoTramBTSController : Controller
    {
        private ISoTramBTS _service = null;

        public SoTramBTSController(ISoTramBTS _service)
        {
            this._service = _service;
        }


        //GetAll
        [HttpGet]
        public DataRespond Get()
        {
            DataRespond respone = new DataRespond() { success = true, message = "success: GetAll" };
            try
            {
                respone.data = _service.GetAll();
            }
            catch (Exception e)
            {
                respone = new DataRespond() { success = false, message = e.Message, error = e };
            }
            return respone;
        }

        //GetById
        [HttpGet("{id}")]
        public DataRespond Get(int id)
        {
            DataRespond respone = new DataRespond() { success = true, message = "success: GetById" };
            try
            {
                respone.data = _service.GetById(id);
                if (respone.data == null)
                {
                    respone.success = false;
                    respone.message = "Not found: GetById";

                }
            }
            catch (Exception e)
            {
                respone = new DataRespond() { success = false, message = e.Message, error = e };
            }
            return respone;
        }

        //Insert
        [HttpPost]
        public DataRespond Post([FromBody] SoTramBTS newObj)
        {
            DataRespond respone = new DataRespond() { success = true, message = "success: Insert" };
            try
            {
                var res = _service.Insert(newObj);
                if (res == false)
                {
                    respone.success = false;
                    respone.message = "duplicated id: Insert";
                }
            }
            catch (Exception e)
            {
                respone = new DataRespond() { success = false, message = e.Message, error = e };
            }
            return respone;
        }

        //insertAll
        [HttpPost("insertall")]
        public DataRespond Post([FromBody] SoTramBTS[] newObjs)
        {
            DataRespond respone = new DataRespond() { success = true, message = "success: Insert" };
            try
            {
                ////Bussiness: khóa bảng không cho cập nhật

                //var res = _service.InsertCheckKhoaBang(newObjs);
                //respone.success = res.Item1;
                //respone.data = res.Item2;

                var check = 1;
                var res = _service.Insert(newObjs, check);
                if (check == 1 && res.Item1 == false)
                {
                    respone.data = res.Item2;
                    respone.success = false;
                    respone.message = "duplicated id: insertAll";
                }
                respone.data = res.Item2;
            }
            catch (Exception e)
            {
                respone = new DataRespond() { success = false, message = e.Message, error = e };
            }
            return respone;
        }

        //Update
        [HttpPut("{id}")]
        public DataRespond Put(int id, [FromBody] SoTramBTS upObj)
        {
            DataRespond respone = new DataRespond() { success = true, message = "success: Update_Put" };
            try
            {
                upObj._id = (int)id;
                var res = _service.Update(upObj);
                if (res == false)
                {
                    respone.success = false;
                    respone.message = "Not found: Update_Put";
                }
            }
            catch (Exception e)
            {
                respone = new DataRespond() { success = false, message = e.Message, error = e };
            }
            return respone;
        }


        ////Update
        //[HttpPatch("{id}")]
        //public DataRespond Patch(int id, [FromBody] Roles upObj)
        //{
        //    DataRespond respone = new DataRespond() { success = true, message = "success: Update_Patch" };
        //    try
        //    {
        //        _service.Update(upObj);
        //    }
        //    catch (Exception e)
        //    {
        //        respone = new DataRespond() { success = false, message = e.Message, error = e };
        //    }
        //    return respone;
        //}

        //Delete
        [HttpDelete("{id}")]
        public DataRespond Delete(int id)
        {
            DataRespond respone = new DataRespond() { success = true, message = "success: Delete" };
            try
            {
                var res = _service.Delete(id);
                if (res == false)
                {
                    respone.success = false;
                    respone.message = "Not Found: Delete";
                }
            }
            catch (Exception e)
            {
                respone = new DataRespond() { success = false, message = e.Message, error = e };
            }
            return respone;
        }


        //deleteAll
        [HttpPost("deleteall")]
        public DataRespond deteleAll([FromBody] List<SoTramBTS> objs)
        {
            DataRespond respone = new DataRespond() { success = true, message = "success: deleteall(ids)" };
            try
            {
                var res = _service.DeleteAll(objs);
                respone.success = res;
            }
            catch (Exception e)
            {
                respone = new DataRespond() { success = false, message = e.Message, error = e };
            }
            return respone;
        }


        //GetByThangNam
        [HttpGet("{thang}/{nam}")]
        public DataRespond GetByThangNam(int thang, int nam)
        {
            DataRespond respone = new DataRespond() { success = true, message = "success: GetByThangNam" };
            try
            {
                respone.data = _service.GetByThangNam(thang, nam);
            }
            catch (Exception e)
            {
                respone = new DataRespond() { success = false, message = e.Message, error = e };
            }
            return respone;
        }

    }
}