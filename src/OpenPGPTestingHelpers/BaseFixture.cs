using System;
using System.IO;
using System.Reflection;

namespace OpenPGPTestingHelpers
{
    public class BaseFixture
    {
        protected static Stream GetTestDataAsStream(string name)
        {
            var asm = Assembly.GetCallingAssembly();
            var resourceName = string.Format("{0}.TestData.{1}", asm.GetName().Name, name);
            var stream = ResourceHelper.GetResourceAsStream(asm, resourceName);
            if (stream == null)
            {
                throw new InvalidOperationException(string.Format("Could not find {0}", resourceName));
            }
            return stream;
        }
    }
}
