using habilitations2024.dal;
using habilitations2024.model;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// Demande de suppression d'un profil
        /// </summary>
        /// <param name="profil">objet profil à supprimer</param>
        public void DelProfil(Profil profil) {
            _profilAccess.DelProfil(profil);
        }
        
        /// <summary>
        /// Demande d'ajout d'un profil 
        /// </summary>
        /// <param name="profil"></param>
        public void AddProfil(Profil profil) {
            _profilAccess.AddProfil(profil);
        }

        /// <summary>
        /// Conrtôle si le pwd respecte les règles :
        /// au moins une minuscule, une majuscule, un chiffre, un caractère spécial, pas d'espace
        /// et longueur entre 8 et 30 caractères
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool PwdFort(string pwd) {
            if (pwd.Length < 8 || pwd.Length > 30) return false;
            if (!Regex.Match(pwd, @"\p{L1}").Success) return false;
            if (!Regex.Match(pwd, @"\p{Lu}").Success) return false;
            if (!Regex.Match(pwd, @"[0-9]").Success) return false;
            if (!Regex.Match(pwd, @"\W_").Success) return false;
            return !Regex.Match(pwd, @"\s").Success;
        }
    }
}
