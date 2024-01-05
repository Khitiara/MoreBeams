using Terraria.ID;
using Terraria.ModLoader;

namespace MoreBeams.Items;

[Autoload(false)]
public class BeamItem : ModItem
{
    public override string Name { get; }
    private readonly short   _baseWood;
    private readonly ushort  _tileId;
    private readonly bool    _isAncient;
    private readonly string? _ancientVariant;

    protected override bool CloneNewInstances => true;

    public BeamItem(string name, short baseWood, ushort tileId, bool isAncient = false, string? ancientVariant = null) {
        Name = name;
        _baseWood = baseWood;
        _tileId = tileId;
        _isAncient = isAncient;
        _ancientVariant = ancientVariant;
    }

    public override void SetDefaults() {
        Item.CloneDefaults(ItemID.WoodenBeam);
        Item.ResearchUnlockCount = 50;
        Item.createTile = _tileId;
    }

    public override void AddRecipes() {
        if (!_isAncient) {
            CreateRecipe(2)
                .AddIngredient(_baseWood)
                .AddTile(TileID.Sawmill)
                .Register();
        }

        if (_ancientVariant is not null) {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.GetInstance<MoreBeams>().BeamItems[_ancientVariant];
        }
    }

    public override void Unload() {
        if (_ancientVariant is not null) {
            ItemID.Sets.ShimmerTransformToItem[Type] = 0;
        }
    }
}