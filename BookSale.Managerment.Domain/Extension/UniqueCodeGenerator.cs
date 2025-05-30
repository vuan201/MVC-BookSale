namespace BookSale.Managerment.Domain.Extension
{
    public static class UniqueCodeGenerator
    {
        public static string GenerateUniqueGuid() => Guid.NewGuid().ToString();
    }
}
