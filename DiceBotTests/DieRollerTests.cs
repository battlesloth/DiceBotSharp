using DiceBotSharp.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiceBotTests
{
    [TestClass]
    public class DieRollerTests
    {
        [TestMethod]
        public void Advantage()
        {
            var result = string.Empty;

            var roller = new DieRoller();

            var passed = roller.TryParseDieRoll("d20+", "joe", out result);

            Assert.IsTrue(passed);
            Assert.IsTrue(result.Contains("joe rolled D20 with advantage:"));
        }

        [TestMethod]
        public void Disadvantage()
        {
            var result = string.Empty;

            var roller = new DieRoller();

            var passed = roller.TryParseDieRoll("d20-", "joe", out result);

            Assert.IsTrue(passed);
            Assert.IsTrue(result.Contains("joe rolled D20 with disadvantage:"));
        }

        [TestMethod]
        public void BadInput()
        {
            var result = string.Empty;

            var roller = new DieRoller();

            var passed = roller.TryParseDieRoll("nomatch", "joe", out result);

            Assert.IsFalse(passed);
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Simple()
        {
            var result = string.Empty;

            var roller = new DieRoller();

            var passed = roller.TryParseDieRoll("d4", "joe", out result);

            Assert.IsTrue(passed);
            Assert.IsTrue(result.Contains("joe rolled 1D4:"));
        }

        [TestMethod]
        public void MultiDie()
        {
            var result = string.Empty;

            var roller = new DieRoller();

            var passed = roller.TryParseDieRoll("3D8", "joe", out result);

            Assert.IsTrue(passed);
            Assert.IsTrue(result.Contains("joe rolled 3D8:"));
        }

        [TestMethod]
        public void PositiveMod()
        {
            var result = string.Empty;

            var roller = new DieRoller();

            var passed = roller.TryParseDieRoll("4d6+2", "joe", out result);

            Assert.IsTrue(passed);
            Assert.IsTrue(result.Contains("joe rolled 4D6+2:"));
        }

        [TestMethod]
        public void NegativeMod()
        {
            var result = string.Empty;

            var roller = new DieRoller();

            var passed = roller.TryParseDieRoll("3d8-3", "joe", out result);

            Assert.IsTrue(passed);
            Assert.IsTrue(result.Contains("joe rolled 3D8-3:"));
        }
    }
}
