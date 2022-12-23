using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Morceau
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Artiste { get; set; }
        public string Duree { get; set; }
        public string Genre { get; set; }
        public string Annee { get; set; }
        public bool Apprecie { get; set; }
        public string Chemin { get; set; }
    }
}
