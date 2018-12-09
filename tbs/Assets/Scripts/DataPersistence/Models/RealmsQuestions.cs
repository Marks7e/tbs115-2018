using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Models
{
    public class RealmsQuestions : IDataType
    {

        public Dictionary<string, List<string>> rQuestions;
        public List<string> Reino1;


        public RealmsQuestions()
        {
            rQuestions = new Dictionary<string, List<string>>();
            Reino1 = new List<string>();
            Reino1.Add("¿Caminas sin hacer ruido cuando vas a tu cuarto?");
            Reino1.Add("¿Compartes el baño de la casa con tu familia o visitas?");
            Reino1.Add("¿Levantas tu mano para pedir turno en una conversación?");
            Reino1.Add("¿Saludas cuando llegas de visita a casa de un familiar?");
            Reino1.Add("¿Compartes tus pertenencias con otras personas si te lo piden?");
            Reino1.Add("¿Sueles utilizar las palabras 'Por favor' y 'Gracias'?");
            Reino1.Add("¿Si alguien te solicita algo, sueles ayudar?");
            Reino1.Add("¿Si alguien cumple años, sueles felicitarlo?");
            Reino1.Add("¿Si estas enojado, te alejas para tranquilizarte?");
            Reino1.Add("¿Sueles saludar a tus amigos, aunque los veas todos los dias?");
            Reino1.Add("¿Eres capaz de controlar el temor por los sonidos fuertes?");
        }

        public string GetData(string key)
        {
            if (key == "Minijuego 1" || key == "Minijuego 2" || key == "Minijuego 3" ||)
                return Reino1.First();
            return null;
                
        }

        public void SaveData(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
