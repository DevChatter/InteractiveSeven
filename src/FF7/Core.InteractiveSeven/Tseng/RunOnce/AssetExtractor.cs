using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Shojy.FF7.Elena;
using Shojy.FF7.Elena.Converters;

namespace DevChatter.InteractiveGames.Core.Seven.Tseng.RunOnce
{
    public static class AssetExtractor
    {
        private const string AssetBaseLocation = "wwwroot/images";

        private static readonly AssetMap[] RequiredAssets =
        {
            Assets.CloudPortrait,
            Assets.BarretPortrait,
            Assets.TifaPortrait,
            Assets.AerisPortrait,
            Assets.RedXiiiPortrait,
            Assets.YuffiePortrait,
            Assets.CaitSithPortrait,
            Assets.VincentPortrait,
            Assets.CidPortrait,
            Assets.YoungCloudPortrait,
            Assets.SephirothPortrait,

            Assets.WeaponSword,
            Assets.WeaponArm,
            Assets.WeaponGlove,
            Assets.WeaponStaff,
            Assets.WeaponHairpin,
            Assets.WeaponShuriken,
            Assets.WeaponMegaphone,
            Assets.WeaponGun,
            Assets.WeaponPole,

            Assets.Armlet,
            Assets.Accessory,

            Assets.MateriaMagic,
            Assets.MateriaSupport,
            Assets.MateriaSummon,
            Assets.MateriaCommand,
            Assets.MateriaIndependent,

            Assets.SlotNormal,
            Assets.SlotNothing,
            Assets.SlotLink,
        };


        public static List<AssetMap> IsAssetExtractionRequired()
        {
            return RequiredAssets
                .Where(asset => !File.Exists(Path.Combine(AssetBaseLocation, asset.ExtractedFile)))
                .ToList();
        }

        public static void ExtractAssets(string ff7Path, IReadOnlyList<AssetMap> requiredAssets)
        {
            // Ensure containing directory is present
            Directory.CreateDirectory(AssetBaseLocation);

            // Group by containing archive
            var archiveGroup = requiredAssets
                .GroupBy(a => a.LgpArchiveFile)
                .ToDictionary(g => g.Key, g => g.ToList());


            foreach (var archive in archiveGroup)
            {
                // Group by containing file
                var fileGroup = archive.Value
                    .GroupBy(f => f.FileWithinLgp)
                    .ToDictionary(g => g.Key, g => g.ToList());

                using var lgp = new LgpReader(Path.Combine(ff7Path, archive.Key));
                foreach (var file in fileGroup)
                {
                    ProcessPalettesInFile(file, lgp);
                }
            }
        }

        private static void ProcessPalettesInFile(KeyValuePair<string, List<AssetMap>> file, LgpReader lgp)
        {
            // Group by palette
            var paletteGroup = file.Value
                .GroupBy(p => p.ColorPalette)
                .ToDictionary(p => p.Key, p => p.ToList());


            using var dataStream = lgp.ExtractFile(file.Key);
            foreach (var palette in paletteGroup)
            {
                ProcessFileContents(dataStream, palette);
            }
        }

        private static void ProcessFileContents(Stream dataStream, KeyValuePair<int, List<AssetMap>> palette)
        {
            var bmp = TexConverter.ToBitmap(dataStream, palette.Key);

            foreach (var asset in palette.Value)
            {
                ExtractSegment(asset, bmp);
            }
        }

        private static void ExtractSegment(AssetMap asset, Bitmap bmp)
        {
            // Crop and save
            using var fileWriter = new StreamWriter(Path.Combine(AssetBaseLocation, asset.ExtractedFile));

            var croppedWidth = (int)(bmp.Width * asset.CropXRatio);
            var croppedHeight = (int)(bmp.Height * asset.CropYRatio);

            var crop = new Rectangle(
                (int)(bmp.Width * asset.LocationXRatio),
                (int)(bmp.Height * asset.LocationYRatio),
                croppedWidth,
                croppedHeight);
            var croppedBmp = new Bitmap(croppedWidth, croppedHeight);

            using (var g = Graphics.FromImage(croppedBmp))
            {
                g.DrawImage(
                    bmp,
                    new Rectangle(0, 0, croppedBmp.Width, croppedBmp.Height),
                    crop,
                    GraphicsUnit.Pixel);
            }

            croppedBmp.Save(fileWriter.BaseStream, ImageFormat.Png);
        }
    }
}
