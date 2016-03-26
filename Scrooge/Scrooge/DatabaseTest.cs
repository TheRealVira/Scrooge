using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrooge;
using Scrooge.Model;
using Scrooge.Service;
using Scrooge.Service.Implementations.Storage;

namespace DatabaseTest
{
    [TestClass]
    public class EfSQLiteStorageServiceTest
    {
        [TestMethod]
        public void AddKilometerEntry()
        {
            Singleton<ServiceController>.Instance.InitializeServices();
            var instance = Singleton<EfSQLiteStorageService>.Instance;
            instance.ApplicationInitialized();

            var list = new List<KilometerEntryViewModel>
            {
                new KilometerEntryViewModel() { Date = DateTime.Now, DrivenRoute = "Driven1", NewKilometerCount = 1000, StartedKilometerCount = 800, Purpose = "Purpose1"},
                new KilometerEntryViewModel() { Date = DateTime.Now, DrivenRoute = "Driven2", NewKilometerCount = 200, StartedKilometerCount = 100, Purpose = "Purpose2"},
                new KilometerEntryViewModel() { Date = DateTime.Now, DrivenRoute = "Driven3", NewKilometerCount = 600, StartedKilometerCount = 400, Purpose = "Purpose3"}
            };

            instance.UpdateKilometerEntry(list);

            instance.ApplicationClosing();
            instance.ApplicationInitialized();

            var testList = instance.RetrieveKilometerEntryViewModels();

            Assert.IsTrue(testList.Contains(list[0]));
            Assert.IsTrue(testList.Contains(list[1]));
            Assert.IsTrue(testList.Contains(list[2]));

            instance.ApplicationClosing();
        }

        [TestMethod]
        public void RemoveKilometerEntry()
        {
            Singleton<ServiceController>.Instance.InitializeServices();
            var instance = Singleton<EfSQLiteStorageService>.Instance;
            instance.ApplicationInitialized();

            var list = new List<KilometerEntryViewModel>
            {
                new KilometerEntryViewModel() { Date = DateTime.Now, DrivenRoute = "Driven1", NewKilometerCount = 1000, StartedKilometerCount = 800, Purpose = "Purpose1"},
                new KilometerEntryViewModel() { Date = DateTime.Now, DrivenRoute = "Driven2", NewKilometerCount = 200, StartedKilometerCount = 100, Purpose = "Purpose2"},
                new KilometerEntryViewModel() { Date = DateTime.Now, DrivenRoute = "Driven3", NewKilometerCount = 600, StartedKilometerCount = 400, Purpose = "Purpose3"}
            };

            instance.UpdateKilometerEntry(list);

            instance.ApplicationClosing();
            instance.ApplicationInitialized();

            var list2 = instance.RetrieveKilometerEntryViewModels();
            list2.Remove(list[0]);

            instance.ApplicationClosing();
            instance.ApplicationInitialized();

            var testList = instance.RetrieveKilometerEntryViewModels();

            Assert.IsFalse(testList.Contains(list[0]));
            Assert.IsTrue(testList.Contains(list[1]));
            Assert.IsTrue(testList.Contains(list[2]));

            instance.ApplicationClosing();
        }

        [TestMethod]
        public void ModifyKilometerEntry()
        {
            Singleton<ServiceController>.Instance.InitializeServices();
            var instance = Singleton<EfSQLiteStorageService>.Instance;
            instance.ApplicationInitialized();

            var list = new List<KilometerEntryViewModel>
            {
                new KilometerEntryViewModel() { Date = DateTime.Now, DrivenRoute = "Driven1", NewKilometerCount = 1000, StartedKilometerCount = 800, Purpose = "Purpose1"}
            };

            instance.UpdateKilometerEntry(list);

            instance.ApplicationClosing();
            instance.ApplicationInitialized();

            var testList = instance.RetrieveKilometerEntryViewModels();
            testList.First(x => x.Equals(list[0])).NewKilometerCount = 1200;

            instance.ApplicationClosing();
            instance.ApplicationInitialized();

            var newList = instance.RetrieveKilometerEntryViewModels();
            var val = newList.First(x => x.Equals(list[0])).NewKilometerCount;
            Assert.IsTrue(val == 1200, "Expected 1200, got: " + val);

            instance.ApplicationClosing();
        }
    }
}
