using ClassLibrary;

namespace Skyjo
{
    public static class GameSession
    {
        public static Game? CurrentSession { get; set; }

        /// <summary>
        /// Chemin complet du fichier de sauvegarde actuellement chargé.
        /// Null si c'est une nouvelle partie pas encore sauvegardée.
        /// </summary>
        public static string? CurrentSaveFile { get; set; }
    }
}
