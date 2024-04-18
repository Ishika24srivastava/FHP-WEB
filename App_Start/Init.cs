using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Web.App_Start
{
    public class Init
    {

        public object mainFormObject;
        private object inputFormObject;
        private object vlObject;
        private object resourcesObject;
        private object blObject;

        public void Intilalize()
        {
            
            Assembly assemblyVO = Assembly.Load("VO_FIleHandlingSystem");
            Assembly assemblyResources = Assembly.Load("Rsc_Resources");
            Assembly assemblyBM = Assembly.Load("BL_FileHandlingSystem");

         

            Type valueLayer = assemblyVO.GetType("FIleHandlingSystem.VO.ValueLayerObject");
            vlObject = Activator.CreateInstance(valueLayer);

            Type resources = assemblyResources.GetType("FileHandlingSystem.Resources.clsResoucresModuleResources");
            resourcesObject = Activator.CreateInstance(resources);

            Type businessLayer = assemblyBM.GetType("FileHandlingSystem.BL.clsBusinessLogicBL");
            blObject = Activator.CreateInstance(businessLayer);


            FieldInfo vloInfo2 = businessLayer.GetRuntimeField("vlo");
            vloInfo2.SetValue(blObject, vlObject);

            HttpContext.Current.Application["FHP.vo"] = vlObject;
            HttpContext.Current.Application["FHP.bl"] = blObject;
            HttpContext.Current.Application["FHP.resources"] = resourcesObject;

            HttpContext.Current.Application["AuthorizedViewAccessed"] = false;


        }

        public bool Configure()
        {
            var setConfigurationsMethod = blObject.GetType().GetMethod("SetConfigurations");

            if (setConfigurationsMethod != null)
            {
                Console.WriteLine("configured");
                return (bool)setConfigurationsMethod.Invoke(blObject, null);
            }
            else
            {
                Console.WriteLine("notconfigured");
                return false;
            }
        }

        public void ReleaseObjects()
        {
            resourcesObject = null;
            vlObject = null;
            blObject = null;
            inputFormObject = null;
            mainFormObject = null;
            GC.Collect();
        }

    }
}