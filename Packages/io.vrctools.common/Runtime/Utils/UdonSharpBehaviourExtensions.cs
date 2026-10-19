using UdonSharp;
using VRC.SDKBase;

namespace VRCTools.Common.Utils
{
    /// <summary>
    /// Provides various extensions around <see cref="UdonSharpBehaviour"/> to simplify common tasks.
    /// </summary>
    public static class UdonSharpBehaviourExtensions
    {

        /// <summary>
        /// Evaluates whether a given <see cref="UdonSharpBehaviour"/> is currently owned by the local player.
        ///
        /// When a given behaviour is invalid (e.g. has already been destroyed), false is returned instead.
        /// </summary>
        /// <param name="behaviour">a target behaviour</param>
        /// <returns>true if locally owned, false otherwise</returns>
        public static bool IsLocallyOwned(this UdonSharpBehaviour behaviour)
        {
            if (!Utilities.IsValid(behaviour))
            {
                return false;
            }
            
            return behaviour.gameObject.IsLocallyOwned();
        }

        /// <summary>
        /// Evaluates whether a given <see cref="UdonSharpBehaviour"/> is currently owned by a given player.
        ///
        /// When a given behaviour is invalid (e.g. has already been destroyed), false is returned instead.
        /// </summary>
        /// <param name="behaviour">a target behaviour</param>
        /// <param name="player">a player</param>
        /// <returns>true if locally owned, false otherwise</returns>
        public static bool HasOwnership(this UdonSharpBehaviour behaviour, VRCPlayerApi player)
        {
            if (!Utilities.IsValid(behaviour))
            {
                return false;
            }
            
            return behaviour.gameObject.HasOwnership(player);
        }

        /// <summary>
        /// Sets ownership of a given <see cref="UdonSharpBehaviour"/> to a given player.
        ///
        /// When a given behaviour is invalid (e.g. has already been destroyed), this method is effectively a noop.
        /// </summary>
        /// <param name="behaviour">a target behaviour</param>
        /// <param name="player">a player</param>
        public static void SetOwnership(this UdonSharpBehaviour behaviour, VRCPlayerApi player)
        {
            if (!Utilities.IsValid(behaviour))
            {
                return;
            }
            
            behaviour.gameObject.SetOwnership(player);
        }

        /// <summary>
        /// Takes ownership for a given <see cref="UdonSharpBehaviour"/>.
        ///
        /// When a given behaviour is invalid (e.g. has already been destroyed), this method is effectively a noop.
        ///
        /// If the given behaviour is already owned by the local player, this method also acts as a noop.
        /// </summary>
        /// <param name="behaviour"></param>
        public static void TakeOwnership(this UdonSharpBehaviour behaviour)
        {
            if (!Utilities.IsValid(behaviour))
            {
                return;
            }
            
            behaviour.gameObject.TakeOwnership();
        }
    }
}