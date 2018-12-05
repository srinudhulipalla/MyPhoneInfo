using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            ListView listView = (ListView)view.FindViewById(Resource.Id.listMain);

            List<ListItemModel> items = GetCpuInfo();
            listView.Adapter = new ListViewAdapter(Activity, items);

            // to update the latest info to UI (cpu core frequency)
            TimerCallback timerCallback = new TimerCallback(RunInBackground);

            TimerState state = new TimerState();
            state.ListView = listView;

            Timer timer = new Timer(timerCallback, state, 1000, 1000);
            state.Timer = timer;

            return view;
        }

        private void RunInBackground(object objState)
        {
            TimerState state = (TimerState)objState;

            if (this.Activity == null)
            {
                state.Timer.Dispose();
                state.Timer = null;
                return;
            }

            ListView listView = state.ListView;
            ListViewAdapter adapter = listView.Adapter as ListViewAdapter;

            this.Activity.RunOnUiThread(() =>
            {
                List<CpuCore> cpuCores = GetCpuCoresFrequency();

                foreach (CpuCore core in cpuCores)
                {
                    var item = adapter.Items.FirstOrDefault(i => i.Name.Trim() == core.ClockName);

                    if (item != null)
                    {
                        item.Value = core.FrequencyMHz + " MHz";
                    }
                }

                adapter.NotifyDataSetChanged();
            });
        }

        List<ListItemModel> GetCpuInfo()
        {
            string supportedAbi = string.Join(", ", Build.SupportedAbis.ToArray());
            string supported32BitAbi = string.Join(", ", Build.Supported32BitAbis.ToArray());
            string supported64BitAbi = string.Join(", ", Build.Supported64BitAbis.ToArray());
            string totalCores = Runtime.GetRuntime().AvailableProcessors().ToString();
            string architecture = JavaSystem.GetProperty("os.arch");

            List<ListItemModel> items = new List<ListItemModel>();
            items.Add(new ListItemModel() { Name = "Architecture", Value = architecture });
            items.Add(new ListItemModel() { Name = "CPU Cores", Value = totalCores });

            long[] coreSpeedRange = GetMinMaxClockSpeedRange();

            if (coreSpeedRange != null && coreSpeedRange[0] != 0 && coreSpeedRange[1] != 0)
            {
                items.Add(new ListItemModel() { Name = "Clock Speed Range", Value = $"{coreSpeedRange[0]} MHz - {coreSpeedRange[1]} MHz" });
            }

            List<CpuCore> cpuCores = GetCpuCoresFrequency();

            foreach (CpuCore core in cpuCores)
            {
                items.Add(new ListItemModel() { Name = core.ClockName.PadLeft(15, ' '), Value = core.FrequencyMHz + " MHz" });
            }

            items.Add(new ListItemModel() { Name = "Scaling Governor", Value = GetScalingGovernor() });
            items.Add(new ListItemModel() { Name = "Supported ABIs", Value = supportedAbi });
            items.Add(new ListItemModel() { Name = "Supported 32-bit ABIs", Value = supported32BitAbi });
            items.Add(new ListItemModel() { Name = "Supported 64-bit ABIs", Value = supported64BitAbi });

          
            

            return items;

            //# cat "/sys/devices/system/cpu/cpu0/cpufreq/scaling_cur_freq"
            //# cat "/sys/devices/system/cpu/cpu0/cpufreq/cpuinfo_min_freq"
            //# cat "/sys/devices/system/cpu/cpu0/cpufreq/cpuinfo_max_freq"
            ///sys/devices/system/cpu/cpu0/cpufreq/scaling_governor
            ///sys/devices/system/cpu/cpu0/cpufreq/scaling_available_frequencies
        }


        List<CpuCore> GetCpuCoresFrequency()
        {
            List<CpuCore> cpuCores = GetCpuCoresFilePath();

            foreach (CpuCore core in cpuCores)
            {
                try
                {
                    string scalingFreqFilePath = $"{core.FilePath}/cpufreq/scaling_cur_freq".Replace("//", "/");

                    string frequency = Utils.ReadFile(scalingFreqFilePath);

                    if (!string.IsNullOrWhiteSpace(frequency))
                    {
                        core.FrequencyKHz = long.Parse(frequency);
                    }
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }
            }

            return cpuCores;
        }


        List<CpuCore> GetCpuCoresFilePath()
        {
            List<CpuCore> cpuFiles = new List<CpuCore>();

            try
            {
                File directory = new File("/sys/devices/system/cpu/");
                File[] files = directory.ListFiles();

                foreach (File f in files)
                {
                    if (System.Text.RegularExpressions.Regex.Match(f.Name.ToLower(), "cpu[0-9]+").Success)
                    {
                        cpuFiles.Add(new CpuCore() { Name = f.Name, FilePath = f.Path });
                    }
                }
            }
            catch (Java.Lang.Exception ex)
            { }

            return cpuFiles;
        }

        string GetScalingGovernor()
        {
            string path = "/sys/devices/system/cpu/cpu0/cpufreq/scaling_governor";
            return Utils.ReadFile(path);
        }

        long[] GetMinMaxClockSpeedRange()
        {
            long[] iRange = new long[2];

            try
            {
                int cores = Runtime.GetRuntime().AvailableProcessors();

                string minPath = "/sys/devices/system/cpu/cpu0/cpufreq/cpuinfo_min_freq";
                string maxPath = $"/sys/devices/system/cpu/cpu{cores - 1}/cpufreq/cpuinfo_max_freq";

                string minValue = Utils.ReadFile(minPath);
                string maxValue = Utils.ReadFile(maxPath);

                if (!string.IsNullOrWhiteSpace(minValue))
                {
                    iRange[0] = long.Parse(minValue) / 1000;
                }

                if (!string.IsNullOrWhiteSpace(maxValue))
                {
                    iRange[1] = long.Parse(maxValue) / 1000;
                }

                return iRange;
            }
            catch (Java.Lang.Exception ex)
            {
                ex.PrintStackTrace();
                return null;
            }
        }



    }

}