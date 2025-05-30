using System.Collections.Generic;
using MySqlConnector;

namespace habilitations2024.bddmanager {
    /// <summary>
    /// Singleton : connexion à la base de données et exécution des requêtes
    /// </summary>
    public class BddManager {
        /// <summary>
        /// instance unique de la classe
        /// </summary>
        private static BddManager _instance;
        
        /// <summary>
        /// objet de connexion à la BDD à partir d'une chaîne de connexion
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Constructeur pour créer la connexion à la BDD et l'ouvrir
        /// </summary>
        /// <param name="stringConnect">chaine de connexion</param>
        private BddManager(string stringConnect) {
            _connection = new MySqlConnection(stringConnect);
            _connection.Open();
        }

        /// <summary>
        /// Création d'une seule instance de la classe
        /// </summary>
        /// <param name="stringConnect">chaine de connexion</param>
        /// <returns>instance unique de la classe</returns>
        public static BddManager GetInstance(string stringConnect) {
            return _instance ?? (_instance = new BddManager(stringConnect));
        }

        /// <summary>
        /// Exécution d'une requête de type LCT (begin transaction, commit, rollback)
        /// </summary>
        /// <param name="stringQuery">requête</param>
        public void ReqControle(string stringQuery) {
            var command = new MySqlCommand(stringQuery, _connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Exécution d'une requête de type LMD (insert, update, delete)
        /// </summary>
        /// <param name="stringQuery">requête autre que select</param>
        /// <param name="parameters">dictionnire contenant les parametres</param>
        public void ReqUpdate(string stringQuery, Dictionary<string, object> parameters = null) {
            var command = new MySqlCommand(stringQuery, _connection);
            if (!(parameters is null)) {
                foreach (var parameter in parameters) {
                    command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                }
            }
            command.Prepare();
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Execution d'une requête de type LIT (select)
        /// </summary>
        /// <param name="stringQuery">requête select</param>
        /// <param name="parameters">dictoinnaire contenant les parametres</param>
        /// <returns>liste de tableaux d'objets contenant les valeurs des colonnes</returns>
        public List<object[]> ReqSelect(string stringQuery, Dictionary<string, object> parameters = null) {
            var command = new MySqlCommand(stringQuery, _connection);
            if (!(parameters is null)) {
                foreach (var parameter in parameters) {
                    command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                }
            }
            command.Prepare();
            var reader = command.ExecuteReader();
            var nbCols = reader.FieldCount;
            var records = new List<object[]>();
            while (reader.Read()) {
                var attributs = new object[nbCols];
                reader.GetValues(attributs);
                records.Add(attributs);
            }
            reader.Close();
            return records;
        }

    }
}