using habilitations2024.dal;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace habilitations2024Tests.dal {
    [TestClass]
    [TestSubject(typeof(DeveloppeurAccess))]
    public class DeveloppeurAccessTest {
        private readonly Access _access = Access.GetInstance();
        private readonly DeveloppeurAccess _developpeurAccess = new DeveloppeurAccess();
        
        [TestMethod]
        public void GetLesDeveloppeurs_WithoutIdProfil() {
            const string req = "select * from developpeur;";
            var expectedTotalNumberOfDevelopers = _access.Manager.ReqSelect(req).Count;
            
            var result = _developpeurAccess.GetLesDeveloppeurs();
            Assert.AreEqual(expectedTotalNumberOfDevelopers, result.Count);
        }
        
        [TestMethod]
        public void GetLesDeveloppeurs_WithIdProfil() {
            const int expectedIdProfil = 5;
            
            var req = $"select * from developpeur where idprofil = {expectedIdProfil};";
            var expectedTotalNumberOfDevelopers  = _access.Manager.ReqSelect(req).Count;
            
            var result = _developpeurAccess.GetLesDeveloppeurs(expectedIdProfil);
            Assert.AreEqual(expectedTotalNumberOfDevelopers, result.Count);
        }
    }
}