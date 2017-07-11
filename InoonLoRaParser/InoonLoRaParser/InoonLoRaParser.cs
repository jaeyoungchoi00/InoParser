using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InoonLoRaParser.Upstream;


namespace InoonLoRaParser
{
    public partial class InoonLoRaParser : Form
    {
        public InoonLoRaParser()
        {
            InitializeComponent();
        }

        private void btnUpstreamConvert_Click(object sender, EventArgs e)
        {
            String upInputStr = tbUpstreamInput.Text;
            upInputStr = upInputStr.Trim();
            tbUpstreamInput.Text = upInputStr;

            //parse common 
            UpstreamCommon uc = new UpstreamCommon();
            String parsedResult = uc.parseUpstreamCommon(upInputStr);
            
            // parse application payload 
            if (false == String.IsNullOrEmpty(parsedResult))
            {
                tbUpstreamCommon.Text = parsedResult;
                String payloadStr = upInputStr.Substring(16);
                UpstreamApplicationPayload uap = new UpstreamApplicationPayload();
                tbUpstreamApplicationPayload.Text = uap.parseApplicationPayload(uc.upPacketType, payloadStr, uc.versionNumber);
            }
            else
            {
                tbUpstreamCommon.Text = "Invalid input";
            }            
        }

        private void tbUpstreamInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnUpstreamConvert_Click(sender, e);
            
        }
    }
}
