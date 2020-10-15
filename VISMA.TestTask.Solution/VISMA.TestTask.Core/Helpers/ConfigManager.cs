using System;
using System.Web.Configuration;

namespace VISMA.TestTask.Core.Helpers
{
    public class ConfigManager : IConfigManager
    {
        private static readonly int EmployeePageSizeValue;

        static ConfigManager()
        {
            EmployeePageSizeValue = Int32.TryParse(WebConfigurationManager.AppSettings["EmployeePageSize"] as string, out var value) ? value : 10;
        }

        public int EmployeePageSize => EmployeePageSizeValue;
    }
}