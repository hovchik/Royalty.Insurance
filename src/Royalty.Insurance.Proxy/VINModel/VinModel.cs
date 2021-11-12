using System.Collections.Generic;
using System.Linq;

namespace Royalty.Insurance.Proxy.VINModel
{
    public class VinModel
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public List<Result> Results { get; set; }

        public string this[string variableName]
        {
            get
            {
                return Results.FirstOrDefault(var => var.Variable.Equals(variableName))?.Value;
            }
        }
    }

    public class Result
    {
        public string Value { get; set; }
        public string ValueId { get; set; }
        public string Variable { get; set; }
        public int VariableId { get; set; }
    }
}
