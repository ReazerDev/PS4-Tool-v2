using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Android.Content;
using Android.Widget;

namespace PS4_Tool_v2
{
    class Functions
    {
        readonly string pathToFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/PS4 Tool/";
        readonly string pathToDownload = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).ToString();

        public void Send(Context context, string file)
        {
            SaveSettings settings = new SaveSettings();
            settings.Load();
            if (string.IsNullOrWhiteSpace(settings.IPAdress))
            {
                Toast.MakeText(context, "Please enter your PS4's IP Adress in the Settings Section!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrWhiteSpace(settings.port))
            {
                Toast.MakeText(context, "Please enter a Port in the Settings Section!", ToastLength.Short).Show();
                return;
            }
            try
            {
                
                Socket client = new Socket(SocketType.Stream, ProtocolType.Tcp);
                int port = int.Parse(settings.port);
                IPAddress IPAdress = IPAddress.Parse(settings.IPAdress);
                IPEndPoint ipEndpoint = new IPEndPoint(IPAdress, port);

                client.Connect(ipEndpoint);
                Toast.MakeText(context, "Sending...", ToastLength.Short).Show();
                client.SendFile(pathToFolder + file);
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch
            {
                Toast.MakeText(context, "Can't connect to PS4!", ToastLength.Short).Show();
            }
        }

        public void getFilesInDownload()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(pathToDownload);
            FileInfo[] files = directoryInfo.GetFiles("*.bin");
            foreach (FileInfo f in files)
            {
                if (File.Exists(pathToFolder + f.Name))
                {
                    File.Delete(pathToFolder + f.Name);
                    File.Copy(f.FullName, pathToFolder + f.Name);
                }
                else
                {
                    File.Copy(f.FullName, pathToFolder + f.Name);
                }
            }
        }

        public List<string> getFilesInDirectory()
        {
            List<string> payloadList = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(pathToFolder);
            FileInfo[] files = directoryInfo.GetFiles("*.bin");
            foreach (FileInfo f in files)
            {
                payloadList.Add(f.Name);
            }
            return payloadList;
        }

        public void createDirectories(Context context)
        {
            if (!Directory.Exists(pathToFolder))
            {
                Directory.CreateDirectory(pathToFolder);
            }

            if (!Directory.Exists(pathToFolder + "Fan Payloads/"))
            {
                Directory.CreateDirectory(pathToFolder + "Fan Payloads/");
            }



        }

        public void moveFromAssets(Context context)
        {
             Directory.CreateDirectory(pathToFolder);

             string[] files = context.Assets.List("Payloads");
             foreach (string f in files)
             {
                if (!File.Exists(pathToFolder + f))
                {
                    using (FileStream writeStream = new FileStream(pathToFolder + f, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        context.Assets.Open("Payloads/" + f).CopyTo(writeStream);
                    }
                }
             }

            string[] fanFiles = context.Assets.List("FanPayloads");
            foreach (string f in fanFiles)
            {
                if (File.Exists(pathToFolder + "Fan Payloads/" + f))
                {
                    using (FileStream writeStream = new FileStream(pathToFolder + "Fan Payloads/" + f, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        context.Assets.Open("FanPayloads/" + f).CopyTo(writeStream);
                    }
                }
            }
        }

        public void importFromV1(Context context, bool first, bool second, bool third)
        {
            //Messy Code incoming ^^

            //Check if 1.76 and 4.05 Folder exists, if so move them up one directory
            if (first)
            {
                if (!Directory.Exists(pathToFolder + "1.764.05"))
                {
                    Toast.MakeText(context, "No 1.76 or 4.05 Payloads where found!", ToastLength.Short).Show();
                }
                else
                {
                    int count = 0;
                    DirectoryInfo di = new DirectoryInfo(pathToFolder + "1.764.05");
                    FileInfo[] files = di.GetFiles("*.bin");
                    foreach (FileInfo f in files)
                    {
                        if (File.Exists(pathToFolder + f.Name))
                        {
                            continue;
                        }
                        File.Move(f.FullName, pathToFolder + f.Name);
                        count++;
                    }
                    Toast.MakeText(context, count + " Payloads where imported from 1.76 and 4.05!", ToastLength.Short).Show();
                    Directory.Delete(pathToFolder + "1.764.05");
                }
            }



            //Check if 4.55 Folder exists, if so move them up one directory
            if (second)
            {
                if (!Directory.Exists(pathToFolder + "4.55"))
                {
                    Toast.MakeText(context, "No 4.55 Payloads where found!", ToastLength.Short).Show();
                }
                else
                {
                    int count = 0;
                    DirectoryInfo di = new DirectoryInfo(pathToFolder + "4.55");
                    FileInfo[] files = di.GetFiles("*.bin");
                    foreach (FileInfo f in files)
                    {
                        if (File.Exists(pathToFolder + f.Name))
                        {
                            continue;
                        }
                        File.Move(f.FullName, pathToFolder + f.Name);
                        count++;
                    }
                    Toast.MakeText(context, count + " Payloads where imported from 4.05!", ToastLength.Short).Show();
                    Directory.Delete(pathToFolder + "4.55");
                }
            }



            //Check if 5.05 Folder exists, if so move them up one directory
            if (third)
            {
                if (!Directory.Exists(pathToFolder + "5.05"))
                {
                    Toast.MakeText(context, "No 5.05 Payloads where found!", ToastLength.Short).Show();
                }
                else
                {
                    int count = 0;
                    DirectoryInfo di = new DirectoryInfo(pathToFolder + "5.05");
                    FileInfo[] files = di.GetFiles("*.bin");
                    foreach (FileInfo f in files)
                    {
                        if(File.Exists(pathToFolder + f.Name))
                        {
                            continue;
                        }
                        File.Move(f.FullName, pathToFolder + f.Name);
                        count++;
                    }
                    Toast.MakeText(context, count + " Payloads where imported from 5.05!", ToastLength.Short).Show();
                    Directory.Delete(pathToFolder + "5.05");
                }
            }
        }


    }
}