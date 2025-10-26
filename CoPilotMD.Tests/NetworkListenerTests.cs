using CoPilotMD.NetworkListener;
using System.Net.Sockets;
using System.Text;

namespace CoPilotMD.Tests
{
    public class NetworkListenerTests
    {
        private MemoryStream mockStream;

        private void SendTestData(int port)
        {
            using TcpClient client = new TcpClient("127.0.0.1", port);
            using NetworkStream stream = client.GetStream();

            using FileStream fs = new FileStream("testdata.txt", FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
            {
                stream.Write(buffer, 0, bytesRead);
            }

        }

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

        [Test]
        public void TestTcpReciever()
        {
            TcpReceiver reciever = new TcpReceiver();

            reciever.Start();
            SendTestData(reciever.Port);
            var fid = reciever.LastFileId;
            Thread.Sleep(1000);
            var path = Path.Combine($"Storage", $"{fid}", $"{fid}.dcm");

            var savedContent = File.ReadAllText(path);
            Assert.AreEqual("TESTDATA", savedContent);

            File.Delete(path);
        }
    }
}