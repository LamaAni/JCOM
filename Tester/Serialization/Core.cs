using JCOM.Serializer.Javascript;
using JCOM.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Serialization
{
    [TestClass]
    public class Core
    {
        [TestMethod]
        /// <summary>
        /// Tests the basic json serialization, by converting objects from json and to json.
        /// Then the objects will be compared.
        /// </summary>
        public void TestBasicJsonSerialization()
        {
            var o = new
            {
                A = "12",
                B = "31",
                C = 3,
            };

            string json = o.ToJson();

            // loading the object back from json.
            var no = json.FromJson(o.GetType());

            // parsing to jsin again.
            string njson = no.ToJson();

            Assert.AreEqual(njson, json, "Invalid json conversion on unknown simple type.");
            Trace.WriteLine("All ok in basic json.");
        }

        [TestMethod]
        public void TestSimpleNestedObject()
        {
            JsonSimpleNestedObject o = new JsonSimpleNestedObject(100);

            string json = o.ToJson();
            
            // creating the new object back from the json.
            JsonSimpleNestedObject no = json.FromJson<JsonSimpleNestedObject>();

            string njson = no.ToJson();

            Assert.AreEqual(njson, json, "Invalid json conversion on unknown simple type.");
            Trace.WriteLine("All ok in nested object json.");
        }

        [TestMethod]
        public void TestNestedObjectInRefrenceBank()
        {
            CodeTimer timer = new CodeTimer();
            JsonSimpleNestedObject o = new JsonSimpleNestedObject(100);
            timer.Mark("Prepare object");

            DoTestBankStorage(o, timer);
            Trace.WriteLine("Test results: ");
            Trace.WriteLine(timer.ToTraceString());
        }

        [TestMethod]
        public void TestPartialLoadNestedObjectInRefrenceBank()
        {
            CodeTimer timer = new CodeTimer();
            JsonPartialLoadNestedObject o = new JsonPartialLoadNestedObject(100);
            timer.Mark("Prepare object");
            
            DoTestBankStorage(o, timer);
            Trace.WriteLine("Test results: ");
            Trace.WriteLine(timer.ToTraceString());
        }

        [TestMethod]
        public void TestPartialLoadHeavyNestedObject()
        {
            CodeTimer timer = new CodeTimer();
            int curindex = 0;
            JsonPartialLoadHeavyNestedObject o = new JsonPartialLoadHeavyNestedObject(5, 10, ref curindex);
            timer.Mark("Prepare objects: " + curindex);

            DoTestBankStorage(o, timer);
            Trace.WriteLine("Test results: ");
            Trace.WriteLine(timer.ToTraceString());
        }

        bool DoTestBankStorage(object o, CodeTimer timer=null)
        {
            timer = timer == null ? new CodeTimer() : timer;

            JsonStringArrayDataProvider dataProvider = new JsonStringArrayDataProvider();
            timer.Mark("Prepare data provider");

            // create the old bank and store the object.
            var bank = o.ToJsonBank(dataProvider, false);
            timer.Mark("Make json bank");

            // calling the bank to update itself.
            bank.Update();
            timer.Mark("Update json bank");

            // create the new fresh bank, so we can crete new fresh objects.
            var nbank = dataProvider.Clone().FromBankDataProvider();
            timer.Mark("Clone bank");

            // creating the new object from the new bank.
            var no = nbank.Load(0);
            timer.Mark("Load object from bank.");

            // converting back to json to compare.
            string json = o.ToJson();
            timer.Mark("Convert original object");

            string njson = no.ToJson();
            timer.Mark("Convert new object");

            Assert.AreEqual(njson, json, "Invalid json conversion on unknown simple type.");
            return true;
        }
    }
}
