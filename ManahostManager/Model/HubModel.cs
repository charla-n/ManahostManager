using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManahostManager.Model
{
    public class HubModel
    {
        public HubModel(int status, object dataObject)
        {
            StatusCode = status;
            Data = dataObject;
        }
        public int StatusCode { get; set; }

        public object Data { get; set; }
    }
}