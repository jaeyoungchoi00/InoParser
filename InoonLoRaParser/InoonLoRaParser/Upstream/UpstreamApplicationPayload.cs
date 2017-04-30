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
                case "08":
                    eventString = "Flip";
                    break;
                default:
                    eventString = "Collapse";
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

    }
}
