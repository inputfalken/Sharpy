using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types.Name;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types.Name {
    [TestClass]
    public class NameFactoryTests {
        #region Exception Handling

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LastNameCollectionExceptionHandling()
            => NameFactory.LastNameCollection("foobar");

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FirstNameCollectionExceptionHandling()
            => NameFactory.FirstNameCollection("foobar");

        #endregion

        #region First Name 

        #endregion

        #region Last Name 

        #endregion
    }
}