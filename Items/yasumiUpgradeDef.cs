using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System.IO;

namespace yasumi.Items
{
		public class yasumiUpgradeDefense : GlobalItem {
		public override bool InstancePerEntity => true;
		
		public int defenseplus;
		public int defUp;
		public bool CheckAcc(Item Item) {return (Item.accessory == true);}
		public bool CheckArm(Item Item) {return (Item.headSlot != -1 || Item.bodySlot != -1 || Item.legSlot != -1);}
		public bool DefUpgrader() {
			if (Main.mouseItem.type == ModContent.ItemType<DefenseUP>()) {
				defUp = 1;
				return true;
			}
			return false;
		}
		public int LvLimit() {
			if (!Main.hardMode) {return 5;}
			if (Main.hardMode) {return 10;}
			else return 0;
		}
		public bool ArmorLimit() {
			if (!Main.hardMode && ((DefUpgrader() && defenseplus < LvLimit()))) {return true;}
			if (Main.hardMode && ((DefUpgrader() && defenseplus < LvLimit()))) {return true;}
			if (NPC.downedMoonlord && DefUpgrader()) {return true;}
			return false;
		}
		public override bool CanRightClick(Item Item)
		{
			if ((CheckAcc(Item) || CheckArm(Item)) && ArmorLimit()) {return true;}
			return false;
		}
		public override void RightClick(Item Item, Player player)
		{
			if ((CheckArm(Item) || CheckAcc(Item)) && DefUpgrader()) {
				defenseplus += defUp;
				Item.stack++;
				Main.mouseItem.stack--;
			}
		}
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if ((CheckArm(item) || CheckAcc(item)) && defenseplus > 0) {
				var line = new TooltipLine(Mod, "yasumi", $"[i:{ModContent.ItemType<DefenseUP>()}] [c/92f892:Defense +{defenseplus}]");
				var line2 = new TooltipLine(Mod, "yasumi", "[c/ffeb3b:*Stats will be updated upon being worn.]");
				tooltips.Add(line);
				tooltips.Add(line2);
			}
		}		
		public override void UpdateEquip(Item Item, Player player)
		{
			if (defenseplus > 0) {
				Item.defense = Item.OriginalDefense + defenseplus;
			}
		}
		public override void SaveData(Item item, TagCompound tag)
		{
			tag["defenseplus"] = defenseplus;
		}
		public override void LoadData(Item item, TagCompound tag)
		{
			if (tag["defenseplus"] != null) {
				defenseplus = (int) tag["defenseplus"];
			}
		}
		public override void NetSend(Item item, BinaryWriter writer)
		{
			writer.Write7BitEncodedInt(defenseplus);
		}
		public override void NetReceive(Item item, BinaryReader reader)
		{
			defenseplus = reader.Read7BitEncodedInt();
		}
	}
}