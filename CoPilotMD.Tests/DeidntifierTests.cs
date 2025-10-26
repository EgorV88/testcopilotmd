

using CoPilotMD.Deidentifier;
using FellowOakDicom;

namespace CoPilotMD.Tests
{
    public class DeidntifierTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestDeidentification()
        {
            DeidentifierService diedntif = new DeidentifierService();
            var tmp = "temp.dcm";
            File.Copy("test.dcm", tmp, true);
            diedntif.ProcessFile(tmp);

            var dicomFile = DicomFile.Open(tmp);
            if (dicomFile != null)
            {
                var dataset = dicomFile.Dataset;

                Assert.AreEqual(dataset.GetString(DicomTag.PatientName),"");
                Assert.AreEqual(dataset.GetString(DicomTag.PatientID), "");
                Assert.AreEqual(dataset.GetString(DicomTag.PatientBirthDate), "");
                Assert.AreEqual(dataset.GetString(DicomTag.PatientSex), "");
                Assert.AreEqual(dataset.GetString(DicomTag.InstitutionName), "");
                Assert.AreEqual(dataset.GetString(DicomTag.ReferringPhysicianName), "");
                Assert.AreEqual(dataset.GetString(DicomTag.StudyDate), "");
                Assert.AreEqual(dataset.GetString(DicomTag.StudyTime), "");
                Assert.AreEqual(dataset.GetString(DicomTag.AccessionNumber), "");
            }

            File.Delete(tmp);
        }
    }
}
