using habilitations2024.model;
using System;
using System.Collections.Generic;

namespace habilitations2024.dal {
    /// <summary>
    /// Classe permettant de gérer les demandes concernant les profils
    /// </summary>
    public class ProfilAccess {
        /// <summary>
        /// Instance unique de l'accès aux données
        /// </summary>
        private readonly Access _access;

        /// <summary>
        /// Constructeur pour créer l'accès aux données
        /// </summary>
        public ProfilAccess() {
            _access = Access.GetInstance();
        }

        /// <summary>
        /// Récupère et retourne les profils
        /// </summary>
        /// <returns>liste des profils</returns>
        public List<Profil> GetLesProfils() {
            var lesProfils = new List<Profil>();
            if (_access.Manager == null) return lesProfils;
            const string req = "select * from profil order by nom;";
            try {
                var records = _access.Manager.ReqSelect(req);
                if (records != null) {
                    foreach (var record in records) {
                        var profil = new Profil((int)record[0], (string)record[1]);
                        lesProfils.Add(profil);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
            
            return lesProfils;
        }
    }
}
