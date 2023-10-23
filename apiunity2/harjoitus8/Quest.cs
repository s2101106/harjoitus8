using System.ComponentModel.DataAnnotations;
namespace harjoitus8
{
    //CREATE TABLE Quests (tehtavaId INTEGER PRIMARY KEY,
    //tehtavaNimi TEXT, palkkioMaara INTEGER, tehtavaKuvaus TEXT,
    //kokemusPisteet INTEGER, onkoAloitettu BIT, onkoSuoritettu BIT);
    public class Quest
    {
        [Key]
        public int tehtavaId { get; set; }
        public string tehtavaNimi {  get; set; }
        public int palkkioMaara { get; set; }
        public string tehtavaKuvaus {  get; set; }
        public int kokemusPisteet {  get; set; }
        public bool onkoAloitettu { get; set; }
        public bool onkoSuoritettu { get; set; }
    }
}
