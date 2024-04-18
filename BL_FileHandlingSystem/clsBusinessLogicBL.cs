using System;
using FIleHandlingSystem.VO;
using FileHandlingSystem.DL;
using FileHandlingSystem.Resources;
using System.Collections.Generic;

namespace FileHandlingSystem.BL
{
    /// <summary>
    /// Business Logic Class to use the Method of Business Module
    /// </summary>
    public class clsBusinessLogicBL : IclsBusinessLogicBL
    {
        public static clsFileHandlingSystem DL;
        public static  ValueLayerObject vlo;

        /// <summary>
        /// Open the Configuration file and set the propertie s in vlo
        /// </summary>
        /// <param name="vlo"></param>
        /// <returns></returns>
        public bool SetConfigurations()
        {
            clsConfiguration config = new clsConfiguration();
            if (config.GetConfig(vlo))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Open or create the data file
        /// </summary>
        /// <returns></returns>
        public  bool Initialize()
        {
            try
            {

               // ValueLayerObject vlo = new ValueLayerObject();
                vlo.records = new List<string>();
                DL = new clsFileHandlingSystem();
                if (DL.CreateOrOpenFile(vlo))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// release the object of Data library class
        /// </summary>
        /// <returns></returns>
        public bool ReleaseObjects()
        {
            try
            {
                DL.CloseFile(vlo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate the user for authenticity
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="vlo"></param>
        /// <returns></returns>
        public bool CheckValidUser(AuthenticatorValueObject vo)
        {
            ClsAuthenticator dlobj = new ClsAuthenticator();
            if (dlobj.Authenticate(vo, vlo))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Save the Record into the file
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="vlo"></param>
        /// <returns></returns>
        public bool Save(clsEmployeeVO vo)
        {
           // this.vlo = vlo;

            if (Validate(vo))
            {
                if (vo.editMode == EditMode.Create)
                {
                    if (!DL.FinalSave(vo, vlo))
                    {
                        vo.caption = caption.invalidformdata;
                        return false;
                    }

                    vlo.records.Add(vo.dataToStore);
                    vlo.serialNumber += 1;
                }
                else if (vo.editMode == EditMode.Update)
                {
                    string data = PadDataAsPerRecord();
                    vlo.records.Remove(data);
                    vlo.records.Add(vo.dataToStore);
                    vlo.AssigndataToEMployeeData(vo.EmployeeData, vo.dataToStore);
                    DL.FinalSave(vo, vlo);
                }
                else if (vo.editMode == EditMode.Delete)
                {
                    string data = PadDataAsPerRecord();
                    vlo.records.Remove(data);
                    vlo.AssigndataToEMployeeData(vo.EmployeeData, data);
                    if (!DL.FinalSave(vo, vlo))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Validate the Data inserted by the user
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        private bool Validate(clsEmployeeVO vo)
        {
            if (vo.editMode == EditMode.Create || vo.editMode == EditMode.Update)
            {
                if (vo.EmployeeData.FirstName == "")
                {
                    vo.error = ErrorMessage.firstnameEmpty;
                    return false;
                }
                else if (vo.EmployeeData.Qualification == 0)
                {
                    vo.error = ErrorMessage.qualificationEmpty;
                    return false;
                }
                else if (vo.EmployeeData.CurrentCompany == "")
                {
                    vo.error = ErrorMessage.currentCompanyEmpty;
                    return false;
                }
                else if (vo.EmployeeData.JoiningDate == DateTime.Parse("01/01/1800"))
                {
                    vo.error = ErrorMessage.emptyJoiningDate;
                    return false;
                }
                else if (vo.EmployeeData.DateOfBirth >= vo.EmployeeData.JoiningDate)
                {
                    vo.error = ErrorMessage.errordateOfBirthGreaterThanjoiningdate;
                    return false;
                }
            }
            if (vo.editMode == EditMode.Update)
            {
                if (vo.EmployeeData.Qualification < (int)(Qualification)Enum.Parse(typeof(Qualification), vlo.values[6].ToString()))
                {
                    if (vlo.userType >= UserTypes.Self)
                    {
                        vo.error = ErrorMessage.qualificationdowngrade;
                        return false;
                    }

                }
            }
            return true;
        }

        /// <summary>
        /// Function to take the data from value array and pad it accrodint to record structure
        /// </summary>
        /// <returns></returns>
        private string PadDataAsPerRecord()
        {
            string data = "";
            data += PadString(vlo.values[0].ToString(), 18);
            data += PadString(vlo.values[1].ToString(), 7);
            data += PadString(vlo.values[2].ToString(), 50);
            data += PadString(vlo.values[3].ToString(), 25);
            data += PadString(vlo.values[4].ToString(), 50);
            data += PadString(vlo.values[5].ToString(), 10);
            if (Enum.IsDefined(typeof(Qualification), vlo.values[6].ToString()))
            {
                Qualification qualification = (Qualification)Enum.Parse(typeof(Qualification), vlo.values[6].ToString());
                int enumIndex = (int)qualification;
                data += PadString(enumIndex.ToString(), 2);
            }
            data += PadString(vlo.values[7].ToString(), 255);
            data += PadString(vlo.values[8].ToString(), 10);
            data += PadString(vlo.values[9].ToString(), 500);
            return data;
        }

        private string PadString(string input, int length)
        {
            if (input.Length >= length)
            {
                return input.Substring(0, length);
            }
            else
            {
                return input.PadRight(length);
            }
        }

    }
}
