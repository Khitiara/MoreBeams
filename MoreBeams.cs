using System.Collections.Generic;
using System.Linq;
using MoreBeams.Items;
using MoreBeams.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MoreBeams;

public class MoreBeams : Mod
{
	private readonly List<int> _beamTilesAdded = new();
	// private Dictionary<(int,int), int[]> _restoreTileData = new(5);
	// private static readonly int[] vanillaTilesToModify = new[] {
	// 	442,593,630,631,136,
	// };
	public override void Load() {
		AddBeam("AshWood", DustID.Ash, ItemID.AshWood);
		AddBeam("Ebonwood", DustID.Ebonwood, ItemID.Ebonwood);
		AddBeam("Shadewood", DustID.Shadewood, ItemID.Shadewood);
		AddBeam("PalmWood", DustID.PalmWood, ItemID.PalmWood);
		AddBeam("DynastyWood", DustID.DynastyWood, ItemID.DynastyWood);
		AddBeam("Pearlwood", DustID.Pearlwood, ItemID.Pearlwood);
		AddBeam("SpookyWood", DustID.SpookyWood, ItemID.SpookyWood);

		On_TileObjectData.isValidAlternateAnchor += OnTileObjectDataOnIsValidAlternateAnchor;

	}

	private bool OnTileObjectDataOnIsValidAlternateAnchor(On_TileObjectData.orig_isValidAlternateAnchor orig, TileObjectData self, int type) {
		bool ret = orig(self, type);
		if (!ret && self.AnchorAlternateTiles.Contains(TileID.WoodenBeam)) {
			return _beamTilesAdded.Contains(type);
		}
        return ret;
	}

	private void AddBeam(string name, short dust, short item) {
		BeamTile tile = new($"{name}Beam", dust);
		AddContent(tile);
		_beamTilesAdded.Add(tile.Type);
		AddContent(new BeamItem($"{name}BeamItem", item, tile.Type));
	}

	// public override void Unload() {
	// 	base.Unload();
	// }
}