using FIleHandlingSystem.VO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class InputDataController : Controller
    {
        public ActionResult GetFirstRecord()
        {
            // Your server-side logic to fetch the data
            ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;

            string json;


            int index = 0;


            DateTime.TryParse(vlo.userData[index, 5], out DateTime dateOfBirth);
            DateTime.TryParse(vlo.userData[index, 8], out DateTime joiningDate);

            // Create a new object with the extracted data
            var data = new
            {
                SerialNumber = vlo.userData[index, 0],
                Prefix = vlo.userData[index, 1],
                FirstName = vlo.userData[index, 2],
                MiddleName = vlo.userData[index, 3],
                LastName = vlo.userData[index, 4],
                DateOfBirth = (dateOfBirth != DateTime.Parse("01/01/1800")) ? dateOfBirth.ToString("yyyy-MM-dd") : "1800-01-01",
                Qualification = vlo.userData[index, 6],
                CurrentCompany = vlo.userData[index, 7],
                JoiningDate = (joiningDate != DateTime.Parse("01/01/1800")) ? joiningDate.ToString("yyyy-MM-dd") : "1800-01-01",
                CurrentAddress = vlo.userData[index, 9]
            };
            json = JsonConvert.SerializeObject(data);



            // Return JSON response
            return Content(json, "application/json");
        }

        public ActionResult GetPreviousRecord()
        {
            // Your server-side logic to fetch the data
            ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;

            string json;


            int index = (int)vlo.values[0];
            index -= 1;
            vlo.values[0] = index;
            bool firstidex = false;
            if (index == 0)
            {
                firstidex = true;
            }


            DateTime.TryParse(vlo.userData[index, 5], out DateTime dateOfBirth);
            DateTime.TryParse(vlo.userData[index, 8], out DateTime joiningDate);

            // Create a new object with the extracted data
            var data = new
            {
                SerialNumber = vlo.userData[index, 0],
                Prefix = vlo.userData[index, 1],
                FirstName = vlo.userData[index, 2],
                MiddleName = vlo.userData[index, 3],
                LastName = vlo.userData[index, 4],
                DateOfBirth = (dateOfBirth != DateTime.Parse("01/01/1800")) ? dateOfBirth.ToString("yyyy-MM-dd") : "1800-01-01",
                Qualification = vlo.userData[index, 6],
                CurrentCompany = vlo.userData[index, 7],
                JoiningDate = (joiningDate != DateTime.Parse("01/01/1800")) ? joiningDate.ToString("yyyy-MM-dd") : "1800-01-01",
                CurrentAddress = vlo.userData[index, 9],
                firstindex = firstidex
            };

            json = JsonConvert.SerializeObject(data);



            // Return JSON response
            return Content(json, "application/json");
        }

        public ActionResult GetNextRecord()
        {
            // Your server-side logic to fetch the data
            ValueLayerObject vlo = HttpContext.Application["fhp.vlo"] as ValueLayerObject;

            string json;

            int index = (int)vlo.values[0];
            index += 1;
            vlo.values[0] = index;
            bool lastindex = false;
            if (index == vlo.userData.GetLength(0) - 1)
            {
                lastindex = true;
            }


            DateTime.TryParse(vlo.userData[index, 5], out DateTime dateOfBirth);
            DateTime.TryParse(vlo.userData[index, 8], out DateTime joiningDate);

            // Create a new object with the extracted data
            var data = new
            {
                SerialNumber = vlo.userData[index, 0],
                Prefix = vlo.userData[index, 1],
                FirstName = vlo.userData[index, 2],
                MiddleName = vlo.userData[index, 3],
                LastName = vlo.userData[index, 4],
                DateOfBirth = (dateOfBirth != DateTime.Parse("01/01/1800")) ? dateOfBirth.ToString("yyyy-MM-dd") : "1800-01-01",
                Qualification = vlo.userData[index, 6],
                CurrentCompany = vlo.userData[index, 7],
                JoiningDate = (joiningDate != DateTime.Parse("01/01/1800")) ? joiningDate.ToString("yyyy-MM-dd") : "1800-01-01",
                CurrentAddress = vlo.userData[index, 9],
                lastIndex = lastindex
            };

            json = JsonConvert.SerializeObject(data);



            // Return JSON response
            return Content(json, "application/json");
        }

        public ActionResult GetlastRecord()
        {
            // Your server-side logic to fetch the data
            ValueLayerObject vlo = HttpContext.Application["fhp.vlo"] as ValueLayerObject;

            string json;


            int index = vlo.userData.GetLength(0) - 1;


            DateTime.TryParse(vlo.userData[index, 5], out DateTime dateOfBirth);
            DateTime.TryParse(vlo.userData[index, 8], out DateTime joiningDate);

            // Create a new object with the extracted data
            var data = new
            {
                SerialNumber = vlo.userData[index, 0],
                Prefix = vlo.userData[index, 1],
                FirstName = vlo.userData[index, 2],
                MiddleName = vlo.userData[index, 3],
                LastName = vlo.userData[index, 4],
                DateOfBirth = (dateOfBirth != DateTime.Parse("01/01/1800")) ? dateOfBirth.ToString("yyyy-MM-dd") : "1800-01-01",
                Qualification = vlo.userData[index, 6],
                CurrentCompany = vlo.userData[index, 7],
                JoiningDate = (joiningDate != DateTime.Parse("01/01/1800")) ? joiningDate.ToString("yyyy-MM-dd") : "1800-01-01",
                CurrentAddress = vlo.userData[index, 9]
            };

            json = JsonConvert.SerializeObject(data);



            // Return JSON response
            return Content(json, "application/json");
        }
    }
}