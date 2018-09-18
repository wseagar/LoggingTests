using System;
using System.Collections.Generic;

namespace Logging.Core
{
    public class LogDetail
    {
        public string Message { get; set; }
        
        // WHERE
        
        public string Product { get; set; }
        
        public string Layer { get; set; }
        
        public string Location { get; set; }
        
        public string Hostname { get; set; }
        
        public string OrgId { get; set; }
        
        public string User { get; set; }
        
        public string CorrelationId { get; set; }
        
        public Dictionary<string, object> AdditionalInfo { get; set; }
        
        
    }
}