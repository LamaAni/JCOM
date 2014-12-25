﻿using JCOM.OOSerializer.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCOM.Core.Strings;

namespace JCOM.OOSerializer.Core.JavscriptJson
{
    public class JsonStringReader : JsonReader<char, string>
    {
        public JsonStringReader()
            : this(JsonStringGlobals.StringLanguage, JsonStringGlobals.JsonDefintions)
        {
        }

        protected JsonStringReader(LanguageDefinitions<char> language, JsonDefinitions<string> definitions)
            : base(language, definitions)
        {
        }

        internal override char IndexSource(string source, int index)
        {
            return source[index];
        }

        internal override int GetSourceCount(string source)
        {
            return source.Length;
        }
    }
}
