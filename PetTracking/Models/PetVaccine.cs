namespace PetTracking.Models
{
    public class PetVaccine
    {
        public int PetId { get; set; } //FK
        public Pet Pet { get; set; } //Navigational property

        public int VaccineId { get; set; } //FK
        public Vaccine Vaccine { get; set; } //Navigational property
    }
}
