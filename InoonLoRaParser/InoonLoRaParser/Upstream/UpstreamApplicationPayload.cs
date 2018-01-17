using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace InoonLoRaParser.Upstream
{
    public class UpstreamApplicationPayload
    {

        public const Double accScale = 3.91; //mg for 2G range. 7.81 for 4G range   
        private const int aliveLengthVer1 = 14;
        private const int aliveLengthVer2 = 50;
        private const int aliveLengthVer3 = 50;
        private const int eventLengthVer1 = 6;
        private const int eventLengthVer2 = 10;

        //parse application payload 
        public string parseApplicationPayload(UpstreamCommon.UpPacketType pktType, string pktStr, int version)
        {
            string res = String.Empty;

            if (pktType == UpstreamCommon.UpPacketType.Alive)
                res = parseAlive(pktStr, version);
            else if (pktType == UpstreamCommon.UpPacketType.Event)
                res = parseEvent(pktStr, version);
            else if (pktType == UpstreamCommon.UpPacketType.Error)
                res = parseError(pktStr, version);
            else if (pktType == UpstreamCommon.UpPacketType.Ack)
                res = parseAck(pktStr, version);
            else if (pktType == UpstreamCommon.UpPacketType.Notice)
                res = parseNotice(pktStr, version);
            else if (pktType == UpstreamCommon.UpPacketType.DataLog)
                res = parseDataLog(pktStr, version);
            else
                res = "Unknown packet";


            return res; 
        }

        public string parseAlive(string payload, int version)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            String subStr;
            int len = 2;
            int strLen = 0; 

            sb.Append("Alive");
            sb.AppendLine();
            
            // Trim whitespace
            payload = payload.Trim();

            strLen = payload.Length;

            if ((1 == version) && (aliveLengthVer1 == strLen))
            {

                // X axis 
                len = 4;
                subStr = payload.Substring(index, len);
                int xaxis = Convert.ToInt16(subStr, 16);
                Double xconv = accScale * (Double)xaxis;
                sb.AppendFormat("X axis: {0:N1} mg", xconv);
                sb.AppendLine();
                index += len;


                // Y axis 
                len = 4;
                subStr = payload.Substring(index, len);
                int yaxis = Convert.ToInt16(subStr, 16);
                Double yconv = accScale * (Double)yaxis;
                sb.AppendFormat("Y axis: {0:N1} mg", yconv);
                sb.AppendLine();
                index += len;

                // Z axis 
                len = 4;
                subStr = payload.Substring(index, len);
                int zaxis = Convert.ToInt16(subStr, 16);
                Double zconv = accScale * (Double)zaxis;
                sb.AppendFormat("Z axis: {0:N1} mg", zconv);
                sb.AppendLine();
                index += len;

                // RSSI
                len = 2;
                subStr = payload.Substring(index, len);
                int rssi = Convert.ToInt16(subStr, 16);
                rssi = convertSignedByteToInt(rssi);
                sb.AppendFormat("RSSI: {0} ", rssi);
                sb.AppendLine();
                index += len;

            }            
            else if ((2 == version) && (aliveLengthVer2 == strLen))// version 2 
            {
                // X axis 
                len = 4;
                subStr = payload.Substring(index, len);
                int xaxis = Convert.ToInt16(subStr, 16);
                Double xconv = accScale * (Double)xaxis;
                sb.AppendFormat("X axis: {0:N1} mg", xconv);
                sb.AppendLine();
                index += len;


                // Y axis 
                len = 4;
                subStr = payload.Substring(index, len);
                int yaxis = Convert.ToInt16(subStr, 16);
                Double yconv = accScale * (Double)yaxis;
                sb.AppendFormat("Y axis: {0:N1} mg", yconv);
                sb.AppendLine();
                index += len;

                // Z axis 
                len = 4;
                subStr = payload.Substring(index, len);
                int zaxis = Convert.ToInt16(subStr, 16);
                Double zconv = accScale * (Double)zaxis;
                sb.AppendFormat("Z axis: {0:N1} mg", zconv);
                sb.AppendLine();
                index += len;


                // Alive Period  (min) 
                len = 4;
                subStr = payload.Substring(index, len);
                int alivePeriod = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("Alive period: {0} min", alivePeriod);
                sb.AppendLine();
                index += len;

                
                // Acc Config -- Sensitivity level 
                len = 2;
                subStr = payload.Substring(index, len);
                int sensitivityLevel = Convert.ToInt16(subStr, 16);
                int sensitivityGrav = 1; 
                for (int i = 0; i < sensitivityLevel; i++)
                {
                    sensitivityGrav *= 2; 
                }
                sb.AppendFormat("Sensitivity: {0} G ", sensitivityGrav.ToString());
                sb.AppendLine();
                index += len;

                // Acc Config -- Sensitivity Threshlod 
                len = 4;
                subStr = payload.Substring(index, len);
                int sensitivityThesh = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("Sensitivity Threshlod : {0} mg", sensitivityThesh.ToString());
                sb.AppendLine();
                index += len;

                // Acc Interrupt 
                
                // Acc Interrupt No 
                len = 2;
                subStr = payload.Substring(index, len);
                int accIntrNum = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("Acc Interrupt No: {0} ", accIntrNum);
                sb.AppendLine();
                index += len;

                // Skip reserved field 
                len = 2;
                index += len;

                // Acc Interrupt :  Data field 
                len = 2;
                sb.Append("Acc Interrupt enabled axis : ");
                subStr = payload.Substring(index, len);

                //SByte accByte = Convert.ToSByte(subStr);
                Byte accByte = byte.Parse(subStr, NumberStyles.HexNumber, CultureInfo.InvariantCulture);

                string bitFlag = Convert.ToString(accByte, 2).PadLeft(8, '0'); // 00010001

                var chars = bitFlag.ToCharArray();
                
                if (chars[5].Equals('1'))
                    sb.Append("X");

                if (chars[6].Equals('1'))
                    sb.Append("Y");

                if (chars[7].Equals('1'))
                    sb.Append("Z");

                sb.AppendLine();
                index += len;


                // Data log enable 
                len = 2;
                subStr = payload.Substring(index, len);
                int logEnable = Convert.ToInt16(subStr, 16);
                
                if (logEnable == 0)
                {
                    sb.Append("Acc Data Log Disabled. ");
                }
                else
                {
                    sb.Append("Acc Data Log Enabled. ");
                }
                                
                sb.AppendLine();
                index += len;

                // Data log Interval  
                len = 2;
                subStr = payload.Substring(index, len);
                int logInterval = Convert.ToInt16(subStr, 16);
                logInterval = 10 * logInterval; // predefined spec. 
                sb.AppendFormat("Acc Interval : {0} ms ", logInterval.ToString());

                sb.AppendLine();
                index += len;

                // Data log :  No of Biocks 
                len = 2;
                subStr = payload.Substring(index, len);
                int numBlocks = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("Acc Blocks : {0} ", numBlocks.ToString());

                sb.AppendLine();
                index += len;

                // Setup message : installed or not 
                len = 2;
                subStr = payload.Substring(index, len);
                int installStatus = Convert.ToInt16(subStr, 16);

                if (installStatus == 0)
                {
                    sb.Append("Uninstalled ");
                }
                else
                {
                    sb.Append("Installed ");
                }

                sb.AppendLine();
                index += len;


                // FW Version 
                // Major 
                len = 2;
                subStr = payload.Substring(index, len);
                int fwMajor = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("FW version: {0} ", fwMajor.ToString("00"));
                //sb.AppendLine();
                index += len;

                // Minor 
                len = 2;
                subStr = payload.Substring(index, len);
                int fwMinor = Convert.ToInt16(subStr, 16);
                sb.AppendFormat(" {0} ", fwMinor.ToString("00"));
                //sb.AppendLine();
                index += len;

                // Revision 
                len = 2;
                subStr = payload.Substring(index, len);
                int fwRev = Convert.ToInt16(subStr, 16);
                sb.AppendFormat(" {0} ", fwRev.ToString("00"));
                sb.AppendLine();
                index += len;


                // LoRa module FW Version (SoluM)
                // Major 
                len = 2;
                subStr = payload.Substring(index, len);
                fwMajor = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("LoRa Module FW version: {0} ", fwMajor.ToString("00"));
                //sb.AppendLine();
                index += len;

                // Minor 
                len = 2;
                subStr = payload.Substring(index, len);
                fwMinor = Convert.ToInt16(subStr, 16);
                sb.AppendFormat(" {0} ", fwMinor.ToString("00"));
                //sb.AppendLine();
                index += len;

                // Revision 
                len = 2;
                subStr = payload.Substring(index, len);
                fwRev = Convert.ToInt16(subStr, 16);
                sb.AppendFormat(" {0} ", fwRev.ToString("00"));
                sb.AppendLine();
                index += len;


                // RSSI
                len = 2;
                subStr = payload.Substring(index, len);
                int rssi = Convert.ToInt16(subStr, 16);
                rssi = convertSignedByteToInt(rssi);
                sb.AppendFormat("RSSI: {0} ", rssi);

                sb.AppendLine();
                index += len;
                

            }
            else if ((3 == version) && (aliveLengthVer3 == strLen))// version 2 
            {
                // X axis 
                len = 4;
                subStr = payload.Substring(index, len);
                int xaxis = Convert.ToInt16(subStr, 16);
                Double xconv = accScale * (Double)xaxis;
                sb.AppendFormat("X axis: {0:N1} mg", xconv);
                sb.AppendLine();
                index += len;


                // Y axis 
                len = 4;
                subStr = payload.Substring(index, len);
                int yaxis = Convert.ToInt16(subStr, 16);
                Double yconv = accScale * (Double)yaxis;
                sb.AppendFormat("Y axis: {0:N1} mg", yconv);
                sb.AppendLine();
                index += len;

                // Z axis 
                len = 4;
                subStr = payload.Substring(index, len);
                int zaxis = Convert.ToInt16(subStr, 16);
                Double zconv = accScale * (Double)zaxis;
                sb.AppendFormat("Z axis: {0:N1} mg", zconv);
                sb.AppendLine();
                index += len;


                // Alive Period  (min) 
                len = 4;
                subStr = payload.Substring(index, len);
                int alivePeriod = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("Alive period: {0} min", alivePeriod);
                sb.AppendLine();
                index += len;


                // Acc Config -- Sensitivity level 
                len = 2;
                subStr = payload.Substring(index, len);
                int sensitivityLevel = Convert.ToInt16(subStr, 16);
                int sensitivityGrav = 1;
                for (int i = 0; i < sensitivityLevel; i++)
                {
                    sensitivityGrav *= 2;
                }
                sb.AppendFormat("Sensitivity: {0} G ", sensitivityGrav.ToString());
                sb.AppendLine();
                index += len;

                // Acc Config -- Sensitivity Threshlod 
                len = 4;
                subStr = payload.Substring(index, len);
                int sensitivityThesh = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("Sensitivity Threshlod : {0} mg", sensitivityThesh.ToString());
                sb.AppendLine();
                index += len;

                // Acc Interrupt 

                // Acc Interrupt No 
                len = 2;
                subStr = payload.Substring(index, len);
                int accIntrNum = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("Acc Interrupt No: {0} ", accIntrNum);
                sb.AppendLine();
                index += len;

                // Skip reserved field 
                len = 2;
                index += len;

                // Acc Interrupt :  Data field 
                len = 2;
                sb.Append("Acc Interrupt enabled axis : ");
                subStr = payload.Substring(index, len);

                //SByte accByte = Convert.ToSByte(subStr);
                Byte accByte = byte.Parse(subStr, NumberStyles.HexNumber, CultureInfo.InvariantCulture);

                string bitFlag = Convert.ToString(accByte, 2).PadLeft(8, '0'); // 00010001

                var chars = bitFlag.ToCharArray();

                if (chars[5].Equals('1'))
                    sb.Append("X");

                if (chars[6].Equals('1'))
                    sb.Append("Y");

                if (chars[7].Equals('1'))
                    sb.Append("Z");

                sb.AppendLine();
                index += len;


                // Data log enable 
                len = 2;
                subStr = payload.Substring(index, len);
                int logEnable = Convert.ToInt16(subStr, 16);

                if (logEnable == 0)
                {
                    sb.Append("Acc Data Log Disabled. ");
                }
                else
                {
                    sb.Append("Acc Data Log Enabled. ");
                }

                sb.AppendLine();
                index += len;

                // Data log Interval  
                len = 2;
                subStr = payload.Substring(index, len);
                int logInterval = Convert.ToInt16(subStr, 16);
                logInterval = 10 * logInterval; // predefined spec. 
                sb.AppendFormat("Acc Interval : {0} ms ", logInterval.ToString());

                sb.AppendLine();
                index += len;

                // Data log :  No of Biocks 
                len = 2;
                subStr = payload.Substring(index, len);
                int numBlocks = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("Acc Blocks : {0} ", numBlocks.ToString());

                sb.AppendLine();
                index += len;

                // Setup message : installed or not 
                len = 2;
                subStr = payload.Substring(index, len);
                int installStatus = Convert.ToInt16(subStr, 16);

                if (installStatus == 0)
                {
                    sb.Append("Uninstalled ");
                }
                else
                {
                    sb.Append("Installed ");
                }

                sb.AppendLine();
                index += len;


                // FW Version 
                // Major 
                len = 2;
                subStr = payload.Substring(index, len);
                int fwMajor = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("FW version: {0} ", fwMajor.ToString("00"));
                //sb.AppendLine();
                index += len;

                // Minor 
                len = 2;
                subStr = payload.Substring(index, len);
                int fwMinor = Convert.ToInt16(subStr, 16);
                sb.AppendFormat(" {0} ", fwMinor.ToString("00"));
                //sb.AppendLine();
                index += len;

                // Revision 
                len = 2;
                subStr = payload.Substring(index, len);
                int fwRev = Convert.ToInt16(subStr, 16);
                sb.AppendFormat(" {0} ", fwRev.ToString("00"));
                sb.AppendLine();
                index += len;


                // LoRa module FW Version (SoluM)
                // Major 
                len = 2;
                subStr = payload.Substring(index, len);
                fwMajor = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("LoRa Module FW version: {0} ", fwMajor.ToString("00"));
                //sb.AppendLine();
                index += len;

                // Minor 
                len = 2;
                subStr = payload.Substring(index, len);
                fwMinor = Convert.ToInt16(subStr, 16);
                sb.AppendFormat(" {0} ", fwMinor.ToString("00"));
                //sb.AppendLine();
                index += len;

                // Revision 
                len = 2;
                subStr = payload.Substring(index, len);
                fwRev = Convert.ToInt16(subStr, 16);
                sb.AppendFormat(" {0} ", fwRev.ToString("00"));
                sb.AppendLine();
                index += len;
                
            }
            else
            {
                sb.Append("Invalid Alive Message Format");
                sb.AppendLine();
            }

            return sb.ToString(); 
        }

        public static int convertSignedByteToInt(int rssi)
        {
            int result = 0; 
            if (rssi > 127)
                result = (256 - rssi) * (-1); 
            else
                result = rssi;

            return result;

        }

        public string parseEvent(string payload, int version)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            String eventType; 
            String subStr;
            int len = 2;
            
            sb.Append("Event");
            sb.AppendLine();

            // Trim whitespace
            payload = payload.Trim();

            // Check packet validity 
            int strLen = payload.Length;

            // Version 1 
            if ((1 == version) && (eventLengthVer1 == strLen))
            {
                // Event type 
                len = 2;
                eventType = payload.Substring(index, len);
                string eventString;
                switch (eventType)
                {
                    case "01":
                        eventString = "High-G";
                        break;
                    case "02":
                        eventString = "Tap";
                        break;
                    case "03":
                        eventString = "Double tap";
                        break;
                    case "04":
                        eventString = "Orient";
                        break;
                    case "05":
                        eventString = "Flip";
                        break;
                    case "06":
                        eventString = "Flat";
                        break;
                    case "07":
                        eventString = "Low-G";
                        break;
                    case "08":
                        eventString = "Collapse"; //jychoi 
                        break;
                    default:
                        eventString = "Unknown event";
                        break;
                }


                sb.Append(eventString);
                sb.AppendLine();
                index += len;

                // X Y Z bit 
                len = 2;
                subStr = payload.Substring(index, len);

                SByte accByte = Convert.ToSByte(subStr);


                string bitFlag = Convert.ToString(accByte, 2).PadLeft(8, '0'); // 00010001

                var chars = bitFlag.ToCharArray();

                if (chars[4].Equals('1'))
                    sb.Append("-");
                else
                    sb.Append("+");

                if (chars[5].Equals('1'))
                    sb.Append("Z");

                if (chars[6].Equals('1'))
                    sb.Append("Y");

                if (chars[7].Equals('1'))
                    sb.Append("X");

                sb.AppendLine();
                index += len;

            }
            // Version 2 
            else if ((1 < version) && (eventLengthVer2 == strLen))
            {
                // Event type 
                len = 2;
                eventType = payload.Substring(index, len);
                string eventString;
                switch (eventType)
                {
                    case "01":
                        eventString = "High-G";
                        break;
                    case "02":
                        eventString = "Tap";
                        break;
                    case "03":
                        eventString = "Double tap";
                        break;
                    case "04":
                        eventString = "Orient";
                        break;
                    case "05":
                        eventString = "Flip";
                        break;
                    case "06":
                        eventString = "Flat";
                        break;
                    case "07":
                        eventString = "Low-G";
                        break;
                    case "08":
                        eventString = "Collapse"; //jychoi 
                        break;
                    default:
                        eventString = "Unknown event";
                        break;
                }


                sb.Append(eventString);
                sb.AppendLine();
                index += len;

                // X axis 
                len = 2;
                subStr = payload.Substring(index, len);
                int xnum = Convert.ToInt16(subStr, 16);                
                sb.AppendFormat("Number of X axis interrupts: {0} ", xnum.ToString());
                sb.AppendLine();
                index += len;


                // Y axis 
                len = 2;
                subStr = payload.Substring(index, len);
                int ynum = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("Number of Y axis interrupts: {0} ", ynum.ToString());
                sb.AppendLine();
                index += len;

                // Z axis 
                len = 2;
                subStr = payload.Substring(index, len);
                int znum = Convert.ToInt16(subStr, 16);
                sb.AppendFormat("Number of Z axis interrupts: {0} ", znum.ToString());
                sb.AppendLine();
                index += len;


            }
            else
            {
                sb.Append("Invalid Event Message Format");
                sb.AppendLine();
            }

            if (version < 3)
            {
                // RSSI
                len = 2;
                subStr = payload.Substring(index, len);
                int rssi = Convert.ToInt16(subStr, 16);
                rssi = convertSignedByteToInt(rssi);
                sb.AppendFormat("RSSI: {0} ", rssi);
                sb.AppendLine();
                index += len;

            }

            return sb.ToString();
        }
        
        public string parseError(string payload, int version)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            String eventType;
            String subStr;
            int len = 2;

            sb.Append("Error");
            sb.AppendLine();

            // Trim whitespace
            payload = payload.Trim();

            // Error type 
            len = 2;
            eventType = payload.Substring(index, len);
            string eventString;
            switch (eventType)
            {
                case "00":
                    eventString = "LoraErrorNone";
                    break;
                case "01":
                    eventString = "LoraErrorOverReTx";
                    break;
                case "02":
                    eventString = "LoraErrorJoinFail";
                    break;
                case "03":
                    eventString = "LoraErrorRx";
                    break;
                case "04":
                    eventString = "LoraErrorNok";
                    break;
                case "05":
                    eventString = "LoraErrorOverReTxInfinite";
                    break;
                default:
                    eventString = "LoraErrorUnknown";
                    break;
            }


            sb.Append(eventString);
            sb.AppendLine();
            index += len;

            if (version < 3)
            {

                // RSSI
                len = 2;
                subStr = payload.Substring(index, len);
                int rssi = Convert.ToInt16(subStr, 16);
                rssi = convertSignedByteToInt(rssi);
                sb.AppendFormat("RSSI: {0} ", rssi);
                sb.AppendLine();
                index += len;

            }

            return sb.ToString();
        }
                
        public string parseAck(string payload, int version)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            String eventType;
            String subStr;
            int len = 2;

            sb.Append("Ack");
            sb.AppendLine();

            // Trim whitespace
            payload = payload.Trim();

            // Ack type 
            len = 2;
            eventType = payload.Substring(index, len);
            string eventString;
            switch (eventType)
            {
                case "00":
                    eventString = "ExtDevMgmt";
                    break;
                case "80":
                    eventString = "DevReset";
                    break;
                case "81":
                    eventString = "RepPerChange";
                    break;
                case "82":
                    eventString = "RepImmediate (Class C)";
                    break;
                default:
                    eventString = "Unknown Ack";
                    break;
            }


            sb.Append(eventString);
            sb.AppendLine();
            index += len;

            if (version < 3)
            {

                // RSSI
                len = 2;
                subStr = payload.Substring(index, len);
                int rssi = Convert.ToInt16(subStr, 16);
                rssi = convertSignedByteToInt(rssi);
                sb.AppendFormat("RSSI: {0} ", rssi);
                sb.AppendLine();
                index += len;

            }

            return sb.ToString();
        }

        public string parseNotice(string payload, int version)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            String noticeType;
            String subStr;
            int len = 2;

            sb.Append("Notice");
            sb.AppendLine();

            // Trim whitespace
            payload = payload.Trim();

            // Notice type 
            len = 2;
            noticeType = payload.Substring(index, len); // do not touch noticeType. It will be used to parse payload. 
            string notiString;
            switch (noticeType) 
            {
                case "01":
                    notiString = "Power Up";
                    break;
                case "02":
                    notiString = "Power Off";
                    break;
                case "03":
                    notiString = "Reset";
                    break;
                case "04":
                    notiString = "Setup (BLE)";
                    break;
                case "FF":
                    notiString = "String. Not yet supported.";
                    break;
                default:
                    notiString = "Unknown notice";
                    break;
            }


            sb.Append(notiString);
            sb.AppendLine();
            index += len;


            // Notice Length  
            len = 2;
            subStr = payload.Substring(index, len);
            int noticeLength = Convert.ToInt16(subStr, 16);
            
            sb.AppendFormat("Length {0}", noticeLength);

            sb.AppendLine();
            index += len;


            // Notice Payload  
            len = (noticeLength * 2);//1Byte is 2 digit string. 
            subStr = payload.Substring(index, len);
            //subStr = payload.Substring(index, (noticeLength * 2)); 

            switch (noticeType)
            {
                case "01":
                    notiString = parseNoticePowerUp(subStr);
                    break;
                case "02":
                    notiString = parseNoticePowerOff(subStr);
                    break;
                case "03":
                    notiString = parseNoticeReset(subStr);
                    break;
                case "04":
                    notiString = parseNoticeSetup(subStr); // Uninstall / install 
                    break;
                case "FF":
                    notiString = "String payload. Not yet supported.";
                    break;
                default:
                    notiString = "Unknown notice";
                    break;
            }
            sb.Append(notiString);
            sb.AppendLine();
            index += len;

            if (version < 3)
            {
                // RSSI
                len = 2;
                subStr = payload.Substring(index, len);
                int rssi = Convert.ToInt16(subStr, 16);
                rssi = convertSignedByteToInt(rssi);
                sb.AppendFormat("RSSI: {0} ", rssi);
                sb.AppendLine();
                index += len;

            }

            return sb.ToString();
        }

        
        private String parsePowerOffErrorLog(String subStr)
        {
            StringBuilder sb = new StringBuilder();
            try
            {

                switch (subStr)
                {
                    case "00":
                        sb.Append("No Power-off record. ");
                        break;
                    case "01":
                        sb.Append("Alive 주기 초과 (전원 리셋) : 내부 에러 발생으로 인해 정해진 Alive 주기에 전송을 못해서 +5분 초과하는 경우 리셋됨.");
                        break;
                    case "02":
                        sb.Append("LoRa 전송 실패 (전원 꺼짐) : 전송(재전송횟수 8회)이 10회 실패하면 전원 꺼짐.");
                        break;
                    case "04":
                        sb.Append("LoRa Join 실패 (전원 꺼짐) : Join Request 가 연속 24회 실패하면 전원 꺼짐.");
                        break;
                    case "05":
                        sb.Append("PowerOffWatchdogTimeout (리셋): Watchdog Timeout 통한 전원 리셋");
                        break;
                    case "06":
                        sb.Append("PowerOffNoAckOfEndPacket (전원 꺼짐): End packet 전송 후 일정시간 동안 Ack 못 받은 경우 전원 꺼짐");
                        break;
                    case "07":
                        sb.Append("PowerOffLoRaRxCmdPowerOff (전원 꺼짐): LoRa downstream 통해 PowerOff 명령(0x01) 수신하여 전원 꺼짐");
                        break;
                    case "08":
                        sb.Append("PowerOffBLERxCmdPowerOff (전원 꺼짐): BLE 통해 PowerOff 명령(0x92) 수신하여 전원 꺼짐");
                        break;
                    case "09":
                        sb.Append("PowerOffLowBattery (전원 꺼짐): Low Battery 일 때 전원 꺼짐");
                        break;
                    case "0A":
                    case "0a":
                        sb.Append("PowerOffUninstalled (전원 꺼짐): 설치되지 않은 디바이스의 전송 횟수 초과로 전원 꺼짐");
                        break;
                    case "81":
                        sb.Append("PowerOffResetFactorySetting (전원 리셋): Factory Setting 명령어 통해 전원 리셋");
                        break;
                    case "82":
                        sb.Append("PowerOffResetDeviceUpsideDown (전원 리셋): 15초 이하일 때 포지션이 바뀌면 전원 리셋");
                        break;
                    case "83":
                        sb.Append("PowerOffResetWatchdogConfig (전원 리셋): Watchdog configuration을 바꾸었을 때 전원 리셋");
                        break;
                    case "84":
                        sb.Append("PowerOffResetLoRaRxCmdReset (전원 리셋): LoRa downstream 통해 Reset 명령(0x02) 수신하여 전원 리셋");
                        break;
                    case "85":
                        sb.Append("PowerOffResetBLERxCmdReset (전원 리셋): BLE 통해 Reset 명령(0x02) 수신하여 전원 리셋");
                        break;
                    case "86":
                        sb.Append("PowerOffResetFactoryReset (전원 리셋): Factory Reset 수신하여 전원 리셋");
                        break;
                    case "87":
                        sb.Append("PowerOffResetSKTReset (전원 리셋): SKT_DEV_RESET 수신하여 전원 리셋");
                        break;
                    case "88":
                        sb.Append("PowerOffResetSoluMFWUpgrade (전원 리셋): SoluM FW upgrade 후 전원 리셋");
                        break;
                    default:
                        sb.Append("Unknown power off reason");
                        break;
                }


            }
            catch (FormatException)
            {
                sb.Append("Invalid error log in Notice.");
            }

            sb.AppendLine();

            return sb.ToString(); 

        }

        //  1st Byte : Reset reason 
        //  4th Byte : Error log 
        public string parseNoticePowerUp(string payload)
        {
            String subStr = String.Empty;
            int index = 0;
            int len = 2;
            StringBuilder sb = new StringBuilder();

            // power up notice  

            /////////////////////////////////
            // Parse reset reason 
            sb.Append("Reset Reason : ");

            len = 2;
            subStr = payload.Substring(index, len);
            try
            {
                //SByte powerUpByte = Convert.ToSByte(subStr);
                Byte powerUpByte = byte.Parse(subStr, NumberStyles.HexNumber, CultureInfo.InvariantCulture);


                string bitFlag = Convert.ToString(powerUpByte, 2).PadLeft(8, '0'); // 00010001

                var chars = bitFlag.ToCharArray();

                if (chars[1].Equals('1'))
                    sb.Append("Reset due to wake up from system OFF mode when wakeup is triggered from entering into debug interface mode. "); // Not used 

                if (chars[2].Equals('1'))
                    sb.Append("Reset due to wake up from system OFF mode when wakeup is	triggered from ANADETECT signal from LPCOMP. "); // Not used 

                if (chars[3].Equals('1'))
                    sb.Append("Reset due to wake up from system OFF mode when wakeup is	triggered from DETECT signal from GPIO. "); // Not used 

                if (chars[4].Equals('1'))
                    sb.Append("CPU lock-up Reset. ");

                if (chars[5].Equals('1'))
                    sb.Append("SYSRESETREQ Reset. ");

                if (chars[6].Equals('1'))
                    sb.Append("Watchdog Reset. ");

                if (chars[7].Equals('1'))
                    sb.Append("Pin-reset. ");

                if (subStr.Equals("00"))
                    sb.Append("Power-on-reset or a brown out reset. ");

            }
            catch (FormatException)
            {
                sb.Append("Invalid reset reason in Notice.");
            }


            sb.AppendLine();
            index += len;

            ////////////////////////////////////////
            // skip reserved field 

            index += len; //skip 2nd byte 
            index += len; //skip 3rd byte 

            /////////////////////////////////
            // Parse Error log 
            sb.Append("Power Off Error Log : ");

            subStr = payload.Substring(index, len);

            String errStr = parsePowerOffErrorLog(subStr);
            sb.Append(errStr); 

            index += len;

            return sb.ToString();
        }


        public string parseNoticePowerOff(string payload)
        {
            String subStr = String.Empty;
            int index = 0;
            int len = 2;
            StringBuilder sb = new StringBuilder();

            // power off notice  

            len = 2;
            subStr = payload.Substring(index, len);

            /*
            string payloadStr;

            switch (subStr)
            {
                case "01":
                    payloadStr = "LoRa Downstream - Power off command (0x1)";
                    break;
                case "02":
                    payloadStr = "BLE command (0x92)";
                    break;
                case "03":
                    payloadStr = "설치되지 않은 디바이스의 전송 횟수 초과";
                    break;
                case "04":
                    payloadStr = "배터리 부족";
                    break;
                case "05":
                    payloadStr = "에러";
                    break;
                default:
                    payloadStr = "Unknown power off notice ";
                    break;
            }

            sb.Append(payloadStr); 
            sb.AppendLine();
            */

            String errStr = parsePowerOffErrorLog(subStr);
            sb.Append(errStr);
            
            index += len;

            return sb.ToString();
        }


        public string parseNoticeReset(string payload)
        {
            
            StringBuilder sb = new StringBuilder();            

            return sb.ToString();
        }


        public string parseNoticeSetup(string payload)
        {
            String subStr = String.Empty;
            int index = 0;
            int len = 2;
            StringBuilder sb = new StringBuilder();

            // power off notice  

            len = 2;
            subStr = payload.Substring(index, len);

            string payloadStr;
            switch (subStr)
            {
                case "00":
                    payloadStr = "Uninstall";
                    break;
                case "01":
                    payloadStr = "Install";
                    break;
                case "02":
                    payloadStr = "RequestInstall";
                    break;
                default:
                    payloadStr = "Unknown notice setup message";
                    break;
            }

            sb.Append(payloadStr);
            sb.AppendLine();
            index += len;

            return sb.ToString();
        }


        public string parseDataLog(string payload, int version)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            String dataLogPayload;
            String subStr;
            int len = 2;

            sb.Append("Data Log");
            sb.AppendLine();

            // Trim whitespace
            payload = payload.Trim();

            // Data index 
            len = 2;
            dataLogPayload = payload.Substring(index, len); // 
            sb.AppendFormat("Data index: 0x{0}", dataLogPayload);                         
            sb.AppendLine();
            index += len;


            // Data position               
            len = 2;
            subStr = payload.Substring(index, len);

            string payloadStr;
            if (version < 3)
            {
                switch (subStr)
                {
                    case "00":
                        payloadStr = "Start";
                        break;
                    case "01":
                        payloadStr = "Intermediate";
                        break;
                    case "02":
                        payloadStr = "Finish";
                        break;
                    default:
                        payloadStr = "Unknown data log message";
                        break;
                }

            }
            else
            {
                switch (subStr)
                {
                    case "00":
                        payloadStr = "Start";
                        break;
                    case "FF":
                    case "ff":
                        payloadStr = "Finish";
                        break;
                    default:
                        payloadStr = "Intermediate " + subStr;
                        break;
                }

            }

            sb.Append(payloadStr);
            sb.AppendLine();
            index += len;

            // no contents
            // no RSSI
            

            return sb.ToString();
        }


    }
}
