namespace BookSale.Managerment.Domain.Enums
{
  public enum StorageType
  {
    Cloudinary = 1,
    FirebaseStorage = 2, // Google Cloud
    Azure = 3, // Microsoft Azure Blob Storage
    S3 = 4, // Amazon Web Services (AWS) Simple Storage Service (S3)
  }
}