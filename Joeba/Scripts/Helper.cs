using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace Joeba.Scripts
{
    /// <summary>
    /// All around helper class for checking if tiles have properties and things of the sort.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Checks to see if the Tile specified on a certain layer has the property specified. (Returns a Bool)
        /// </summary>
        public static bool DoesTileHaveProperty(int xTile, int yTile, string propertyName, string layerName)
        {
            TiledTile tile = null;
            string output = null;

            if (xTile < 0 || yTile < 0)
            {
                Debug.log("Tile clicked was out of bounds");
                return false;
            }

            if (Game1.CurrentMap.getLayer(layerName) != null)
            {
                tile = Game1.CurrentMap.getLayer<TiledTileLayer>(layerName).getTile(xTile, yTile);
                if (tile != null && tile.tilesetTile != null)
                {

                    if (tile.tilesetTile.properties.TryGetValue(propertyName, out output))
                        return true;

                }
            }
            return false;
        }


        /// <summary>
        /// Checks to see if a tile has a property, if so return its value. (Example : TileType Could return Dirt if specified inside tiled.
        /// </summary>
        public static string GetTileProperty(int xTile, int yTile, string propertyName, string layerName)
        {
            TiledTile tile = null;
            string output = null;

            if (xTile < 0 || yTile < 0)
            {
                Debug.log("Tile clicked was out of bounds");
                return "OutOfBoundsTile";
            }

            if (Game1.CurrentMap.getLayer(layerName) != null)
            {
                tile = Game1.CurrentMap.getLayer<TiledTileLayer>(layerName).getTile(xTile, yTile);
                if (tile != null && tile.tilesetTile != null)
                {

                    if (tile.tilesetTile.properties.TryGetValue(propertyName, out output))
                        return output;

                }
            }

            return "No property";
        }

        /// <summary>
        /// Returns the position in "Tile Space" useful for stuff like checking the players position in tile coordinates.
        /// </summary>
        public static Vector2 GetTilePosition(Vector2 position)
        {
            position.X = (int)(position.X / 16);
            position.Y = (int)(position.Y / 16);

            return position;
        }

        public static Vector2 GetGridPosition(Vector2 position)
        {
            //TODO If map grid placement is weird at any point this might be why hardcoded tilesize.
            position.X = (int)(position.X / 16) * 16;
            position.Y = (int)(position.Y / 16) * 16;
            return position;
        }




    }
}
