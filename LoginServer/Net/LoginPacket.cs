﻿using Server.Common.Constants;
using Server.Common.IO;
using Server.Common.IO.Packet;
using Server.Common.Net;

namespace Server.Ghost
{
    public static class LoginPacket
    {
        public static void GameVersionInfoAck(Client c)
        {
            using (var plew = new OutPacket())
            {
                plew.WriteHexString("AA 55 2F 00 11"); // Packet Header
                plew.WriteInt(2020030403); // Patch Version 2020 / 03 / 03 / 03 --> V
                plew.WriteHexString("00 00 00 00");
                plew.WriteString("http://ghost.pleum.in.th/");
                plew.WriteString("file/test");
                plew.WriteHexString("55 AA"); //END Packet
                c.SendCustom(plew);
            }
        }



        /* NetCafe
         * 會員於特約網咖連線
         */
        public static void Login_Ack(Client c, ServerState.LoginState state, short encryptKey = 0, bool netCafe = false)
        {
            using (var plew = new OutPacket(LoginServerOpcode.LOGIN_ACK))
            {

                int LoginCase;
                if(c.Account.Master == 1 && c.Account.TwoFA == 1)
                {
                    LoginCase = 1;
                }else if(c.Account.Master == 0 && c.Account.TwoFA == 1)
                {
                    LoginCase = 2;
                }
                else if (c.Account.Master == 1 && c.Account.TwoFA == 0)
                {
                    LoginCase = 3;
                }
                else if (c.Account.Master == 0 && c.Account.TwoFA == 0)
                {
                    LoginCase = 4;
                }else
                {

                    LoginCase = 5;
                }

                plew.WriteByte((byte)state);
                plew.WriteHexString("00");
                // plew.WriteInt(isGM); // Send GM Status
                switch (LoginCase)
                {
                    case 1:
                        plew.WriteHexString("01 01");
                        break;
                    case 2:
                        plew.WriteHexString("00 01");
                        break;
                    case 3:
                        plew.WriteHexString("01 00");
                        break;
                    case 4:
                        plew.WriteHexString("00 00");
                        break;
                    case 5:
                        plew.WriteHexString("00 00");
                        break;
                    default:
                        break;
                }
                plew.WriteBytes(new byte[]
                    {0x00,0x00,0x00,0x00,0x00,0x00,0x00, 0x00, 0x00, 0x00});
                //plew.WriteBool(netCafe);
                //plew.WriteShort(encryptKey);
                c.Send(plew);
            }
        }

        public static void ServerList_Ack(Client c)
        {
            using (var plew = new OutPacket(LoginServerOpcode.SERVERLIST_ACK))
            {
                plew.WriteBytes(new byte[]
                {
                    0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    0xFF, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x12, 0x00, 0x00, 0x00, 0x01,
    0x00, 0x01, 0x00, 0x0E, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30, 0x30, 0x30,
    0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30, 0x31, 0xFD, 0x3A, 0x00, 0x00, 0x83,
    0x00, 0x00, 0x00, 0x2C, 0x01, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x01, 0x5F, 0x3B, 0x00, 0x00, 0x02, 0x00, 0x02, 0x00,
    0x0E, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30, 0x30,
    0x30, 0x2E, 0x30, 0x31, 0xFE, 0x3A, 0x00, 0x00, 0x0E, 0x00, 0x00, 0x00,
    0x64, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x01, 0x5E, 0x3B, 0x00, 0x00, 0x03, 0x00, 0x03, 0x00, 0x0E, 0x00, 0x31,
    0x32, 0x37, 0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30,
    0x31, 0xFD, 0x3A, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00,
    0x00, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x5F, 0x3B,
    0x00, 0x00, 0x04, 0x00, 0x04, 0x00, 0x0E, 0x00, 0x31, 0x32, 0x37, 0x2E,
    0x30, 0x30, 0x30, 0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30, 0x31, 0xFE, 0x3A,
    0x00, 0x00, 0x14, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x0C, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x5E, 0x3B, 0x00, 0x00, 0x05,
    0x00, 0x05, 0x00, 0x0E, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30, 0x30, 0x30,
    0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30, 0x31, 0xFF, 0x3A, 0x00, 0x00, 0x05,
    0x00, 0x00, 0x00, 0x2C, 0x01, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x01, 0x5D, 0x3B, 0x00, 0x00, 0x06, 0x00, 0x06, 0x00,
    0x0E, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30, 0x30,
    0x30, 0x2E, 0x30, 0x31, 0xFD, 0x3A, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00,
    0x2C, 0x01, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x01, 0x5F, 0x3B, 0x00, 0x00, 0x07, 0x00, 0x07, 0x00, 0x0E, 0x00, 0x31,
    0x32, 0x37, 0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30,
    0x31, 0xFE, 0x3A, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00,
    0x00, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x5E, 0x3B,
    0x00, 0x00, 0x08, 0x00, 0x08, 0x00, 0x0E, 0x00, 0x31, 0x32, 0x37, 0x2E,
    0x30, 0x30, 0x30, 0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30, 0x31, 0xFF, 0x3A,
    0x00, 0x00, 0x05, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x0C, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x5D, 0x3B, 0x00, 0x00, 0x09,
    0x00, 0x09, 0x00, 0x0F, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30, 0x30, 0x30,
    0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30, 0x30, 0x31, 0xFE, 0x3A, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x5E, 0x3B, 0x00, 0x00, 0x0A, 0x00, 0x0A,
    0x00, 0x0D, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30, 0x30, 0x30, 0x2E, 0x30,
    0x30, 0x30, 0x2E, 0x31, 0xFE, 0x3A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x90, 0x01, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x00, 0x5E, 0x3B, 0x00, 0x00, 0x0B, 0x00, 0x0B, 0x00, 0x09, 0x00, 0x31,
    0x32, 0x37, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x31, 0x00, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x0C,
    0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x31,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00,
    0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
    0x00, 0x0D, 0x00, 0x0D, 0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30,
    0x2E, 0x30, 0x2E, 0x31, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x64, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x02, 0x00, 0x00, 0x00, 0x00, 0x0E, 0x00, 0x0E, 0x00, 0x09, 0x00, 0x31,
    0x32, 0x37, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x31, 0x00, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x00, 0x0F,
    0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x31,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00,
    0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
    0x00, 0x10, 0x00, 0x10, 0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30,
    0x2E, 0x30, 0x2E, 0x31, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x64, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    0x02, 0x00, 0x00, 0x00, 0x00, 0x11, 0x00, 0x11, 0x00, 0x09, 0x00, 0x31,
    0x32, 0x37, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x31, 0x00, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x12, 0x00, 0x12,
    0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x31,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00,
    0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
    0x00
                    //0xff, 0xff, 0xff,
                    //0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    //0xff, 0xff, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x12, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00,
                    //0x0e, 0x00, 0x31, 0x31, 0x39, 0x2e, 0x32, 0x30,
                    //0x35, 0x2e, 0x31, 0x39, 0x39, 0x2e, 0x33, 0x36,
                    //0xfd, 0x3a, 0x00, 0x00, 0x83, 0x00, 0x00, 0x00,
                    //0x2c, 0x01, 0x00, 0x00, 0x0c, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x01, 0x5f, 0x3b, 0x00,
                    //0x00, 0x02, 0x00, 0x02, 0x00, 0x0e, 0x00, 0x31,
                    //0x31, 0x39, 0x2e, 0x32, 0x30, 0x35, 0x2e, 0x31,
                    //0x39, 0x39, 0x2e, 0x33, 0x36, 0xfe, 0x3a, 0x00,
                    //0x00, 0x0e, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00,
                    //0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x01, 0x5e, 0x3b, 0x00, 0x00, 0x03, 0x00,
                    //0x03, 0x00, 0x0e, 0x00, 0x31, 0x31, 0x39, 0x2e,
                    //0x32, 0x30, 0x35, 0x2e, 0x31, 0x39, 0x39, 0x2e,
                    //0x33, 0x37, 0xfd, 0x3a, 0x00, 0x00, 0x10, 0x00,
                    //0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x0c, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x5f,
                    //0x3b, 0x00, 0x00, 0x04, 0x00, 0x04, 0x00, 0x0e,
                    //0x00, 0x31, 0x31, 0x39, 0x2e, 0x32, 0x30, 0x35,
                    //0x2e, 0x31, 0x39, 0x39, 0x2e, 0x33, 0x37, 0xfe,
                    //0x3a, 0x00, 0x00, 0x14, 0x00, 0x00, 0x00, 0x64,
                    //0x00, 0x00, 0x00, 0x0c, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x01, 0x5e, 0x3b, 0x00, 0x00,
                    //0x05, 0x00, 0x05, 0x00, 0x0e, 0x00, 0x31, 0x31,
                    //0x39, 0x2e, 0x32, 0x30, 0x35, 0x2e, 0x31, 0x39,
                    //0x39, 0x2e, 0x33, 0x37, 0xff, 0x3a, 0x00, 0x00,
                    //0x05, 0x00, 0x00, 0x00, 0x2c, 0x01, 0x00, 0x00,
                    //0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x01, 0x5d, 0x3b, 0x00, 0x00, 0x06, 0x00, 0x06,
                    //0x00, 0x0e, 0x00, 0x31, 0x31, 0x39, 0x2e, 0x32,
                    //0x30, 0x35, 0x2e, 0x31, 0x39, 0x39, 0x2e, 0x33,
                    //0x38, 0xfd, 0x3a, 0x00, 0x00, 0x11, 0x00, 0x00,
                    //0x00, 0x2c, 0x01, 0x00, 0x00, 0x0c, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x5f, 0x3b,
                    //0x00, 0x00, 0x07, 0x00, 0x07, 0x00, 0x0e, 0x00,
                    //0x31, 0x31, 0x39, 0x2e, 0x32, 0x30, 0x35, 0x2e,
                    //0x31, 0x39, 0x39, 0x2e, 0x33, 0x38, 0xfe, 0x3a,
                    //0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x64, 0x00,
                    //0x00, 0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x01, 0x5e, 0x3b, 0x00, 0x00, 0x08,
                    //0x00, 0x08, 0x00, 0x0e, 0x00, 0x31, 0x31, 0x39,
                    //0x2e, 0x32, 0x30, 0x35, 0x2e, 0x31, 0x39, 0x39,
                    //0x2e, 0x33, 0x38, 0xff, 0x3a, 0x00, 0x00, 0x05,
                    //0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x0c,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
                    //0x5d, 0x3b, 0x00, 0x00, 0x09, 0x00, 0x09, 0x00,
                    //0x0f, 0x00, 0x31, 0x31, 0x39, 0x2e, 0x32, 0x30,
                    //0x35, 0x2e, 0x31, 0x39, 0x39, 0x2e, 0x32, 0x34,
                    //0x38, 0xfe, 0x3a, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x90, 0x01, 0x00, 0x00, 0x0c, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x5e, 0x3b,
                    //0x00, 0x00, 0x0a, 0x00, 0x0a, 0x00, 0x0d, 0x00,
                    //0x31, 0x39, 0x32, 0x2e, 0x31, 0x36, 0x38, 0x2e,
                    //0x31, 0x32, 0x2e, 0x33, 0x38, 0xfe, 0x3a, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x90, 0x01, 0x00,
                    //0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x5e, 0x3b, 0x00, 0x00, 0x0b, 0x00,
                    //0x0b, 0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2e,
                    //0x30, 0x2e, 0x30, 0x2e, 0x31, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x90, 0x01, 0x00,
                    //0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x0c, 0x00,
                    //0x0c, 0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2e,
                    //0x30, 0x2e, 0x30, 0x2e, 0x31, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x90, 0x01, 0x00,
                    //0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x0d, 0x00,
                    //0x0d, 0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2e,
                    //0x30, 0x2e, 0x30, 0x2e, 0x31, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00,
                    //0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x0e, 0x00,
                    //0x0e, 0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2e,
                    //0x30, 0x2e, 0x30, 0x2e, 0x31, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00,
                    //0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x0f, 0x00,
                    //0x0f, 0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2e,
                    //0x30, 0x2e, 0x30, 0x2e, 0x31, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00,
                    //0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00,
                    //0x10, 0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2e,
                    //0x30, 0x2e, 0x30, 0x2e, 0x31, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00,
                    //0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x11, 0x00,
                    //0x11, 0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2e,
                    //0x30, 0x2e, 0x30, 0x2e, 0x31, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00,
                    //0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x12, 0x00,
                    //0x12, 0x00, 0x09, 0x00, 0x31, 0x32, 0x37, 0x2e,
                    //0x30, 0x2e, 0x30, 0x2e, 0x31, 0x00, 0x00, 0x00,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00,
                    //0x00, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    //0x00, 0x02, 0x00, 0x00, 0x00, 0x00,
                });

                //for (int i = 0; i < 13; i++)
                //{
                //    plew.WriteByte(0xFF);
                //}
                //plew.WriteInt(LoginServer.Worlds.Count); // 伺服器數量
                //foreach (World world in LoginServer.Worlds)
                //{
                //    plew.WriteShort(world.ID); // 伺服器順序
                //    plew.WriteInt(world.Channel); // 頻道數量

                //    for (int i = 0; i < 18; i++)
                //    {
                //        plew.WriteShort(i + 1);
                //        plew.WriteShort(i + 1);
                //        plew.WriteString(ServerConstants.SERVER_IP);
                //        plew.WriteInt(15101 + i);
                //        plew.WriteInt(i < world.Count ? world[i].LoadProportion : 0); // 玩家數量
                //        plew.WriteInt(ServerConstants.CHANNEL_LOAD); // 頻道人數上限
                //        plew.WriteInt(1); // 標章類型
                //        plew.WriteInt(0);
                //        plew.WriteByte(i < world.Count ? 1 : 2);
                //        plew.WriteInt(15199);
                //    }
                //}

                c.Send(plew);
            }
        }

        public static void Game_Ack(Client c, ServerState.ChannelState state)
        {
            using (var plew = new OutPacket(LoginServerOpcode.GAME_ACK))
            {
                plew.WriteByte((byte)state);
                plew.WriteString(ServerConstants.SERVER_IP);
                plew.WriteInt(15101 + c.World.ID);
                plew.WriteInt(ServerConstants.UDP_PORT);

                c.Send(plew);
            }
        }

        public static void SubPassError(Client c)
        {
            using(var plew = new OutPacket(LoginServerOpcode.SubPasswordACK))
            {
                plew.WriteHexString("00 00 00 00 00 00 00 00 ");
                c.Send(plew);
            }
        }

        public static void SubPassLoginOK(Client c)
        {
            using (var plew = new OutPacket(LoginServerOpcode.SubPasswordACK))
            {
                plew.WriteHexString("01 00 00 00 01 00 00 00");
                c.Send(plew);
            }
        }
        public static void SubPassLoginWrong(Client c)
        {
            using (var plew = new OutPacket(LoginServerOpcode.SubPasswordACK))
            {
                plew.WriteHexString("01 00 00 00 00 00 00 00");
                c.Send(plew);
            }
        }
        public static void SubPassAddOK(Client c)
        {
            using (var plew = new OutPacket(LoginServerOpcode.SubPasswordACK))
            {
                plew.WriteHexString("00 00 00 00 01 00 00 00");
                c.Send(plew);
            }
        }
        public static void World_Ack(Client c)
        {
            using (var plew = new OutPacket(LoginServerOpcode.WORLD_ACK))
            {
                try
                {
                    plew.WriteString("127.0.0.1"); // 219.83.162.27
                    plew.WriteString("15010");
                    plew.WriteString("127.0.0.1"); // 219.83.162.27
                    plew.WriteString("15111");
                    //plew.WriteBytes(new byte[] { 0x00, 0x00, 0x00, 0xBB, 0x19, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                }
                catch
                {
                }

                c.Send(plew);
            }
        }
    }
}