using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using yasumi.Items;

namespace yasumi
{
	public class yasumiGlobalDrop : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
			if (!NPCID.Sets.CountsAsCritter[npc.type] && !NPCID.Sets.ProjectileNPC[npc.type] && !npc.townNPC) {
				npcLoot.Add(ItemDropRule.OneFromOptions(500, ModContent.ItemType<AttackUP>(), ModContent.ItemType<DefenseUP>()));
			}
			if (npc.life > 10000 && npc.boss) {
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AttackUP>(),1,3,5));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DefenseUP>(),1,3,5));
			}
			if (npc.life > 50000 && npc.boss) {
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AttackUP>(),1,8,10));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DefenseUP>(),1,8,10));
			}
		}
	}
}