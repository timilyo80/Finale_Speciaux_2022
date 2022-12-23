using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpfApp1.Models;
using System.Windows;

namespace WpfApp1.ViewModel
{
    public class MorceauServices
    {
        public List<Morceau> MorceauxList = null;
        private string filename = @"D:\Users\emg08\Desktop\WPF_Final\WpfApp1\WpfApp1\JSON\Morceaux.json";

        public MorceauServices()
        {
            GetJSON();
        }

        public void GetJSON()
        {

            if (File.Exists(filename))
            {
                var list = JsonConvert.DeserializeObject<List<Morceau>>(File.ReadAllText(filename));

                if (list != null)
                    MorceauxList = list;
                else
                    MorceauxList = new List<Morceau>();
            }
            else
            {
                MorceauxList = new List<Morceau>();
            }
        }

        public bool SaveJSON()
        {
            var json = JsonConvert.SerializeObject(MorceauxList);

            if (File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }

            System.IO.File.WriteAllText(filename, json);

            return (true);
        }

        public bool InsertJSON(Morceau morceau)
        {
            GetJSON();
            if (MorceauxList == null)
                MorceauxList = new List<Morceau>();

            var test = MorceauxList.FirstOrDefault(r => r.Id == morceau.Id);
            if (test == null)
            {
                MorceauxList.Add(morceau);

                bool saved = SaveJSON();
                if (!saved)
                {
                    MessageBox.Show("Error writing json");
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool UpdateJSON(Morceau morceau)
        {
            GetJSON();
            if (MorceauxList == null)
                MorceauxList = new List<Morceau>();

            var test = MorceauxList.FirstOrDefault(r => r.Id == morceau.Id);
            if (test != null)
            {
                test.Titre = morceau.Titre;
                test.Artiste = morceau.Artiste;
                test.Duree = morceau.Duree;
                test.Genre = morceau.Genre;
                test.Annee = morceau.Annee;
                test.Apprecie = test.Apprecie;
                test.Chemin = test.Chemin;

                bool saved = SaveJSON();
                if (!saved)
                {
                    MessageBox.Show("Error updating json");
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool DeleteJSON(Morceau morceau)
        {
            GetJSON();
            if (MorceauxList == null)
                MorceauxList = new List<Morceau>();

            var test = MorceauxList.FirstOrDefault(r => r.Id == morceau.Id);
            if (test != null)
            {
                MorceauxList.RemoveAll(r => r.Id == morceau.Id);

                bool saved = SaveJSON();
                if (!saved)
                {
                    MessageBox.Show("Error deleting json");
                    return false;
                }

                return true;
            }

            return false;
        }

        public List<Morceau> SelectResearch(string type, string input)
        {
            List<Morceau> NewList = new List<Morceau>();
            GetJSON();
            if (MorceauxList == null)
                MorceauxList = new List<Morceau>();

            for (int i = 0; i < MorceauxList.Count; i++)
            {
                switch (type)
                {
                    case "titre":
                        if (MorceauxList[i].Titre == input) NewList.Add(MorceauxList[i]);
                        break;
                    case "artiste":
                        if (MorceauxList[i].Artiste == input) NewList.Add(MorceauxList[i]);
                        break;
                    case "duree":
                        if (MorceauxList[i].Duree == input) NewList.Add(MorceauxList[i]);
                        break;
                    case "genre":
                        if (MorceauxList[i].Genre == input) NewList.Add(MorceauxList[i]);
                        break;
                    case "annee":
                        if (MorceauxList[i].Annee == input) NewList.Add(MorceauxList[i]);
                        break;
                    default:
                        MessageBox.Show("type not registered correctly");
                        break;
                }
            }

            return NewList;
        }
    }
}
