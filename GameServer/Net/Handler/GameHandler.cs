﻿using Server.Common.Constants;
using Server.Common.Data;
using Server.Common.IO;
using Server.Common.IO.Packet;
using Server.Ghost;
using Server.Ghost.Accounts;
using Server.Ghost.Characters;
using Server.Ghost.Provider;
using Server.Net;
using Server.Packet;
using System;
using System.Collections.Generic;

namespace Server.Handler
{
    public static class GameHandler
    {
       

        public static void Game_Log_Req(InPacket lea, Client gc)
        {

            string[] data = lea.ReadString(0x100).Split(new[] { (char)0x20 }, StringSplitOptions.None);

            string[] data1 = lea.ReadString(50).Split(new[] { (char)0x20 }, StringSplitOptions.None);


            //    string character_req = BitConverter.ToString(req_hex);
            //int re = SearchBytes(lea.Content, new byte[] { 0x5B });
            //GamePacket.Game_login_Ack(gc);
            //System.Threading.Thread.Sleep(250);
            //GamePacket.Game_login1_Ack(gc);
            //System.Threading.Thread.Sleep(250);
            //GamePacket.Game_login2_ack(gc);
            //System.Threading.Thread.Sleep(250);
            //GamePacket.Game_login3_Ack(gc);
            //System.Threading.Thread.Sleep(250);
            //GamePacket.Game_login4_Ack(gc);
            //System.Threading.Thread.Sleep(250);
            int encryptKey = int.Parse(data[1]);
          
            
            string username = data[2];
            string password = data[4];
            int selectCharacter = lea.ReadByte();
            System.Net.IPAddress hostid = lea.ReadIPAddress();
            Log.Debug(" Username : {0} ", username);
            Log.Debug(" Password : {0} ", password);
            Log.Debug(" selectCharacter : {0} ", selectCharacter);
            Log.Debug(" hostid : {0} ", hostid);


         //   string CharacterReqName = data1[0] + data1[1];
           // Log.Debug("Request Character Name : {0}", CharacterReqName);

            using (System.IO.StreamWriter writefile =
            new System.IO.StreamWriter(@"C:\EvoziGame\GhostOnline\UserData\login.txt", true))
            {
                writefile.Write("[{3}]  UserName: {0}  Character: {1}  IP: {2}", username, selectCharacter, hostid, DateTime.Now);
                writefile.WriteLine("");

            }

            //    Log.Debug("Request Character POS : {0}", poschr);




            gc.SetAccount(new Account(gc));

            try
            {
                gc.Account.Load(username);
                var pe = new Common.Security.PasswordEncrypt(encryptKey);
              //  string encryptPassword = ServerConstants.PASSWORD_DECODE ? pe.encrypt(gc.Account.Password, gc.RetryLoginCount > 0 ? password.ToCharArray() : null) : gc.Account.Password;

                if (!password.Equals(password))  //default encyptPassword
                {
                    gc.Dispose();
                    Log.Error("Login Fail!");
                }
                else
                {
                    gc.Account.Characters = new List<Character>();
                   
                    foreach (dynamic datum in new Datums("characters").PopulateWith("id", "accountId = '{0}' ORDER BY position ASC", gc.Account.ID))
                    {
                        Log.Debug("Character ID: {0}", datum.id);
                        Character character = new Character(datum.id, gc);
                        character.Load(false);
                        character.IP = hostid;
                        gc.Account.Characters.Add(character);
                    }
                      gc.SetCharacter(gc.Account.Characters[selectCharacter]);

                }
                Log.Inform("Password = {0}", password);
                //Log.Inform("encryptKey = {0}", encryptKey);
                //Log.Inform("encryptPassword = {0}", encryptPassword);
            }
            catch (NoAccountException)
            {
                gc.Dispose();
                Log.Error("Login Fail!");
            }

            Character chr = gc.Character;
            chr.CharacterID = gc.CharacterID;
            GamePacket.Game_LoginStatus(gc);
            GamePacket.Game_ServerTime(gc);
            GamePacket.Game_FristLoad_ACK(gc);
            GamePacket.Game_LOAD_2(gc);
            GamePacket.Game_LOAD_3(gc);
            GamePacket.Game_LOAD_4(gc);
            GamePacket.Game_LOAD_5(gc);
            GamePacket.Game_AvartarJarItem(gc);
            GamePacket.Game_LOAD_7(gc);
            //     StatusPacket.getStatusInfo(gc);
            GamePacket.Game_CHARALL(gc);
            GamePacket.Game_FURY(gc);
            GamePacket.Game_LOAD_471(gc);
            GamePacket.Game_LOAD_471(gc);
            for (int i = 1; i < 7; i++)
            {
               
                for (int j = 0; j < 2; j++)
                {
                    GamePacket.Game_LOAD_431(gc, i, j);
                }
            }

            GamePacket.Game_LOAD_420(gc);
            GamePacket.Game_LOAD_421(gc);
            GamePacket.Game_LOAD_461(gc);
            GamePacket.Game_LOAD_462(gc);
            GamePacket.getQuickSlot(gc, chr.Keymap);
            GamePacket.Game_LOAD_26(gc);
            GamePacket.Game_LOAD_8D(gc);
            GamePacket.Game_LOAD_50(gc);
            GamePacket.Game_LOAD_63(gc);
            GamePacket.Game_LOAD_64(gc);
            GamePacket.Game_LOAD_73(gc);
            GamePacket.Game_LOAD_02(gc);
            GamePacket.Game_LOAD_DB(gc);
            GamePacket.Game_LOAD_42(gc);
            GamePacket.Game_LOAD_79(gc);
            GamePacket.getQuickSlot(gc, chr.Keymap);
            StoragePacket.getStoreInfo(gc);
            // StoragePacket.getStoreMoney(gc);
            GamePacket.Game_LOAD_AB(gc);
            GamePacket.Game_LOAD_1C(gc);
            InventoryPacket.getInvenCash(gc);
            GamePacket.Game_LOAD_81(gc);
            GamePacket.Game_LOAD_E7(gc);
            GamePacket.Game_LOAD_F2(gc);
            GamePacket.Game_LOAD_A5(gc);
            GamePacket.Game_LOAD_1B(gc);
            GamePacket.Game_LOAD_6D(gc);

            //Game_AvartarJarItem
            //    GamePacket.Game_login2_ack(gc);

            //  MapFactory.AllCharacters.Add(chr);
            // StatusPacket.UpdateHpMp(gc, 0, 0, 0, 0);
            //   GamePacket.FW_DISCOUNTFACTION(gc);
            //    StatusPacket.getStatusInfo(gc);
            //    InventoryPacket.getCharacterEquip(gc);
            //SkillPacket.getSkillInfo(gc, chr.Skills.getSkills());
            //QuestPacket.getQuestInfo(gc, chr.Quests.getQuests());
            //GamePacket.getQuickSlot(gc, chr.Keymap);
            //StoragePacket.getStoreInfo(gc);
            //StoragePacket.getStoreMoney(gc);
            //MapPacket.enterMapStart(gc);
            //InventoryPacket.getInvenCash(gc);
            //CashShopPacket.MgameCash(gc);
            //CashShopPacket.GuiHonCash(gc);
            //InventoryPacket.getInvenEquip(gc);
            //InventoryPacket.getInvenEquip1(gc);
            //InventoryPacket.getInvenEquip2(gc);
            //InventoryPacket.getInvenSpend3(gc);
            //InventoryPacket.getInvenOther4(gc);
            //InventoryPacket.getInvenPet5(gc);
        }

        private static int SearchBytes(byte[] content, byte[] v)
        {
            throw new NotImplementedException();
        }

        public static void Character_Info_Req(InPacket lea, Client gc)
        {
          
           

        }
        public static void Command_Req(InPacket lea, Client gc)
        {
            string[] cmd = lea.ReadString(60).Split(new[] { (char)0x20 }, StringSplitOptions.None);

            if (gc.Account.Master == 0 || cmd.Length < 1)
                return;
            var chr = gc.Character;
            Character victim = null;

            switch (cmd[0])
            {
                case "//1":
                case "//公告":
                case "//notice":
                    if (cmd.Length != 2)
                        break;
                    foreach (Character all in MapFactory.AllCharacters)
                    {
                        GamePacket.getNotice(all.Client, 3, cmd[1]);
                    }
                    break;
                case "//item":
                    if (cmd.Length != 2 && cmd.Length != 3)
                        break;

                    short Quantity = 1;

                    if (cmd.Length == 3)
                    {
                        if (int.Parse(cmd[2]) > 100)
                            Quantity = 100;
                        else
                            Quantity = short.Parse(cmd[2]);
                    }

                    if (InventoryType.getItemType(int.Parse(cmd[1])) == 1 || InventoryType.getItemType(int.Parse(cmd[1])) == 2)
                        Quantity = 1;

                    if (InventoryType.getItemType(int.Parse(cmd[1])) == 5)
                        return;

                    chr.Items.Add(new Item(int.Parse(cmd[1]), InventoryType.getItemType(int.Parse(cmd[1])), chr.Items.GetNextFreeSlot((InventoryType.ItemType)InventoryType.getItemType(int.Parse(cmd[1]))), Quantity));
                    InventoryHandler.UpdateInventory(gc, InventoryType.getItemType(int.Parse(cmd[1])));
                    break;
                case "//money":
                    if (cmd.Length != 2)
                        break;
                    chr.Money = int.Parse(cmd[1]);
                    InventoryPacket.getInvenMoney(gc, chr.Money, int.Parse(cmd[1]));
                    break;
                case "//levelup":
                    chr.LevelUp();
                    break;
                case "//gogo":
                    if (cmd.Length != 3)
                        break;
                    MapPacket.warpToMapAuth(gc, true, short.Parse(cmd[1]), short.Parse(cmd[2]), -1, -1);
                    break;
                case "//hp":
                    if (cmd.Length != 2)
                        break;

                    short Hp = short.Parse(cmd[1]);

                    if (Hp > short.MaxValue)
                        Hp = short.MaxValue;

                    chr.MaxHp = Hp;
                    chr.Hp = Hp;
                    StatusPacket.getStatusInfo(gc);
                    break;
                case "//mp":
                    short Mp = short.Parse(cmd[1]);

                    if (Mp > short.MaxValue)
                        Mp = short.MaxValue;

                    chr.MaxMp = Mp;
                    chr.Mp = Mp;
                    StatusPacket.getStatusInfo(gc);
                    break;
                case "//heal":
                    chr.Hp = chr.MaxHp;
                    chr.Mp = chr.MaxMp;
                    chr.Fury = chr.MaxFury;
                    StatusPacket.UpdateHpMp(gc, chr.Hp, chr.Mp, chr.Fury, chr.MaxFury);
                    break;
                case "//warp":
                    if (cmd.Length != 2)
                        break;
                    foreach (Character find in MapFactory.AllCharacters)
                    {
                        if (find.Name.Equals(cmd[1]))
                        {
                            victim = find;
                        }
                    }
                    if (victim != null)
                    {
                        chr.MapX = victim.MapX;
                        chr.MapY = victim.MapY;
                        chr.PlayerX = victim.PlayerX;
                        chr.PlayerY = victim.PlayerY;
                        MapPacket.warpToMapAuth(gc, true, chr.MapX, chr.MapY, chr.PlayerX, chr.PlayerY);
                    }
                    break;
                case "//ban":
                    if (cmd.Length != 2)
                        break;
                    foreach (Character find in MapFactory.AllCharacters)
                    {
                        if (find.Name.Equals(cmd[1]))
                        {
                            victim = find;
                        }
                    }
                    if (victim != null)
                    {
                        dynamic datum = new Datum("accounts");
                        victim.Client.Account.Banned = 7;
                        victim.Client.Dispose();
                    }
                    break;
                case "//save":
                    for (int i = 0; i < MapFactory.AllCharacters.Count; i++)
                    {
                        if (chr.CharacterID == MapFactory.AllCharacters[i].CharacterID)
                            continue;
                        MapFactory.AllCharacters[i].Client.Dispose();
                    }
                    //GameServer.IsAlive = false;
                    break;
                case "//skillhack":
                case "//come":

                case "//選擇正派":
                    Quest Quest = new Quest(60);
                    Quest.QuestState = 0x31;
                    chr.Quests.Add(Quest);
                    QuestPacket.getQuestInfo(gc, chr.Quests.getQuests());
                    chr.Items.Add(new Item(8990019, 4, chr.Items.GetNextFreeSlot(InventoryType.ItemType.Other4)));
                    break;
                case "//選擇邪派":
                    Quest = new Quest(64);
                    Quest.QuestState = 0x31;
                    chr.Quests.Add(Quest);
                    QuestPacket.getQuestInfo(gc, chr.Quests.getQuests());
                    chr.Items.Add(new Item(8990020, 4, chr.Items.GetNextFreeSlot(InventoryType.ItemType.Other4)));
                    break;
                //case "//test":
                //    PartyPacket.PartyInvite(gc);
                //    break;
                //case "//test2":
                //    PartyPacket.PartyInvite(gc, 1, 1);
                //    break;
                //case "//test3":
                //    PartyPacket.PartyInvite(gc, 1 , 0);
                //    break;
                default:
                    break;
            }
        }

        public static void Quick_Slot_Req(InPacket lea, Client gc)
        {
            var chr = gc.Character;
            int KeymapType = lea.ReadShort();
            int KeymapSlot = lea.ReadShort();
            int SkillID = lea.ReadInt();
            int SkillType = lea.ReadShort();
            int SkillSlot = lea.ReadShort();

            string QuickSlotName = "";
            switch (KeymapType)
            {
                case 0:
                    switch (KeymapSlot)
                    {
                        case 0:
                            QuickSlotName = "Z";
                            break;
                        case 1:
                            QuickSlotName = "X";
                            break;
                        case 2:
                            QuickSlotName = "C";
                            break;
                        case 3:
                            QuickSlotName = "V";
                            break;
                        case 4:
                            QuickSlotName = "B";
                            break;
                        case 5:
                            QuickSlotName = "N";
                            break;
                    }
                    break;
                case 1:
                    switch (KeymapSlot)
                    {
                        case 0:
                            QuickSlotName = "1";
                            break;
                        case 1:
                            QuickSlotName = "2";
                            break;
                        case 2:
                            QuickSlotName = "3";
                            break;
                        case 3:
                            QuickSlotName = "4";
                            break;
                        case 4:
                            QuickSlotName = "5";
                            break;
                        case 5:
                            QuickSlotName = "6";
                            break;
                    }
                    break;
                case 2:
                    switch (KeymapSlot)
                    {
                        case 0:
                            QuickSlotName = "Insert";
                            break;
                        case 1:
                            QuickSlotName = "Home";
                            break;
                        case 2:
                            QuickSlotName = "PageUp";
                            break;
                        case 3:
                            QuickSlotName = "Delete";
                            break;
                        case 4:
                            QuickSlotName = "End";
                            break;
                        case 5:
                            QuickSlotName = "PageDown";
                            break;
                    }
                    break;
                case 3:
                    switch (KeymapSlot)
                    {
                        case 0:
                            QuickSlotName = "7";
                            break;
                        case 1:
                            QuickSlotName = "8";
                            break;
                        case 2:
                            QuickSlotName = "9";
                            break;
                        case 3:
                            QuickSlotName = "0";
                            break;
                        case 4:
                            QuickSlotName = "-";
                            break;
                        case 5:
                            QuickSlotName = "=";
                            break;
                    }
                    break;
            }
            if (SkillID == -1 && SkillType == -1 && SkillSlot == -1)
            {
                chr.Keymap.Remove(QuickSlotName);
                return;
            }
            chr.Keymap.Remove(QuickSlotName);
            chr.Keymap.Add(QuickSlotName, new Shortcut(SkillID, (byte)SkillType, (byte)SkillSlot));
        }

        //private static int SearchBytes(byte[] haystack, byte[] needle)
        //{
        //    var len = needle.Length;
        //    var limit = haystack.Length - len;
        //    for (var i = 0; i <= limit; i++)
        //    {
        //        var k = 0;
        //        for (; k < len; k++)
        //        {
        //            if (needle[k] != haystack[i + k]) break;
        //        }
        //        if (k == len) return i;
        //    }
        //    return -1;
        //}
    }
}