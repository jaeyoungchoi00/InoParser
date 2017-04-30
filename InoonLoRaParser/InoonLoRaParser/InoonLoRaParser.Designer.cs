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
            this.gbUpstream = new System.Windows.Forms.GroupBox();
            this.btnUpstreamConvert = new System.Windows.Forms.Button();
            this.tbUpstreamInput = new System.Windows.Forms.TextBox();
            this.lbInput = new System.Windows.Forms.Label();
            this.gbContents = new System.Windows.Forms.GroupBox();
            this.lbCommon = new System.Windows.Forms.Label();
            this.tbUpstreamCommon = new System.Windows.Forms.TextBox();
            this.lbApplicationPayload = new System.Windows.Forms.Label();
            this.tbUpstreamApplicationPayload = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDownstreamCommand = new System.Windows.Forms.TextBox();
            this.gbDownstream = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDownstreamInput = new System.Windows.Forms.TextBox();
            this.btnDownstreamConvert = new System.Windows.Forms.Button();
            this.gbUpstream.SuspendLayout();
            this.gbContents.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbDownstream.SuspendLayout();
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
            // btnUpstreamConvert
            // 
            this.btnUpstreamConvert.Location = new System.Drawing.Point(187, 31);
            this.btnUpstreamConvert.Name = "btnUpstreamConvert";
            this.btnUpstreamConvert.Size = new System.Drawing.Size(75, 23);
            this.btnUpstreamConvert.TabIndex = 1;
            this.btnUpstreamConvert.Text = "Convert";
            this.btnUpstreamConvert.UseVisualStyleBackColor = true;
            this.btnUpstreamConvert.Click += new System.EventHandler(this.btnUpstreamConvert_Click);
            // 
            // tbUpstreamInput
            // 
            this.tbUpstreamInput.Location = new System.Drawing.Point(10, 33);
            this.tbUpstreamInput.Name = "tbUpstreamInput";
            this.tbUpstreamInput.Size = new System.Drawing.Size(171, 21);
            this.tbUpstreamInput.TabIndex = 0;
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
            // gbContents
            // 
            this.gbContents.Controls.Add(this.lbApplicationPayload);
            this.gbContents.Controls.Add(this.tbUpstreamApplicationPayload);
            this.gbContents.Controls.Add(this.lbCommon);
            this.gbContents.Controls.Add(this.tbUpstreamCommon);
            this.gbContents.Location = new System.Drawing.Point(13, 83);
            this.gbContents.Name = "gbContents";
            this.gbContents.Size = new System.Drawing.Size(271, 310);
            this.gbContents.TabIndex = 1;
            this.gbContents.TabStop = false;
            this.gbContents.Text = "Contents";
            // 
            // lbCommon
            // 
            this.lbCommon.AutoSize = true;
            this.lbCommon.Location = new System.Drawing.Point(13, 26);
            this.lbCommon.Name = "lbCommon";
            this.lbCommon.Size = new System.Drawing.Size(57, 12);
            this.lbCommon.TabIndex = 4;
            this.lbCommon.Text = "Common";
            // 
            // tbUpstreamCommon
            // 
            this.tbUpstreamCommon.Location = new System.Drawing.Point(12, 42);
            this.tbUpstreamCommon.Multiline = true;
            this.tbUpstreamCommon.Name = "tbUpstreamCommon";
            this.tbUpstreamCommon.ReadOnly = true;
            this.tbUpstreamCommon.Size = new System.Drawing.Size(250, 110);
            this.tbUpstreamCommon.TabIndex = 3;
            // 
            // lbApplicationPayload
            // 
            this.lbApplicationPayload.AutoSize = true;
            this.lbApplicationPayload.Location = new System.Drawing.Point(11, 165);
            this.lbApplicationPayload.Name = "lbApplicationPayload";
            this.lbApplicationPayload.Size = new System.Drawing.Size(117, 12);
            this.lbApplicationPayload.TabIndex = 6;
            this.lbApplicationPayload.Text = "Application Payload";
            // 
            // tbUpstreamApplicationPayload
            // 
            this.tbUpstreamApplicationPayload.Location = new System.Drawing.Point(10, 181);
            this.tbUpstreamApplicationPayload.Multiline = true;
            this.tbUpstreamApplicationPayload.Name = "tbUpstreamApplicationPayload";
            this.tbUpstreamApplicationPayload.ReadOnly = true;
            this.tbUpstreamApplicationPayload.Size = new System.Drawing.Size(250, 110);
            this.tbUpstreamApplicationPayload.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbDownstreamCommand);
            this.groupBox1.Location = new System.Drawing.Point(299, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 217);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contents";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Command";
            // 
            // tbDownstreamCommand
            // 
            this.tbDownstreamCommand.Location = new System.Drawing.Point(12, 42);
            this.tbDownstreamCommand.Multiline = true;
            this.tbDownstreamCommand.Name = "tbDownstreamCommand";
            this.tbDownstreamCommand.ReadOnly = true;
            this.tbDownstreamCommand.Size = new System.Drawing.Size(332, 162);
            this.tbDownstreamCommand.TabIndex = 3;
            // 
            // gbDownstream
            // 
            this.gbDownstream.Controls.Add(this.label3);
            this.gbDownstream.Controls.Add(this.tbDownstreamInput);
            this.gbDownstream.Controls.Add(this.btnDownstreamConvert);
            this.gbDownstream.Location = new System.Drawing.Point(299, 12);
            this.gbDownstream.Name = "gbDownstream";
            this.gbDownstream.Size = new System.Drawing.Size(354, 63);
            this.gbDownstream.TabIndex = 3;
            this.gbDownstream.TabStop = false;
            this.gbDownstream.Text = "Downstream";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Input";
            // 
            // tbDownstreamInput
            // 
            this.tbDownstreamInput.Location = new System.Drawing.Point(10, 33);
            this.tbDownstreamInput.Name = "tbDownstreamInput";
            this.tbDownstreamInput.Size = new System.Drawing.Size(240, 21);
            this.tbDownstreamInput.TabIndex = 1;
            // 
            // btnDownstreamConvert
            // 
            this.btnDownstreamConvert.Location = new System.Drawing.Point(269, 31);
            this.btnDownstreamConvert.Name = "btnDownstreamConvert";
            this.btnDownstreamConvert.Size = new System.Drawing.Size(75, 23);
            this.btnDownstreamConvert.TabIndex = 0;
            this.btnDownstreamConvert.Text = "Convert";
            this.btnDownstreamConvert.UseVisualStyleBackColor = true;
            // 
            // InoonLoRaParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 406);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbDownstream);
            this.Controls.Add(this.gbContents);
            this.Controls.Add(this.gbUpstream);
            this.Name = "InoonLoRaParser";
            this.Text = "Ino-on LoRa Parser";
            this.gbUpstream.ResumeLayout(false);
            this.gbUpstream.PerformLayout();
            this.gbContents.ResumeLayout(false);
            this.gbContents.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbDownstream.ResumeLayout(false);
            this.gbDownstream.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDownstreamCommand;
        private System.Windows.Forms.GroupBox gbDownstream;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDownstreamInput;
        private System.Windows.Forms.Button btnDownstreamConvert;
    }
}

