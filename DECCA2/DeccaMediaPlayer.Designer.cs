namespace DECCA2
{
    partial class DeccaMediaPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeccaMediaPlayer));
            this.panel1 = new System.Windows.Forms.Panel();
            this.FilesBtn = new System.Windows.Forms.Button();
            this.ArtistBtn = new System.Windows.Forms.Button();
            this.RadioBtn = new System.Windows.Forms.Button();
            this.AlbumsBtn = new System.Windows.Forms.Button();
            this.LiveTvBtn = new System.Windows.Forms.Button();
            this.SongsBtn = new System.Windows.Forms.Button();
            this.Browse_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Close_Btn = new System.Windows.Forms.PictureBox();
            this.Max_Btn = new System.Windows.Forms.PictureBox();
            this.Min_Btn = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DeccaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.VolumeSpeed = new System.Windows.Forms.TrackBar();
            this.FullScreenBtn = new System.Windows.Forms.Button();
            this.MuteBtn = new System.Windows.Forms.Button();
            this.AddMedia_Btn = new System.Windows.Forms.Button();
            this.QuickFFW_Btn = new System.Windows.Forms.Button();
            this.Stop_Btn = new System.Windows.Forms.Button();
            this.FFW_Btn = new System.Windows.Forms.Button();
            this.Pause_Btn = new System.Windows.Forms.Button();
            this.Play_Btn = new System.Windows.Forms.Button();
            this.RWD_Btn = new System.Windows.Forms.Button();
            this.FRWD_Btn = new System.Windows.Forms.Button();
            this.Playlistlb = new System.Windows.Forms.ListBox();
            this.picturebox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Close_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Max_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min_Btn)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeccaPlayer)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panel1.Controls.Add(this.FilesBtn);
            this.panel1.Controls.Add(this.ArtistBtn);
            this.panel1.Controls.Add(this.RadioBtn);
            this.panel1.Controls.Add(this.AlbumsBtn);
            this.panel1.Controls.Add(this.LiveTvBtn);
            this.panel1.Controls.Add(this.SongsBtn);
            this.panel1.Controls.Add(this.Browse_btn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Close_Btn);
            this.panel1.Controls.Add(this.Max_Btn);
            this.panel1.Controls.Add(this.Min_Btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 556);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // FilesBtn
            // 
            this.FilesBtn.FlatAppearance.BorderSize = 0;
            this.FilesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FilesBtn.Font = new System.Drawing.Font("Lucida Sans Typewriter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilesBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FilesBtn.Image = global::DECCA2.Properties.Resources.document_movie_2_icon;
            this.FilesBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FilesBtn.Location = new System.Drawing.Point(30, 425);
            this.FilesBtn.Name = "FilesBtn";
            this.FilesBtn.Size = new System.Drawing.Size(180, 40);
            this.FilesBtn.TabIndex = 2;
            this.FilesBtn.Text = "Your Files";
            this.FilesBtn.UseVisualStyleBackColor = true;
            // 
            // ArtistBtn
            // 
            this.ArtistBtn.FlatAppearance.BorderSize = 0;
            this.ArtistBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ArtistBtn.Font = new System.Drawing.Font("Lucida Sans Typewriter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArtistBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ArtistBtn.Image = global::DECCA2.Properties.Resources.Occupations_Musician_Male_Dark_icon;
            this.ArtistBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ArtistBtn.Location = new System.Drawing.Point(30, 379);
            this.ArtistBtn.Name = "ArtistBtn";
            this.ArtistBtn.Size = new System.Drawing.Size(180, 40);
            this.ArtistBtn.TabIndex = 2;
            this.ArtistBtn.Text = "Artists";
            this.ArtistBtn.UseVisualStyleBackColor = true;
            // 
            // RadioBtn
            // 
            this.RadioBtn.FlatAppearance.BorderSize = 0;
            this.RadioBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RadioBtn.Font = new System.Drawing.Font("Lucida Sans Typewriter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RadioBtn.Image = global::DECCA2.Properties.Resources.radio_metal_2_icon;
            this.RadioBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RadioBtn.Location = new System.Drawing.Point(30, 190);
            this.RadioBtn.Name = "RadioBtn";
            this.RadioBtn.Size = new System.Drawing.Size(180, 40);
            this.RadioBtn.TabIndex = 2;
            this.RadioBtn.Text = "Radio";
            this.RadioBtn.UseVisualStyleBackColor = true;
            // 
            // AlbumsBtn
            // 
            this.AlbumsBtn.FlatAppearance.BorderSize = 0;
            this.AlbumsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AlbumsBtn.Font = new System.Drawing.Font("Lucida Sans Typewriter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlbumsBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AlbumsBtn.Image = global::DECCA2.Properties.Resources.Music_Blue_Folder_icon;
            this.AlbumsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AlbumsBtn.Location = new System.Drawing.Point(30, 333);
            this.AlbumsBtn.Name = "AlbumsBtn";
            this.AlbumsBtn.Size = new System.Drawing.Size(180, 40);
            this.AlbumsBtn.TabIndex = 2;
            this.AlbumsBtn.Text = "Albums";
            this.AlbumsBtn.UseVisualStyleBackColor = true;
            // 
            // LiveTvBtn
            // 
            this.LiveTvBtn.FlatAppearance.BorderSize = 0;
            this.LiveTvBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LiveTvBtn.Font = new System.Drawing.Font("Lucida Sans Typewriter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LiveTvBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LiveTvBtn.Image = global::DECCA2.Properties.Resources.television_08_icon;
            this.LiveTvBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LiveTvBtn.Location = new System.Drawing.Point(30, 144);
            this.LiveTvBtn.Name = "LiveTvBtn";
            this.LiveTvBtn.Size = new System.Drawing.Size(180, 40);
            this.LiveTvBtn.TabIndex = 2;
            this.LiveTvBtn.Text = "Live TV";
            this.LiveTvBtn.UseVisualStyleBackColor = true;
            // 
            // SongsBtn
            // 
            this.SongsBtn.FlatAppearance.BorderSize = 0;
            this.SongsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SongsBtn.Font = new System.Drawing.Font("Lucida Sans Typewriter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SongsBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SongsBtn.Image = global::DECCA2.Properties.Resources.folder_music_share_icon;
            this.SongsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SongsBtn.Location = new System.Drawing.Point(30, 287);
            this.SongsBtn.Name = "SongsBtn";
            this.SongsBtn.Size = new System.Drawing.Size(180, 40);
            this.SongsBtn.TabIndex = 2;
            this.SongsBtn.Text = "Songs";
            this.SongsBtn.UseVisualStyleBackColor = true;
            // 
            // Browse_btn
            // 
            this.Browse_btn.FlatAppearance.BorderSize = 0;
            this.Browse_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Browse_btn.Font = new System.Drawing.Font("Lucida Sans Typewriter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Browse_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Browse_btn.Image = global::DECCA2.Properties.Resources.Folder_Open_icon;
            this.Browse_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Browse_btn.Location = new System.Drawing.Point(30, 98);
            this.Browse_btn.Name = "Browse_btn";
            this.Browse_btn.Size = new System.Drawing.Size(180, 40);
            this.Browse_btn.TabIndex = 2;
            this.Browse_btn.Text = "Browse";
            this.Browse_btn.UseVisualStyleBackColor = true;
            this.Browse_btn.Click += new System.EventHandler(this.Browse_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(26, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sub Menu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(26, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Menu";
            // 
            // Close_Btn
            // 
            this.Close_Btn.Image = global::DECCA2.Properties.Resources.cancel;
            this.Close_Btn.Location = new System.Drawing.Point(126, 3);
            this.Close_Btn.Name = "Close_Btn";
            this.Close_Btn.Size = new System.Drawing.Size(30, 30);
            this.Close_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Close_Btn.TabIndex = 0;
            this.Close_Btn.TabStop = false;
            this.Close_Btn.Click += new System.EventHandler(this.Close_Btn_Click);
            // 
            // Max_Btn
            // 
            this.Max_Btn.Image = global::DECCA2.Properties.Resources.multi_tab;
            this.Max_Btn.Location = new System.Drawing.Point(63, 3);
            this.Max_Btn.Name = "Max_Btn";
            this.Max_Btn.Size = new System.Drawing.Size(30, 30);
            this.Max_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Max_Btn.TabIndex = 0;
            this.Max_Btn.TabStop = false;
            this.Max_Btn.Click += new System.EventHandler(this.Max_Btn_Click);
            // 
            // Min_Btn
            // 
            this.Min_Btn.Image = global::DECCA2.Properties.Resources.substract;
            this.Min_Btn.Location = new System.Drawing.Point(12, 0);
            this.Min_Btn.Name = "Min_Btn";
            this.Min_Btn.Size = new System.Drawing.Size(30, 30);
            this.Min_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Min_Btn.TabIndex = 0;
            this.Min_Btn.TabStop = false;
            this.Min_Btn.Click += new System.EventHandler(this.Min_Btn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(9)))));
            this.panel2.Controls.Add(this.DeccaPlayer);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.Playlistlb);
            this.panel2.Controls.Add(this.picturebox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(239, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(789, 556);
            this.panel2.TabIndex = 1;
            // 
            // DeccaPlayer
            // 
            this.DeccaPlayer.Enabled = true;
            this.DeccaPlayer.Location = new System.Drawing.Point(0, 144);
            this.DeccaPlayer.Name = "DeccaPlayer";
            this.DeccaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("DeccaPlayer.OcxState")));
            this.DeccaPlayer.Size = new System.Drawing.Size(603, 338);
            this.DeccaPlayer.TabIndex = 3;
            this.DeccaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.DeccaPlayer_PlayStateChange);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.VolumeSpeed);
            this.panel3.Controls.Add(this.FullScreenBtn);
            this.panel3.Controls.Add(this.MuteBtn);
            this.panel3.Controls.Add(this.AddMedia_Btn);
            this.panel3.Controls.Add(this.QuickFFW_Btn);
            this.panel3.Controls.Add(this.Stop_Btn);
            this.panel3.Controls.Add(this.FFW_Btn);
            this.panel3.Controls.Add(this.Pause_Btn);
            this.panel3.Controls.Add(this.Play_Btn);
            this.panel3.Controls.Add(this.RWD_Btn);
            this.panel3.Controls.Add(this.FRWD_Btn);
            this.panel3.Location = new System.Drawing.Point(3, 478);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(791, 78);
            this.panel3.TabIndex = 2;
            // 
            // VolumeSpeed
            // 
            this.VolumeSpeed.LargeChange = 10;
            this.VolumeSpeed.Location = new System.Drawing.Point(568, 19);
            this.VolumeSpeed.Maximum = 100;
            this.VolumeSpeed.Name = "VolumeSpeed";
            this.VolumeSpeed.Size = new System.Drawing.Size(126, 45);
            this.VolumeSpeed.TabIndex = 1;
            this.VolumeSpeed.Value = 60;
            this.VolumeSpeed.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // FullScreenBtn
            // 
            this.FullScreenBtn.BackgroundImage = global::DECCA2.Properties.Resources.maximize__2_;
            this.FullScreenBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FullScreenBtn.FlatAppearance.BorderSize = 0;
            this.FullScreenBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FullScreenBtn.ForeColor = System.Drawing.Color.Teal;
            this.FullScreenBtn.Location = new System.Drawing.Point(703, 14);
            this.FullScreenBtn.Name = "FullScreenBtn";
            this.FullScreenBtn.Size = new System.Drawing.Size(50, 50);
            this.FullScreenBtn.TabIndex = 0;
            this.FullScreenBtn.UseVisualStyleBackColor = true;
            this.FullScreenBtn.Click += new System.EventHandler(this.FullScreenBtn_Click);
            // 
            // MuteBtn
            // 
            this.MuteBtn.BackgroundImage = global::DECCA2.Properties.Resources.media_btnvolum1;
            this.MuteBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MuteBtn.FlatAppearance.BorderSize = 0;
            this.MuteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MuteBtn.ForeColor = System.Drawing.Color.Teal;
            this.MuteBtn.Location = new System.Drawing.Point(509, 14);
            this.MuteBtn.Name = "MuteBtn";
            this.MuteBtn.Size = new System.Drawing.Size(50, 50);
            this.MuteBtn.TabIndex = 0;
            this.MuteBtn.UseVisualStyleBackColor = true;
            this.MuteBtn.Click += new System.EventHandler(this.MuteBtn_Click);
            // 
            // AddMedia_Btn
            // 
            this.AddMedia_Btn.BackgroundImage = global::DECCA2.Properties.Resources.mediaplayeraddplaylist;
            this.AddMedia_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AddMedia_Btn.FlatAppearance.BorderSize = 0;
            this.AddMedia_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddMedia_Btn.ForeColor = System.Drawing.Color.Teal;
            this.AddMedia_Btn.Location = new System.Drawing.Point(450, 14);
            this.AddMedia_Btn.Name = "AddMedia_Btn";
            this.AddMedia_Btn.Size = new System.Drawing.Size(50, 50);
            this.AddMedia_Btn.TabIndex = 0;
            this.AddMedia_Btn.UseVisualStyleBackColor = true;
            this.AddMedia_Btn.Click += new System.EventHandler(this.AddMedia_Btn_Click);
            // 
            // QuickFFW_Btn
            // 
            this.QuickFFW_Btn.BackgroundImage = global::DECCA2.Properties.Resources.media_btnfastfarword;
            this.QuickFFW_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.QuickFFW_Btn.FlatAppearance.BorderSize = 0;
            this.QuickFFW_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.QuickFFW_Btn.ForeColor = System.Drawing.Color.Teal;
            this.QuickFFW_Btn.Location = new System.Drawing.Point(391, 14);
            this.QuickFFW_Btn.Name = "QuickFFW_Btn";
            this.QuickFFW_Btn.Size = new System.Drawing.Size(50, 50);
            this.QuickFFW_Btn.TabIndex = 0;
            this.QuickFFW_Btn.UseVisualStyleBackColor = true;
            this.QuickFFW_Btn.Click += new System.EventHandler(this.QuickFFW_Btn_Click);
            // 
            // Stop_Btn
            // 
            this.Stop_Btn.BackgroundImage = global::DECCA2.Properties.Resources.media_btnstop;
            this.Stop_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Stop_Btn.FlatAppearance.BorderSize = 0;
            this.Stop_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stop_Btn.ForeColor = System.Drawing.Color.Teal;
            this.Stop_Btn.Location = new System.Drawing.Point(273, 14);
            this.Stop_Btn.Name = "Stop_Btn";
            this.Stop_Btn.Size = new System.Drawing.Size(50, 50);
            this.Stop_Btn.TabIndex = 0;
            this.Stop_Btn.UseVisualStyleBackColor = true;
            this.Stop_Btn.Click += new System.EventHandler(this.Stop_Btn_Click);
            // 
            // FFW_Btn
            // 
            this.FFW_Btn.BackgroundImage = global::DECCA2.Properties.Resources.media_next;
            this.FFW_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FFW_Btn.FlatAppearance.BorderSize = 0;
            this.FFW_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FFW_Btn.ForeColor = System.Drawing.Color.Teal;
            this.FFW_Btn.Location = new System.Drawing.Point(332, 14);
            this.FFW_Btn.Name = "FFW_Btn";
            this.FFW_Btn.Size = new System.Drawing.Size(50, 50);
            this.FFW_Btn.TabIndex = 0;
            this.FFW_Btn.UseVisualStyleBackColor = true;
            this.FFW_Btn.Click += new System.EventHandler(this.FFW_Btn_Click);
            // 
            // Pause_Btn
            // 
            this.Pause_Btn.BackgroundImage = global::DECCA2.Properties.Resources.media_btnpause;
            this.Pause_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Pause_Btn.FlatAppearance.BorderSize = 0;
            this.Pause_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pause_Btn.ForeColor = System.Drawing.Color.Teal;
            this.Pause_Btn.Location = new System.Drawing.Point(214, 14);
            this.Pause_Btn.Name = "Pause_Btn";
            this.Pause_Btn.Size = new System.Drawing.Size(50, 50);
            this.Pause_Btn.TabIndex = 0;
            this.Pause_Btn.UseVisualStyleBackColor = true;
            this.Pause_Btn.Click += new System.EventHandler(this.Pause_Btn_Click);
            // 
            // Play_Btn
            // 
            this.Play_Btn.BackgroundImage = global::DECCA2.Properties.Resources.media_btnplay;
            this.Play_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Play_Btn.FlatAppearance.BorderSize = 0;
            this.Play_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Play_Btn.ForeColor = System.Drawing.Color.Teal;
            this.Play_Btn.Location = new System.Drawing.Point(155, 14);
            this.Play_Btn.Name = "Play_Btn";
            this.Play_Btn.Size = new System.Drawing.Size(50, 50);
            this.Play_Btn.TabIndex = 0;
            this.Play_Btn.UseVisualStyleBackColor = true;
            this.Play_Btn.Click += new System.EventHandler(this.Play_Btn_Click);
            // 
            // RWD_Btn
            // 
            this.RWD_Btn.BackgroundImage = global::DECCA2.Properties.Resources.media_btnprevous;
            this.RWD_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RWD_Btn.FlatAppearance.BorderSize = 0;
            this.RWD_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RWD_Btn.ForeColor = System.Drawing.Color.Teal;
            this.RWD_Btn.Location = new System.Drawing.Point(96, 14);
            this.RWD_Btn.Name = "RWD_Btn";
            this.RWD_Btn.Size = new System.Drawing.Size(50, 50);
            this.RWD_Btn.TabIndex = 0;
            this.RWD_Btn.UseVisualStyleBackColor = true;
            this.RWD_Btn.Click += new System.EventHandler(this.RWD_Btn_Click);
            // 
            // FRWD_Btn
            // 
            this.FRWD_Btn.BackgroundImage = global::DECCA2.Properties.Resources.media_btnfastreverse;
            this.FRWD_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FRWD_Btn.FlatAppearance.BorderSize = 0;
            this.FRWD_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FRWD_Btn.ForeColor = System.Drawing.Color.Teal;
            this.FRWD_Btn.Location = new System.Drawing.Point(37, 14);
            this.FRWD_Btn.Name = "FRWD_Btn";
            this.FRWD_Btn.Size = new System.Drawing.Size(50, 50);
            this.FRWD_Btn.TabIndex = 0;
            this.FRWD_Btn.UseVisualStyleBackColor = true;
            this.FRWD_Btn.Click += new System.EventHandler(this.FRWD_Btn_Click);
            // 
            // Playlistlb
            // 
            this.Playlistlb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(9)))));
            this.Playlistlb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Playlistlb.Font = new System.Drawing.Font("Segoe MDL2 Assets", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Playlistlb.ForeColor = System.Drawing.Color.Teal;
            this.Playlistlb.FormattingEnabled = true;
            this.Playlistlb.ItemHeight = 21;
            this.Playlistlb.Location = new System.Drawing.Point(603, 144);
            this.Playlistlb.Name = "Playlistlb";
            this.Playlistlb.Size = new System.Drawing.Size(185, 338);
            this.Playlistlb.TabIndex = 1;
            this.Playlistlb.SelectedIndexChanged += new System.EventHandler(this.Playlistlb_SelectedIndexChanged);
            // 
            // picturebox
            // 
            this.picturebox.Dock = System.Windows.Forms.DockStyle.Top;
            this.picturebox.Image = global::DECCA2.Properties.Resources.Trent_Bas_Music_player;
            this.picturebox.Location = new System.Drawing.Point(0, 0);
            this.picturebox.Name = "picturebox";
            this.picturebox.Size = new System.Drawing.Size(789, 146);
            this.picturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picturebox.TabIndex = 0;
            this.picturebox.TabStop = false;
            this.picturebox.Click += new System.EventHandler(this.picturebox_Click);
            this.picturebox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picturebox_MouseDown);
            this.picturebox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picturebox_MouseMove);
            this.picturebox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picturebox_MouseUp);
            // 
            // DeccaMediaPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 556);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DeccaMediaPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Browse";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Close_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Max_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min_Btn)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DeccaPlayer)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Close_Btn;
        private System.Windows.Forms.PictureBox Max_Btn;
        private System.Windows.Forms.PictureBox Min_Btn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picturebox;
        private System.Windows.Forms.Button Browse_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FilesBtn;
        private System.Windows.Forms.Button ArtistBtn;
        private System.Windows.Forms.Button RadioBtn;
        private System.Windows.Forms.Button AlbumsBtn;
        private System.Windows.Forms.Button LiveTvBtn;
        private System.Windows.Forms.Button SongsBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox Playlistlb;
        private AxWMPLib.AxWindowsMediaPlayer DeccaPlayer;
        private System.Windows.Forms.Button FRWD_Btn;
        private System.Windows.Forms.Button QuickFFW_Btn;
        private System.Windows.Forms.Button FFW_Btn;
        private System.Windows.Forms.Button Pause_Btn;
        private System.Windows.Forms.Button Play_Btn;
        private System.Windows.Forms.Button RWD_Btn;
        private System.Windows.Forms.Button Stop_Btn;
        private System.Windows.Forms.Button AddMedia_Btn;
        private System.Windows.Forms.TrackBar VolumeSpeed;
        private System.Windows.Forms.Button MuteBtn;
        private System.Windows.Forms.Button FullScreenBtn;
        private System.Windows.Forms.Panel panel3;
    }
}