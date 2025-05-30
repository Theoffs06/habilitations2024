using habilitations2024.dal;
using habilitations2024.model;
using System.Collections.Generic;

namespace habilitations2024.controller {
    /// <summary>
    /// Contrôleur de FrmHabilitations
    /// </summary>
    public class FrmHabilitationsController {
        /// <summary>
        /// objet d'accès aux opérations possibles sur Developpeur
        /// </summary>
        private readonly DeveloppeurAccess _developpeurAccess;
        /// <summary>
        /// objet d'accès aux opérations possible sur Profil
        /// </summary>
        private readonly ProfilAccess _profilAccess;

        /// <summary>
        /// Récupère les acces aux données
        /// </summary>
        public FrmHabilitationsController() {
            _developpeurAccess = new DeveloppeurAccess();
            _profilAccess = new ProfilAccess();
        }

        /// <summary>
        /// Récupère et retourne les infos des développeurs
        /// </summary>
        /// <returns>liste des développeurs</returns>
        public List<Developpeur> GetLesDeveloppeurs() {
            return _developpeurAccess.GetLesDeveloppeurs();
        }

        /// <summary>
        /// Récupère et retourne les infos des profils
        /// </summary>
        /// <returns>liste des profils</returns>
        public List<Profil> GetLesProfils() {
            return _profilAccess.GetLesProfils();
        }

        /// <summary>
        /// Demande de suppression d'un développeur
        /// </summary>
        /// <param name="developpeur">objet developpeur à supprimer</param>
        public void DelDeveloppeur(Developpeur developpeur) {
            _developpeurAccess.DelDepveloppeur(developpeur);
        }

        /// <summary>
        /// Demande d'ajout d'un développeur
        /// </summary>
        /// <param name="developpeur">objet developpeur à ajouter</param>
        public void AddDeveloppeur(Developpeur developpeur) {
            _developpeurAccess.AddDeveloppeur(developpeur);
        }

        /// <summary>
        /// Demande de modification d'un développeur
        /// </summary>
        /// <param name="developpeur">objet developpeur à modifier</param>
        public void UpdateDeveloppeur(Developpeur developpeur) {
            _developpeurAccess.UpdateDeveloppeur(developpeur);
        }

        /// <summary>
        /// Demande de changement de pwd
        /// </summary>
        /// <param name="developpeur">objet developpeur avec nouveau pwd</param>
        public void UpdatePwd(Developpeur developpeur) {
            _developpeurAccess.UpdatePwd(developpeur);
        }
    }
}
