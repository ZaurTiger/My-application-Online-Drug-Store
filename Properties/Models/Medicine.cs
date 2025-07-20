using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api_v2.Properties.Models;

public class Medicine
{
    public Medicine(string medicineName, int medicinePrice, int medicineQuantity, string medicineImage, string medicineDescription)
    {
        MedicineName = medicineName;
        
        MedicinePrice = medicinePrice;
        
        MedicineQuantity = medicineQuantity;
        
        MedicineImage = medicineImage;
        
        MedicineDescription = medicineDescription;
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MedicineId { get; set; }
    
    public required string MedicineName { get; set; }
    
    public int MedicinePrice { get; set; }
    
    public int MedicineQuantity { get; set; }
    
    public required string MedicineImage { get; set; }
    
    public required string MedicineDescription { get; set; }

    [JsonIgnore]
    public List<OrderItems>? OrderItems { get; set; }

    [JsonIgnore]
    public List<Cart>? Carts { get; set; }
}