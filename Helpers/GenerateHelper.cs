namespace SimoshStore;

public class GenerateHelper
{
    public static int GenerateNumber()
    {
        Random random = new Random();
        // 10000000 ile 99999999 arasında rastgele bir sayı oluştur
        int randomNumber = random.Next(10000000, 100000000);
        return randomNumber;
    }
}
