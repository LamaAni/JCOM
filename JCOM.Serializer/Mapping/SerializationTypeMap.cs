﻿using JCOM.Serializer.Core;
using JCOM.Serializer.Documents;
using JCOM.Serializer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCOM.Serializer.Mapping
{
    /// <summary>
    /// Holds information about a speicific serialization type Mapping.
    /// The type map allows for a specific type in serialization to be done
    /// </summary>
    public class SerializationTypeMap<T>
    {
        public SerializationTypeMap(TypeMapInfo info, SerializationContext<T> context)
        {
            Info = info;
            IsCoreValue = context.CoreTypes.Contains(info.MappedType);
            IsJsonValue = typeof(IJsonValue<T>).IsAssignableFrom(MappedType);
            Parser = context.Definitions.GetParser(info);
            Converter = IsCoreValue || IsParseValue ? null : context.Definitions.GetConvertor(info);
            IsRefrenceType = !info.IsPostDeserialize && info.IsRefrenceType && !IsParseValue && !context.CoreTypes.Contains(info.MappedType);
        }

        #region members

        /// <summary>
        /// If true the current type is to be regarded as a refrence type (pointer) for refrence serialization.
        /// </summary>
        public bool IsRefrenceType { get; private set; }

        /// <summary>
        /// The type mapping info.
        /// </summary>
        public TypeMapInfo Info { get; private set; }

        /// <summary>
        /// The mapped type information.
        /// </summary>
        public Type MappedType { get { return Info.MappedType; } }

        /// <summary>
        /// If true this is a core json value that will be serialized by the json serializer.
        /// </summary>
        public bool IsCoreValue { get; private set; }

        /// <summary>
        /// Returns true if the current is a parse value.
        /// </summary>
        public bool IsParseValue { get { return Parser != null; } }

        /// <summary>
        /// If the current value is simply a json value.
        /// </summary>
        public bool IsJsonValue { get; private set; }

        /// <summary>
        /// The json converter associated with the type.
        /// </summary>
        public IJsonObjectConverter<T> Converter { get; private set; }

        /// <summary>
        /// True if the current is a parse value.
        /// </summary>
        public IJsonObjectParser<T> Parser { get; private set; }

        #endregion
    }
}
