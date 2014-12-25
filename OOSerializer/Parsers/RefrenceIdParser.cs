using JCOM.OOSerializer.Documents;
using JCOM.OOSerializer.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCOM.OOSerializer.Parsers
{
    /// <summary>
    /// Implementds a refrence id parser.
    /// </summary>
    public class RefrenceIdParser<T> : JsonObjectParser<T,RefrenceId>
    {
        public RefrenceIdParser()
            : base("_rid")
        {
        }


        public override Documents.JsonObjectPhrase<T> ParseFromObject(RefrenceId o, SerializationContext<T> context)
        {
            return new Documents.JsonObjectPhrase<T>(Identity, o.Value);
        }

        public override RefrenceId ObjectFromParse(Documents.JsonObjectPhrase<T> parse, SerializationContext<T> context)
        {
            return new RefrenceId((parse.Words[0] as JsonNumber<T>).As<UInt32>());
        }
    }
}
