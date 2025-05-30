namespace habilitations2024.model {
    public class Profil {
        public int Idprofil { get; }
        public string Nom { get; }

        /// <summary>
        /// Valorise les propriétés
        /// </summary>
        /// <param name="idprofil"></param>
        /// <param name="nom"></param>
        public Profil(int idprofil, string nom) {
            Idprofil = idprofil;
            Nom = nom;
        }

        /// <summary>
        /// Définit l'information à afficher (juste le nom)
        /// </summary>
        /// <returns>nom du profil</returns>
        public override string ToString() {
            return Nom;
        }
    }
}
