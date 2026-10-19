using UnityEngine;
using VRC.SDKBase;

namespace VRCTools.Common.Utils
{
    /// <summary>
    /// Provides various extensions to simplify common tasks around <see cref="GameObject"/>.
    /// </summary>
    public static class GameObjectExtensions
    {

        /// <summary>
        /// Evaluates whether the given game object is currently owned by the local player.
        /// </summary>
        /// <param name="gameObject">the target game object</param>
        /// <returns>true if locally owned, false otherwise</returns>
        public static bool IsLocallyOwned(this GameObject gameObject)
        {
            return Networking.IsOwner(gameObject);
        }

        /// <summary>
        /// Checks whether a given game object is owned by a given player.
        /// </summary>
        /// <param name="gameObject">the target game object</param>
        /// <param name="player">a player</param>
        /// <returns>true if owned by player, false otherwise</returns>
        public static bool HasOwnership(this GameObject gameObject, VRCPlayerApi player)
        {
            return Networking.IsOwner(player, gameObject);
        }

        /// <summary>
        /// Assigns ownership for a given game object to a given player.
        /// </summary>
        /// <param name="gameObject">a target game object</param>
        /// <param name="player">a player</param>
        public static void SetOwnership(this GameObject gameObject, VRCPlayerApi player)
        {
            Networking.SetOwner(player, gameObject);
        }

        /// <summary>
        /// Takes ownership for a given game object.
        ///
        /// If the given object is already owned by the local player, this method acts as a noop.
        /// </summary>
        /// <param name="gameObject">a target game object</param>
        public static void TakeOwnership(this GameObject gameObject)
        {
            if (gameObject.IsLocallyOwned())
            {
                return;
            }
            
            gameObject.SetOwnership(Networking.LocalPlayer);
        }
    }
}