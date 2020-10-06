namespace ScreenShotExplorer
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.정렬ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.크기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.날짜ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.이름ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.최종수정일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.새로고침ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.아이콘크기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.크게ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.중간ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.작게ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel2.Controls.Add(this.menuStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(1072, 718);
            this.splitContainer1.SplitterDistance = 727;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(727, 718);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(341, 690);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.정렬ToolStripMenuItem,
            this.새로고침ToolStripMenuItem,
            this.아이콘크기ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(341, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 정렬ToolStripMenuItem
            // 
            this.정렬ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.크기ToolStripMenuItem,
            this.날짜ToolStripMenuItem,
            this.이름ToolStripMenuItem,
            this.최종수정일ToolStripMenuItem});
            this.정렬ToolStripMenuItem.Name = "정렬ToolStripMenuItem";
            this.정렬ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.정렬ToolStripMenuItem.Text = "정렬";
            // 
            // 크기ToolStripMenuItem
            // 
            this.크기ToolStripMenuItem.Name = "크기ToolStripMenuItem";
            this.크기ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.크기ToolStripMenuItem.Text = "크기";
            this.크기ToolStripMenuItem.Click += new System.EventHandler(this.크기ToolStripMenuItem_Click);
            // 
            // 날짜ToolStripMenuItem
            // 
            this.날짜ToolStripMenuItem.Name = "날짜ToolStripMenuItem";
            this.날짜ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.날짜ToolStripMenuItem.Text = "날짜";
            this.날짜ToolStripMenuItem.Click += new System.EventHandler(this.날짜ToolStripMenuItem_Click);
            // 
            // 이름ToolStripMenuItem
            // 
            this.이름ToolStripMenuItem.Name = "이름ToolStripMenuItem";
            this.이름ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.이름ToolStripMenuItem.Text = "이름";
            this.이름ToolStripMenuItem.Click += new System.EventHandler(this.이름ToolStripMenuItem_Click);
            // 
            // 최종수정일ToolStripMenuItem
            // 
            this.최종수정일ToolStripMenuItem.Name = "최종수정일ToolStripMenuItem";
            this.최종수정일ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.최종수정일ToolStripMenuItem.Text = "최종수정일";
            this.최종수정일ToolStripMenuItem.Click += new System.EventHandler(this.최종수정일ToolStripMenuItem_Click);
            // 
            // 새로고침ToolStripMenuItem
            // 
            this.새로고침ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("새로고침ToolStripMenuItem.Image")));
            this.새로고침ToolStripMenuItem.Name = "새로고침ToolStripMenuItem";
            this.새로고침ToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.새로고침ToolStripMenuItem.Text = "새로고침";
            this.새로고침ToolStripMenuItem.Click += new System.EventHandler(this.새로고침ToolStripMenuItem_Click);
            // 
            // 아이콘크기ToolStripMenuItem
            // 
            this.아이콘크기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.크게ToolStripMenuItem,
            this.중간ToolStripMenuItem,
            this.작게ToolStripMenuItem});
            this.아이콘크기ToolStripMenuItem.Name = "아이콘크기ToolStripMenuItem";
            this.아이콘크기ToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.아이콘크기ToolStripMenuItem.Text = "아이콘크기";
            // 
            // 크게ToolStripMenuItem
            // 
            this.크게ToolStripMenuItem.Name = "크게ToolStripMenuItem";
            this.크게ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.크게ToolStripMenuItem.Text = "크게";
            this.크게ToolStripMenuItem.Click += new System.EventHandler(this.크게ToolStripMenuItem_Click);
            // 
            // 중간ToolStripMenuItem
            // 
            this.중간ToolStripMenuItem.Name = "중간ToolStripMenuItem";
            this.중간ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.중간ToolStripMenuItem.Text = "중간";
            this.중간ToolStripMenuItem.Click += new System.EventHandler(this.중간ToolStripMenuItem_Click);
            // 
            // 작게ToolStripMenuItem
            // 
            this.작게ToolStripMenuItem.Name = "작게ToolStripMenuItem";
            this.작게ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.작게ToolStripMenuItem.Text = "작게";
            this.작게ToolStripMenuItem.Click += new System.EventHandler(this.작게ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 718);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "ScreenShotExplorer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 정렬ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 크기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 날짜ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 이름ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 최종수정일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 새로고침ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 아이콘크기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 크게ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 중간ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 작게ToolStripMenuItem;
    }
}

