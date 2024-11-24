using Terraria.ID;
using Terraria.ModLoader;

namespace MoreBeams.Items;

[Autoload(false)]
public class BeamItem : ModItem
{
    public override string Name { get; }
    private readonly short _baseWood;
    private readonly ushort _tileId;
    private readonly bool _isAncient;
    private readonly string? _ancientVariant;
    private readonly bool _isLiving;
    private readonly bool _useWoodGroup;

    protected override bool CloneNewInstances => true;

    public BeamItem(string name, short baseWood, ushort tileId, bool isAncient = false, string? ancientVariant = null,
        bool isLiving = false, bool useWoodGroup = false)
    {
        Name = name;
        _baseWood = baseWood;
        _tileId = tileId;
        _isAncient = isAncient;
        _ancientVariant = ancientVariant;
        _isLiving = isLiving;
        _useWoodGroup = useWoodGroup;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.WoodenBeam);
        Item.ResearchUnlockCount = 50;
        Item.createTile = _tileId;
    }

    public override void AddRecipes()
    {
        if (!_isAncient) {
            if (_useWoodGroup) {
                CreateRecipe(2)
                    .AddRecipeGroup(RecipeGroupID.Wood)
                    .AddTile(_isLiving ? TileID.LivingLoom : TileID.Sawmill)
                    .Register();
            } else {
                CreateRecipe(2)
                    .AddIngredient(_baseWood)
                    .AddTile(_isLiving ? TileID.LivingLoom : TileID.Sawmill)
                    .Register();
            }
        }

        if (_ancientVariant is not null) {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.GetInstance<MoreBeams>().BeamItems[_ancientVariant];
        }
    }

    public override void Unload()
    {
        if (_ancientVariant is not null && Type < ItemID.Sets.ShimmerTransformToItem.Length) {
            ItemID.Sets.ShimmerTransformToItem[Type] = 0;
        }
    }
}