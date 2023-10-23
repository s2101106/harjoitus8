using System.ComponentModel.DataAnnotations;
namespace harjoitus7
{
    public class Stat
    {
        [Key]
        public int Id { get; set; }
        public int CurrentHitpoints { get; set; }
        public int MaxHitPoints { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
    }
}
    
