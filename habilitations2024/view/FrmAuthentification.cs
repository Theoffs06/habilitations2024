﻿using habilitations2024.controller;
using habilitations2024.model;
using System;
using System.Windows.Forms;

namespace habilitations2024.view {
    /// <summary>
    /// Fenêtre d'authentification (seuls les developpeurs profil "admin" peuvent accéder à l'application)
    /// </summary>
    public partial class FrmAuthentification : Form {
        /// <summary>
        /// Contrôleur de la fenêtre
        /// </summary>
        private FrmAuthentificationController _controller;

        /// <summary>
        /// Conrtuction des composants graphiques et appel des autres initialisations
        /// </summary>
        public FrmAuthentification() {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Initialisations : 
        /// Création du controleur
        /// </summary>
        private void Init() {
            _controller = new FrmAuthentificationController();
        }

        /// <summary>
        /// Demande au controleur de controler l'authentification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConnect_Click(object sender, EventArgs e) {
            var nom = txtNom.Text;
            var prenom = txtPrenom.Text;
            var pwd = txtPwd.Text;
            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(pwd)) {
                MessageBox.Show("Tous les champs doivent être remplis.", "Information");
            }
            else {
                var admin = new Admin(nom, prenom, pwd);
                if (_controller.ControleAuthentification(admin)) {
                    var frm = new FrmHabilitations();
                    frm.ShowDialog();
                }
                else {
                    MessageBox.Show("Authentification incorrecte ou vous n'êtes pas admin", "Alerte");
                }
            }
        }

    }
}
