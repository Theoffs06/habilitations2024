using habilitations2024.model;
using System;
using System.Collections.Generic;
using Serilog;

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
                    Log.Debug("ProfilAccess.GesLesProfils nb records = {0}", records.Count);
                    foreach (var record in records) {
                        Log.Debug("ProfilAccess.GestLesProfils id={0} nom={1}", record[0], record[1]);
                        var profil = new Profil((int)record[0], (string)record[1]);
                        lesProfils.Add(profil);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Log.Error("ProfilAccess.GetLesProfils catch req={0} erreur={1}", req, e.Message);
                Environment.Exit(0);
            }
            return lesProfils;
        }

        /// <summary>
        /// Ajout d'un profil
        /// </summary>
        /// <param name="profil"></param>
        public void AddProfil(Profil profil) {
            if (_access.Manager == null) return;
            const string req = "insert into profil (nom) values (@nom);";
            var parameters = new Dictionary<string, object> {{ "@nom", profil.Nom }};
            try {
                _access.Manager.ReqUpdate(req, parameters);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Log.Error("DeveloppeurAccess.AddDeveloppeur catch req={0} erreur={1}", req, e.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Suppression d'un profil
        /// </summary>
        /// <param name="profil"></param>
        public void DelProfil(Profil profil) {
            if (_access.Manager == null) return;
            const string req = "delete from profil where idprofil = @idprofil;";
            var parameters = new Dictionary<string, object> {{ "@idprofil", profil.Idprofil }};
            try {
                _access.Manager.ReqUpdate(req, parameters);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Log.Error("ProfilAccess.DelProfil catch req={0} erreur={1}", req, e.Message);
                Environment.Exit(0);
            }
        }
    }
}
