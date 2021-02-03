
namespace SpotifyAdsMuting
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.txtCurrentPlaying = new System.Windows.Forms.Label();
            this.btnStartSpotify = new System.Windows.Forms.Button();
            this.btnMuteAction = new System.Windows.Forms.Button();
            this.chbAutoStart = new System.Windows.Forms.CheckBox();
            this.chbAutoMute = new System.Windows.Forms.CheckBox();
            this.timerCheck = new System.Windows.Forms.Timer(this.components);
            this.txtAdsBlocked = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.muteSpotifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSpotifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripNotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Playing:";
            // 
            // txtCurrentPlaying
            // 
            this.txtCurrentPlaying.AutoEllipsis = true;
            this.txtCurrentPlaying.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentPlaying.ForeColor = System.Drawing.Color.Green;
            this.txtCurrentPlaying.Location = new System.Drawing.Point(81, 10);
            this.txtCurrentPlaying.Name = "txtCurrentPlaying";
            this.txtCurrentPlaying.Size = new System.Drawing.Size(320, 20);
            this.txtCurrentPlaying.TabIndex = 1;
            this.txtCurrentPlaying.Text = "N/A";
            // 
            // btnStartSpotify
            // 
            this.btnStartSpotify.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartSpotify.Location = new System.Drawing.Point(260, 80);
            this.btnStartSpotify.Name = "btnStartSpotify";
            this.btnStartSpotify.Size = new System.Drawing.Size(141, 37);
            this.btnStartSpotify.TabIndex = 2;
            this.btnStartSpotify.Text = "Open Spotify";
            this.btnStartSpotify.UseVisualStyleBackColor = true;
            this.btnStartSpotify.Click += new System.EventHandler(this.btnStartSpotify_Click);
            // 
            // btnMuteAction
            // 
            this.btnMuteAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMuteAction.Location = new System.Drawing.Point(260, 37);
            this.btnMuteAction.Name = "btnMuteAction";
            this.btnMuteAction.Size = new System.Drawing.Size(141, 37);
            this.btnMuteAction.TabIndex = 3;
            this.btnMuteAction.Text = "Mute Spotify";
            this.btnMuteAction.UseVisualStyleBackColor = true;
            this.btnMuteAction.Click += new System.EventHandler(this.btnMuteAction_Click);
            // 
            // chbAutoStart
            // 
            this.chbAutoStart.AutoSize = true;
            this.chbAutoStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbAutoStart.Location = new System.Drawing.Point(16, 93);
            this.chbAutoStart.Name = "chbAutoStart";
            this.chbAutoStart.Size = new System.Drawing.Size(133, 20);
            this.chbAutoStart.TabIndex = 4;
            this.chbAutoStart.Text = "Start with windows";
            this.chbAutoStart.UseVisualStyleBackColor = true;
            this.chbAutoStart.CheckedChanged += new System.EventHandler(this.chbAutoStart_CheckedChanged);
            // 
            // chbAutoMute
            // 
            this.chbAutoMute.AutoSize = true;
            this.chbAutoMute.Checked = true;
            this.chbAutoMute.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAutoMute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbAutoMute.Location = new System.Drawing.Point(16, 67);
            this.chbAutoMute.Name = "chbAutoMute";
            this.chbAutoMute.Size = new System.Drawing.Size(137, 20);
            this.chbAutoMute.TabIndex = 5;
            this.chbAutoMute.Text = "Auto mute/un-mute";
            this.chbAutoMute.UseVisualStyleBackColor = true;
            this.chbAutoMute.CheckedChanged += new System.EventHandler(this.chbAutoMute_CheckedChanged);
            // 
            // timerCheck
            // 
            this.timerCheck.Enabled = true;
            this.timerCheck.Interval = 200;
            this.timerCheck.Tick += new System.EventHandler(this.timerCheck_Tick);
            // 
            // txtAdsBlocked
            // 
            this.txtAdsBlocked.AutoSize = true;
            this.txtAdsBlocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdsBlocked.Location = new System.Drawing.Point(13, 48);
            this.txtAdsBlocked.Name = "txtAdsBlocked";
            this.txtAdsBlocked.Size = new System.Drawing.Size(97, 16);
            this.txtAdsBlocked.TabIndex = 6;
            this.txtAdsBlocked.Text = "Ads blocked: 0";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripNotifyIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Spotify Ads Muting";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStripNotifyIcon
            // 
            this.contextMenuStripNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muteSpotifyToolStripMenuItem,
            this.openSpotifyToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStripNotifyIcon.Name = "contextMenuStripNotifyIcon";
            this.contextMenuStripNotifyIcon.Size = new System.Drawing.Size(144, 70);
            this.contextMenuStripNotifyIcon.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripNotifyIcon_Opening);
            // 
            // muteSpotifyToolStripMenuItem
            // 
            this.muteSpotifyToolStripMenuItem.Name = "muteSpotifyToolStripMenuItem";
            this.muteSpotifyToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.muteSpotifyToolStripMenuItem.Text = "Mute Spotify";
            this.muteSpotifyToolStripMenuItem.Click += new System.EventHandler(this.muteSpotifyToolStripMenuItem_Click);
            // 
            // openSpotifyToolStripMenuItem
            // 
            this.openSpotifyToolStripMenuItem.Name = "openSpotifyToolStripMenuItem";
            this.openSpotifyToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openSpotifyToolStripMenuItem.Text = "Open Spotify";
            this.openSpotifyToolStripMenuItem.Click += new System.EventHandler(this.openSpotifyToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 130);
            this.Controls.Add(this.txtAdsBlocked);
            this.Controls.Add(this.chbAutoMute);
            this.Controls.Add(this.chbAutoStart);
            this.Controls.Add(this.btnMuteAction);
            this.Controls.Add(this.btnStartSpotify);
            this.Controls.Add(this.txtCurrentPlaying);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spotify Ads Muting v1.2";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
            this.contextMenuStripNotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtCurrentPlaying;
        private System.Windows.Forms.Button btnStartSpotify;
        private System.Windows.Forms.Button btnMuteAction;
        private System.Windows.Forms.CheckBox chbAutoStart;
        private System.Windows.Forms.CheckBox chbAutoMute;
        private System.Windows.Forms.Timer timerCheck;
        private System.Windows.Forms.Label txtAdsBlocked;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem openSpotifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem muteSpotifyToolStripMenuItem;
    }
}

