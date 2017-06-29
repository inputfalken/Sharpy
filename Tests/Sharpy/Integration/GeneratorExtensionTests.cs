using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorAPI;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Implementation;

namespace Tests.Sharpy.Integration
{
    [TestFixture]
    public class GeneratorExtensionTests
    {
        [Test]
        public void FirstName_No_Arg_Is_Not_Null() {
            var firstName = Generator.Factory.FirstName();
            Assert.IsNotNull(firstName.Generate());
        }
        [Test]
        public void FirstName_Gender_Arg_Is_Not_Null() {
            var firstName = Generator.Factory.FirstName(Gender.Female);
            Assert.IsNotNull(firstName.Generate());
        }
        [Test]
        public void FirstName_INameProvider_Arg_Is_Not_Null() {
            var firstName = Generator.Factory.FirstName(new NameByOrigin());
            Assert.IsNotNull(firstName.Generate());
        }

    }
}
