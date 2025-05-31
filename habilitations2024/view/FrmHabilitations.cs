using habilitations2024.controller;
using habilitations2024.model;
using System;
using System.Windows.Forms;

namespace habilitations2024.view {
    /// <summary>
    /// Fenêtre d'affichage des développeurs et de leurs profils
    /// </summary>
    public partial class FrmHabilitations : Form {
        /// <summary>
        /// Booléen pour savoir si une modification est demandée
        /// </summary>
        private bool enCoursDeModifDeveloppeur;
        
        /// <summary>
        /// Objet pour gérer la liste des développeurs
        /// </summary>
        private BindingSource bdgDeveloppeurs = new BindingSource();
        
        /// <summary>
        /// Objet pour gérer la liste des profils
        /// </summary>
        private BindingSource bdgProfils = new BindingSource();
        
        /// <summary>
        /// Objet pour gérer le filtre des profils
        /// </summary>
        private BindingSource bdgFilterProfils = new BindingSource();
        
        /// <summary>
        /// Controleur de la fenêtre
        /// </summary>
        private FrmHabilitationsController controller;

        /// <summary>
        /// construction des composants graphiques et appel des autres initialisations
        /// </summary>
        public FrmHabilitations() {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Initialisations :
        /// Création du controleur et remplissage des listes
        /// </summary>
        private void Init() {
            controller = new FrmHabilitationsController();
            RemplirListeDeveloppeurs();
            RemplirListeProfils();
            RemplirFiltreProfils();
            EnCourseModifDeveloppeur(false);
            EnCoursModifPwd(false);
        }

        /// <summary>
        /// Affiche les développeurs
        /// Si un identifiant de profil est fourni, seuls les développeurs associés à ce profil sont affichés.
        /// Si aucun identifiant de profil n'est fourni ou si l'identifiant est -1, tous les développeurs sont affichés.
        /// </summary>
        /// <param name="idProfil">L'identifiant du profil pour filtrer les développeurs. Par défaut, -1 (aucun filtre).</param>
        private void RemplirListeDeveloppeurs(int idProfil = -1) {
            var lesDeveloppeurs = controller.GetLesDeveloppeurs(idProfil);
            bdgDeveloppeurs.DataSource = lesDeveloppeurs;
            dgvDeveloppeurs.DataSource = bdgDeveloppeurs;
            dgvDeveloppeurs.Columns["iddeveloppeur"].Visible = false;
            dgvDeveloppeurs.Columns["pwd"].Visible = false;
            dgvDeveloppeurs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        /// <summary>
        /// Affiche les profils
        /// </summary>
        private void RemplirListeProfils() {
            var lesProfils = controller.GetLesProfils();
            bdgProfils.DataSource = lesProfils;
            cboProfil.DataSource = bdgProfils;
        }
        
        /// <summary>
        /// Affiche les profils
        /// </summary>
        private void RemplirFiltreProfils() {
            var lesProfils = controller.GetLesProfils();
            var emptyItem = new Profil(-1, string.Empty);
            lesProfils.Insert(0, emptyItem);
            
            bdgFilterProfils.DataSource = lesProfils;
            cboFiltreProfil.DataSource = bdgFilterProfils;
            cboFiltreProfil.DisplayMember = "Nom";
            cboFiltreProfil.SelectedIndex = 0;
        }

        /// <summary>
        ///  Demande de modification d'un développeur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDemandeModifDev_Click(object sender, EventArgs e) {
            if (dgvDeveloppeurs.SelectedRows.Count > 0) {
                EnCourseModifDeveloppeur(true);
                var developpeur = (Developpeur)bdgDeveloppeurs.List[bdgDeveloppeurs.Position];
                txtNom.Text = developpeur.Nom;
                txtPrenom.Text = developpeur.Prenom;
                txtTel.Text = developpeur.Tel;
                txtMail.Text = developpeur.Mail;
                cboProfil.SelectedIndex = cboProfil.FindStringExact(developpeur.Profil.Nom);
            }
            else {
                MessageBox.Show("Une ligne doit être sélectionnée.", "Information");
            }
        }

        /// <summary>
        /// Demande de suppression d'un développeur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDemandeSupprDev_Click(object sender, EventArgs e) {
            if (dgvDeveloppeurs.SelectedRows.Count > 0) {
                var developpeur = (Developpeur)bdgDeveloppeurs.List[bdgDeveloppeurs.Position];
                if (MessageBox.Show("Voulez-vous vraiment supprimer " + developpeur.Nom + " " + developpeur.Prenom + " ?", "Confirmation de suppression", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                controller.DelDeveloppeur(developpeur);
                if (cboFiltreProfil.SelectedItem is Profil selectedProfil) RemplirListeDeveloppeurs(selectedProfil.Idprofil);
            }
            else {
                MessageBox.Show("Une ligne doit être sélectionnée.", "Information");
            }
        }
        
        /// <summary>
        /// Demande de rechargement de la liste de dévéloppeurs par rapport au filtre séléctionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFiltreProfil_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboFiltreProfil.SelectedItem is Profil selectedProfil) {
                RemplirListeDeveloppeurs(selectedProfil.Idprofil);
            }
        }

        /// <summary>
        /// Demande de changement du pwd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDemandeChangePwd_Click(object sender, EventArgs e) {
            if (dgvDeveloppeurs.SelectedRows.Count > 0) {
                EnCoursModifPwd(true);
            }
            else {
                MessageBox.Show("Une ligne doit être sélectionnée.", "Information");
            }
        }

        /// <summary>
        /// Demande d'enregistrement de l'ajout ou de la modification d'un développeur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregDev_Click(object sender, EventArgs e) {
            if (!txtNom.Text.Equals("") && !txtPrenom.Text.Equals("") && !txtTel.Text.Equals("") && !txtMail.Text.Equals("") && cboProfil.SelectedIndex != -1) {
                var profil = (Profil)bdgProfils.List[bdgProfils.Position];
                if (enCoursDeModifDeveloppeur) {
                    var developpeur = (Developpeur)bdgDeveloppeurs.List[bdgDeveloppeurs.Position];
                    developpeur.Nom = txtNom.Text;
                    developpeur.Prenom = txtPrenom.Text;
                    developpeur.Tel = txtTel.Text;
                    developpeur.Mail = txtMail.Text;
                    developpeur.Profil = profil;
                    controller.UpdateDeveloppeur(developpeur);
                }
                else {
                    var developpeur = new Developpeur(0, txtNom.Text, txtPrenom.Text, txtTel.Text, txtMail.Text, profil);
                    controller.AddDeveloppeur(developpeur);
                }

                if (cboFiltreProfil.SelectedItem is Profil selectedProfil) RemplirListeDeveloppeurs(selectedProfil.Idprofil);
                EnCourseModifDeveloppeur(false);
            }
            else {
                MessageBox.Show("Tous les champs doivent être remplis.", "Information");
            }
        }

        /// <summary>
        /// Annule la demande d'ajout ou de modification d'un développeur
        /// Vide les zones de saisie du développeur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAnnulDev_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Voulez-vous vraiment annuler ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                EnCourseModifDeveloppeur(false);
            }
        }

        /// <summary>
        /// Demande d'enregistrement du nouveau pwd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregPwd_Click(object sender, EventArgs e) {
            if (!txtPwd1.Text.Equals("") && !txtPwd2.Text.Equals("") && txtPwd1.Text.Equals(txtPwd2.Text)) {
                var developpeur = (Developpeur)bdgDeveloppeurs.List[bdgDeveloppeurs.Position];
                developpeur.Pwd = txtPwd1.Text;
                controller.UpdatePwd(developpeur);
                EnCoursModifPwd(false);
            }
            else {
                MessageBox.Show("Les 2 zones doivent être remplies et de contenu identique", "Information");
            }
        }

        /// <summary>
        /// Annulation de demande d'enregistrement d'un nouveau pwd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAnnulPwd_Click(object sender, EventArgs e) {
            EnCoursModifPwd(false);
        }

        /// <summary>
        /// Modification d'affichage suivant si on est en cours de modif ou d'ajout d'un developpeur
        /// </summary>
        /// <param name="modif"></param>
        private void EnCourseModifDeveloppeur(bool modif) {
            enCoursDeModifDeveloppeur = modif;
            grbLesDeveloppeurs.Enabled = !modif;
            if (modif) {
                grbDeveloppeur.Text = "modifier un développeur";
            }
            else {
                grbDeveloppeur.Text = "ajouter un développeur";
                txtNom.Text = "";
                txtPrenom.Text = "";
                txtTel.Text = "";
                txtMail.Text = "";
            }
        }

        /// <summary>
        /// Modification d'affichage suivant si on est ou non en cours de modif du pwd
        /// </summary>
        /// <param name="modif"></param>
        private void EnCoursModifPwd(bool modif) {
            grbPwd.Enabled = modif;
            grbLesDeveloppeurs.Enabled = !modif;
            grbDeveloppeur.Enabled = !modif;
            txtPwd1.Text = "";
            txtPwd2.Text = "";
        }
    }
}
