using CoPilodMD.Core;
using CoPilodMD.Core.pipes;
using FellowOakDicom;
using FellowOakDicom.Imaging.Reconstruction;

namespace CoPilotMD.Deidentifier
{
    public class DeidentifierService: BaseService
    {
        protected override void Start()
        {
        }

        public void ProcessFileById(string fileId)
        {
            try
            {
                ProcessFile(StorageHandler.GetFileName(fileId));
                SqlLogger.Info($"File {fileId} is deidentified");
            }
            catch (Exception ex)
            {
                SqlLogger.Error($"ProcessFileById {fileId} ERROR: {ex.Message}");
            }
        }

        public void ProcessFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                SqlLogger.Error($"Cannot find file {fileName}");
                return;
            }

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

        protected override void Receive(object? sender, ServiceMessage msg)
        {
        }
    }
}
