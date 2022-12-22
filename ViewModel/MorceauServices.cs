using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpfApp1.Models;

namespace WpfApp1.ViewModel
{
    public class MorceauServices
    {
        public List<Morceau> MorceauxList = null;

        public MorceauServices()
        {
            getJSON();
        }

        public void getJSON()
        {
            string filename = @"D:\Users\emg08\Desktop\Morceaux.json";

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
    }
}
