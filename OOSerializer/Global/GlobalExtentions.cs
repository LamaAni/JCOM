
using JCOM.OOSerializer.Documents;
using JCOM.OOSerializer.Javascript;
using JCOM.OOSerializer.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class JCOM_OOSerializer_GlobalExtentions
{
    /// <summary>
    /// Converts the object into javascript json.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="o"></param>
    /// <returns></returns>
    public static string ToJson<T>(this T o, bool isPreety =
#if DEBUG
 true)
#else 
            false)
#endif
    {
        return JCOM.OOSerializer.Javascript.JsonStringSerializer.Global.Serialize(o, true, isPreety);
    }

    /// <summary>
    /// Converts the json source into an object using the string json serializer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static T FromJson<T>(this string source)
    {
        return JCOM.OOSerializer.Javascript.JsonStringSerializer.Global.Deserialize<T>(source, true);
    }

    /// <summary>
    /// Converts the json value into an object using the string json serializer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static T FromJson<T>(this IJsonValue<string> val)
    {
        return JCOM.OOSerializer.Javascript.JsonStringSerializer.Global.Deserialize<T>(val, true);
    }

    /// <summary>
    /// Converts the json source into an object using the string json serializer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static object FromJson(this string source, Type t)
    {
        return JCOM.OOSerializer.Javascript.JsonStringSerializer.Global.Deserialize(source, t, true);
    }

    /// <summary>
    /// Converts the json value into an object using the string json serializer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static object FromJson(this IJsonValue<string> val, Type t)
    {
        return JCOM.OOSerializer.Javascript.JsonStringSerializer.Global.Deserialize(val, t, true);
    }

    /// <summary>
    /// Creates a json refrence bank object, that can store and load objects with id. The refrence bank stores
    /// object given thier refrences ids (managned internally). The bank can clean its own memory unless
    /// an object is decleared anchored, where the object will never be deleted unless manually done so.
    /// See JCOM.Documents.PostDeserialize for more info about how to handle refrencing objects and to partially load an object.
    /// </summary>
    /// <param name="o">The object which is the root of the bank, whos Id is always zero. It is recommended 
    /// not to delete this object from the bank. This object is anchord and will not be cleaned unless deleted manally.</param>
    /// <param name="dataProvider">The data provider that supports this bank.</param>
    /// <param name="doUpdate">If true, after setting the root object the bank would uodate the source that changes were made.</param>
    /// <returns>A newly created json object bank.</returns>
    public static JsonStringRefrenceBank ToJsonBank(this object o, IJsonRefrenceDataProvider<string> dataProvider = null, bool doUpdate = true)
    {
        JsonStringRefrenceBank bank = new JsonStringRefrenceBank(dataProvider);
        bank.Store(o, true);
        if (doUpdate)
            bank.Update();
        return bank;
    }

    /// <summary>
    /// Creates a json string refrence bank from its data provider.
    /// </summary>
    /// <param name="dataProvider">The data provider that holds information about the bank</param>
    /// <returns>The new data provider</returns>
    public static JsonStringRefrenceBank FromBankDataProvider(this IJsonRefrenceDataProvider<string> dataProvider)
    {
        return new JsonStringRefrenceBank(dataProvider);
    }
}

