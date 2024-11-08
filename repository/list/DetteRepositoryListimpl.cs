using System;
using System.Collections.Generic;
using System.Linq;
using csharp.core.config;
using csharp.entities;
using csharp.core.interfaces;
using csharp.enums;

namespace csharp.repository.list
{
    public class DetteRepositoryListImpl : RepositoryListImpl<Dept>, IDeptRepository
    {
        private readonly List<Dept> list = new();

        // Retourne toutes les dettes non soldées
        public List<Dept> SelectAllDeptNonSoldees()
        {
            return list.Where(dept => dept.Etat == EtatDette.NONSOLDEES).ToList();
        }

        // Retourne toutes les dettes d'un client spécifique
        public List<Dept> SelectAllMyDept(int clientId)
        {
            return list.Where(dept => dept.Client.Id == clientId).ToList();
        }

        // Retourne toutes les dettes annulées d'un client spécifique
        public List<Dept> SelectAllMyDeptAnnuler(int clientId)
        {
            return list.Where(dept => dept.Client.Id == clientId && dept.TypeDette == TypeDette.ANNULER).ToList();
        }

        // Retourne toutes les dettes non soldées d'un client spécifique
        public List<Dept> SelectAllMyDeptNonSoldees(int clientId)
        {
            return list.Where(dept => dept.Client.Id == clientId && dept.Etat == EtatDette.NONSOLDEES).ToList();
        }

        // Retourne toutes les dettes d'un client par son identifiant
        public List<Dept> SelectByClientId(int clientId)
        {
            return list.Where(dept => dept.Client.Id == clientId).ToList();
        }

        public List<Dept> SelectByEtat(TypeDette etat)
        {
            return list.Where(dept => dept.TypeDette == etat).ToList();
        }
    }
}
