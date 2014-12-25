using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCOM.Core.Strings;
using JCOM.Serializer.Attributes;
using JCOM.Serializer.Documents;

namespace Tester.Serialization
{
    public class JsonPartialLoadNestedObject
    {
        public JsonPartialLoadNestedObject(int nestDepth)
            : this(nestDepth, 0)
        {
        }

        private JsonPartialLoadNestedObject(int nestDepth, int currentLevel)
        {
            if (nestDepth > 0)
                Internal = new JsonPartialLoadNestedObject(nestDepth - 1, currentLevel + 1);

            NestLevel = currentLevel;
            RandomString = "rs".GenerateCode(5);
        }

        public int NestLevel { get; private set; }
        public string RandomString { get; private set; }

        [JCOMMember]
        private PostDeserialize<JsonPartialLoadNestedObject> m_internal;

        [JCOMIgnore]
        /// <summary>
        /// A post deserialzed internal
        /// </summary>
        public JsonPartialLoadNestedObject Internal
        {
            get
            {
                if (m_internal == null)
                    return null;
                return m_internal.Value;
            }
            set
            {
                m_internal = new PostDeserialize<JsonPartialLoadNestedObject>(value);
            }
        }
    }
}
