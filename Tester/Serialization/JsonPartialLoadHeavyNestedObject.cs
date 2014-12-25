using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCOM.Core.Strings;
using JCOM.OOSerializer.Attributes;
using JCOM.OOSerializer.Documents;

namespace Tester.Serialization
{
    public class JsonPartialLoadHeavyNestedObject
    {
        public JsonPartialLoadHeavyNestedObject(int nestDepth, int listSize, ref int index)
            : this(nestDepth, listSize, ref index, 0)
        {
        }

        private JsonPartialLoadHeavyNestedObject(int nestDepth, int listSize, ref int index, int currentLevel)
        {
            Index = index;
            index += 1;
            if (nestDepth > 0)
            {
                Internal = new List<JsonPartialLoadHeavyNestedObject>();
                for (int i = 0; i < listSize; i++)
                    Internal.Add(new JsonPartialLoadHeavyNestedObject(nestDepth - 1, listSize, ref index, currentLevel + 1));
            }

            NestLevel = currentLevel;
            RandomString = "rs".GenerateCode(5);
        }

        public int NestLevel { get; private set; }
        public string RandomString { get; private set; }
        public int Index { get; private set; }

        [JCOMMember]
        private PostDeserialize<List<JsonPartialLoadHeavyNestedObject>> m_internal;

        [JCOMIgnore]
        /// <summary>
        /// A post deserialzed internal
        /// </summary>
        public List<JsonPartialLoadHeavyNestedObject> Internal
        {
            get
            {
                if (m_internal == null)
                    return null;
                return m_internal.Value;
            }
            set
            {
                m_internal =
                    new PostDeserialize<List<JsonPartialLoadHeavyNestedObject>>(value);
            }
        }
    }
}
