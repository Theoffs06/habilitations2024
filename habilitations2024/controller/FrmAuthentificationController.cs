using habilitations2024.dal;
using habilitations2024.model;

namespace habilitations2024.controller {
    /// <summary>
    /// Contrôleur de FrmAuthentification
    /// </summary>
    internal class FrmAuthentificationController {
        /// <summary>
        /// objet d'accès aux opérations possibles sur Developpeur
        /// </summary>
        private readonly DeveloppeurAccess _developpeurAccess;

        /// <summary>
        /// Récupère l'acces aux données
        /// </summary>
        public FrmAuthentificationController() {
            _developpeurAccess = new DeveloppeurAccess();
        }

        /// <summary>
        /// Vérifie l'authentification
        /// </summary>
        /// <param name="admin">objet contenant les informations de connexion</param>
        /// <returns> vrai si les informations de connexion sont correctes</returns>
        public bool ControleAuthentification(Admin admin) {
            return _developpeurAccess.ControleAuthentification(admin);
        }
    }
}
