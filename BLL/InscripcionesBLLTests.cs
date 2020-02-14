using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registro.Entidades;
using System.Collections.Generic;

namespace Registro.BLL.Tests {
    [TestClass()]
    public class InscripcionesBLLTests { //Profesor, cree un método en PersonasBLL, que calcula el balance de una persona 




        private readonly int personaPruebaId = 1; //**Esta persona ya existe. Fines de prueba; No borrar

        [TestMethod()]
        public void GuardarTest() {


            Inscripcion nuevaInscripcion = new Inscripcion();
            nuevaInscripcion.Balance = 100;

            Persona persona = PersonasBLL.Buscar(personaPruebaId);

            decimal balance = 0.0m;

            decimal balanceInicial = persona.Balance;//Para verificar si cambio el balance

            List<Inscripcion> inscripcionesList = new List<Inscripcion>();

            inscripcionesList = InscripcionesBLL.GetList(i => i.PersonaId == personaPruebaId);
            inscripcionesList.Add(nuevaInscripcion); // Simulando la nueva inscripción


            foreach (Inscripcion i in inscripcionesList) {
                balance += i.Balance;
            }

            persona.Balance = balance;

            bool personaModificada = PersonasBLL.Modificar(persona);

            Assert.AreNotEqual(persona.Balance , balanceInicial);
        }

        [TestMethod()]
        public void ModificarTest() {

            bool testPass = false;

            List<Inscripcion> inscripcionesList = InscripcionesBLL.GetList(i => i.PersonaId == personaPruebaId);

            decimal balanceInicialInscripcion = inscripcionesList[0].Balance;

            inscripcionesList[0].Balance += 100;

            //bool inscripcionModificada = InscripcionesBLL.Modificar(inscripcionesList[0]);

            Persona persona = PersonasBLL.Buscar(personaPruebaId);

            decimal balanceInicialPersona = persona.Balance;
            decimal balanceFinalPersona = 0.0m;

            foreach (Inscripcion i in inscripcionesList) {
                balanceFinalPersona += i.Balance;
            }

            testPass = ( balanceInicialPersona != balanceFinalPersona );


            Assert.IsTrue(testPass);
        }

        [TestMethod()]
        public void EliminarTest() {

            decimal balanceInicialPersona = 0;
            decimal balanceFinalPersona = 0;

            List<Inscripcion> inscripcioneslist = InscripcionesBLL.GetList(i => i.PersonaId == personaPruebaId);

            Persona persona = PersonasBLL.Buscar(personaPruebaId);
            balanceInicialPersona = persona.Balance;

            inscripcioneslist.RemoveAt(0); // Simulando eliminación. InscripcionesBLL.Eliminar()

            foreach (Inscripcion i in inscripcioneslist) {
                balanceFinalPersona += i.Balance;
            }

            Assert.IsTrue(balanceFinalPersona < balanceInicialPersona);
        }

        [TestMethod()]
        public void BuscarTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetListTest() {
            Assert.Fail();
        }
    }
}