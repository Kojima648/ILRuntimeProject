using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project
{
    class TestInheritance : TestClassBase
    {
        public static TestInheritance NewObject()
        {
            return new TestInheritance();
        }

        public override int Value { get; set; } = 123;

        public override void TestVirtual(string str)
        {
            UnityEngine.Debug.Log("HotFix_Project_TestInheritance.TestVirtual :" + str);
        }

        public override void TestAbstract(int gg)
        {
            UnityEngine.Debug.Log("HotFix_Project_TestInheritance.TestAbstract :" + gg);
        }

    }
}
