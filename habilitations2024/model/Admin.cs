namespace habilitations2024.model {
    /// <summary>
    /// Classe métier interne pour mémoriser les informations d'authentification
    /// </summary>
    public class Admin {
        public string Nom { get; }
        public string Prenom { get; }
        public string Pwd { get; }

        /// <summary>
        /// Valorise les propriétés
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="pwd"></param>
        public Admin(string nom, string prenom, string pwd) {
            Nom = nom;
            Prenom = prenom;
            Pwd = pwd;
        }
    }
}
