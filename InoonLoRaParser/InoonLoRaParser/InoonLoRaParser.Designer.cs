namespace InoonLoRaParser
{
    partial class InoonLoRaParser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InoonLoRaParser));
            this.gbUpstream = new System.Windows.Forms.GroupBox();
            this.lbInput = new System.Windows.Forms.Label();
            this.tbUpstreamInput = new System.Windows.Forms.TextBox();
            this.btnUpstreamConvert = new System.Windows.Forms.Button();
            this.gbContents = new System.Windows.Forms.GroupBox();
            this.lbApplicationPayload = new System.Windows.Forms.Label();
            this.tbUpstreamApplicationPayload = new System.Windows.Forms.TextBox();
            this.lbCommon = new System.Windows.Forms.Label();
            this.tbUpstreamCommon = new System.Windows.Forms.TextBox();
            this.gbAliveXYZ = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSensorData = new System.Windows.Forms.TextBox();
            this.btnOpenAliveMsgFile = new System.Windows.Forms.Button();
            this.gbUpstream.SuspendLayout();
            this.gbContents.SuspendLayout();
            this.gbAliveXYZ.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUpstream
            // 
            this.gbUpstream.Controls.Add(this.lbInput);
            this.gbUpstream.Controls.Add(this.tbUpstreamInput);
            this.gbUpstream.Controls.Add(this.btnUpstreamConvert);
            this.gbUpstream.Location = new System.Drawing.Point(13, 13);
            this.gbUpstream.Name = "gbUpstream";
            this.gbUpstream.Size = new System.Drawing.Size(271, 63);
            this.gbUpstream.TabIndex = 0;
            this.gbUpstream.TabStop = false;
            this.gbUpstream.Text = "Upstream";
            // 
            // lbInput
            // 
            this.lbInput.AutoSize = true;
            this.lbInput.Location = new System.Drawing.Point(11, 17);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(32, 12);
            this.lbInput.TabIndex = 2;
            this.lbInput.Text = "Input";
            // 
            // tbUpstreamInput
            // 
            this.tbUpstreamInput.Location = new System.Drawing.Point(10, 33);
            this.tbUpstreamInput.Name = "tbUpstreamInput";
            this.tbUpstreamInput.Size = new System.Drawing.Size(171, 21);
            this.tbUpstreamInput.TabIndex = 0;
            this.tbUpstreamInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbUpstreamInput_KeyUp);
            // 
            // btnUpstreamConvert
            // 
            this.btnUpstreamConvert.Location = new System.Drawing.Point(185, 31);
            this.btnUpstreamConvert.Name = "btnUpstreamConvert";
            this.btnUpstreamConvert.Size = new System.Drawing.Size(75, 23);
            this.btnUpstreamConvert.TabIndex = 1;
            this.btnUpstreamConvert.Text = "Convert";
            this.btnUpstreamConvert.UseVisualStyleBackColor = true;
            this.btnUpstreamConvert.Click += new System.EventHandler(this.btnUpstreamConvert_Click);
            // 
            // gbContents
            // 
            this.gbContents.Controls.Add(this.lbApplicationPayload);
            this.gbContents.Controls.Add(this.tbUpstreamApplicationPayload);
            this.gbContents.Controls.Add(this.lbCommon);
            this.gbContents.Controls.Add(this.tbUpstreamCommon);
            this.gbContents.Location = new System.Drawing.Point(13, 83);
            this.gbContents.Name = "gbContents";
            this.gbContents.Size = new System.Drawing.Size(271, 483);
            this.gbContents.TabIndex = 1;
            this.gbContents.TabStop = false;
            this.gbContents.Text = "Contents";
            // 
            // lbApplicationPayload
            // 
            this.lbApplicationPayload.AutoSize = true;
            this.lbApplicationPayload.Location = new System.Drawing.Point(11, 187);
            this.lbApplicationPayload.Name = "lbApplicationPayload";
            this.lbApplicationPayload.Size = new System.Drawing.Size(117, 12);
            this.lbApplicationPayload.TabIndex = 6;
            this.lbApplicationPayload.Text = "Application Payload";
            // 
            // tbUpstreamApplicationPayload
            // 
            this.tbUpstreamApplicationPayload.Location = new System.Drawing.Point(10, 202);
            this.tbUpstreamApplicationPayload.Multiline = true;
            this.tbUpstreamApplicationPayload.Name = "tbUpstreamApplicationPayload";
            this.tbUpstreamApplicationPayload.ReadOnly = true;
            this.tbUpstreamApplicationPayload.Size = new System.Drawing.Size(250, 270);
            this.tbUpstreamApplicationPayload.TabIndex = 5;
            // 
            // lbCommon
            // 
            this.lbCommon.AutoSize = true;
            this.lbCommon.Location = new System.Drawing.Point(13, 21);
            this.lbCommon.Name = "lbCommon";
            this.lbCommon.Size = new System.Drawing.Size(57, 12);
            this.lbCommon.TabIndex = 4;
            this.lbCommon.Text = "Common";
            // 
            // tbUpstreamCommon
            // 
            this.tbUpstreamCommon.Location = new System.Drawing.Point(10, 36);
            this.tbUpstreamCommon.Multiline = true;
            this.tbUpstreamCommon.Name = "tbUpstreamCommon";
            this.tbUpstreamCommon.ReadOnly = true;
            this.tbUpstreamCommon.Size = new System.Drawing.Size(250, 143);
            this.tbUpstreamCommon.TabIndex = 3;
            // 
            // gbAliveXYZ
            // 
            this.gbAliveXYZ.Controls.Add(this.label1);
            this.gbAliveXYZ.Controls.Add(this.tbSensorData);
            this.gbAliveXYZ.Controls.Add(this.btnOpenAliveMsgFile);
            this.gbAliveXYZ.Location = new System.Drawing.Point(13, 579);
            this.gbAliveXYZ.Name = "gbAliveXYZ";
            this.gbAliveXYZ.Size = new System.Drawing.Size(269, 56);
            this.gbAliveXYZ.TabIndex = 2;
            this.gbAliveXYZ.TabStop = false;
            this.gbAliveXYZ.Text = "XYZ from Alive messages";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "P2P";
            // 
            // tbSensorData
            // 
            this.tbSensorData.Location = new System.Drawing.Point(125, 22);
            this.tbSensorData.Name = "tbSensorData";
            this.tbSensorData.Size = new System.Drawing.Size(135, 21);
            this.tbSensorData.TabIndex = 1;
            // 
            // btnOpenAliveMsgFile
            // 
            this.btnOpenAliveMsgFile.Location = new System.Drawing.Point(10, 20);
            this.btnOpenAliveMsgFile.Name = "btnOpenAliveMsgFile";
            this.btnOpenAliveMsgFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenAliveMsgFile.TabIndex = 0;
            this.btnOpenAliveMsgFile.Text = "Open";
            this.btnOpenAliveMsgFile.UseVisualStyleBackColor = true;
            this.btnOpenAliveMsgFile.Click += new System.EventHandler(this.btnOpenAliveMsgFile_Click);
            // 
            // InoonLoRaParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 641);
            this.Controls.Add(this.gbAliveXYZ);
            this.Controls.Add(this.gbContents);
            this.Controls.Add(this.gbUpstream);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InoonLoRaParser";
            this.Text = "Ino-on LoRa Parser";
            this.gbUpstream.ResumeLayout(false);
            this.gbUpstream.PerformLayout();
            this.gbContents.ResumeLayout(false);
            this.gbContents.PerformLayout();
            this.gbAliveXYZ.ResumeLayout(false);
            this.gbAliveXYZ.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUpstream;
        private System.Windows.Forms.Label lbInput;
        private System.Windows.Forms.TextBox tbUpstreamInput;
        private System.Windows.Forms.Button btnUpstreamConvert;
        private System.Windows.Forms.GroupBox gbContents;
        private System.Windows.Forms.Label lbApplicationPayload;
        private System.Windows.Forms.TextBox tbUpstreamApplicationPayload;
        private System.Windows.Forms.Label lbCommon;
        private System.Windows.Forms.TextBox tbUpstreamCommon;
        private System.Windows.Forms.GroupBox gbAliveXYZ;
        private System.Windows.Forms.TextBox tbSensorData;
        private System.Windows.Forms.Button btnOpenAliveMsgFile;
        private System.Windows.Forms.Label label1;
    }
}

