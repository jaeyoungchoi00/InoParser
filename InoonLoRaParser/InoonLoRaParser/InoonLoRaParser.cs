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
using System.IO;

namespace InoonLoRaParser
{
    public partial class InoonLoRaParser : Form
    {

        public const int CommonPayloadLengthVer2 = 16;
        public const int CommonPayloadLengthVer3 = 20;

        //public enum CommonPacketStartIndex { cpVer = 0, cpResv = 2, cpPacketID = 4, cpDevType = 6, cpPacketType = 10, cpReqType = 11, cpBatt = 12, cpTemp = 14};

        public InoonLoRaParser()
        {
            InitializeComponent();
        }

        private void btnUpstreamConvert_Click(object sender, EventArgs e)
        {
            String upInputStr = tbUpstreamInput.Text;
            upInputStr = upInputStr.Trim();
            tbUpstreamInput.Text = upInputStr;
            int headerLength = CommonPayloadLengthVer2; 

            //parse common 
            UpstreamCommon uc = new UpstreamCommon();
            String parsedResult = uc.parseUpstreamCommon(upInputStr);
            
            // parse application payload 
            if (false == String.IsNullOrEmpty(parsedResult))
            {
                tbUpstreamCommon.Text = parsedResult;
                                
                headerLength = getHeaderLength(uc.versionNumber); 
                
                String payloadStr = upInputStr.Substring(headerLength);
                UpstreamApplicationPayload uap = new UpstreamApplicationPayload();
                tbUpstreamApplicationPayload.Text = uap.parseApplicationPayload(uc.upPacketType, payloadStr, uc.versionNumber);

                // Checksum 
                String fcsResult = compareFCS(upInputStr, uc.versionNumber);
                tbUpstreamApplicationPayload.Text += fcsResult; 
            }
            else
            {
                tbUpstreamCommon.Text = "Invalid input";
            }            
        }

        private String compareFCS(String frame, int version)
        {
            String fcsResult = String.Empty;
            int length = frame.Length; 

            if (version < 3)
            {
                fcsResult = "No FCS\n";
            }
            else
            {
                if (length < 2 )
                {
                    fcsResult = "Frame too short. " + length.ToString() + "B\n";
                }
                else
                {
                    String validFrame = frame.Substring(0, length - 2);
                    String fcs = frame.Substring(length - 2, 2);

                    int fcsInPacket = Convert.ToInt32(fcs, 16);
                    int fcsCalc = calculateChecksum(validFrame);

                    if (fcsInPacket == fcsCalc)
                    {
                        fcsResult = "Checksum OK\n";
                    }
                    else
                    {
                        fcsResult = "Checksum FAIL\n";
                    }
                }
            }

            return fcsResult; 
        }

        private int calculateChecksum(String dataToCalculate)
        {
            /*
            byte[] byteToCalculate = Encoding.ASCII.GetBytes(dataToCalculate);
            int checksum = 0;
            foreach (byte chData in byteToCalculate)
            {
                checksum += chData;
            }
            checksum &= 0xff;
            //return checksum.ToString("X2");
            return checksum;
            */
            
            int checksum = 0;
            
            // You'll need to add error checking that the string only contains [0-9A-F], 
            // is an even number of characters, etc.
            for (int i = 0; i < dataToCalculate.Length; i += 2)
            {
                int value = int.Parse(dataToCalculate.Substring(i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                checksum = (checksum + value) & 0xFF;
            }
            return checksum; 
        }

        private int getHeaderLength(int versionNumer)
        {
            int res = CommonPayloadLengthVer2; 
            if (versionNumer < 3)
                res = CommonPayloadLengthVer2;
            else
                res = CommonPayloadLengthVer3;

            return res; 
        }

        private void tbUpstreamInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnUpstreamConvert_Click(sender, e);
            
        }

        private void btnOpenAliveMsgFile_Click(object sender, EventArgs e)
        {

            int CommonPayloadLengthVer2 = 16;
            int AlivePayloadLength = 50;
            int packetTypeIndex = 10;

            int data_size = 4;
            int index_X = CommonPayloadLengthVer2 + 0;
            int index_Y = CommonPayloadLengthVer2 + data_size;
            int index_Z = CommonPayloadLengthVer2 + data_size + data_size;

            string xval, yval, zval;

            double xconv = Double.NaN, yconv = Double.NaN, zconv = Double.NaN;

            List<double> xlist = new List<double>();
            List<double> ylist = new List<double>();
            List<double> zlist = new List<double>();

            StringBuilder sb = new StringBuilder();
            
            sb.Append("X\tY\tZ");
            sb.AppendLine();

            //Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            // string separator 
            char[] delimiterChars = { ' ', '\t' };
            string[] words;
            string alivemsg; 


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    string fileName = openFileDialog1.FileName; 
                    foreach (string line in File.ReadLines(fileName))
                    {
                        
                        words = line.Split(delimiterChars);
                        alivemsg = words.Last(); 

                        if (alivemsg.Length == (CommonPayloadLengthVer2 + AlivePayloadLength) && (alivemsg.ElementAt(packetTypeIndex - 1).Equals('2')))
                        {
                            xval = alivemsg.Substring(index_X, data_size); 
                            yval = alivemsg.Substring(index_Y, data_size);
                            zval = alivemsg.Substring(index_Z, data_size);

                            xconv = convertSensorData(xval); 
                            sb.AppendFormat("{0:N1}\t", xconv);
                            xlist.Add(xconv);

                            yconv = convertSensorData(yval);
                            sb.AppendFormat("{0:N1}\t", yconv);
                            ylist.Add(yconv);

                            zconv = convertSensorData(zval);
                            sb.AppendFormat("{0:N1}", zconv);
                            zlist.Add(zconv); 

                            sb.AppendLine();                           

                        }
                        else
                        {                            
                            sb.AppendFormat("Parsing Error: {0}", line);
                            sb.AppendLine();
                        }
                    }

                    ///////////////////////////////////////
                    // Parse OK. Write result file 
                    ///////////////////////////////////////

                    // delete old file 
                    string resultFileName = fileName.Replace(".txt", "_s.txt");
                    if (File.Exists(resultFileName))
                    {
                        File.Delete(resultFileName);
                    }
                        
                    // create result file 
                    try
                    {
                        File.WriteAllText(resultFileName, sb.ToString());

                        StringBuilder sbp2p = new StringBuilder();
                        
                        sbp2p.AppendFormat("{0:N1}, ", xlist.Max() - xlist.Min());
                        sbp2p.AppendFormat("{0:N1}, ", ylist.Max() - ylist.Min());
                        sbp2p.AppendFormat("{0:N1}", zlist.Max() - zlist.Min());

                        tbSensorData.Text = sbp2p.ToString();
                        
                        MessageBox.Show("Write success. " + resultFileName); 
                    }
                    catch(PathTooLongException)
                    {
                        MessageBox.Show("Path too long. " + resultFileName);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("File write exception. " + ex.Message);
                    }                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        // Convert string sensor data to double value 
        private Double convertSensorData(string org)
        {
            
            Double scale = UpstreamApplicationPayload.accScale; // 3.91 mg for 2G range. 7.81 for 4G range   
            int sensorData = Convert.ToInt16(org, 16);
            Double convData = scale * (Double)sensorData;
            return convData;            

        }
    }
}
