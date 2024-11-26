namespace InventoryManagment.Models.Entities;

public class Condition
{
    [Key]
    public int IdCondition { get; set; }
    
    [Required]
    public string ConditionName { get; set; }
    
    public ICollection<Equipment> Equipments { get; set; }
}