using habilitations2024.bddmanager;
using System;

namespace habilitations2024.dal {
    /// <summary>
    /// Singleton : classe d'accès à BddManager
    /// </summary>
    public class Access {
        /// <summary>
        /// chaine de connexion à la bdd
        /// </summary>
        private const string ConnectionString = "server=localhost;user id=habilitations;password=motdepasseuser;database=habilitations;";

        /// <summary>
        /// instance unique de la classe
        /// </summary>
        private static Access _instance;
        
        /// <summary>
        /// Getter sur l'objet d'accès aux données
        /// </summary>
        public BddManager Manager { get; }

        /// <summary>
        /// Création unique de l'objet de type BddManager
        /// Arrête le programme si l'accès à la BDD a échoué
        /// </summary>
        private Access() {
            try {
                Manager = BddManager.GetInstance(ConnectionString);
            }
            catch (Exception) {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Création d'une seule instance de la classe
        /// </summary>
        /// <returns></returns>
        public static Access GetInstance() {
            return _instance ?? (_instance = new Access());
        }
    }
}
