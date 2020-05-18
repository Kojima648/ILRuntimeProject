using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project
{
    class TestCLRBinding
    {
        public static void RunTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                CLRBindingTestClass.DoSomeTest(5,12.19f);
            }
        }
    }
}
