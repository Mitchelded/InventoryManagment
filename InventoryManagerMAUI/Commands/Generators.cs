namespace InventoryManagerMAUI.Commands;

public class Generators
{
    	
    private static int _counter = 0;
    public static string GenerateSerialNumber()
    {
        // Get current date components
        var date = DateTime.Now;
        string year = date.ToString("yy");   // Last two digits of the year
        string month = date.ToString("MM");  // Month
        string day = date.ToString("dd");    // Day
        
        // Increment counter to get a unique sequence for the day
        _counter++;
        
        // Generate serial number in the format YYMMDDXXXX
        string serialNumber = $"{year}{month}{day}{_counter:D4}";
        
        return serialNumber;
    }
}