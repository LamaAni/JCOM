using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCOM.Core.Strings;

namespace Tester.Serialization
{
    /// <summary>
    /// A json object that is nested inside another object.
    /// </summary>
    public class JsonSimpleNestedObject
    {
        public JsonSimpleNestedObject(int nestDepth)
            : this(nestDepth, 0)
        {
        }

        private JsonSimpleNestedObject(int nestDepth, int currentLevel)
        {
            if (nestDepth > 0)
                Internal = new JsonSimpleNestedObject(nestDepth - 1, currentLevel + 1);

            NestLevel = currentLevel;
            RandomString = "rs".GenerateCode(5);
        }

        public int NestLevel { get; private set; }
        public string RandomString { get; private set; }

        public JsonSimpleNestedObject Internal { get; set; }
    }
}
