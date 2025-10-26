using FellowOakDicom;

namespace CoPilotMD.Deidentifier
{
    public class DeidentifierService
    {
        public void ProcessFile(string fileName)
        {
            var dicomFile = DicomFile.Open(fileName);
            if (dicomFile != null)
            {
                var dataset = dicomFile.Dataset;

                dataset.AddOrUpdate(DicomTag.PatientName, "");
                dataset.AddOrUpdate(DicomTag.PatientID, "");
                dataset.AddOrUpdate(DicomTag.PatientBirthDate, "");
                dataset.AddOrUpdate(DicomTag.PatientSex, "");
                dataset.AddOrUpdate(DicomTag.InstitutionName, "");
                dataset.AddOrUpdate(DicomTag.ReferringPhysicianName, "");
                dataset.AddOrUpdate(DicomTag.StudyDate, "");
                dataset.AddOrUpdate(DicomTag.StudyTime, "");
                dataset.AddOrUpdate(DicomTag.AccessionNumber, "");

                dicomFile.Save(fileName);
            }
        }
    }
}
