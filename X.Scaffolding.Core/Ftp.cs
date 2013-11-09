using System;
using System.IO;
using System.Net;

namespace X.Scaffolding.Core
{
    internal class Ftp
    {
        /// <summary>
        /// Upload file to ftp
        /// </summary>
        /// <param name="bytes">File content</param>
        /// <param name="path">Path for uploaded file</param>
        public void UploadFile(byte[] bytes, string path)
        {
            var request = CreateFtpRequest(path, WebRequestMethods.Ftp.UploadFile);
            
            request.ContentLength = bytes.Length;

            var stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            var response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] DownloadFile(string path)
        {
            var request = CreateFtpRequest(path, WebRequestMethods.Ftp.DownloadFile);
            
            var stream = request.GetResponse().GetResponseStream();

            return ReadToEnd(stream);
        }

        private static FtpWebRequest CreateFtpRequest(string path, string method)
        {
            var request = (FtpWebRequest)WebRequest.Create(path);
            request.Method = method;
            return request;
        }

        private static byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                var readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();

                        if (nextByte != -1)
                        {
                            var temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}
