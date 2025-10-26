using CoPilotMD.NetworkListener;
using System.Text;

namespace CoPilotMD.Tests
{
    public class NetworkListenerTests
    {
        private MemoryStream mockStream;

        [SetUp]
        public void Setup()
        {
            mockStream = new MemoryStream();

            string testContent = "TEST";
            byte[] fileContent = Encoding.UTF8.GetBytes(testContent);


            mockStream.Write(fileContent, 0, fileContent.Length);
            mockStream.Position = 0;
        }

        [Test]
        public void TestFileReciever()
        {
            var receiver = new FileLoader(null);
            var fid = Guid.NewGuid().ToString();
            receiver.SaveFileFromStream(mockStream, fid);
            var path = Path.Combine($"Storage",$"{fid}", $"{fid}.dcm");

            var savedContent = File.ReadAllText(path);
            Assert.AreEqual("TEST", savedContent);

            File.Delete(path);
        }
    }
}