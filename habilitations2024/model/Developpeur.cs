namespace habilitations2024.model {
    public class Developpeur {
        public int Iddeveloppeur { get; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public string Pwd { get; set; }
        public Profil Profil { get; set; }
        
        /// <summary>
        /// Valorise les propriétés
        /// </summary>
        /// <param name="idDeveloppeur"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="tel"></param>
        /// <param name="mail"></param>
        /// <param name="pwd"></param>
        /// <param name="profil"></param>
        public Developpeur(int iddeveloppeur, string nom, string prenom, string tel, string mail, Profil profil) {
            Iddeveloppeur = iddeveloppeur;
            Nom = nom;
            Prenom = prenom;
            Tel = tel;
            Mail = mail;
            Pwd = Pwd;
            Profil = profil;
        }
    }
}
