﻿using Kili.Models.Dons;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kili.Models.General
{
    public class UserAccount
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Prenom { get; set; }
        [MaxLength(25)]
        public string Nom { get; set; }

        [MaxLength(50)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Votre mot de passe doit avoir au moins 8 caractères dont une lettre en majuscule, un nombre et un caractère spécial")]
        public string Password { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreation { get; set; }
        public bool Actif { get; set; }
        //public bool IsConnected { get; set; }

        [EmailAddress(ErrorMessage = "L'adresse email saisie n'est pas valide")]
        public string Mail { get; set; }
        public string Telephone { get; set; }
        public string URLPhoto { get; set; }

        public TypeRole Role { get; set; }
        
        public int? DonateurId { get; set; }
        public virtual Donateur Donateur { get; set; }
        public int? AssociationId { get; set; }
        public virtual Association Association { get; set; }


        public int? AdresseId { get; set; }
        public virtual Adresse Adresse { get; set; }


        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }



    }

    public enum TypeRole
    {
        Admin,
        Association,
        Utilisateur
    }
}
