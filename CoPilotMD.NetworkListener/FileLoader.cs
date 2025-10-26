using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CoPilotMD.NetworkListener
{
    public class FileLoader
    {
        private TcpClient client;
        internal string lastFileId;

        public FileLoader(TcpClient client)
        {
            this.client = client;
        }

        public string StartLoading()
        {
            lastFileId = Guid.NewGuid().ToString();
            ThreadPool.QueueUserWorkItem(Loading);
            return lastFileId;
        }

        private void Loading(object? state)
        {
            try
            {
                using NetworkStream stream = client.GetStream();
                {
                    
                    SaveFileFromStream(stream, lastFileId);
                }
            }
            catch (Exception exc)
            {

            }
            finally
            {
                client.Close();
            }
        }

        public void SaveFileFromStream(Stream stream, string fileId)
        {
            var dir = Path.Combine("Storage", fileId);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string fullPath = Path.Combine(dir, $"{fileId}.dcm");
            using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fs);
            }
        }
    }
}
