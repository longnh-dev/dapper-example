using DapperExample.Sharedkernel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharedkernel.Helper
{
    public class Helper
    {
        /// <summary>
        /// Transform data to http response
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ActionResult TransformData(Response data)
        {
            var result = new ObjectResult(data) { StatusCode = (int)data.Code };
            return result;
        }
    }
    
}
