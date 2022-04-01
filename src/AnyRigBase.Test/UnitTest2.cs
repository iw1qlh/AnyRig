using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace AnyRigBase.Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
			Random r = new Random();
			TRig cr = new TRig();

			MethodInfo? miToBcdBS = typeof(TRig).GetMethod("ToBcdBS",
					BindingFlags.NonPublic | BindingFlags.Instance);

			MethodInfo? miFromBcdBU = typeof(TRig).GetMethod("FromBcdBU",
					BindingFlags.NonPublic | BindingFlags.Instance);


			for (int n = 0; n < 64; n++)
			{
				long value = r.Next();
				Console.Write(value);
				Console.Write(" ");
				byte[] b = new byte[20];
				


				object[] parameters = { b, value };
				miToBcdBS.Invoke(cr, parameters);
				foreach (var bb in b)
					Console.Write($"{bb:X2} ");
				long vv = (long) miFromBcdBU.Invoke(cr, new object[] { b });
				Console.WriteLine(vv);
				if (value != vv)
					throw new Exception();
			}

		}
    }
}