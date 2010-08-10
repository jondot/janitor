using MbUnit.Framework;
using Jobs;

namespace Jobs.Test
{
    [TestFixture]
    public class BirthDayJobTest
    {
        [Test]
        public void TestConnection()
        {
            BirthDayJob job = new BirthDayJob();
            job.Execute(null);
            Assert.IsNotNull(job.Result);
        }
    }
}
