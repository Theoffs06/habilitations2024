﻿using habilitations2024.controller;
using habilitations2024.model;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly BindingSource bdgDeveloppeurs = new BindingSource();
        
        /// <summary>
        /// Objet pour gérer la liste des profils
        /// </summary>
        private readonly BindingSource bdgProfils = new BindingSource();
        
        /// <summary>
        /// Controleur de la fenêtre
        /// </summary>
        private FrmHabilitationsController controller;
        
        /// <summary>
        /// Titre des fenêtres d'information
        /// </summary>
        private readonly string titreFenetreInformation = "Information";
        
        /// <summary>
        /// profil particulier "admin" qui ne peut pas être supprimé
        /// </summary>
        private const string ADMIN = "admin";

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
            EnCourseModifDeveloppeur(false);
            EnCoursModifPwd(false);
        }

        /// <summary>
        /// Affiche les développeurs
        /// </summary>
        private void RemplirListeDeveloppeurs() {
            var lesDeveloppeurs = controller.GetLesDeveloppeurs();
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
                MessageBox.Show("Une ligne doit être sélectionnée.", titreFenetreInformation);
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
                RemplirListeDeveloppeurs();
            }
            else {
                MessageBox.Show("Une ligne doit être sélectionnée.", titreFenetreInformation);
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
                MessageBox.Show("Une ligne doit être sélectionnée.", titreFenetreInformation);
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
                RemplirListeDeveloppeurs();
                EnCourseModifDeveloppeur(false);
            }
            else {
                MessageBox.Show("Tous les champs doivent être remplis.", titreFenetreInformation);
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
                if (FrmHabilitationsController.PwdFort(txtPwd1.Text)) {
                    var developpeur = (Developpeur)bdgDeveloppeurs.List[bdgDeveloppeurs.Position];
                    developpeur.Pwd = txtPwd1.Text;
                    controller.UpdatePwd(developpeur);
                    EnCoursModifPwd(false);
                }
                else {
                    MessageBox.Show("Le pwd doit contenir entre 8 et 30 caractères constitués de : au moins une minuscule, une majuscule, un chiffre, un caractère spécial et pas d'espace", "Information");
                }
            }
            else {
                MessageBox.Show("Les 2 zones doivent être remplies et de contenu identique", titreFenetreInformation);
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
        private void EnCourseModifDeveloppeur(Boolean modif) {
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

        /// <summary>
        /// Demande d'ajout d'un profil
        /// à condition qu'il n'existe pas déjà
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddProfil_Click(object sender, EventArgs e) {
            var nom = txtProfil.Text.ToLower();
            if (string.IsNullOrEmpty(nom)) {
                MessageBox.Show("Saisir un profil", "Information");
            }
            else if (cboProfil.Items.Cast<Profil>().Any(profil => profil.Nom == nom)) {
                MessageBox.Show("Profil déjà présent dans la liste", "Information");
            }
            else {
                controller.AddProfil(new Profil(0, nom));
                txtProfil.Text = "";
                RemplirListeProfils();
            }
        }

        /// <summary>
        /// Demande de suppression d'un profil
        /// à condition que ce ne soit pas le profil "admin"
        /// et qu'il ne soit pas attribué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelProfil_Click(object sender, EventArgs e) {
            var profil = (Profil)bdgProfils.List[bdgProfils.Position];
            if (profil.Nom.Equals(ADMIN)) {
                MessageBox.Show("Le profil 'admin' ne peut pas être supprimé", "Information");
            }
            else if (((List<Developpeur>)bdgDeveloppeurs.DataSource).Exists(x => x.Profil.Idprofil == profil.Idprofil)) {
                MessageBox.Show("Le profil " + profil.Nom + " ne peut pas être supprimé car il est utilisé");
            }
            else if (MessageBox.Show("Voulez-vous vraiment supprimer " + profil.Nom + " ?", "Confirmation de suppression", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                controller.DelProfil(profil);
                RemplirListeProfils();
            }
        }
    }
}
