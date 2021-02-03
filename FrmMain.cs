using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using RestSharp;
using SpotifyAdsMuting.Models;
using SpotifyAdsMuting.Properties;

namespace SpotifyAdsMuting
{
    public partial class FrmMain : Form
    {
        private Process _mainSprotifyProcess;
        private string _lastSpotifyTitle = "";
        private bool _isSpotifyMuting;
        private int _totalBlockedAds;
        private bool _isProcessing;
        private DateTime? _lastGetSprotifyProcess;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void timerCheck_Tick(object sender, EventArgs e)
        {
            if (!_appConfig.AutoMute)
                return;
            if (_isProcessing)
                return;
            _isProcessing = true;
            AutoMute();

            _isProcessing = false;
        }

        private void AutoMute()
        {
            _mainSprotifyProcess?.Refresh();

            if (_mainSprotifyProcess == null || _mainSprotifyProcess.HasExited)
            {
                GetSpotifyProcess();
            }

            if (_mainSprotifyProcess == null)
            {
                return;
            }

            var currentTile = _mainSprotifyProcess.MainWindowTitle;
            if (currentTile?.Length > 60)
                currentTile = currentTile?.Substring(0, 60);
            if (!string.Equals(_lastSpotifyTitle, currentTile))
            {
                txtCurrentPlaying.Text = currentTile;
                _lastSpotifyTitle = currentTile;
                notifyIcon.Text = currentTile;
            }

            _isSpotifyMuting = VolumeMixer.GetApplicationMute(_mainSprotifyProcess.Id) ?? true;
            var isAdvertisement = IsAdvertisement(currentTile);
            if (isAdvertisement && _isSpotifyMuting != true)
            {
                VolumeMixer.SetApplicationMute(_mainSprotifyProcess.Id, true);
                _totalBlockedAds++;
                txtAdsBlocked.Text = $"Ads blocked: {_totalBlockedAds:N0}";
                btnMuteAction.Text = "Un-mute Spotify";
                //Console.WriteLine("Mute");
                txtCurrentPlaying.ForeColor = Color.Red;
                notifyIcon.Icon = Resources.notifyIconRed_Icon;
            }
            else if (!isAdvertisement && _isSpotifyMuting)
            {
                btnMuteAction.Text = "Mute Spotify";
                Thread.Sleep(1_000);
                VolumeMixer.SetApplicationMute(_mainSprotifyProcess.Id, false);
                txtCurrentPlaying.ForeColor = Color.Green;
                notifyIcon.Icon = Resources.notifyIcon_Icon;
            }
        }


        private void GetSpotifyProcess()
        {
            if (_lastGetSprotifyProcess == null || (DateTime.Now - _lastGetSprotifyProcess.Value).TotalMilliseconds > 5_000)
            {
                _mainSprotifyProcess = Process.GetProcessesByName("spotify").FirstOrDefault(process => !String.IsNullOrEmpty(process.MainWindowTitle));
                _lastGetSprotifyProcess = DateTime.Now;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ReadConfig();
            LoadConfig();
            notifyIcon.Icon = Resources.notifyIconRed_Icon;


            GetSpotifyProcess();
            if (_mainSprotifyProcess != null)
            {
                _appConfig.SpotifyPath = _mainSprotifyProcess?.MainModule?.FileName;
            }
            _lastSpotifyTitle = "";



            WriteConfig();
        }

        private void LoadConfig()
        {
            chbAutoMute.Checked = _appConfig.AutoMute;
            chbAutoStart.Checked = _appConfig.StartWithWindows;
        }

        private readonly string _appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SpotifyAdsMuting");
        private AppConfig _appConfig;

        void ReadConfig()
        {
            var config = new AppConfig() { AutoMute = true, StartWithWindows = true, SpotifyPath = null };
            var jsonPath = Path.Combine(_appDataFolder, "config.json");
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                config = SimpleJson.SimpleJson.DeserializeObject<AppConfig>(json);
            }

            _appConfig = config;
        }

        void WriteConfig()
        {
            Directory.CreateDirectory(_appDataFolder);
            var jsonPath = Path.Combine(_appDataFolder, "config.json");
            var json = SimpleJson.SimpleJson.SerializeObject(_appConfig);
            File.WriteAllText(jsonPath, json);
        }

        bool IsAdvertisement(string title)
        {
            return string.Equals(title, "Advertisement") || !title.Contains(" - ");

        }

        private void btnMuteAction_Click(object sender, EventArgs e)
        {
            MuteSpotify();
        }

        private void MuteSpotify()
        {
            chbAutoMute.Checked = false;
            if (_isSpotifyMuting)
            {
                _isSpotifyMuting = false;
                VolumeMixer.SetApplicationMute(_mainSprotifyProcess.Id, false);
                btnMuteAction.Text = "Mute Spotify";
                txtCurrentPlaying.ForeColor = Color.Green;
                notifyIcon.Icon = (Icon) new System.ComponentModel.ComponentResourceManager(GetType()).GetObject("notifyIcon.Icon");
            }
            else
            {
                _isSpotifyMuting = true;
                VolumeMixer.SetApplicationMute(_mainSprotifyProcess.Id, true);
                btnMuteAction.Text = "Un-mute Spotify";
                txtCurrentPlaying.ForeColor = Color.Red;
                notifyIcon.Icon = (Icon) new System.ComponentModel.ComponentResourceManager(GetType()).GetObject("notifyIconRed.Icon");
            }

            _appConfig.AutoMute = chbAutoMute.Checked;
            WriteConfig();
        }

        private void chbAutoMute_CheckedChanged(object sender, EventArgs e)
        {
            _appConfig.AutoMute = chbAutoMute.Checked;
            WriteConfig();
        }

        private void chbAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            _appConfig.StartWithWindows = chbAutoStart.Checked;
            SetAutoStart(_appConfig.StartWithWindows);
            WriteConfig();
        }

        private static void SetAutoStart(bool startWithWindows)
        {
            var reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (startWithWindows)
            {
                string value = Application.ExecutablePath;
                reg.SetValue("SpotifyAdsMuting", value);
            }
            else
            {
                reg.DeleteValue("SpotifyAdsMuting");
            }
        }

        private void btnStartSpotify_Click(object sender, EventArgs e)
        {
            OpenSpotify();
        }

        private void OpenSpotify()
        {
            if (!string.IsNullOrEmpty(_appConfig.SpotifyPath))
            {
                try
                {
                    var processStartInfo = new ProcessStartInfo(_appConfig.SpotifyPath);
                    Process.Start(processStartInfo);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else
            {
                MessageBox.Show("Please open Spotify manually at the first time, so the app can remember Spotify execution path");
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = true;
                WindowState = FormWindowState.Normal;
                Show();
            }
            else
            {
                WindowState = FormWindowState.Minimized;
                Hide();
            }
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void openSpotifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSpotify();
        }

        private void muteSpotifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MuteSpotify();

            muteSpotifyToolStripMenuItem.Checked = _isSpotifyMuting;
        }

        private void contextMenuStripNotifyIcon_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            muteSpotifyToolStripMenuItem.Checked = _isSpotifyMuting;
        }
    }
}
