using habilitations2024.bddmanager;
using System;
using Serilog;
using System.Configuration;

namespace habilitations2024.dal {
    /// <summary>
    /// Singleton : classe d'accès à BddManager
    /// </summary>
    public class Access {
        /// <summary>
        /// nom de connexion à la bdd
        /// </summary>
        private const string ConnectionName = "habilitations2024.Properties.Settings.habilitationsConnectionString";

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
            string connectionString = null;
            try {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.Console()
                    .WriteTo.File("logs/log.txt")
                    .CreateLogger();
                connectionString = GetConnectionStringByName(ConnectionName);
                Manager = BddManager.GetInstance(connectionString);
            }
            catch (Exception e) {
                Log.Fatal("Access.Access catch connectionString={0} erreur={1}", connectionString, e.Message);
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

        /// <summary>
        /// Récupération de la chaîne de connexion
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetConnectionStringByName(string name) {
            string returnValue = null;
            var settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null) returnValue = settings.ConnectionString;
            return returnValue;
        }
    }
}
