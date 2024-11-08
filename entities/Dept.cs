using System;
using System.Collections.Generic;
using csharp.enums;

namespace csharp.entities
{
    public class Dept
    {
        private int id;
        private double montant;
        private double montantVerser = 0;
        private DateTime date;
        private EtatDette etat;
        private TypeDette typeDette;
        private List<Detail> details = new List<Detail>();
        private Client client;

        public int Id { get => id; set => id = value; }
        public double Montant { get => montant; set => montant = value; }
        public double MontantVerser { get => montantVerser; set => montantVerser = value; }
        public double MontantRestant => Montant - MontantVerser;
        public DateTime Date { get => date; set => date = value; }
        public EtatDette Etat { get => etat; set => etat = value; }
        public List<Detail> Details => details;
        public Client Client { get => client; set => client = value; }
        public TypeDette TypeDette { get => typeDette; set => typeDette = value; }

        private static int count = 0;

        // Constructeur
        public Dept()
        {
            id = count++;
            date = DateTime.Now;
            this.typeDette = TypeDette.ENCOURS;
            UpdateEtat();
        }

        // Mise à jour de l'état
        public void UpdateEtat()
        {
            if (MontantRestant > 0)
            {
                this.etat = EtatDette.NONSOLDEES;
            }
            else
            {
                this.etat = EtatDette.SOLDEES;
            }
        }
        
        public override string ToString()
        {
            string detailsStr = (details.Count > 0) ? string.Join(", ", details) : "Aucun détail";
            return $"Dept {id} : {Montant} - {date} - {etat} - {detailsStr} - {typeDette}";
        }
    }
}
