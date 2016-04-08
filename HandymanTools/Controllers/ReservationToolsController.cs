using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HandymanTools.Common.Enums;
using HandymanTools.Infrastructure.Repositories;
using HandymanTools.Models;

namespace HandymanTools.Controllers
{
    public class ReservationToolsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public AvailableToolsViewModel Get(int toolType, string startDate, string endDate)
        {
            var returnList = new AvailableToolsViewModel();
            var reqToolType = (ToolType) toolType;
            var reqStartDate = DateTime.Parse(startDate);
            var reqEndDate = DateTime.Parse(endDate);
            foreach (var tool in new ToolRepository().CheckToolAvailability(reqToolType, reqStartDate, reqEndDate))
            {
                returnList.Add(new AvailableToolViewModel()
                {
                    AbbreviatedDescription = tool.AbbrDescription,
                    Deposit = tool.DepositAmount,
                    RentalPrice = tool.RentalPrice,
                    ToolID = tool.ToolId
                });
            }

            return returnList;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}