using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InoonLoRaParser.Upstream
{
    public class UpstreamApplicationPayload
    {

        private const Double accScale = 3.91; //mg for 2G range. 7.81 for 4G range   

        //parse application payload 
        public string parseApplicationPayload(UpstreamCommon.UpPacketType pktType, string pktStr)
        {
            string res = String.Empty;

            if (pktType == UpstreamCommon.UpPacketType.Alive)
                res = parseAlive(pktStr);
            else if (pktType == UpstreamCommon.UpPacketType.Event)
                res = parseEvent(pktStr);
            else if (pktType == UpstreamCommon.UpPacketType.Error)
                res = parseError(pktStr);
            else if (pktType == UpstreamCommon.UpPacketType.Ack)
                res = parseAck(pktStr);
            else if (pktType == UpstreamCommon.UpPacketType.Notice)
                res = parseNotice(pktStr);
            else if (pktType == UpstreamCommon.UpPacketType.DataLog)
                res = parseDataLog(pktStr);
            else
                res = "Unknown packet";


            return res; 
        }

        public string parseAlive(string payload)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            String subStr;
            int len = 2;

            sb.Append("Alive");
            sb.AppendLine();
            
            // Trim whitespace
            payload = payload.Trim();

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
            sb.AppendFormat("RSSI: {0} ", rssi);
            sb.AppendLine();
            index += len;

            return sb.ToString(); 
        }

        public string parseEvent(string payload)
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
                sb.Append("X");

            if (chars[6].Equals('1'))
                sb.Append("Y");

            if (chars[7].Equals('1'))
                sb.Append("Z");

            sb.AppendLine();
            index += len;

            // RSSI
            len = 2;
            subStr = payload.Substring(index, len);
            int rssi = Convert.ToInt16(subStr, 16);
            sb.AppendFormat("RSSI: {0} ", rssi);
            sb.AppendLine();
            index += len;

            return sb.ToString();
        }
        
        public string parseError(string payload)
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
                default:
                    eventString = "LoraErrorUnknown";
                    break;
            }


            sb.Append(eventString);
            sb.AppendLine();
            index += len;

            

            // RSSI
            len = 2;
            subStr = payload.Substring(index, len);
            int rssi = Convert.ToInt16(subStr, 16);
            sb.AppendFormat("RSSI: {0} ", rssi);
            sb.AppendLine();
            index += len;

            return sb.ToString();
        }
                
        public string parseAck(string payload)
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



            // RSSI
            len = 2;
            subStr = payload.Substring(index, len);
            int rssi = Convert.ToInt16(subStr, 16);
            sb.AppendFormat("RSSI: {0} ", rssi);
            sb.AppendLine();
            index += len;

            return sb.ToString();
        }

        public string parseNotice(string payload)
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
                    notiString = "Setup";
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
            len = 2;
            subStr = payload.Substring(index, len);

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



            // RSSI
            len = 2;
            subStr = payload.Substring(index, len);
            int rssi = Convert.ToInt16(subStr, 16);
            sb.AppendFormat("RSSI: {0} ", rssi);
            sb.AppendLine();
            index += len;

            return sb.ToString();
        }

        public string parseNoticePowerUp(string payload)
        {
            String subStr = String.Empty;
            int index = 0;
            int len = 2;
            StringBuilder sb = new StringBuilder();

            // power up notice  

            len = 2;
            subStr = payload.Substring(index, len);

            SByte powerUpByte = Convert.ToSByte(subStr);


            string bitFlag = Convert.ToString(powerUpByte, 2).PadLeft(8, '0'); // 00010001

            var chars = bitFlag.ToCharArray();

            if (chars[4].Equals('1'))
                sb.Append("CPU lock-up Reset");          

            if (chars[5].Equals('1'))
                sb.Append("Software Reset");

            if (chars[6].Equals('1'))
                sb.Append("Watchdog Reset");

            if (chars[7].Equals('1'))
                sb.Append("Reset Pin");

            if (subStr.Equals("00"))
                sb.Append("전원 꺼진 상태에서 전원이 인가됨");
       
            sb.AppendLine();
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
                    payloadStr = "Unknown notice power off message ";
                    break;
            }

            sb.Append(payloadStr); 
            sb.AppendLine();
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
                default:
                    payloadStr = "Unknown notice setup message";
                    break;
            }

            sb.Append(payloadStr);
            sb.AppendLine();
            index += len;

            return sb.ToString();
        }


        public string parseDataLog(string payload)
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

            sb.Append(payloadStr);
            sb.AppendLine();
            index += len;

            // no contents
            // no RSSI
            

            return sb.ToString();
        }


    }
}
