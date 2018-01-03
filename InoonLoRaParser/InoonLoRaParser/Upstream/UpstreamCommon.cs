using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InoonLoRaParser.Upstream
{
    public class UpstreamCommon
    {
        //public String version; // default 1 
        public int versionNumber; // default 1 

        public enum DeviceType { Unknown = 0, ParkingPlex = 1, InoVibe = 2 };
        public DeviceType devType; // 
         
        /*
         *  0x1 | Alive - 주기적 상태 전송
            0x2 | Event - 센서 값 변화, 배터리 부족 등의 이벤트 발생을 알림
            0x3 | Error - 에러 발생을 알림
            0x4 | Ack - 전송 후 응답이 필요한 제어 명령에 대한 Ack
            0x5 | Notice - 단말기에서 알리고자 하는 정보를 서버로 전달 (Power Up, Power Off, Reset)
            0x6 | Data log - 단말기에서 수집한 센서 데이터를 서버로 전송
         * 
         * */
        public enum UpPacketType { Unknown = 0, Alive = 1, Event, Error, Ack, Notice, DataLog };
        public UpPacketType upPacketType;

        /*
         *  0x0 | No downlink response
            0x1 | Sync - 동기화를 위해 사용됨. (Normal downlink response)
            0x2 | Config - 부팅 후 자신의 상태를 문의 함. 어플리케이션 서버는 Power Off, Setup, Sensor Config 명령어를 통해 센서를 제어함.
        */

        public enum UpRequestType { NoDownlinkResponse = 0, Sync, Config };
        public UpRequestType upRequestType;

        public uint batteryLevel; // 0~100%

        public int temperature; // -30 ~ 80  

 

        private void setVersionNumber(String verStr)
        {
            this.versionNumber = 1;
            bool result = Int32.TryParse(verStr, out this.versionNumber); // it's simple program 
            return; 
        }

        /// <summary>
        /// Tab separated result 
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public string parseUpstreamCommon(string inputStr)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            String subStr;
            int len = 2; 

            // Trim whitespace
            inputStr = inputStr.Trim();

            if (inputStr.Length < 15)
                return String.Empty; 

            // Version 
            len = 2;
            subStr = inputStr.Substring(index, len);
            setVersionNumber(subStr); // Need to distinguish version 1 and version 2 
            sb.AppendFormat ("Version: {0}", subStr);
            sb.AppendLine();
            index += len;

            if (this.versionNumber < 3)
            {
                // Reserved   
                sb.AppendFormat("Reserved: {0}", inputStr.Substring(index, len));
                sb.AppendLine();
                index += len;

                // PacketID   
                sb.AppendFormat("PacketID: 0x{0}", inputStr.Substring(index, len));
                sb.AppendLine();
                index += len;



                // Device Type    
                len = 4;
                subStr = inputStr.Substring(index, len);
                this.devType = getDeviceType(subStr);

                sb.AppendFormat("Device Type: {0}", getDeviceTypeString(subStr));
                sb.AppendLine();
                index += len;



                // Packet Type    
                len = 1;
                subStr = inputStr.Substring(index, len);
                this.upPacketType = getPacketType(subStr);
                sb.AppendFormat("Packet Type: {0}", getPacketTypeString(subStr));
                sb.AppendLine();
                index += len;


                // Request Type    
                subStr = inputStr.Substring(index, len);
                this.upRequestType = getRequestType(subStr);
                sb.AppendFormat("Request Type: {0}", getRequestTypeString(subStr));
                sb.AppendLine();
                index += len;


                // Battery level   
                len = 2;
                subStr = inputStr.Substring(index, len);
                int batterylevel = Convert.ToInt32(subStr, 16);
                sb.AppendFormat("Battery: {0} %", batterylevel);
                sb.AppendLine();
                index += len;

                // temperature   
                subStr = inputStr.Substring(index, len);
                int temperature = Convert.ToInt32(subStr, 16);
                sb.AppendFormat("Temperature: {0} ", temperature);
                sb.AppendLine();
                index += len;
            }
            else // version 3 
            {

                // Device Type    
                len = 2;
                subStr = inputStr.Substring(index, len);
                this.devType = getDeviceType(subStr);

                sb.AppendFormat("Device Type: {0}", getDeviceTypeString(subStr));
                sb.AppendLine();
                index += len;

                // Sequence number     
                len = 2;
                subStr = inputStr.Substring(index, len);
                int seqno = Convert.ToInt32(subStr, 16);

                sb.AppendFormat("Sequence number: {0}", seqno);
                sb.AppendLine();
                index += len;

                //Device status 
                // Battery level   
                len = 2;
                subStr = inputStr.Substring(index, len);
                int batterylevel = Convert.ToInt32(subStr, 16);
                sb.AppendFormat("Battery: {0} %", batterylevel);
                sb.AppendLine();
                index += len;

                // temperature   
                subStr = inputStr.Substring(index, len);
                int temperature = Convert.ToInt32(subStr, 16);
                sb.AppendFormat("Temperature: {0} ", temperature);
                sb.AppendLine();
                index += len;

                // LoRa 
                // LoRa Error  
                len = 2;
                subStr = inputStr.Substring(index, len);
                int loraError = Convert.ToInt32(subStr, 16);
                sb.AppendFormat("Fail LoRa count: {0} ", loraError);
                sb.AppendLine();
                index += len;

                // LoRa RSSI    
                subStr = inputStr.Substring(index, len);
                int loraRssi = Convert.ToInt16(subStr, 16);
                loraRssi = UpstreamApplicationPayload.convertSignedByteToInt(loraRssi);
                sb.AppendFormat("LoRa RSSI: {0} ", loraRssi);
                sb.AppendLine();
                index += len;


                // Packet Type    
                len = 1;
                subStr = inputStr.Substring(index, len);
                this.upPacketType = getPacketType(subStr);
                sb.AppendFormat("Packet Type: {0}", getPacketTypeString(subStr));
                sb.AppendLine();
                index += len;


                // Request Type    
                subStr = inputStr.Substring(index, len);
                this.upRequestType = getRequestType(subStr);
                sb.AppendFormat("Request Type: {0}", getRequestTypeString(subStr));
                sb.AppendLine();
                index += len;


                // Reserved 
                len = 4;
                subStr = inputStr.Substring(index, len); 
                sb.AppendFormat("Reserved: {0}", subStr);
                sb.AppendLine();
                index += len;

            }
            
            return sb.ToString(); 
        }

        private DeviceType getDeviceType(string subStr)
        {
            DeviceType devType; 

            if ((subStr.Equals("0001")) || (subStr.Equals("01")))
                devType = DeviceType.ParkingPlex;
            else if ((subStr.Equals("0002")) || (subStr.Equals("02")))
                devType = DeviceType.InoVibe;
            else
                devType = DeviceType.Unknown;

            return devType; 
        }

        private string getDeviceTypeString(string subStr)
        {
            string str;

            if ((subStr.Equals("0001")) || (subStr.Equals("01")))
                str = "ParkingPlex";
            else if ((subStr.Equals("0002")) || (subStr.Equals("02")))
                str = "Ino-Vibe";
            else
                str = "Unknown device type";

            return str;
        }

        private UpPacketType getPacketType(string subStr)
        {
            UpPacketType type;
            switch (subStr)
            {
                case "1":
                    type = UpPacketType.Alive; 
                    break;
                case "2":
                    type = UpPacketType.Event;
                    break;
                case "3":
                    type = UpPacketType.Error;
                    break;
                case "4":
                    type = UpPacketType.Ack;
                    break;
                case "5":
                    type = UpPacketType.Notice;
                    break;
                case "6":
                    type = UpPacketType.DataLog;
                    break;
                default:
                    type = UpPacketType.Unknown;
                    break;
            }

            return type; 

        }


        private string getPacketTypeString(string subStr)
        {
            string type;
            switch (subStr)
            {
                case "1":
                    type = "Alive";
                    break;
                case "2":
                    type = "Event";
                    break;
                case "3":
                    type = "Error";
                    break;
                case "4":
                    type = "Ack";
                    break;
                case "5":
                    type = "Notice";
                    break;
                case "6":
                    type = "DataLog";
                    break;
                default:
                    type = "Unknown";
                    break;
            }

            return type;

        }


        private UpRequestType getRequestType(string subStr)
        {
            UpRequestType reqType;

            if (subStr.Equals("1"))
                reqType = UpRequestType.Sync;
            else if (subStr.Equals("2"))
                reqType = UpRequestType.Config;
            else
                reqType = UpRequestType.NoDownlinkResponse;

            return reqType;
        }

        private string getRequestTypeString(string subStr)
        {
            string str;           

            if (subStr.Equals("1"))
                str = "Sync";
            else if (subStr.Equals("2"))
                str = "Config";
            else
                str = "No Downlink Response";

            return str;
        }

    }
}
