using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang;
using MyPhoneInfo.ViewModel;
using Xamarin.Essentials;

namespace MyPhoneInfo.Fragments
{
    public class CPU : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.list_main, container, false);

            ListView lstData = (ListView)view.FindViewById(Resource.Id.listMain);

            List<ListItemModel> items = GetCpuInfo();
            lstData.Adapter = new ListViewAdapter(Activity, items);

            return view;
        }

        List<ListItemModel> GetCpuInfo()
        {
            string supportedAbi= string.Join(", ", Build.SupportedAbis.ToArray());
            string supported32BitAbi = string.Join(", ", Build.Supported32BitAbis.ToArray());
            string supported64BitAbi = string.Join(", ", Build.Supported64BitAbis.ToArray());
            string totalCores = Runtime.GetRuntime().AvailableProcessors().ToString();
            string architecture = JavaSystem.GetProperty("os.arch");

            List<ListItemModel> mItems = new List<ListItemModel>();
            mItems.Add(new ListItemModel() { Name = "Architecture", Value = architecture });
            mItems.Add(new ListItemModel() { Name = "CPU Cores", Value = totalCores });
            mItems.Add(new ListItemModel() { Name = "Supported Abis", Value = supportedAbi });
            mItems.Add(new ListItemModel() { Name = "Supported 32-bit Abis", Value = supported32BitAbi });
            mItems.Add(new ListItemModel() { Name = "Supported 64-bit Abis", Value = supported64BitAbi });





            //string[] DATA = new string[] { "/system/bin/cat", "/proc/cpuinfo" };
            ////byte[] byteArry = new byte[1024];
            //ProcessBuilder processBuilder = null;

            //try
            //{
            //    processBuilder = new ProcessBuilder(DATA);

            //    Java.Lang.Process  process = processBuilder.Start();

            //    System.IO.Stream  inputStream = process.InputStream;

            //    byte[] bytes = new byte[inputStream.Length];
            //    inputStream.Position = 0;
            //    inputStream.Read(bytes, 0, (int)inputStream.Length);
            //    string data = Encoding.ASCII.GetString(bytes); // this is your string

            //}
            //catch (IOException ex)
            //{

            //    ex.PrintStackTrace();
            //}

            //string cmd = "top -m 1000 -d 1 -n 1 | grep \"" + pid + "\" ";
            //Runtime.GetRuntime().Exec(cmd);



            //var display = Activity.WindowManager.DefaultDisplay;
            

            //int[] cpu = getCpuUsageStatistic();
            //if (cpu != null)
            //{
            //    int DEVICE_TOTAL_CPU_USAGE = cpu[0] + cpu[1] + cpu[2] + cpu[3];
            //    int DEVICE_TOTAL_CPU_USAGE_SYSTEM = cpu[1];
            //    int DEVICE_TOTAL_CPU_USAGE_USER = cpu[0];
            //    int DEVICE_TOTAL_CPU_IDLE = cpu[2];
            //}

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("abi: ").Append(Build.CpuAbi).Append("\n");

            if (new File("/proc/cpuinfo").Exists())
            {
                try
                {
                    //BufferedReader br = new BufferedReader(new FileReader(new File("/proc/cpuinfo")));
                    //String aLine;

                    BufferedReader br = new BufferedReader(new FileReader(new File("/proc/cpuinfo")));
                    string aLine;
                    while ((aLine = br.ReadLine()) != null)
                    {
                        sb.Append(aLine + "\n");
                    }
                    if (br != null)
                    {
                        br.Close();
                    }
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }
            }

            string ss = sb.ToString();

            //https://stackoverflow.com/questions/3021054/how-to-read-cpu-frequency-on-android-device
            //# cat "/sys/devices/system/cpu/cpu0/cpufreq/scaling_cur_freq"
            //# cat "/sys/devices/system/cpu/cpu0/cpufreq/cpuinfo_min_freq"
            //# cat "/sys/devices/system/cpu/cpu0/cpufreq/cpuinfo_max_freq"
            ///sys/devices/system/cpu/cpu0/cpufreq/scaling_governor
            //////sys/devices/system/cpu/cpu0/cpufreq/scaling_available_frequencies

            var files = GetCPUCores(); //loop below random access file code for each core

            foreach (string file in files)
            {
                ///sys/devices/system/cpu/cpu4/cpufreq/scaling_cur_freq
                string newFile = file + "/cpufreq/scaling_cur_freq";
                sb = new System.Text.StringBuilder();

                if (new File(newFile).Exists())
                {
                    try
                    {
                        //BufferedReader br = new BufferedReader(new FileReader(new File("/proc/cpuinfo")));
                        //String aLine;

                        BufferedReader br = new BufferedReader(new FileReader(new File(newFile)));
                        string aLine;
                        while ((aLine = br.ReadLine()) != null)
                        {
                            sb.Append(aLine + "\n");
                        }
                        if (br != null)
                        {
                            br.Close();
                        }
                    }
                    catch (IOException e)
                    {
                        e.PrintStackTrace();
                    }
                }

                //RandomAccessFile reader = new RandomAccessFile(file, "r");

                //try
                //{
                //    //string load = reader.ReadLine();
                //    string load = sb.ToString();

                //    string[] toks = load.Split(" ");  // Split on one or more spaces

                //    long idle1 = Long.ParseLong(toks[4]);
                //    long cpu1 = Long.ParseLong(toks[2]) + Long.ParseLong(toks[3]) + Long.ParseLong(toks[5])
                //          + Long.ParseLong(toks[6]) + Long.ParseLong(toks[7]) + Long.ParseLong(toks[8]);

                //    try
                //    {
                //        Thread.Sleep(360);
                //    }
                //    catch (System.Exception e) { }

                //    //reader.Seek(0);
                //    //load = reader.ReadLine();
                //    //reader.Close();

                //    toks = load.Split(" +");

                //    long idle2 = Long.ParseLong(toks[4]);
                //    long cpu2 = Long.ParseLong(toks[2]) + Long.ParseLong(toks[3]) + Long.ParseLong(toks[5])
                //        + Long.ParseLong(toks[6]) + Long.ParseLong(toks[7]) + Long.ParseLong(toks[8]);

                //    var value = (float)(cpu2 - cpu1) / ((cpu2 + idle2) - (cpu1 + idle1));
                //}
                //catch { }

            }







            return mItems;
        }


        List<string> GetCPUCores()
        {
            List<string> listFiles = new List<string>();

            try
            {
                //Get directory containing CPU info
                File dir = new File("/sys/devices/system/cpu/");
                //Filter to only list the devices we care about
                File[] files = dir.ListFiles();
                //Return the number of cores (virtual CPU devices)



                foreach (File f in files)
                {
                    if (System.Text.RegularExpressions.Regex.Match(f.Name, "cpu[0-9]+").Success)
                    {
                        listFiles.Add(f.Path);
                    }
                }

                return listFiles;
            }
            catch (System.Exception e)
            {
                //Default to return 1 core
                return listFiles;
            }
        }

        private static int[] getCpuUsageStatistic()
        {
            try
            {
                string tempString = executeTop();

                tempString = tempString.Replace(",", "");
                tempString = tempString.Replace("User", "");
                tempString = tempString.Replace("System", "");
                tempString = tempString.Replace("IOW", "");
                tempString = tempString.Replace("IRQ", "");
                tempString = tempString.Replace("%", "");
                for (int i = 0; i < 10; i++)
                {
                    tempString = tempString.Replace("  ", " ");
                }
                tempString = tempString.Trim();
                string[] myString = tempString.Split(" ");
                int[] cpuUsageAsInt = new int[myString.Length];
                for (int i = 0; i < myString.Length; i++)
                {
                    myString[i] = myString[i].Trim();
                    cpuUsageAsInt[i] = Integer.ParseInt(myString[i]);
                }
                return cpuUsageAsInt;

            }
            catch (Java.Lang.Exception e)
            {
                e.PrintStackTrace();
                //Log.e("executeTop", "error in getting cpu statics");
                return null;
            }
        }

        private static string executeTop()
        {
            Java.Lang.Process p = null;
            BufferedReader in1 = null;
            string returnString = null;
            try
            {
                p = Runtime.GetRuntime().Exec("top -n 1");
                in1 = new BufferedReader(new InputStreamReader(p.InputStream));
                while (returnString == null || returnString == "")
                {
                    returnString = in1.ReadLine();
                }
            }
            catch (IOException e)
            {
                //Log.e("executeTop", "error in getting first line of top");
                e.PrintStackTrace();
            }
            finally
            {
                try
                {
                    in1.Close();
                    p.Destroy();
                }
                catch (IOException e)
                {
                    //Log.e("executeTop", "error in closing and destroying top process");
                    e.PrintStackTrace();
                }
            }
            return returnString;
        }

    }

    //public class CpuFilter : IFileFilter
    //{
    //    public IntPtr Handle => new IntPtr(0); // throw new NotImplementedException();

    //    public bool Accept(File pathname)
    //    {
    //        //Pattern.matches("cpu[0-9]+", pathname.Name)

    //        //Check if filename is "cpu", followed by one or more digits
    //        if (System.Text.RegularExpressions.Regex.Match(pathname.Name, "cpu[0-9]+").Success)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }

    //    public void Dispose()
    //    {
    //        return;
    //    }
    //}
}