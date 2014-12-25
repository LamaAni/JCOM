using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCOM.OOSerializer.Reference;

namespace JCOM.OOSerializer.Javascript
{
    public class JsonStringRefrenceBank : JsonRefrenceBank<string>
    {
        public JsonStringRefrenceBank(IJsonRefrenceDataProvider<string> dataProvider = null)
            : base(dataProvider == null ? new Javascript.JsonStringArrayDataProvider() : dataProvider)
        {
        }
    }
}
