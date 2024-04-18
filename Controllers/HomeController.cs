using FileHandlingSystem.Resources;
using FIleHandlingSystem.VO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using FileHandlingSystem.Resources;
using FileHandlingSystem.BL;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.Application["AuthorizedViewAccessed"] != null &&
        (bool)HttpContext.Application["AuthorizedViewAccessed"] == true)
            {
                // Clear the session variable

                ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;
                ViewBag.records = vlo.records;
                ExtractDataIntoUserDataArray(vlo, HttpContext.Application["FHP.resources"] as clsResoucresModuleResources);
                return View();
            }
            else

            {
                // Redirect the user to another view
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpDelete]
        public ActionResult DeleteRecords()
        {
            ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;
            if (vlo.values != null)
            {
                int index = (int)vlo.values[0];

                DateTime.TryParse(vlo.userData[index, 5], out DateTime dateOfBirth);
                DateTime.TryParse(vlo.userData[index, 8], out DateTime joiningDate);



                // Create a new object with the extracted data
                var formData = new InputData
                {
                    SerialNumber = vlo.userData[index, 0],
                    Prefix = vlo.userData[index, 1],
                    FirstName = vlo.userData[index, 2],
                    MiddleName = vlo.userData[index, 3],
                    LastName = vlo.userData[index, 4],
                    DateOfBirth = (dateOfBirth != DateTime.Parse("01/01/1800")) ? dateOfBirth : DateTime.Parse("01/01/1800"),
                    Qualification = vlo.userData[index, 6],
                    CurrentCompany = vlo.userData[index, 7],
                    JoiningDate = (joiningDate != DateTime.Parse("01/01/1800")) ? joiningDate : DateTime.Parse("01/01/1800"),
                    CurrentAddress = vlo.userData[index, 9]
                };



                object[] formDataArray = new object[]
                {
                    formData.SerialNumber,
                    formData.Prefix,
                    formData.FirstName,
                    formData.MiddleName,
                    formData.LastName,
                    formData.DateOfBirth.ToString("MM/dd/yyyy"), // Assuming DateOfBirth is nullable DateTime
                    formData.Qualification,
                    formData.CurrentCompany,
                    formData.JoiningDate.ToString("MM/dd/yyyy"), // Assuming JoiningDate is nullable DateTime
                    formData.CurrentAddress
                };

                vlo.values = new object[10];
                InsertDataToValues(formDataArray);


                clsEmployeeVO vo = new clsEmployeeVO();
                clsBusinessLogicBL bl = HttpContext.Application["FHP.bl"] as clsBusinessLogicBL;

                vo.editMode = EditMode.Delete;
                if (bl.Save(vo))
                {
                    return Json(new { success = true });
                }

            }

            return Json(new { success = false });
        }


        private void InsertDataToValues(object[] array)
        {

            ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;
            for (int i = 0; i < 10; i++)
            {
                object data = array[i];
                vlo.values[i] = data?.ToString();
            }
        }
        public ActionResult About()
        {
            if (HttpContext.Application["AuthorizedViewAccessed"] != null &&
                (bool)HttpContext.Application["AuthorizedViewAccessed"] == true)
            {
                return View();
            }
            else
            {
                // Redirect the user to another view
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult InputDataForm()
        {
            if (HttpContext.Application["AuthorizedViewAccessed"] != null &&
                (bool)HttpContext.Application["AuthorizedViewAccessed"] == true)
            {
                ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;
                ViewBag.SerialNumber = vlo.serialNumber;
                return View();
            }
            else
            {
                // Redirect the user to another view
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpPost]
        public ActionResult InputDataForm(InputData formdata)
        {
            ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;

            string data = GetPaddedFormData(formdata, vlo.serialNumber.ToString());

            clsEmployeeVO vo = new clsEmployeeVO();
            vo.dataToStore = data;
            clsBusinessLogicBL bl = HttpContext.Application["FHP.bl"] as clsBusinessLogicBL;


            if (vlo.AssigndataToEMployeeData(vo.EmployeeData, data))
            {
                vo.editMode = EditMode.Create;
                if (bl.Save(vo))
                {
                    return Json(new { success = true });
                }
            }

            // Process the form data
            // For demonstration purposes, you can simply return a view
            // Here, assuming you have a Success view to redirect to
            return Json(new { success = false });
        }


        public ActionResult Update(InputData  updatedUserFormData)
        {
            ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;
            if (vlo.values != null)
            {
                int index = (int)vlo.values[0];

                DateTime.TryParse(vlo.userData[index, 5], out DateTime dateOfBirth);
                DateTime.TryParse(vlo.userData[index, 8], out DateTime joiningDate);

                var formData = new InputData
                {
                    SerialNumber = vlo.userData[index, 0],
                    Prefix = vlo.userData[index, 1],
                    FirstName = vlo.userData[index, 2],
                    MiddleName = vlo.userData[index, 3],
                    LastName = vlo.userData[index, 4],
                    DateOfBirth = (dateOfBirth != DateTime.Parse("01/01/1800")) ? dateOfBirth : DateTime.Parse("01/01/1800"),
                    Qualification = vlo.userData[index, 6],
                    CurrentCompany = vlo.userData[index, 7],
                    JoiningDate = (joiningDate != DateTime.Parse("01/01/1800")) ? joiningDate : DateTime.Parse("01/01/1800"),
                    CurrentAddress = vlo.userData[index, 9]
                };

                object[] formDataArray = new object[]
                {
                    formData.SerialNumber,
                    formData.Prefix,
                    formData.FirstName,
                    formData.MiddleName,
                    formData.LastName,
                    formData.DateOfBirth.ToString("MM/dd/yyyy"), // Assuming DateOfBirth is nullable DateTime
                    formData.Qualification,
                    formData.CurrentCompany,
                    formData.JoiningDate.ToString("MM/dd/yyyy"), // Assuming JoiningDate is nullable DateTime
                    formData.CurrentAddress
                };

                vlo.values = new object[10];
                InsertDataToValues(formDataArray);


                clsEmployeeVO vo = new clsEmployeeVO();
                clsBusinessLogicBL   bl = HttpContext.Application["FHP.bl"] as clsBusinessLogicBL;

                string data = GetPaddedFormData(updatedUserFormData, updatedUserFormData.SerialNumber);
                vo.dataToStore = data;
                vlo.AssigndataToEMployeeData(vo.EmployeeData, data);

                vo.editMode = EditMode.Update;
                if (bl.Save(vo))
                {
                    return Json(new { success = true });
                }

            }

            return Json(new { success = false });
        }


        private string PadString(string input, int length)
        {
            if (input != null)
            {
                if (input.Length >= length)
                    return input.Substring(0, length);
                else
                {
                    return input.PadRight(length); // Return the padded string
                }
            }
            return "".PadRight(length);
        }

        private string GetPaddedFormData(InputData formData, string serialNumber)
        {
            string data = "";
            data += PadString(serialNumber, 18);
            data += PadString(formData.Prefix, 7);
            data += PadString(formData.FirstName, 50);
            data += PadString(formData.MiddleName, 25);
            data += PadString(formData.LastName, 50);
            data += PadString(formData.DateOfBirth.ToString("MM/dd/yyyy"), 10);
            data += PadString(formData.Qualification, 2); // Assuming qualification is a string
            data += PadString(formData.CurrentCompany, 255);
            data += PadString(formData.JoiningDate.ToString("MM/dd/yyyy"), 10);
            data += PadString(formData.CurrentAddress, 500);

            return data;
        }


        public static void ExtractDataIntoUserDataArray(ValueLayerObject vlo, clsResoucresModuleResources rsc)
        {
            vlo.userData = new string[vlo.records.Count, 10];

            for (int i = 0; i < vlo.records.Count; i++)
            {
                string[] columnData = new string[10];
                columnData[0] = vlo.records[i].Substring(0, 18).Trim();
                columnData[1] = vlo.records[i].Substring(18, 7).Trim();
                columnData[2] = vlo.records[i].Substring(25, 50).Trim();
                columnData[3] = vlo.records[i].Substring(75, 25).Trim();
                columnData[4] = vlo.records[i].Substring(100, 50).Trim();

                string dob = vlo.records[i].Substring(150, 10).Trim();
                columnData[5] = (dob == "01/01/1800") ? "" : dob;

                columnData[6] = vlo.records[i].Substring(160, 2).Trim();

                columnData[7] = vlo.records[i].Substring(162, 255).Trim();

                string jd = vlo.records[i].Substring(417, 10).Trim();
                columnData[8] = (jd == "01/01/1800") ? "" : jd;

                columnData[9] = vlo.records[i].Substring(427, 500).Trim();

                // Add data to vlo.userData array
                for (int column = 0; column < 10; column++)
                {
                    if (column == 5 && columnData[5] == "")
                    {
                        vlo.userData[i, column] = "01/01/1800";
                    }
                    else if (column == 8 && columnData[8] == "")
                    {
                        vlo.userData[i, column] = "01/01/1800";
                    }
                    else
                    {
                        vlo.userData[i, column] = columnData[column];
                    }
                }
                vlo.serialNumber = Convert.ToInt32(vlo.userData[i, 0]) + 1;
            }
        }

        [HttpPost]
        public ActionResult StoreRowIndex(int rowIndex)
        {
            ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;
            vlo.values = new object[1];
            vlo.values[0] = FindIndexInUserData(rowIndex, vlo.userData);
            return Json(new { success = true });
        }

        public int FindIndexInUserData(int value, object[,] userData)
        {
            int rows = userData.GetLength(0); // Get the number of rows in the 2D array

            // Iterate through each row of the 2D array
            for (int i = 0; i < rows; i++)
            {
                // Check if the value matches the first element of the current row
                if ((string)userData[i, 0] == value.ToString())
                {
                    return i; // Found the value at index i
                }
            }

            // If value is not found, return -1
            return -1;
        }

        [HttpGet]
        public ActionResult GetDataFromServer()
        {
            // Your server-side logic to fetch the data
            ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;

            string json;
            if (vlo.values != null)
            {
                int index = (int)vlo.values[0];
                bool first = false;
                bool last = false;
                if (index == 0)
                {
                    first = true;
                }
                if (index == vlo.userData.GetLength(0) - 1)
                {
                    last = true;
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
                    firstindex = first,
                    lastIndex = last
                };
                json = JsonConvert.SerializeObject(data);
            }
            else
            {
                var data = new
                {
                    SerialNumber = "",
                    Prefix = "",
                    FirstName = "",
                    MiddleName = "",
                    LastName = "",
                    DateOfBirth = "",
                    Qualification = "",
                    CurrentCompany = "",
                    JoiningDate = "",
                    CurrentAddress = "",
                    firstindex = false,
                    lastIndex = false
                };
                json = JsonConvert.SerializeObject(data);
            }


            // Return JSON response
            return Content(json, "application/json");
        }
    }
}