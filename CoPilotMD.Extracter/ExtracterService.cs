using CoPilodMD.Core;
using CoPilodMD.Core.pipes;
using FellowOakDicom;
using FellowOakDicom.Imaging;
using SixLabors.ImageSharp;

namespace CoPilotMD.Extracter
{
    public class ExtracterService : BaseService
    {
        public ExtracterService()
        {
            settings = ServiceSettings.LoadSettings<ServiceSettings>("exract.json");
            new DicomSetupBuilder()
                .RegisterServices(s => s.AddImageManager<ImageSharpImageManager>())
                .Build();
        }
        protected override void Receive(object? sender, ServiceMessage msg)
        {
            if (msg == null) return;
            switch (msg.Topic.ToLower())
            {
                case ServiceMessage.TopicFile:
                    {
                        Task.Run(() => { ProcessFileById(msg.Message); });
                        break;
                    }
            }
        }

        protected override void Start()
        {
        }

        public void ProcessFileById(string fileId)
        {
            try
            {
                var fileName = StorageHandler.GetFileName(fileId);
                var dir = StorageHandler.GetDir(fileId);
                if (!File.Exists(fileName))
                {
                    SqlLogger.Error($"Cannot find file {fileName}");
                    return;
                }

                var dicomFile = DicomFile.Open(fileName);
                if (dicomFile != null)
                {
                    var dicomImage = new DicomImage(dicomFile.Dataset);

                    int frameCount = dicomImage.NumberOfFrames;

                    for (int i = 0; i < frameCount; i++)
                    {
                        IImage image = dicomImage.RenderImage(i);
                        var img = image.AsSharpImage();
                        img.Save(Path.Combine(dir, $"frame_{i + 1}.png"));
                    }
                }


                SqlLogger.Info($"File {fileId} is extracted");
                SendFinishNotif(new ServiceMessage()
                {
                    Topic = ServiceMessage.TopicFile,
                    Message = fileId
                });
            }
            catch (Exception ex)
            {
                SqlLogger.Error($"ProcessFileById {fileId} ERROR: {ex.Message}");
            }
        }

    }
}
