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

            tbUpstreamCommon.Text = parsedResult;

            // parse application payload 
            String payloadStr = upInputStr.Substring(16);
            UpstreamApplicationPayload uap = new UpstreamApplicationPayload(); 
            tbUpstreamApplicationPayload.Text = uap.parseApplicationPayload(uc.upPacketType, payloadStr, uc.versionNumber); 
        }
    }
}
