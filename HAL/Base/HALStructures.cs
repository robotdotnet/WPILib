using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

//These are all of the structures used by HAL-RoboRIO and HAL-Simulation. 
//Changes to these will always require a rebuild of the local HALs, which we want to avoid doing.
//So please do not change these without explicit reasoning.
namespace HAL.Base
{

    #region HAL
    /// <summary>
    /// 
    /// </summary>
    public enum HALAllianceStationID
    {
        // ReSharper disable InconsistentNaming
        HALAllianceStationID_red1,

        HALAllianceStationID_red2,

        HALAllianceStationID_red3,

        HALAllianceStationID_blue1,

        HALAllianceStationID_blue2,


        HALAllianceStationID_blue3,
        // ReSharper restore InconsistentNaming
    }

    /// <summary>
    /// Joystick Axes Struct
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HALJoystickAxes
    {
        /// unsigned short
        public ushort count;

        /// short[] hack
        public HALJoystickAxesArray axes;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct HALJoystickPOVs
    {
        /// unsigned short
        public ushort count;

        /// short[] hack
        public HALJoystickPOVArray povs;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HALJoystickAxesArray
    {
        public short axes0;
        public short axes1;
        public short axes2;
        public short axes3;
        public short axes4;
        public short axes5;
        public short axes6;
        public short axes7;
        public short axes8;
        public short axes9;
        public short axes10;
        public short axes11;

        public short this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return axes0;
                    case 1:
                        return axes1;
                    case 2:
                        return axes2;
                    case 3:
                        return axes3;
                    case 4:
                        return axes4;
                    case 5:
                        return axes5;
                    case 6:
                        return axes6;
                    case 7:
                        return axes7;
                    case 8:
                        return axes8;
                    case 9:
                        return axes9;
                    case 10:
                        return axes10;
                    case 11:
                        return axes11;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }

            set
            {
                switch (i)
                {
                    case 0:
                        axes0 = value;
                        return;
                    case 1:
                        axes1 = value;
                        return;
                    case 2:
                        axes2 = value;
                        return;
                    case 3:
                        axes3 = value;
                        return;
                    case 4:
                        axes4 = value;
                        return;
                    case 5:
                        axes5 = value;
                        return;
                    case 6:
                        axes6 = value;
                        return;
                    case 7:
                        axes7 = value;
                        return;
                    case 8:
                        axes8 = value;
                        return;
                    case 9:
                        axes9 = value;
                        return;
                    case 10:
                        axes10 = value;
                        return;
                    case 11:
                        axes11 = value;
                        return;
                    default:
                        throw new IndexOutOfRangeException();

                }
            }

        }

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HALJoystickPOVArray
    {
        private short pov0;
        private short pov1;
        private short pov2;
        private short pov3;
        private short pov4;
        private short pov5;
        private short pov6;
        private short pov7;
        private short pov8;
        private short pov9;
        private short pov10;
        private short pov11;

        public short this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return pov0;
                    case 1:
                        return pov1;
                    case 2:
                        return pov2;
                    case 3:
                        return pov3;
                    case 4:
                        return pov4;
                    case 5:
                        return pov5;
                    case 6:
                        return pov6;
                    case 7:
                        return pov7;
                    case 8:
                        return pov8;
                    case 9:
                        return pov9;
                    case 10:
                        return pov10;
                    case 11:
                        return pov11;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }

            set
            {
                switch (i)
                {
                    case 0:
                        pov0 = value;
                        return;
                    case 1:
                        pov1 = value;
                        return;
                    case 2:
                        pov2 = value;
                        return;
                    case 3:
                        pov3 = value;
                        return;
                    case 4:
                        pov4 = value;
                        return;
                    case 5:
                        pov5 = value;
                        return;
                    case 6:
                        pov6 = value;
                        return;
                    case 7:
                        pov7 = value;
                        return;
                    case 8:
                        pov8 = value;
                        return;
                    case 9:
                        pov9 = value;
                        return;
                    case 10:
                        pov10 = value;
                        return;
                    case 11:
                        pov11 = value;
                        return;
                    default:
                        throw new IndexOutOfRangeException();

                }
            }

        }

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HALJoystickButtons
    {
        /// unsigned int
        public uint buttons;

        /// byte
        public byte count;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HALJoystickDescriptor
    {
        /// byte
        public byte isXbox;

        /// byte
        public byte type;

        /// char[256] hack
        public HALJoystickNameArray name;

        /// byte
        public byte axisCount;

        /// byte[] hack
        public HALJoystickAxesTypesArray axisTypes;

        /// byte
        public byte buttonCount;

        /// byte
        public readonly byte povCount;
    }




    [StructLayout(LayoutKind.Sequential)]
    public struct HALJoystickAxesTypesArray
    {
        public byte axes0;
        public byte axes1;
        public byte axes2;
        public byte axes3;
        public byte axes4;
        public byte axes5;
        public byte axes6;
        public byte axes7;
        public byte axes8;
        public byte axes9;
        public byte axes10;
        public byte axes11;

        public byte this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return axes0;
                    case 1:
                        return axes1;
                    case 2:
                        return axes2;
                    case 3:
                        return axes3;
                    case 4:
                        return axes4;
                    case 5:
                        return axes5;
                    case 6:
                        return axes6;
                    case 7:
                        return axes7;
                    case 8:
                        return axes8;
                    case 9:
                        return axes9;
                    case 10:
                        return axes10;
                    case 11:
                        return axes11;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }

            set
            {
                switch (i)
                {
                    case 0:
                        axes0 = value;
                        return;
                    case 1:
                        axes1 = value;
                        return;
                    case 2:
                        axes2 = value;
                        return;
                    case 3:
                        axes3 = value;
                        return;
                    case 4:
                        axes4 = value;
                        return;
                    case 5:
                        axes5 = value;
                        return;
                    case 6:
                        axes6 = value;
                        return;
                    case 7:
                        axes7 = value;
                        return;
                    case 8:
                        axes8 = value;
                        return;
                    case 9:
                        axes9 = value;
                        return;
                    case 10:
                        axes10 = value;
                        return;
                    case 11:
                        axes11 = value;
                        return;
                    default:
                        throw new IndexOutOfRangeException();

                }
            }

        }

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HALJoystickNameArray
    {
        public override string ToString()
        {
            var data = new List<byte>();
            var off = 0;
            while (true)
            {
                var ch = this[off++];
                //var ch = Marshal.ReadByte(ptr, off++);
                if (ch == 0)
                {
                    break;
                }
                data.Add(ch);
            }
            return Encoding.UTF8.GetString(data.ToArray());
        }

        public byte byte0;
        public byte byte1;
        public byte byte2;
        public byte byte3;
        public byte byte4;
        public byte byte5;
        public byte byte6;
        public byte byte7;
        public byte byte8;
        public byte byte9;
        public byte byte10;
        public byte byte11;
        public byte byte12;
        public byte byte13;
        public byte byte14;
        public byte byte15;
        public byte byte16;
        public byte byte17;
        public byte byte18;
        public byte byte19;
        public byte byte20;
        public byte byte21;
        public byte byte22;
        public byte byte23;
        public byte byte24;
        public byte byte25;
        public byte byte26;
        public byte byte27;
        public byte byte28;
        public byte byte29;
        public byte byte30;
        public byte byte31;
        public byte byte32;
        public byte byte33;
        public byte byte34;
        public byte byte35;
        public byte byte36;
        public byte byte37;
        public byte byte38;
        public byte byte39;
        public byte byte40;
        public byte byte41;
        public byte byte42;
        public byte byte43;
        public byte byte44;
        public byte byte45;
        public byte byte46;
        public byte byte47;
        public byte byte48;
        public byte byte49;
        public byte byte50;
        public byte byte51;
        public byte byte52;
        public byte byte53;
        public byte byte54;
        public byte byte55;
        public byte byte56;
        public byte byte57;
        public byte byte58;
        public byte byte59;
        public byte byte60;
        public byte byte61;
        public byte byte62;
        public byte byte63;
        public byte byte64;
        public byte byte65;
        public byte byte66;
        public byte byte67;
        public byte byte68;
        public byte byte69;
        public byte byte70;
        public byte byte71;
        public byte byte72;
        public byte byte73;
        public byte byte74;
        public byte byte75;
        public byte byte76;
        public byte byte77;
        public byte byte78;
        public byte byte79;
        public byte byte80;
        public byte byte81;
        public byte byte82;
        public byte byte83;
        public byte byte84;
        public byte byte85;
        public byte byte86;
        public byte byte87;
        public byte byte88;
        public byte byte89;
        public byte byte90;
        public byte byte91;
        public byte byte92;
        public byte byte93;
        public byte byte94;
        public byte byte95;
        public byte byte96;
        public byte byte97;
        public byte byte98;
        public byte byte99;
        public byte byte100;
        public byte byte101;
        public byte byte102;
        public byte byte103;
        public byte byte104;
        public byte byte105;
        public byte byte106;
        public byte byte107;
        public byte byte108;
        public byte byte109;
        public byte byte110;
        public byte byte111;
        public byte byte112;
        public byte byte113;
        public byte byte114;
        public byte byte115;
        public byte byte116;
        public byte byte117;
        public byte byte118;
        public byte byte119;
        public byte byte120;
        public byte byte121;
        public byte byte122;
        public byte byte123;
        public byte byte124;
        public byte byte125;
        public byte byte126;
        public byte byte127;
        public byte byte128;
        public byte byte129;
        public byte byte130;
        public byte byte131;
        public byte byte132;
        public byte byte133;
        public byte byte134;
        public byte byte135;
        public byte byte136;
        public byte byte137;
        public byte byte138;
        public byte byte139;
        public byte byte140;
        public byte byte141;
        public byte byte142;
        public byte byte143;
        public byte byte144;
        public byte byte145;
        public byte byte146;
        public byte byte147;
        public byte byte148;
        public byte byte149;
        public byte byte150;
        public byte byte151;
        public byte byte152;
        public byte byte153;
        public byte byte154;
        public byte byte155;
        public byte byte156;
        public byte byte157;
        public byte byte158;
        public byte byte159;
        public byte byte160;
        public byte byte161;
        public byte byte162;
        public byte byte163;
        public byte byte164;
        public byte byte165;
        public byte byte166;
        public byte byte167;
        public byte byte168;
        public byte byte169;
        public byte byte170;
        public byte byte171;
        public byte byte172;
        public byte byte173;
        public byte byte174;
        public byte byte175;
        public byte byte176;
        public byte byte177;
        public byte byte178;
        public byte byte179;
        public byte byte180;
        public byte byte181;
        public byte byte182;
        public byte byte183;
        public byte byte184;
        public byte byte185;
        public byte byte186;
        public byte byte187;
        public byte byte188;
        public byte byte189;
        public byte byte190;
        public byte byte191;
        public byte byte192;
        public byte byte193;
        public byte byte194;
        public byte byte195;
        public byte byte196;
        public byte byte197;
        public byte byte198;
        public byte byte199;
        public byte byte200;
        public byte byte201;
        public byte byte202;
        public byte byte203;
        public byte byte204;
        public byte byte205;
        public byte byte206;
        public byte byte207;
        public byte byte208;
        public byte byte209;
        public byte byte210;
        public byte byte211;
        public byte byte212;
        public byte byte213;
        public byte byte214;
        public byte byte215;
        public byte byte216;
        public byte byte217;
        public byte byte218;
        public byte byte219;
        public byte byte220;
        public byte byte221;
        public byte byte222;
        public byte byte223;
        public byte byte224;
        public byte byte225;
        public byte byte226;
        public byte byte227;
        public byte byte228;
        public byte byte229;
        public byte byte230;
        public byte byte231;
        public byte byte232;
        public byte byte233;
        public byte byte234;
        public byte byte235;
        public byte byte236;
        public byte byte237;
        public byte byte238;
        public byte byte239;
        public byte byte240;
        public byte byte241;
        public byte byte242;
        public byte byte243;
        public byte byte244;
        public byte byte245;
        public byte byte246;
        public byte byte247;
        public byte byte248;
        public byte byte249;
        public byte byte250;
        public byte byte251;
        public byte byte252;
        public byte byte253;
        public byte byte254;
        public byte byte255;

        public byte this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return byte0;
                    case 1:
                        return byte1;
                    case 2:
                        return byte2;
                    case 3:
                        return byte3;
                    case 4:
                        return byte4;
                    case 5:
                        return byte5;
                    case 6:
                        return byte6;
                    case 7:
                        return byte7;
                    case 8:
                        return byte8;
                    case 9:
                        return byte9;
                    case 10:
                        return byte10;
                    case 11:
                        return byte11;
                    case 12:
                        return byte12;
                    case 13:
                        return byte13;
                    case 14:
                        return byte14;
                    case 15:
                        return byte15;
                    case 16:
                        return byte16;
                    case 17:
                        return byte17;
                    case 18:
                        return byte18;
                    case 19:
                        return byte19;
                    case 20:
                        return byte20;
                    case 21:
                        return byte21;
                    case 22:
                        return byte22;
                    case 23:
                        return byte23;
                    case 24:
                        return byte24;
                    case 25:
                        return byte25;
                    case 26:
                        return byte26;
                    case 27:
                        return byte27;
                    case 28:
                        return byte28;
                    case 29:
                        return byte29;
                    case 30:
                        return byte30;
                    case 31:
                        return byte31;
                    case 32:
                        return byte32;
                    case 33:
                        return byte33;
                    case 34:
                        return byte34;
                    case 35:
                        return byte35;
                    case 36:
                        return byte36;
                    case 37:
                        return byte37;
                    case 38:
                        return byte38;
                    case 39:
                        return byte39;
                    case 40:
                        return byte40;
                    case 41:
                        return byte41;
                    case 42:
                        return byte42;
                    case 43:
                        return byte43;
                    case 44:
                        return byte44;
                    case 45:
                        return byte45;
                    case 46:
                        return byte46;
                    case 47:
                        return byte47;
                    case 48:
                        return byte48;
                    case 49:
                        return byte49;
                    case 50:
                        return byte50;
                    case 51:
                        return byte51;
                    case 52:
                        return byte52;
                    case 53:
                        return byte53;
                    case 54:
                        return byte54;
                    case 55:
                        return byte55;
                    case 56:
                        return byte56;
                    case 57:
                        return byte57;
                    case 58:
                        return byte58;
                    case 59:
                        return byte59;
                    case 60:
                        return byte60;
                    case 61:
                        return byte61;
                    case 62:
                        return byte62;
                    case 63:
                        return byte63;
                    case 64:
                        return byte64;
                    case 65:
                        return byte65;
                    case 66:
                        return byte66;
                    case 67:
                        return byte67;
                    case 68:
                        return byte68;
                    case 69:
                        return byte69;
                    case 70:
                        return byte70;
                    case 71:
                        return byte71;
                    case 72:
                        return byte72;
                    case 73:
                        return byte73;
                    case 74:
                        return byte74;
                    case 75:
                        return byte75;
                    case 76:
                        return byte76;
                    case 77:
                        return byte77;
                    case 78:
                        return byte78;
                    case 79:
                        return byte79;
                    case 80:
                        return byte80;
                    case 81:
                        return byte81;
                    case 82:
                        return byte82;
                    case 83:
                        return byte83;
                    case 84:
                        return byte84;
                    case 85:
                        return byte85;
                    case 86:
                        return byte86;
                    case 87:
                        return byte87;
                    case 88:
                        return byte88;
                    case 89:
                        return byte89;
                    case 90:
                        return byte90;
                    case 91:
                        return byte91;
                    case 92:
                        return byte92;
                    case 93:
                        return byte93;
                    case 94:
                        return byte94;
                    case 95:
                        return byte95;
                    case 96:
                        return byte96;
                    case 97:
                        return byte97;
                    case 98:
                        return byte98;
                    case 99:
                        return byte99;
                    case 100:
                        return byte100;
                    case 101:
                        return byte101;
                    case 102:
                        return byte102;
                    case 103:
                        return byte103;
                    case 104:
                        return byte104;
                    case 105:
                        return byte105;
                    case 106:
                        return byte106;
                    case 107:
                        return byte107;
                    case 108:
                        return byte108;
                    case 109:
                        return byte109;
                    case 110:
                        return byte110;
                    case 111:
                        return byte111;
                    case 112:
                        return byte112;
                    case 113:
                        return byte113;
                    case 114:
                        return byte114;
                    case 115:
                        return byte115;
                    case 116:
                        return byte116;
                    case 117:
                        return byte117;
                    case 118:
                        return byte118;
                    case 119:
                        return byte119;
                    case 120:
                        return byte120;
                    case 121:
                        return byte121;
                    case 122:
                        return byte122;
                    case 123:
                        return byte123;
                    case 124:
                        return byte124;
                    case 125:
                        return byte125;
                    case 126:
                        return byte126;
                    case 127:
                        return byte127;
                    case 128:
                        return byte128;
                    case 129:
                        return byte129;
                    case 130:
                        return byte130;
                    case 131:
                        return byte131;
                    case 132:
                        return byte132;
                    case 133:
                        return byte133;
                    case 134:
                        return byte134;
                    case 135:
                        return byte135;
                    case 136:
                        return byte136;
                    case 137:
                        return byte137;
                    case 138:
                        return byte138;
                    case 139:
                        return byte139;
                    case 140:
                        return byte140;
                    case 141:
                        return byte141;
                    case 142:
                        return byte142;
                    case 143:
                        return byte143;
                    case 144:
                        return byte144;
                    case 145:
                        return byte145;
                    case 146:
                        return byte146;
                    case 147:
                        return byte147;
                    case 148:
                        return byte148;
                    case 149:
                        return byte149;
                    case 150:
                        return byte150;
                    case 151:
                        return byte151;
                    case 152:
                        return byte152;
                    case 153:
                        return byte153;
                    case 154:
                        return byte154;
                    case 155:
                        return byte155;
                    case 156:
                        return byte156;
                    case 157:
                        return byte157;
                    case 158:
                        return byte158;
                    case 159:
                        return byte159;
                    case 160:
                        return byte160;
                    case 161:
                        return byte161;
                    case 162:
                        return byte162;
                    case 163:
                        return byte163;
                    case 164:
                        return byte164;
                    case 165:
                        return byte165;
                    case 166:
                        return byte166;
                    case 167:
                        return byte167;
                    case 168:
                        return byte168;
                    case 169:
                        return byte169;
                    case 170:
                        return byte170;
                    case 171:
                        return byte171;
                    case 172:
                        return byte172;
                    case 173:
                        return byte173;
                    case 174:
                        return byte174;
                    case 175:
                        return byte175;
                    case 176:
                        return byte176;
                    case 177:
                        return byte177;
                    case 178:
                        return byte178;
                    case 179:
                        return byte179;
                    case 180:
                        return byte180;
                    case 181:
                        return byte181;
                    case 182:
                        return byte182;
                    case 183:
                        return byte183;
                    case 184:
                        return byte184;
                    case 185:
                        return byte185;
                    case 186:
                        return byte186;
                    case 187:
                        return byte187;
                    case 188:
                        return byte188;
                    case 189:
                        return byte189;
                    case 190:
                        return byte190;
                    case 191:
                        return byte191;
                    case 192:
                        return byte192;
                    case 193:
                        return byte193;
                    case 194:
                        return byte194;
                    case 195:
                        return byte195;
                    case 196:
                        return byte196;
                    case 197:
                        return byte197;
                    case 198:
                        return byte198;
                    case 199:
                        return byte199;
                    case 200:
                        return byte200;
                    case 201:
                        return byte201;
                    case 202:
                        return byte202;
                    case 203:
                        return byte203;
                    case 204:
                        return byte204;
                    case 205:
                        return byte205;
                    case 206:
                        return byte206;
                    case 207:
                        return byte207;
                    case 208:
                        return byte208;
                    case 209:
                        return byte209;
                    case 210:
                        return byte210;
                    case 211:
                        return byte211;
                    case 212:
                        return byte212;
                    case 213:
                        return byte213;
                    case 214:
                        return byte214;
                    case 215:
                        return byte215;
                    case 216:
                        return byte216;
                    case 217:
                        return byte217;
                    case 218:
                        return byte218;
                    case 219:
                        return byte219;
                    case 220:
                        return byte220;
                    case 221:
                        return byte221;
                    case 222:
                        return byte222;
                    case 223:
                        return byte223;
                    case 224:
                        return byte224;
                    case 225:
                        return byte225;
                    case 226:
                        return byte226;
                    case 227:
                        return byte227;
                    case 228:
                        return byte228;
                    case 229:
                        return byte229;
                    case 230:
                        return byte230;
                    case 231:
                        return byte231;
                    case 232:
                        return byte232;
                    case 233:
                        return byte233;
                    case 234:
                        return byte234;
                    case 235:
                        return byte235;
                    case 236:
                        return byte236;
                    case 237:
                        return byte237;
                    case 238:
                        return byte238;
                    case 239:
                        return byte239;
                    case 240:
                        return byte240;
                    case 241:
                        return byte241;
                    case 242:
                        return byte242;
                    case 243:
                        return byte243;
                    case 244:
                        return byte244;
                    case 245:
                        return byte245;
                    case 246:
                        return byte246;
                    case 247:
                        return byte247;
                    case 248:
                        return byte248;
                    case 249:
                        return byte249;
                    case 250:
                        return byte250;
                    case 251:
                        return byte251;
                    case 252:
                        return byte252;
                    case 253:
                        return byte253;
                    case 254:
                        return byte254;
                    case 255:
                        return byte255;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        byte0 = value;
                        return;
                    case 1:
                        byte1 = value;
                        return;
                    case 2:
                        byte2 = value;
                        return;
                    case 3:
                        byte3 = value;
                        return;
                    case 4:
                        byte4 = value;
                        return;
                    case 5:
                        byte5 = value;
                        return;
                    case 6:
                        byte6 = value;
                        return;
                    case 7:
                        byte7 = value;
                        return;
                    case 8:
                        byte8 = value;
                        return;
                    case 9:
                        byte9 = value;
                        return;
                    case 10:
                        byte10 = value;
                        return;
                    case 11:
                        byte11 = value;
                        return;
                    case 12:
                        byte12 = value;
                        return;
                    case 13:
                        byte13 = value;
                        return;
                    case 14:
                        byte14 = value;
                        return;
                    case 15:
                        byte15 = value;
                        return;
                    case 16:
                        byte16 = value;
                        return;
                    case 17:
                        byte17 = value;
                        return;
                    case 18:
                        byte18 = value;
                        return;
                    case 19:
                        byte19 = value;
                        return;
                    case 20:
                        byte20 = value;
                        return;
                    case 21:
                        byte21 = value;
                        return;
                    case 22:
                        byte22 = value;
                        return;
                    case 23:
                        byte23 = value;
                        return;
                    case 24:
                        byte24 = value;
                        return;
                    case 25:
                        byte25 = value;
                        return;
                    case 26:
                        byte26 = value;
                        return;
                    case 27:
                        byte27 = value;
                        return;
                    case 28:
                        byte28 = value;
                        return;
                    case 29:
                        byte29 = value;
                        return;
                    case 30:
                        byte30 = value;
                        return;
                    case 31:
                        byte31 = value;
                        return;
                    case 32:
                        byte32 = value;
                        return;
                    case 33:
                        byte33 = value;
                        return;
                    case 34:
                        byte34 = value;
                        return;
                    case 35:
                        byte35 = value;
                        return;
                    case 36:
                        byte36 = value;
                        return;
                    case 37:
                        byte37 = value;
                        return;
                    case 38:
                        byte38 = value;
                        return;
                    case 39:
                        byte39 = value;
                        return;
                    case 40:
                        byte40 = value;
                        return;
                    case 41:
                        byte41 = value;
                        return;
                    case 42:
                        byte42 = value;
                        return;
                    case 43:
                        byte43 = value;
                        return;
                    case 44:
                        byte44 = value;
                        return;
                    case 45:
                        byte45 = value;
                        return;
                    case 46:
                        byte46 = value;
                        return;
                    case 47:
                        byte47 = value;
                        return;
                    case 48:
                        byte48 = value;
                        return;
                    case 49:
                        byte49 = value;
                        return;
                    case 50:
                        byte50 = value;
                        return;
                    case 51:
                        byte51 = value;
                        return;
                    case 52:
                        byte52 = value;
                        return;
                    case 53:
                        byte53 = value;
                        return;
                    case 54:
                        byte54 = value;
                        return;
                    case 55:
                        byte55 = value;
                        return;
                    case 56:
                        byte56 = value;
                        return;
                    case 57:
                        byte57 = value;
                        return;
                    case 58:
                        byte58 = value;
                        return;
                    case 59:
                        byte59 = value;
                        return;
                    case 60:
                        byte60 = value;
                        return;
                    case 61:
                        byte61 = value;
                        return;
                    case 62:
                        byte62 = value;
                        return;
                    case 63:
                        byte63 = value;
                        return;
                    case 64:
                        byte64 = value;
                        return;
                    case 65:
                        byte65 = value;
                        return;
                    case 66:
                        byte66 = value;
                        return;
                    case 67:
                        byte67 = value;
                        return;
                    case 68:
                        byte68 = value;
                        return;
                    case 69:
                        byte69 = value;
                        return;
                    case 70:
                        byte70 = value;
                        return;
                    case 71:
                        byte71 = value;
                        return;
                    case 72:
                        byte72 = value;
                        return;
                    case 73:
                        byte73 = value;
                        return;
                    case 74:
                        byte74 = value;
                        return;
                    case 75:
                        byte75 = value;
                        return;
                    case 76:
                        byte76 = value;
                        return;
                    case 77:
                        byte77 = value;
                        return;
                    case 78:
                        byte78 = value;
                        return;
                    case 79:
                        byte79 = value;
                        return;
                    case 80:
                        byte80 = value;
                        return;
                    case 81:
                        byte81 = value;
                        return;
                    case 82:
                        byte82 = value;
                        return;
                    case 83:
                        byte83 = value;
                        return;
                    case 84:
                        byte84 = value;
                        return;
                    case 85:
                        byte85 = value;
                        return;
                    case 86:
                        byte86 = value;
                        return;
                    case 87:
                        byte87 = value;
                        return;
                    case 88:
                        byte88 = value;
                        return;
                    case 89:
                        byte89 = value;
                        return;
                    case 90:
                        byte90 = value;
                        return;
                    case 91:
                        byte91 = value;
                        return;
                    case 92:
                        byte92 = value;
                        return;
                    case 93:
                        byte93 = value;
                        return;
                    case 94:
                        byte94 = value;
                        return;
                    case 95:
                        byte95 = value;
                        return;
                    case 96:
                        byte96 = value;
                        return;
                    case 97:
                        byte97 = value;
                        return;
                    case 98:
                        byte98 = value;
                        return;
                    case 99:
                        byte99 = value;
                        return;
                    case 100:
                        byte100 = value;
                        return;
                    case 101:
                        byte101 = value;
                        return;
                    case 102:
                        byte102 = value;
                        return;
                    case 103:
                        byte103 = value;
                        return;
                    case 104:
                        byte104 = value;
                        return;
                    case 105:
                        byte105 = value;
                        return;
                    case 106:
                        byte106 = value;
                        return;
                    case 107:
                        byte107 = value;
                        return;
                    case 108:
                        byte108 = value;
                        return;
                    case 109:
                        byte109 = value;
                        return;
                    case 110:
                        byte110 = value;
                        return;
                    case 111:
                        byte111 = value;
                        return;
                    case 112:
                        byte112 = value;
                        return;
                    case 113:
                        byte113 = value;
                        return;
                    case 114:
                        byte114 = value;
                        return;
                    case 115:
                        byte115 = value;
                        return;
                    case 116:
                        byte116 = value;
                        return;
                    case 117:
                        byte117 = value;
                        return;
                    case 118:
                        byte118 = value;
                        return;
                    case 119:
                        byte119 = value;
                        return;
                    case 120:
                        byte120 = value;
                        return;
                    case 121:
                        byte121 = value;
                        return;
                    case 122:
                        byte122 = value;
                        return;
                    case 123:
                        byte123 = value;
                        return;
                    case 124:
                        byte124 = value;
                        return;
                    case 125:
                        byte125 = value;
                        return;
                    case 126:
                        byte126 = value;
                        return;
                    case 127:
                        byte127 = value;
                        return;
                    case 128:
                        byte128 = value;
                        return;
                    case 129:
                        byte129 = value;
                        return;
                    case 130:
                        byte130 = value;
                        return;
                    case 131:
                        byte131 = value;
                        return;
                    case 132:
                        byte132 = value;
                        return;
                    case 133:
                        byte133 = value;
                        return;
                    case 134:
                        byte134 = value;
                        return;
                    case 135:
                        byte135 = value;
                        return;
                    case 136:
                        byte136 = value;
                        return;
                    case 137:
                        byte137 = value;
                        return;
                    case 138:
                        byte138 = value;
                        return;
                    case 139:
                        byte139 = value;
                        return;
                    case 140:
                        byte140 = value;
                        return;
                    case 141:
                        byte141 = value;
                        return;
                    case 142:
                        byte142 = value;
                        return;
                    case 143:
                        byte143 = value;
                        return;
                    case 144:
                        byte144 = value;
                        return;
                    case 145:
                        byte145 = value;
                        return;
                    case 146:
                        byte146 = value;
                        return;
                    case 147:
                        byte147 = value;
                        return;
                    case 148:
                        byte148 = value;
                        return;
                    case 149:
                        byte149 = value;
                        return;
                    case 150:
                        byte150 = value;
                        return;
                    case 151:
                        byte151 = value;
                        return;
                    case 152:
                        byte152 = value;
                        return;
                    case 153:
                        byte153 = value;
                        return;
                    case 154:
                        byte154 = value;
                        return;
                    case 155:
                        byte155 = value;
                        return;
                    case 156:
                        byte156 = value;
                        return;
                    case 157:
                        byte157 = value;
                        return;
                    case 158:
                        byte158 = value;
                        return;
                    case 159:
                        byte159 = value;
                        return;
                    case 160:
                        byte160 = value;
                        return;
                    case 161:
                        byte161 = value;
                        return;
                    case 162:
                        byte162 = value;
                        return;
                    case 163:
                        byte163 = value;
                        return;
                    case 164:
                        byte164 = value;
                        return;
                    case 165:
                        byte165 = value;
                        return;
                    case 166:
                        byte166 = value;
                        return;
                    case 167:
                        byte167 = value;
                        return;
                    case 168:
                        byte168 = value;
                        return;
                    case 169:
                        byte169 = value;
                        return;
                    case 170:
                        byte170 = value;
                        return;
                    case 171:
                        byte171 = value;
                        return;
                    case 172:
                        byte172 = value;
                        return;
                    case 173:
                        byte173 = value;
                        return;
                    case 174:
                        byte174 = value;
                        return;
                    case 175:
                        byte175 = value;
                        return;
                    case 176:
                        byte176 = value;
                        return;
                    case 177:
                        byte177 = value;
                        return;
                    case 178:
                        byte178 = value;
                        return;
                    case 179:
                        byte179 = value;
                        return;
                    case 180:
                        byte180 = value;
                        return;
                    case 181:
                        byte181 = value;
                        return;
                    case 182:
                        byte182 = value;
                        return;
                    case 183:
                        byte183 = value;
                        return;
                    case 184:
                        byte184 = value;
                        return;
                    case 185:
                        byte185 = value;
                        return;
                    case 186:
                        byte186 = value;
                        return;
                    case 187:
                        byte187 = value;
                        return;
                    case 188:
                        byte188 = value;
                        return;
                    case 189:
                        byte189 = value;
                        return;
                    case 190:
                        byte190 = value;
                        return;
                    case 191:
                        byte191 = value;
                        return;
                    case 192:
                        byte192 = value;
                        return;
                    case 193:
                        byte193 = value;
                        return;
                    case 194:
                        byte194 = value;
                        return;
                    case 195:
                        byte195 = value;
                        return;
                    case 196:
                        byte196 = value;
                        return;
                    case 197:
                        byte197 = value;
                        return;
                    case 198:
                        byte198 = value;
                        return;
                    case 199:
                        byte199 = value;
                        return;
                    case 200:
                        byte200 = value;
                        return;
                    case 201:
                        byte201 = value;
                        return;
                    case 202:
                        byte202 = value;
                        return;
                    case 203:
                        byte203 = value;
                        return;
                    case 204:
                        byte204 = value;
                        return;
                    case 205:
                        byte205 = value;
                        return;
                    case 206:
                        byte206 = value;
                        return;
                    case 207:
                        byte207 = value;
                        return;
                    case 208:
                        byte208 = value;
                        return;
                    case 209:
                        byte209 = value;
                        return;
                    case 210:
                        byte210 = value;
                        return;
                    case 211:
                        byte211 = value;
                        return;
                    case 212:
                        byte212 = value;
                        return;
                    case 213:
                        byte213 = value;
                        return;
                    case 214:
                        byte214 = value;
                        return;
                    case 215:
                        byte215 = value;
                        return;
                    case 216:
                        byte216 = value;
                        return;
                    case 217:
                        byte217 = value;
                        return;
                    case 218:
                        byte218 = value;
                        return;
                    case 219:
                        byte219 = value;
                        return;
                    case 220:
                        byte220 = value;
                        return;
                    case 221:
                        byte221 = value;
                        return;
                    case 222:
                        byte222 = value;
                        return;
                    case 223:
                        byte223 = value;
                        return;
                    case 224:
                        byte224 = value;
                        return;
                    case 225:
                        byte225 = value;
                        return;
                    case 226:
                        byte226 = value;
                        return;
                    case 227:
                        byte227 = value;
                        return;
                    case 228:
                        byte228 = value;
                        return;
                    case 229:
                        byte229 = value;
                        return;
                    case 230:
                        byte230 = value;
                        return;
                    case 231:
                        byte231 = value;
                        return;
                    case 232:
                        byte232 = value;
                        return;
                    case 233:
                        byte233 = value;
                        return;
                    case 234:
                        byte234 = value;
                        return;
                    case 235:
                        byte235 = value;
                        return;
                    case 236:
                        byte236 = value;
                        return;
                    case 237:
                        byte237 = value;
                        return;
                    case 238:
                        byte238 = value;
                        return;
                    case 239:
                        byte239 = value;
                        return;
                    case 240:
                        byte240 = value;
                        return;
                    case 241:
                        byte241 = value;
                        return;
                    case 242:
                        byte242 = value;
                        return;
                    case 243:
                        byte243 = value;
                        return;
                    case 244:
                        byte244 = value;
                        return;
                    case 245:
                        byte245 = value;
                        return;
                    case 246:
                        byte246 = value;
                        return;
                    case 247:
                        byte247 = value;
                        return;
                    case 248:
                        byte248 = value;
                        return;
                    case 249:
                        byte249 = value;
                        return;
                    case 250:
                        byte250 = value;
                        return;
                    case 251:
                        byte251 = value;
                        return;
                    case 252:
                        byte252 = value;
                        return;
                    case 253:
                        byte253 = value;
                        return;
                    case 254:
                        byte254 = value;
                        return;
                    case 255:
                        byte255 = value;
                        return;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }


    public struct HALControlWord
    {
        private readonly bool m_enabled;
        private readonly bool m_autonomous;
        private readonly bool m_test;
        private readonly bool m_eStop;
        private readonly bool m_fmsAttached;
        private readonly bool m_dsAttached;

        public HALControlWord(bool enabled, bool autonomous, bool test, bool eStop,
            bool fmsAttached, bool dsAttached)
        {
            m_enabled = enabled;
            m_autonomous = autonomous;
            m_test = test;
            m_eStop = eStop;
            m_fmsAttached = fmsAttached;
            m_dsAttached = dsAttached;
        }

        public bool GetEnabled()
        {
            return m_enabled;
        }

        public bool GetAutonomous()
        {
            return m_autonomous;
        }

        public bool GetTest()
        {
            return m_test;
        }

        public bool GetEStop()
        {
            return m_eStop;
        }

        public bool GetFMSAttached()
        {
            return m_fmsAttached;
        }

        public bool GetDSAttached()
        {
            return m_dsAttached;
        }
    }

    #endregion

    #region Accelerometer

    public enum HALAccelerometerRange
    {
        /// kRange_2G -> 0
        Range_2G = 0,

        /// kRange_4G -> 1
        Range_4G = 1,

        /// kRange_8G -> 2
        Range_8G = 2,
    }

    #endregion

    #region Analog
    public enum AnalogTriggerType
    {
        /// kInWindow -> 0
        InWindow = 0,

        /// kState -> 1
        State = 1,

        /// kRisingPulse -> 2
        RisingPulse = 2,

        /// kFallingPulse -> 3
        FallingPulse = 3,
    }
    #endregion

    #region CANTalonSRX
    public enum CTR_Code
    {
        CTR_OKAY,

        CTR_RxTimeout,

        CTR_TxTimeout,

        CTR_InvalidParamValue,

        CTR_UnexpectedArbId,

        CTR_TxFailed,

        CTR_SigNotUpdated,
    }
    #endregion

    #region Digital
    public enum Mode
    {
        /// kTwoPulse -> 0
        TwoPulse = 0,

        /// kSemiperiod -> 1
        Semiperiod = 1,

        /// kPulseLength -> 2
        PulseLength = 2,

        /// kExternalDirection -> 3
        ExternalDirection = 3,
    }
    #endregion

    #region CAN

    [StructLayout(LayoutKind.Sequential)]
    public struct CANStreamMessage
    {
        public readonly uint messageID;
        public readonly uint timeStamp;
        public CANDataArray data;
        public readonly byte dataSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CANDataArray
    {
        private byte data0;
        private byte data1;
        private byte data2;
        private byte data3;
        private byte data4;
        private byte data5;
        private byte data6;
        private byte data7;

        public byte this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return data0;
                    case 1:
                        return data1;
                    case 2:
                        return data2;
                    case 3:
                        return data3;
                    case 4:
                        return data4;
                    case 5:
                        return data5;
                    case 6:
                        return data6;
                    case 7:
                        return data7;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        data0 = value;
                        return;
                    case 1:
                        data1 = value;
                        return;
                    case 2:
                        data2 = value;
                        return;
                    case 3:
                        data3 = value;
                        return;
                    case 4:
                        data4 = value;
                        return;
                    case 5:
                        data5 = value;
                        return;
                    case 6:
                        data6 = value;
                        return;
                    case 7:
                        data7 = value;
                        return;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }


    #endregion
}
