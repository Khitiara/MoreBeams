using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MoreBeams.Items;
using MoreBeams.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MoreBeams;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]
public sealed class MoreBeams : Mod
{
	private readonly List<int>               _beamTilesAdded = new();
	internal readonly Dictionary<string, int> BeamItems       = new();

	public override void Load() {
		// AddBeam(beam_name, dust, material_id)
		// Beam tile textures will be at Tiles/{beam_name}Beam.png
		// Beam item texture will be at Items/{beam_name}BeamItem.png

		AddBeam(nameof(ItemID.AshWood), DustID.Ash, ItemID.AshWood);
		AddBeam(nameof(ItemID.Ebonwood), DustID.Ebonwood, ItemID.Ebonwood);
		AddBeam(nameof(ItemID.Shadewood), DustID.Shadewood, ItemID.Shadewood);
		AddBeam(nameof(ItemID.PalmWood), DustID.PalmWood, ItemID.PalmWood);
		AddBeam(nameof(ItemID.DynastyWood), DustID.DynastyWood, ItemID.DynastyWood, true, "NewDynastyWood");
		AddBeam(nameof(ItemID.Pearlwood), DustID.Pearlwood, ItemID.Pearlwood);
		AddBeam(nameof(ItemID.SpookyWood), DustID.SpookyWood, ItemID.SpookyWood, true, "NewSpookyWood");
		AddBeam("FancyDynastyWood", DustID.DynastyWood, ItemID.DynastyWood);
		AddBeam("NewDynastyWood", DustID.DynastyWood, ItemID.DynastyWood, ancientVariant: "DynastyWood");
		AddBeam("NewSpookyWood", DustID.SpookyWood, ItemID.SpookyWood, ancientVariant: "SpookyWood");
		AddBeam(nameof(ItemID.GrayBrick), DustID.Stone, ItemID.GrayBrick);
		AddBeam(nameof(ItemID.StoneSlab), DustID.Stone, ItemID.StoneSlab);

		On_TileObjectData.isValidAlternateAnchor += OnTileObjectDataOnIsValidAlternateAnchor;
	}

	/// <summary>
	/// Patch isValidAlternateAnchor to allow placing switches etc on mod beams.
	/// </summary>
	private static bool OnTileObjectDataOnIsValidAlternateAnchor(On_TileObjectData.orig_isValidAlternateAnchor orig, TileObjectData self, int type) {
		bool ret = orig(self, type);
		// only patch in ours if the vanilla thing makes an exception for vanilla beams
		if (!ret && self.AnchorAlternateTiles.Contains(TileID.WoodenBeam)) {
			// ModContent to avoid capturing mod
			return ModContent.GetInstance<MoreBeams>()._beamTilesAdded.Contains(type);
		}
        return ret;
	}

	/// <summary>
	/// Add a new beam type
	/// </summary>
	/// <param name="name">
	///     The name of the beam - tile textures will be at Tiles/{name}Beam.png and item texture will be at
	///     Items/{name}BeamItem.png
	/// </param>
	/// <param name="dust">The dust ID to kick up when breaking the tile</param>
	/// <param name="item">
	///     The item ID of the material to craft the beam with. Recipe will always be 1 wood to 2 beams at the sawmill.
	/// </param>
	/// <param name="isAncient"></param>
	/// <param name="ancientVariant"></param>
	private void AddBeam(string name, short dust, short item, bool isAncient = false, string? ancientVariant = null) {
		BeamTile tile = new($"{name}Beam", dust);
		AddContent(tile);
		_beamTilesAdded.Add(tile.Type);
		BeamItem beamItem = new($"{name}BeamItem", item, tile.Type, isAncient, ancientVariant);
		AddContent(beamItem);
		BeamItems[name] = beamItem.Type;
	}

	public override void Unload() {
		_beamTilesAdded.Clear();
		BeamItems.Clear();
	}
}