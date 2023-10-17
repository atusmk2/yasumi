using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System.IO;
using yasumi.Common;

namespace yasumi.Items
{
		public class yasumiUpgradeDefense : GlobalItem {
		public override bool InstancePerEntity => true;
		public int defenseplus;
		internal int defUp;
		public bool CheckAccessories(Item Item) {return (Item.accessory == true);}
		public bool CheckArmor(Item Item) {return (Item.headSlot != -1 || Item.bodySlot != -1 || Item.legSlot != -1);}
		public bool DefUpgrader() {
			if (Main.mouseItem.type == ModContent.ItemType<DefenseUP>()) {
				defUp = 1;
				return true;
			}
			return false;
		}
		public bool Resetter() {
			if (Main.mouseItem.type == ModContent.ItemType<Resetter>()) {
				return true;
			}
			return false;
		}
		public override bool CanRightClick(Item Item)
		{
			if ((CheckAccessories(Item) || CheckArmor(Item)) && DefUpgrader()) {return true;}
			if ((CheckAccessories(Item) || CheckArmor(Item)) && defenseplus != 0 && Resetter()) {return true;}
			return false;
		}
		public override void RightClick(Item Item, Player player)
		{
			if ((CheckArmor(Item) || CheckAccessories(Item)) && DefUpgrader()) {
				defenseplus += defUp;
				Item.stack++;
				Main.mouseItem.stack--;
			}
			if ((CheckArmor(Item) || CheckAccessories(Item)) && Resetter()) {
				player.QuickSpawnItem(player.GetSource_Misc("drop"), ModContent.ItemType<DefenseUP>(), defenseplus);
				defenseplus -= defenseplus;
				Item.stack++;
				Main.mouseItem.stack--;
			}
		}
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if ((CheckArmor(item) || CheckAccessories(item)) && defenseplus > 0) {
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
			if (defenseplus == 0) {
				Item.defense = Item.OriginalDefense;
			}
		}
		public override void SaveData(Item item, TagCompound tag)
		{
			if (defenseplus > 0) {
				tag["defenseplus"] = defenseplus;
			}
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