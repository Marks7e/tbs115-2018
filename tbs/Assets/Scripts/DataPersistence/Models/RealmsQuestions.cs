using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.DataPersistence.Models
{
    [Serializable]
    public class RealmsQuestions : IDataType
    {

        public Dictionary<string, List<string>> rQuestions;
        public List<string> reino1;
        private GameDataPersistence gdp = new GameDataPersistence();


        public RealmsQuestions()
        {
            rQuestions = new Dictionary<string, List<string>>();
            reino1 = new List<string>
            {
                "¿Caminas sin hacer ruido cuando vas a tu cuarto?",
                "¿Compartes el baño de la casa con tu familia o visitas?",
                "¿Levantas tu mano para pedir turno en una conversación?",
                "¿Saludas cuando llegas de visita a casa de un familiar?",
                "¿Compartes tus pertenencias con otras personas si te lo piden?",
                "¿Sueles utilizar las palabras 'Por favor' y 'Gracias'?",
                "¿Si alguien te solicita algo, sueles ayudar?",
                "¿Si alguien cumple años, sueles felicitarlo?",
                "¿Si estas enojado, te alejas para tranquilizarte?",
                "¿Sueles saludar a tus amigos, aunque los veas todos los dias?",
                "¿Eres capaz de controlar el temor por los sonidos fuertes?"
            };
            rQuestions.Add("Realm1", reino1);
            gdp.SaveDataToFile(GameDataPersistence.DataType.PostGameTestData, this);
          
        }

        public string LoadDataLocally(string key)
        {
            if (key == "Minijuego 1" || key == "Minijuego 2" || key == "Minijuego 3")
                return GetRealmRandomQuestion("Realm1");
            return null;

        }
        public string GetRealmRandomQuestion(string realm)
        {
            if (rQuestions.ContainsKey(realm))
            {
                List<string> questions = rQuestions[realm];
                System.Random r = new System.Random();
                int pQuestion = r.Next(0, questions.Count - 1);
                string question = questions[pQuestion].ToString();
                question.Remove(pQuestion, 1);
                return question;
            }
            return null;

        }
        public bool SaveDataLocally(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
